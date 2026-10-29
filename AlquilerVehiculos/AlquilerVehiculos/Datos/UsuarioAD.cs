using AlquilerVehiculos.Models;
using SistemaAlquilerVehiculos.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlquilerVehiculos.Datos
{
    public class UsuarioAD
    {
        ConexionDB conexionDB = new ConexionDB();
        // Obtener usuarios
        public DataTable ObtenerUsuarios()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = conexionDB.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("PR_ObtenerUsuarios", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        // Insertar usuario y devuelve el id del registro
        public int Insertar(Usuario usuario)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("PR_InsertarUsuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value =
                        usuario.Nombre;

                    cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 150).Value =
                        usuario.Correo;

                    cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value =
                        usuario.IdRol;

                    cn.Open();

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // insertar el usuario ya con su contraseña, todo de una vez
        // asi no queda un usuario creado sin clave si algo falla a la mitad
        public int Insertar(Usuario usuario, byte[] passwordHash, byte[] passwordSalt)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("PR_InsertarUsuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = usuario.Nombre;
                    cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 150).Value = usuario.Correo;
                    cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = usuario.IdRol;
                    cmd.Parameters.Add("@PasswordHash", SqlDbType.VarBinary, 32).Value = passwordHash;
                    cmd.Parameters.Add("@PasswordSalt", SqlDbType.VarBinary, 16).Value = passwordSalt;

                    cn.Open();

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // Actualizar usuario
        public void Actualizar(Usuario usuario)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("PR_ActualizarUsuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = usuario.IdUsuario;
                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = usuario.Nombre;
                    cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 150).Value = usuario.Correo;
                    cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = usuario.IdRol;
                    cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = usuario.Activo;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Desactivar usuario
        public void Eliminar(int idUsuario)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("PR_EliminarUsuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int AsignarPassword(int idUsuario, byte[] passwordHash, byte[] passwordSalt)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(
                    "PR_AsignarPasswordUsuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdUsuario", SqlDbType.Int)
                        .Value = idUsuario;

                    cmd.Parameters.Add("@PasswordHash", SqlDbType.VarBinary, 32)
                        .Value = passwordHash;

                    cmd.Parameters.Add("@PasswordSalt", SqlDbType.VarBinary, 16)
                        .Value = passwordSalt;

                    cn.Open();

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
    }
}
