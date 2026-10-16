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
    public partial class FrmUsuarios : Form
    {
        UsuarioAD usuarioAD = new UsuarioAD();
        RolAD rolAD = new RolAD();
        List<Rol> lRoles = new List<Rol>();
        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            try
            {
                CargarUsuarios();
                CargarRoles();
                cbRoles.DisplayMember = "NombreRol";
                cbRoles.ValueMember = "IdRol";
                cbRoles.DataSource = lRoles;

                // No seleccionar ningún rol inicialmente
                cbRoles.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void CargarRoles()
        {
            DataTable dt = rolAD.ObtenerRoles();
            lRoles = dt.AsEnumerable()
                .Select(fila => new Rol
                {
                    IdRol = Convert.ToInt32(fila["IdRol"]),
                    NombreRol = fila["NombreRol"].ToString(),
                    Descripcion = fila["Descripcion"].ToString()
                })
                .ToList();
        }


        private void CargarUsuarios()
        {
            DataTable dt = usuarioAD.ObtenerUsuarios();

            dgvUsuarios.AutoGenerateColumns = true;
            dgvUsuarios.DataSource = null;
            dgvUsuarios.Columns.Clear();

            dgvUsuarios.DataSource = dt;

            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public bool InsertarUsuario(Usuario usuario)
        {

            usuario.IdRol = int.Parse(cbRoles.SelectedValue.ToString());//lRoles.Where(x => x.NombreRol == cbRoles.Text).FirstOrDefault().IdRol;

            int resultado = 0;
            resultado = usuarioAD.Insertar(usuario);
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
            try
            {
                if(!Validaciones.ValidarNoVacio(txtNombre.Text.Trim()))
                {
                    MessageBox.Show("Debe ingresar un nombre para el usuario");
                    return;
                }
                if(!Validaciones.ValidarNoVacio(txtCorreo.Text.Trim()))
                {
                    MessageBox.Show("Debe ingresar un correo para el usuario");
                    return;
                }
                if (!Validaciones.ValidarCorreo(txtCorreo.Text.Trim()))
                {
                    MessageBox.Show("Debe ingresar un correo con formato válido");
                    return;
                }
                if (cbRoles.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe ingresar un rol para el usuario");
                    return;
                }
                Usuario usuario = new Usuario();
                usuario.Nombre = txtNombre.Text;
                usuario.Correo = txtCorreo.Text;
                usuario.FechaCreacion = DateTime.Now;

                bool resultado = false;
                resultado = InsertarUsuario(usuario);
                if (resultado)
                {
                    MessageBox.Show("Usuario " + usuario.Nombre + " insertado correctamente");
                    CargarUsuarios();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("Error al insertar el usuario " + usuario.Nombre);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void asignarContraseñasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmContraseñaUsuarios frmContraseñaUsuarios = new FrmContraseñaUsuarios();
            frmContraseñaUsuarios.Show();
        }

        public void Limpiar()
        {
            txtCorreo.Clear();
            txtNombre.Clear();
            cbRoles.SelectedIndex = -1;
            cbkActivar.Checked = true;
        }
    }
}
