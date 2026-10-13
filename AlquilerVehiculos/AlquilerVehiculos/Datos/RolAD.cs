using AlquilerVehiculos.Models;
using SistemaAlquilerVehiculos.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlquilerVehiculos.Datos
{
    public class RolAD
    {
        ConexionDB conexionDB = new ConexionDB();

        // Obtener lista roles
        public DataTable ObtenerRoles()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = conexionDB.ObtenerConexion())
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("PR_ObtenerRoles", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener roles: " + ex.Message);
            }

            return dt;
        }

        // Insertar un rol
        public int Insertar(Rol rol)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("PR_InsertarRol", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value =
                        rol.NombreRol;

                    cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 250).Value =
                        (object)rol.Descripcion ?? DBNull.Value;

                    cn.Open();

                    // Ejecuta el SP y obtiene el ID generado
                    int idRol = Convert.ToInt32(cmd.ExecuteScalar());

                    return idRol;
                }
            }
        }

        // Actualizar un rol
        public void Actualizar(Rol rol)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("PR_ActualizarRol", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = rol.IdRol;
                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = rol.NombreRol;
                    cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 250).Value = (object)rol.Descripcion ?? DBNull.Value;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}