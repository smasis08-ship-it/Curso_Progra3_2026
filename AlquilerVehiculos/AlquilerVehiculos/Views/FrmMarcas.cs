using AlquilerVehiculos.Datos;
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
    public partial class FrmMarcas : Form
    {
        MarcaAD marcaAD = new MarcaAD();
        public FrmMarcas()
        {
            InitializeComponent();
        }

        private void FrmMarcas_Load(object sender, EventArgs e)
        {
            try
            {
                CargarMarcas();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
            

        }

        private void CargarMarcas()
        {
            DataTable dt = marcaAD.ObtenerMarca();

            dgvMarca.AutoGenerateColumns = true;
            dgvMarca.DataSource = null;
            dgvMarca.Columns.Clear();

            dgvMarca.DataSource = dt;

            dgvMarca.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvMarca.ReadOnly = true;
            dgvMarca.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
                if (!Validaciones.ValidarNoVacio(txtMarca.Text))
                {
                    MessageBox.Show("Debe ingresar un detalle en la marca");
                    return;
                }
                if (!Validaciones.ValidarNoVacio(txtDescripcion.Text))
                {
                    MessageBox.Show("Debe ingresar una descripción de la marca");
                    return;
                }
                Marca marca = new Marca();
                marca.NombreMarca = txtMarca.Text;
                marca.Descripcion = txtDescripcion.Text;

                bool resultado = false;
                resultado = InsertarMarca(marca);
                if (resultado)
                {
                    MessageBox.Show("Marca " + marca.NombreMarca + " insertado correctamente");
                    CargarMarcas();
                }
                else
                {
                    MessageBox.Show("Error al insertar la marca " + marca.NombreMarca);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        public bool InsertarMarca(Marca marca)
        {
            
            int resultado = 0;
            resultado = marcaAD.Insertar(marca);
            if (resultado > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
