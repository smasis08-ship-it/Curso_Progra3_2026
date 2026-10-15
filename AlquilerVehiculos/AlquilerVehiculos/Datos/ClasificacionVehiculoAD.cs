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
    public class ClasificacionVehiculoAD
    {
        ConexionDB conexionDB = new ConexionDB();
        // Obtener clasificaciones
        public DataTable ObtenerClasificaciones()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = conexionDB.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(
                    "PR_ObtenerClasificacionesVehiculos", cn))
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

        // Insertar clasificación
        public int Insertar(ClasificacionVehiculo clasificacion)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(
                    "PR_InsertarClasificacionVehiculo", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100)
                        .Value = clasificacion.Nombre;

                    cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 250)
                        .Value = (object)clasificacion.Descripcion ?? DBNull.Value;

                    cn.Open();

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // Actualizar clasificación
        public void Actualizar(ClasificacionVehiculo clasificacion)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(
                    "PR_ActualizarClasificacionVehiculo", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdClasificacion", SqlDbType.Int)
                        .Value = clasificacion.IdClasificacion;

                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100)
                        .Value = clasificacion.Nombre;

                    cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 250)
                        .Value = (object)clasificacion.Descripcion ?? DBNull.Value;

                    cmd.Parameters.Add("@Estado", SqlDbType.Bit)
                        .Value = clasificacion.Estado;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Desactivar clasificación
        public void Eliminar(int idClasificacion)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(
                    "PR_EliminarClasificacionVehiculo", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdClasificacion", SqlDbType.Int)
                        .Value = idClasificacion;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
