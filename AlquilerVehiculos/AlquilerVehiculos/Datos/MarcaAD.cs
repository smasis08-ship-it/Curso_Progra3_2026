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
    public class MarcaAD
    {
        ConexionDB conexionDB = new ConexionDB();

        public DataTable ObtenerMarca()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = conexionDB.ObtenerConexion())
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("PR_ObtenerMarcas", cn))
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

        // Insertar un Marca
        public int Insertar(Marca marca)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("PR_InsertarMarca", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@NombreMarca", SqlDbType.VarChar, 100).Value =
                        marca.NombreMarca;

                    cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 250).Value =
                        (object)marca.Descripcion ?? DBNull.Value;

                    cn.Open();

                    // Ejecuta el SP y obtiene el ID generado
                    int idRol = Convert.ToInt32(cmd.ExecuteScalar());

                    return idRol;
                }
            }
        }
    }
}
