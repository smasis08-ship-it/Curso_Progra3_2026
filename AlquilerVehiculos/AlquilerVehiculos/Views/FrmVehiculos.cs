﻿using AlquilerVehiculos.Datos;
using AlquilerVehiculos.Models;
using AlquilerVehiculos.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlquilerVehiculos
{
    public partial class FrmVehiculos : Form
    {
        MarcaAD marcaAD = new MarcaAD();
        VehiculoAD vehiculoAD = new VehiculoAD();
        List<Marca> lMarcas = new List<Marca>();
        public FrmVehiculos()
        {
            InitializeComponent();
        }

        private void FrmVehiculos_Load(object sender, EventArgs e)
        {
            try
            {
                CargarVehiculos();
                CargarMarcasVehiculos();

                cbMarcas.DisplayMember = "NombreMarca";
                cbMarcas.ValueMember = "IdMarca";
                cbMarcas.DataSource = lMarcas;

                // No seleccionar ninguna marca inicialmente
                cbMarcas.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }


        }

        private void CargarVehiculos()
        {
            DataTable dt = vehiculoAD.ObtenerVehiculos();

            dgvMarca.AutoGenerateColumns = true;
            dgvMarca.DataSource = null;
            dgvMarca.Columns.Clear();

            dgvMarca.DataSource = dt;

            dgvMarca.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvMarca.ReadOnly = true;
            dgvMarca.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void CargarMarcasVehiculos()
        {
            DataTable dt = marcaAD.ObtenerMarca();
            lMarcas = dt.AsEnumerable()
                .Select(fila => new Marca
                {
                    IdMarca = Convert.ToInt32(fila["IdMarca"]),
                    NombreMarca = fila["Marca"].ToString(),
                    Descripcion = fila["Descripcion"].ToString()
                })
                .ToList();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Validaciones.ValidarNoVacio(txtModelo.Text))
                {
                    MessageBox.Show("Debe ingresar un modelo para el vehículo");
                    return;
                }
                if(!Validaciones.ValidarNoVacio(txtPlaca.Text))
                {
                    MessageBox.Show("Debe ingresar una placa para el vehículo");
                    return;
                }
                if(Validaciones.ValidarMonto(txtModelo.Text))
                {
                    MessageBox.Show("Debe ingresar una precio válido para el alquiler vehículo");
                    return;
                }
                if(cbMarcas.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe ingresar una marca para vehículo");
                    return;
                }

                Vehiculo vehiculo = new Vehiculo();
                vehiculo.Modelo = txtModelo.Text;
                vehiculo.Placa = txtPlaca.Text;
                vehiculo.PrecioDia = Convert.ToDecimal(txtPrecioDia.Text);
                vehiculo.MarcaId = int.Parse(cbMarcas.SelectedValue.ToString());
                vehiculo.Estado = cbkActivar.Checked;

                bool resultado = false;
                resultado = InsertarMarca(vehiculo);
                if (resultado)
                {
                    MessageBox.Show("Vehiculo " + vehiculo.Modelo + " insertado correctamente");
                    CargarVehiculos();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("Error al insertar el vehiculo " + vehiculo.Modelo);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        public bool InsertarMarca(Vehiculo vehiculo)
        {

            int resultado = 0;
            resultado = vehiculoAD.Insertar(vehiculo);
            if (resultado > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Limpiar()
        {
            txtModelo.Clear();
            txtPlaca.Clear();
            txtPrecioDia.Clear();
            cbMarcas.SelectedIndex = -1;
            cbkActivar.Checked = true;
        }
    }
}