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
    public partial class FrmClasificacionVehiculos : Form
    {
        ClasificacionVehiculoAD clasificacionAd = new ClasificacionVehiculoAD();
        public FrmClasificacionVehiculos()
        {
            InitializeComponent();
        }

        private void FrmClasificacionVehiculos_Load(object sender, EventArgs e)
        {
            try
            {
                CargarClasificacion();
            }
            catch(Exception ex) {
                MessageBox.Show("Error: " + ex.Message);            
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void CargarClasificacion()
        {
            DataTable dt = clasificacionAd.ObtenerClasificaciones();

            dgvClasificacion.AutoGenerateColumns = true;
            dgvClasificacion.DataSource = null;
            dgvClasificacion.Columns.Clear();

            dgvClasificacion.DataSource = dt;

            dgvClasificacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvClasificacion.ReadOnly = true;
            dgvClasificacion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public bool InsertarClasificacion(ClasificacionVehiculo clasificacion)
        {

            
            var resultado = clasificacionAd.Insertar(clasificacion);
            if (resultado > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!Validaciones.ValidarNoVacio(txtTipo.Text.Trim()))
            {
                MessageBox.Show("Debe ingresar un nombre para la clasificación");
                return;
            }
            ClasificacionVehiculo clasificacion = new ClasificacionVehiculo();
            clasificacion.Nombre = txtTipo.Text;
            clasificacion.Descripcion = txtDetalle.Text;
            clasificacion.FechaCreacion = DateTime.Now;
            clasificacion.Estado = cbkActivar.Checked;

            var resultado =  InsertarClasificacion(clasificacion);

            if (resultado)
            {
                MessageBox.Show("Clasificación " + clasificacion.Nombre + " insertada correctamente");
                LimpiarCampos();
                CargarClasificacion();
            }
            else
            {
                MessageBox.Show("Error al insertar la clasificación " + clasificacion.Nombre);
            }
        }

        private void LimpiarCampos()
        {
            txtDetalle.Clear();
            txtTipo.Clear();
            cbkActivar.Checked = true;
        }
    }
}
