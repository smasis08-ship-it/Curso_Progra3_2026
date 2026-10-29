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
        ClasificacionVehiculoAD clasificacionAD = new ClasificacionVehiculoAD();
        List<Marca> lMarcas = new List<Marca>();
        List<ClasificacionVehiculo> lClasificaciones = new List<ClasificacionVehiculo>();
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

                // el tipo de vehiculo (sedan, suv...) sale de la tabla de clasificaciones
                CargarClasificaciones();
                cbClasificacion.DisplayMember = "Nombre";
                cbClasificacion.ValueMember = "IdClasificacion";
                cbClasificacion.DataSource = lClasificaciones;
                cbClasificacion.SelectedIndex = -1;

                // el año arranca en el actual para no tener que subirlo desde 1990
                nudAnnio.Maximum = DateTime.Now.Year + 1;
                nudAnnio.Value = DateTime.Now.Year;
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

        private void CargarClasificaciones()
        {
            // solo las clasificaciones activas, las desactivadas no deberian salir para escoger
            DataTable dt = clasificacionAD.ObtenerClasificaciones();
            lClasificaciones = dt.AsEnumerable()
                .Where(fila => Convert.ToBoolean(fila["Estado"]))
                .Select(fila => new ClasificacionVehiculo
                {
                    IdClasificacion = Convert.ToInt32(fila["IdClasificacion"]),
                    Nombre = fila["Nombre"].ToString()
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
                // ojo aca hay que validar el precio y no el modelo, antes estaba al reves
                // y si el precio venia vacio se caia en el Convert de abajo
                if(!Validaciones.ValidarMonto(txtPrecioDia.Text))
                {
                    MessageBox.Show("Debe ingresar una precio válido para el alquiler vehículo");
                    return;
                }
                if(cbMarcas.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe ingresar una marca para vehículo");
                    return;
                }
                if(cbClasificacion.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe escoger el tipo de vehículo");
                    return;
                }

                Vehiculo vehiculo = new Vehiculo();
                vehiculo.Modelo = txtModelo.Text.Trim();
                vehiculo.Placa = txtPlaca.Text.Trim().ToUpper();
                vehiculo.PrecioDia = Convert.ToDecimal(txtPrecioDia.Text);
                vehiculo.MarcaId = int.Parse(cbMarcas.SelectedValue.ToString());
                vehiculo.Annio = (int)nudAnnio.Value;
                vehiculo.IdClasificacion = int.Parse(cbClasificacion.SelectedValue.ToString());
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

        // para mandar un carro a mantenimiento o sacarlo de ahi
        // "Alquilado" no se pone a mano, eso lo hace el sistema al aprobar un alquiler
        private void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            if (dgvMarca.CurrentRow == null)
            {
                MessageBox.Show("Debe escoger un vehículo de la lista");
                return;
            }
            if (cbEstadoVehiculo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe escoger el estado");
                return;
            }

            int idVehiculo = Convert.ToInt32(dgvMarca.CurrentRow.Cells["IdVehiculo"].Value);

            try
            {
                vehiculoAD.CambiarEstado(idVehiculo, cbEstadoVehiculo.Text);
                MessageBox.Show("Estado del vehículo actualizado a " + cbEstadoVehiculo.Text);
                cbEstadoVehiculo.SelectedIndex = -1;
                CargarVehiculos();
            }
            catch (System.Data.SqlClient.SqlException ex) when (ex.Number == AlquilerAD.ErrorRegla)
            {
                // si el carro esta alquilado no se deja cambiar
                MessageBox.Show(ex.Message, "No se pudo cambiar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            cbClasificacion.SelectedIndex = -1;
            nudAnnio.Value = DateTime.Now.Year;
            cbkActivar.Checked = true;
        }
    }
}