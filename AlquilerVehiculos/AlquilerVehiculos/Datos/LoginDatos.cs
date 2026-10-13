
using AlquilerVehiculos.Models;
using SistemaAlquilerVehiculos.Datos;
using SistemaAlquilerVehiculos.Utilities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SistemaAlquilerVehiculos.Datos
{
    public class LoginDatos
    {
        // Método para validar el inicio de sesión
        public Usuario IniciarSesion(Login login)
        {
            // Si el usuario no existe o la contraseña
            // es incorrecta, devolveremos null.
            Usuario usuario = null;

            ConexionDB db = new ConexionDB();
            Seguridad seguridad = new Seguridad();

            using (SqlConnection cn = db.ObtenerConexion())
            {
                // Preparamos el procedimiento almacenado
                SqlCommand cmd = db.CrearComando(
                    "PR_ValidarLogin", cn);

                // Enviamos el correo al procedimiento
                cmd.Parameters.AddWithValue(
                    "@Correo", login.Correo);

                // Abrimos la conexión
                cn.Open();

                // Ejecutamos el procedimiento almacenado
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        // Obtenemos el Hash y Salt
                        // almacenados en SQL Server
                        byte[] hash =
                            (byte[])dr["ClaveHash"];

                        byte[] salt =
                            (byte[])dr["ClaveSalt"];

                        // Validamos la contraseña
                        bool claveCorrecta =
                            seguridad.ValidarClave(
                                login.Password, hash, salt);

                        if (claveCorrecta)
                        {
                            // Creamos el objeto Usuario
                            usuario = new Usuario();

                            usuario.IdUsuario =
                                Convert.ToInt32(dr["IdUsuario"]);

                            usuario.Nombre =
                                dr["Nombre"].ToString();

                            usuario.Correo =
                                dr["Correo"].ToString();

                            usuario.IdRol =
                                Convert.ToInt32(dr["IdRol"]);

                            usuario.Activo = true;
                        }
                    }
                }
            }

            return usuario;
        }
    }
}
