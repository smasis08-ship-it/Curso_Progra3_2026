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
    public class VehiculoAD
    {
        ConexionDB conexionDB = new ConexionDB();
        public DataTable ObtenerVehiculos()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = conexionDB.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(
                    "PR_ObtenerVehiculos", cn))
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

        // Obtener un vehículo por su ID
        public Vehiculo ObtenerPorId(int idVehiculo)
        {
            Vehiculo vehiculo = null;

            using (SqlConnection cn = conexionDB.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(
                    "PR_ObtenerVehiculoPorId", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdVehiculo", SqlDbType.Int)
                        .Value = idVehiculo;

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            vehiculo = new Vehiculo
                            {
                                IdVehiculo = Convert.ToInt32(dr["IdVehiculo"]),
                                MarcaId = Convert.ToInt32(dr["MarcaId"]),
                                Modelo = dr["Modelo"].ToString(),
                                Placa = dr["Placa"].ToString(),
                                PrecioDia = Convert.ToDecimal(dr["PrecioDia"]),
                                // en la tabla la columna se llama Activo, con "Estado" se caia
                                Estado = Convert.ToBoolean(dr["Activo"]),
                                Annio = dr["Annio"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["Annio"]),
                                IdClasificacion = dr["IdClasificacion"] == DBNull.Value
                                    ? (int?)null : Convert.ToInt32(dr["IdClasificacion"])
                            };
                        }
                    }
                }
            }

            return vehiculo;
        }

        // Insertar un nuevo vehículo y devolver su ID
        public int Insertar(Vehiculo vehiculo)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(
                    "PR_InsertarVehiculo", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MarcaId", SqlDbType.Int)
                        .Value = vehiculo.MarcaId;

                    // si vienen vacios se manda DBNull, si se manda null el parametro ni se envia
                    cmd.Parameters.Add("@IdClasificacion", SqlDbType.Int)
                        .Value = (object)vehiculo.IdClasificacion ?? DBNull.Value;

                    cmd.Parameters.Add("@Annio", SqlDbType.Int)
                        .Value = (object)vehiculo.Annio ?? DBNull.Value;

                    cmd.Parameters.Add("@Modelo", SqlDbType.VarChar, 100)
                        .Value = vehiculo.Modelo;

                    cmd.Parameters.Add("@Placa", SqlDbType.VarChar, 20)
                        .Value = vehiculo.Placa;

                    cmd.Parameters.Add("@PrecioDia", SqlDbType.Decimal)
                        .Value = vehiculo.PrecioDia;

                    cmd.Parameters["@PrecioDia"].Precision = 18;
                    cmd.Parameters["@PrecioDia"].Scale = 2;

                    // en la base Activo es bit, entonces se manda como bit y no como texto
                    cmd.Parameters.Add("@Estado", SqlDbType.Bit)
                        .Value = vehiculo.Estado;

                    cn.Open();

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // Actualizar un vehículo existente
        public int Actualizar(Vehiculo vehiculo)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(
                    "PR_ActualizarVehiculo", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdVehiculo", SqlDbType.Int)
                        .Value = vehiculo.IdVehiculo;

                    cmd.Parameters.Add("@MarcaId", SqlDbType.Int)
                        .Value = vehiculo.MarcaId;

                    cmd.Parameters.Add("@Modelo", SqlDbType.VarChar, 100)
                        .Value = vehiculo.Modelo;

                    cmd.Parameters.Add("@Placa", SqlDbType.VarChar, 20)
                        .Value = vehiculo.Placa;

                    cmd.Parameters.Add("@PrecioDia", SqlDbType.Decimal)
                        .Value = vehiculo.PrecioDia;

                    cmd.Parameters["@PrecioDia"].Precision = 18;
                    cmd.Parameters["@PrecioDia"].Scale = 2;

                    cmd.Parameters.Add("@Estado", SqlDbType.Bit)
                        .Value = vehiculo.Estado;

                    // antes no se mandaban y el SP pide la clasificacion, por eso fallaba al actualizar
                    cmd.Parameters.Add("@Annio", SqlDbType.Int)
                        .Value = (object)vehiculo.Annio ?? DBNull.Value;

                    cmd.Parameters.Add("@IdClasificacion", SqlDbType.Int)
                        .Value = (object)vehiculo.IdClasificacion ?? DBNull.Value;

                    cn.Open();

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // disponible / alquilado / mantenimiento
        // si el carro tiene un alquiler activo el SP no deja cambiarlo
        public int CambiarEstado(int idVehiculo, string estadoVehiculo)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("PR_CambiarEstadoVehiculo", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdVehiculo", SqlDbType.Int).Value = idVehiculo;
                    cmd.Parameters.Add("@EstadoVehiculo", SqlDbType.VarChar, 20).Value = estadoVehiculo;

                    cn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // Desactivar un vehículo (eliminación lógica)
        public int Eliminar(int idVehiculo)
        {
            using (SqlConnection cn = conexionDB.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(
                    "PR_EliminarVehiculo", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdVehiculo", SqlDbType.Int)
                        .Value = idVehiculo;

                    cn.Open();

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // Obtener únicamente los vehículos disponibles
        // los filtros son opcionales, si vienen null se traen todos los tipos / marcas
        public DataTable ObtenerDisponibles(int? idClasificacion = null, int? idMarca = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = conexionDB.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(
                    "PR_ObtenerVehiculosDisponibles", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdClasificacion", SqlDbType.Int).Value = (object)idClasificacion ?? DBNull.Value;
                    cmd.Parameters.Add("@IdMarca", SqlDbType.Int).Value = (object)idMarca ?? DBNull.Value;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }
    }
}
