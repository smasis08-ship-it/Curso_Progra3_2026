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
    public partial class FrmRoles : Form
    {
        RolAD rolAD = new RolAD();
        public FrmRoles()
        {
            InitializeComponent();
        }

        private void FrmRoles_Load(object sender, EventArgs e)
        {
            try
            {
                CargarRoles();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
            

        }

        private void CargarRoles()
        {
            DataTable dt = rolAD.ObtenerRoles();

            dgvRoles.AutoGenerateColumns = true;
            dgvRoles.DataSource = null;
            dgvRoles.Columns.Clear();

            dgvRoles.DataSource = dt;

            dgvRoles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvRoles.ReadOnly = true;
            dgvRoles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
                if (!Validaciones.ValidarNoVacio(txtNombreRol.Text.Trim()))
                {
                    MessageBox.Show("Debe ingresar un nombre para el rol");
                    return;
                }

                Rol rol = new Rol();
                rol.NombreRol = txtNombreRol.Text;
                rol.Descripcion = txtDescripcion.Text;
                rol.FechaCreacion = DateTime.Now;

                bool resultado = false;
                resultado = InsertarRol(rol);
                if (resultado)
                {
                    MessageBox.Show("Rol " + rol.NombreRol + " insertado correctamente");
                    CargarRoles();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("Error al insertar el rol " + rol.NombreRol);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        public bool InsertarRol(Rol rol)
        {
            
            int resultado = 0;
            resultado = rolAD.Insertar(rol);
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
            txtNombreRol.Text = "";
            txtDescripcion.Text = "";
            cbkActivar.Checked = true;
        }
    }
}
