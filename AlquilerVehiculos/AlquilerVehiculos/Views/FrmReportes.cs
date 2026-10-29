using AlquilerVehiculos.Datos;
using AlquilerVehiculos.Utilities;
using System;
using System.Data;
using System.Windows.Forms;

namespace AlquilerVehiculos
{
    // los 3 reportes que pide el enunciado, cada uno en su pestaña
    public partial class FrmReportes : Form
    {
        AlquilerAD alquilerAD = new AlquilerAD();

        public FrmReportes()
        {
            InitializeComponent();
        }

        private void FrmReportes_Load(object sender, EventArgs e)
        {
            try
            {
                // por defecto los ingresos del mes en curso
                dtpDesdeIng.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                dtpHastaIng.Value = DateTime.Today;

                CargarMasAlquilados();
                CargarIngresos();
                CargarClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        // ---------- vehiculos mas alquilados ----------

        private void CargarMasAlquilados()
        {
            dgvMasAlquilados.DataSource = alquilerAD.ReporteVehiculosMasAlquilados();
            FormatoGrid.Aplicar(dgvMasAlquilados);
        }

        private void btnActualizarMas_Click(object sender, EventArgs e)
        {
            CargarMasAlquilados();
        }

        // ---------- ingresos por periodo ----------

        private void CargarIngresos()
        {
            if (dtpDesdeIng.Value.Date > dtpHastaIng.Value.Date)
            {
                MessageBox.Show("La fecha desde no puede ser mayor a la fecha hasta");
                return;
            }

            DataTable dt = alquilerAD.ReporteIngresosPorPeriodo(dtpDesdeIng.Value, dtpHastaIng.Value);
            dgvIngresos.DataSource = dt;
            FormatoGrid.Aplicar(dgvIngresos);

            // en los pagos si sirve ver la hora
            dgvIngresos.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            lblTotalIngresos.Text = "Total de ingresos: ₡" + FormatoGrid.Sumar(dt, "Monto").ToString("N2") +
                "  (" + dt.Rows.Count + " pago(s))";
        }

        private void btnConsultarIng_Click(object sender, EventArgs e)
        {
            CargarIngresos();
        }

        // ---------- historial por cliente ----------

        private void CargarClientes()
        {
            cbCliente.DisplayMember = "Nombre";
            cbCliente.ValueMember = "IdUsuario";
            cbCliente.DataSource = alquilerAD.ObtenerClientes();
            cbCliente.SelectedIndex = -1;
        }

        private void cbCliente_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int idUsuario = Convert.ToInt32(cbCliente.SelectedValue);

            DataTable dt = alquilerAD.ObtenerAlquileres(null, idUsuario);
            dgvHistorial.DataSource = dt;

            // ya se sabe de que cliente es, no hace falta repetirlo en cada fila
            FormatoGrid.Aplicar(dgvHistorial, "Cliente");

            lblResumenCliente.Text = dt.Rows.Count + " alquiler(es)  ·  total alquilado ₡" +
                FormatoGrid.Sumar(dt, "Monto", "Estado IN ('Aprobado', 'Devuelto')").ToString("N2") +
                "  ·  saldo pendiente ₡" + FormatoGrid.Sumar(dt, "Saldo").ToString("N2");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
