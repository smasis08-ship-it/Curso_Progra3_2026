using AlquilerVehiculos.Models;
using SistemaAlquilerVehiculos.Datos;
using SistemaAlquilerVehiculos.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlquilerVehiculos
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Valida los campos obligatorios
            if (string.IsNullOrWhiteSpace(txtUser.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Debe ingresar el correo y la contraseña.");
                return;
            }

            try
            {
                Login login = new Login();

                login.Correo = txtUser.Text.Trim();
                login.Password = txtPassword.Text;

                // Instancia para consultar SQL Server
                LoginDatos datos = new LoginDatos();

                // Validar credenciales
                Usuario usuario = datos.IniciarSesion(login);

                if (usuario != null)
                {
                    // Guardar los datos del usuario en sesión
                    SesionUsuario.IniciarSesion(
                        usuario.IdUsuario,
                        usuario.Nombre,
                        usuario.Correo,
                        usuario.IdRol
                    );

                    MessageBox.Show(
                        "Bienvenido " + SesionUsuario.Nombre,
                        "Inicio de sesión",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Validar rol
                    if (SesionUsuario.IdRol == 1)
                    {
                        FrmPrincipal frmPrincipal = new FrmPrincipal();

                        // Cerrar la aplicación cuando se cierre el principal
                        frmPrincipal.FormClosed += (s, args) => this.Close();

                        frmPrincipal.Show();
                        this.Hide();
                    }
                    else if (SesionUsuario.IdRol == 2)
                    {
                        FrmPrincipal frmPrincipal = new FrmPrincipal();

                        // Cerrar la aplicación cuando se cierre el principal
                        frmPrincipal.FormClosed += (s, args) => this.Close();

                        frmPrincipal.Show();
                        this.Hide();
                        SesionUsuario.CerrarSesion();
                    }
                    else
                    {
                        MessageBox.Show("El usuario no tiene un rol autorizado.");
                        SesionUsuario.CerrarSesion();
                    }
                }
                else
                {
                    MessageBox.Show(
                        "Correo o contraseña incorrectos.",
                        "Acceso denegado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al iniciar sesión: " + ex.Message
                );
            }
        }

        //Método requerido solo para crear el usuario admin
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Genera la contraseña protegida
                Seguridad seguridad = new Seguridad();

                byte[] hash;
                byte[] salt;

                seguridad.CrearHash("Admin123*", out hash, out salt);

                // Conecta con SQL Server
                ConexionDB db = new ConexionDB();

                using (SqlConnection cn = db.ObtenerConexion())
                {
                    string sql = @" INSERT INTO Usuarios (Nombre, Correo, ClaveHash, ClaveSalt, IdRol, Activo)
                                    VALUES
                                    (@Nombre, @Correo, @Hash, @Salt, @IdRol, 1)";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@Nombre", "Administrador");

                        cmd.Parameters.AddWithValue(
                            "@Correo", "admin@alquiler.com");

                        cmd.Parameters.Add("@Hash", SqlDbType.VarBinary, 32)
                            .Value = hash;

                        cmd.Parameters.Add("@Salt", SqlDbType.VarBinary, 16)
                            .Value = salt;

                        cmd.Parameters.AddWithValue("@IdRol", 1);

                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Usuario creado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void cbMostrarPassword_CheckedChanged(object sender, EventArgs e)
        {
            //Mostrar campos de password como texto
            txtPassword.UseSystemPasswordChar = !cbMostrarPassword.Checked;
        }
    }
}
