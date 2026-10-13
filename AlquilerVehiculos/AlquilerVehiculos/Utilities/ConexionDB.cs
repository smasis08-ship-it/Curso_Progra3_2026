using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SistemaAlquilerVehiculos.Utilities
{
    public class ConexionDB
    {
        // Cadena de conexión a SQL Server
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionBD"]
            .ConnectionString;

        // Devuelve una conexión a SQL Server
        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }

        public SqlCommand CrearComando(string nombreSP,
            SqlConnection conexion)
        {
            SqlCommand comando =
                new SqlCommand(nombreSP, conexion);

            comando.CommandType = CommandType.StoredProcedure;

            return comando;
        }
    }
}
