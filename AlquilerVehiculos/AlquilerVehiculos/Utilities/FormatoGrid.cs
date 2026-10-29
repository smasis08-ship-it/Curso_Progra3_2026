using System;
using System.Linq;
using System.Windows.Forms;

namespace AlquilerVehiculos.Utilities
{
    // para que todas las tablas se vean igual: montos con 2 decimales y fechas cortas
    public static class FormatoGrid
    {
        static readonly string[] columnasMonto =
            { "Monto", "Multas", "Pagado", "Saldo", "Monto generado", "Precio por día", "PrecioDia" };

        public static void Aplicar(DataGridView dgv, params string[] columnasOcultas)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn columna in dgv.Columns)
            {
                if (columnasMonto.Contains(columna.Name))
                {
                    columna.DefaultCellStyle.Format = "N2";
                    columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                else if (columna.ValueType == typeof(DateTime))
                {
                    columna.DefaultCellStyle.Format = "dd/MM/yyyy";
                }
            }

            // columnas que la pantalla no ocupa mostrar
            foreach (string nombre in columnasOcultas)
            {
                if (dgv.Columns.Contains(nombre))
                    dgv.Columns[nombre].Visible = false;
            }
        }

        // suma una columna de la tabla, si no hay filas devuelve 0 en vez de DBNull
        public static decimal Sumar(System.Data.DataTable dt, string columna, string filtro = "")
        {
            object total = dt.Compute("SUM([" + columna + "])", filtro);
            return total == DBNull.Value ? 0 : Convert.ToDecimal(total);
        }
    }
}
