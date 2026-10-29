using SistemaAlquilerVehiculos.Utilities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AlquilerVehiculos.Datos
{
    // todo lo del alquiler: solicitudes, aprobaciones, devoluciones, pagos y reportes
    // las reglas de negocio (deudas, multas, fechas) estan en los SP, aca solo se llaman
    public class AlquilerAD
    {
        ConexionDB conexionDB = new ConexionDB();

        // los SP tiran este numero cuando una regla de negocio no se cumple
        public const int ErrorRegla = 50001;

        // el cliente pide un carro, devuelve el id del alquiler creado
        public int Solicitar(int idUsuario, int idVehiculo, DateTime fechaInicio, DateTime fechaFin)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            using (SqlCommand cmd = conexionDB.CrearComando("PR_SolicitarAlquiler", cn))
            {
                cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;
                cmd.Parameters.Add("@IdVehiculo", SqlDbType.Int).Value = idVehiculo;
                cmd.Parameters.Add("@FechaInicio", SqlDbType.Date).Value = fechaInicio.Date;
                cmd.Parameters.Add("@FechaFin", SqlDbType.Date).Value = fechaFin.Date;

                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // el admin aprueba o rechaza una solicitud pendiente
        public void Responder(int idAlquiler, bool aprobar, string observaciones)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            using (SqlCommand cmd = conexionDB.CrearComando("PR_ResponderSolicitud", cn))
            {
                cmd.Parameters.Add("@IdAlquiler", SqlDbType.Int).Value = idAlquiler;
                cmd.Parameters.Add("@Aprobar", SqlDbType.Bit).Value = aprobar;
                cmd.Parameters.Add("@Observaciones", SqlDbType.VarChar, 250).Value =
                    string.IsNullOrWhiteSpace(observaciones) ? (object)DBNull.Value : observaciones.Trim();

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // registra la devolucion, devuelve los dias de retraso (si es mayor a 0 se genero multa)
        public int RegistrarDevolucion(int idAlquiler, DateTime fechaDevolucion, decimal montoDanio, string descripcionDanio)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            using (SqlCommand cmd = conexionDB.CrearComando("PR_RegistrarDevolucion", cn))
            {
                cmd.Parameters.Add("@IdAlquiler", SqlDbType.Int).Value = idAlquiler;
                cmd.Parameters.Add("@FechaDevolucion", SqlDbType.Date).Value = fechaDevolucion.Date;

                cmd.Parameters.Add("@MontoDanio", SqlDbType.Decimal).Value = montoDanio;
                cmd.Parameters["@MontoDanio"].Precision = 18;
                cmd.Parameters["@MontoDanio"].Scale = 2;

                cmd.Parameters.Add("@DescripcionDanio", SqlDbType.VarChar, 250).Value =
                    string.IsNullOrWhiteSpace(descripcionDanio) ? (object)DBNull.Value : descripcionDanio.Trim();

                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int RegistrarPago(int idAlquiler, decimal monto, string metodoPago)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            using (SqlCommand cmd = conexionDB.CrearComando("PR_RegistrarPago", cn))
            {
                cmd.Parameters.Add("@IdAlquiler", SqlDbType.Int).Value = idAlquiler;

                cmd.Parameters.Add("@Monto", SqlDbType.Decimal).Value = monto;
                cmd.Parameters["@Monto"].Precision = 18;
                cmd.Parameters["@Monto"].Scale = 2;

                cmd.Parameters.Add("@MetodoPago", SqlDbType.VarChar, 30).Value = metodoPago;

                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // lo que debe el cliente sumando todos sus alquileres (alquiler + multas - pagos)
        public decimal ObtenerDeuda(int idUsuario)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            using (SqlCommand cmd = conexionDB.CrearComando("PR_ObtenerDeudaCliente", cn))
            {
                cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;

                cn.Open();
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        // lista de alquileres, los filtros son opcionales (null = todos)
        public DataTable ObtenerAlquileres(string estado = null, int? idUsuario = null, bool soloConSaldo = false)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            using (SqlCommand cmd = conexionDB.CrearComando("PR_ObtenerAlquileres", cn))
            {
                cmd.Parameters.Add("@Estado", SqlDbType.VarChar, 20).Value = (object)estado ?? DBNull.Value;
                cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = (object)idUsuario ?? DBNull.Value;
                cmd.Parameters.Add("@SoloConSaldo", SqlDbType.Bit).Value = soloConSaldo;

                return Llenar(cmd);
            }
        }

        public DataTable ReporteVehiculosMasAlquilados()
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            using (SqlCommand cmd = conexionDB.CrearComando("PR_ReporteVehiculosMasAlquilados", cn))
            {
                return Llenar(cmd);
            }
        }

        public DataTable ReporteIngresosPorPeriodo(DateTime desde, DateTime hasta)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            using (SqlCommand cmd = conexionDB.CrearComando("PR_ReporteIngresosPorPeriodo", cn))
            {
                cmd.Parameters.Add("@Desde", SqlDbType.Date).Value = desde.Date;
                cmd.Parameters.Add("@Hasta", SqlDbType.Date).Value = hasta.Date;

                return Llenar(cmd);
            }
        }

        public DataTable ObtenerClientes()
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            using (SqlCommand cmd = conexionDB.CrearComando("PR_ObtenerClientes", cn))
            {
                return Llenar(cmd);
            }
        }

        // el SqlDataAdapter abre y cierra la conexion solo
        private DataTable Llenar(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }
            return dt;
        }
    }
}
