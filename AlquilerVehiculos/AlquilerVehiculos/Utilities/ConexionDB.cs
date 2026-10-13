
using System.Data;
using System.Data.SqlClient;

namespace SistemaAlquilerVehiculos.Utilities
{
    public class ConexionDB
    {
        // Cadena de conexión a SQL Server
        private string cadenaConexion = @"Server=localhost;Database=AlquilerVehiculos;Integrated Security=True;";

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
