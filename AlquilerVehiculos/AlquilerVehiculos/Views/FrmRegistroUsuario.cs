using AlquilerVehiculos.Datos;
using AlquilerVehiculos.Models;
using AlquilerVehiculos.Utilities;
using SistemaAlquilerVehiculos.Utilities;
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
    public partial class FrmRegistroUsuario : Form
    {
        UsuarioAD usuarioAD = new UsuarioAD();
        RolAD rolAD = new RolAD();
        List<Rol> lRoles = new List<Rol>();
        public FrmRegistroUsuario()
        {
            InitializeComponent();
        }

        private void FrmRegistroUsuario_Load(object sender, EventArgs e)
        {
            try
            {
                CargarRoles();
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

                Usuario usuario = new Usuario();
                usuario.Nombre = txtNombre.Text;
                usuario.Correo = txtCorreo.Text;
                usuario.IdRol = 2;
                usuario.FechaCreacion = DateTime.Now;

                int resultado = 0;
                resultado = usuarioAD.Insertar(usuario);
                if (resultado > 0)
                {
                    Seguridad seguridad = new Seguridad();
                    byte[] hash = null;
                    byte[] salt = null;
                    seguridad.CrearHash(txtPassword.Text, out hash, out salt);
                    var respuesta = usuarioAD.AsignarPassword(resultado, hash, salt);
                    if (respuesta > 0)
                    {
                        MessageBox.Show("Usuario " + usuario.Nombre + " insertado correctamente");
                        this.Close();
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("Error al insertar el usuario " + usuario.Nombre);
                    }
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
        }

        private void cbMostrarPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !cbMostrarPassword.Checked;
        }
    }
}
