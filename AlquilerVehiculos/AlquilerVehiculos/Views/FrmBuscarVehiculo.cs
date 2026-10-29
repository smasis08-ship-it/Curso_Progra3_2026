using AlquilerVehiculos.Datos;
using AlquilerVehiculos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlquilerVehiculos
{
    // pantalla del cliente: ve los carros disponibles, escoge fechas y pide el alquiler
    public partial class FrmBuscarVehiculo : Form
    {
        VehiculoAD vehiculoAD = new VehiculoAD();
        MarcaAD marcaAD = new MarcaAD();
        ClasificacionVehiculoAD clasificacionAD = new ClasificacionVehiculoAD();
        AlquilerAD alquilerAD = new AlquilerAD();

        public FrmBuscarVehiculo()
        {
            InitializeComponent();

            // en el Load la tabla todavia no tiene fila seleccionada, el total se calcula ya con la ventana visible
            this.Shown += (s, e) => CalcularTotal();
        }

        private void FrmBuscarVehiculo_Load(object sender, EventArgs e)
        {
            try
            {
                CargarFiltros();

                // no se puede alquilar para dias que ya pasaron, y minimo es un dia
                dtpDesde.MinDate = DateTime.Today;
                dtpDesde.Value = DateTime.Today;
                dtpHasta.MinDate = DateTime.Today.AddDays(1);
                dtpHasta.Value = DateTime.Today.AddDays(1);

                CargarVehiculos();
                MostrarDeuda();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void CargarFiltros()
        {
            // tipos activos para el filtro
            List<ClasificacionVehiculo> tipos = clasificacionAD.ObtenerClasificaciones().AsEnumerable()
                .Where(fila => Convert.ToBoolean(fila["Estado"]))
                .Select(fila => new ClasificacionVehiculo
                {
                    IdClasificacion = Convert.ToInt32(fila["IdClasificacion"]),
                    Nombre = fila["Nombre"].ToString()
                })
                .ToList();

            cbTipo.DisplayMember = "Nombre";
            cbTipo.ValueMember = "IdClasificacion";
            cbTipo.DataSource = tipos;
            cbTipo.SelectedIndex = -1;

            // marcas, las que tienen Activo en null tambien se toman como activas
            List<Marca> marcas = marcaAD.ObtenerMarca().AsEnumerable()
                .Where(fila => fila["Activo"] == DBNull.Value || Convert.ToBoolean(fila["Activo"]))
                .Select(fila => new Marca
                {
                    IdMarca = Convert.ToInt32(fila["IdMarca"]),
                    NombreMarca = fila["Marca"].ToString()
                })
                .ToList();

            cbMarca.DisplayMember = "NombreMarca";
            cbMarca.ValueMember = "IdMarca";
            cbMarca.DataSource = marcas;
            cbMarca.SelectedIndex = -1;
        }

        private void CargarVehiculos()
        {
            // si no escogio nada en el filtro se manda null y salen todos
            int? idTipo = cbTipo.SelectedIndex == -1 ? (int?)null : Convert.ToInt32(cbTipo.SelectedValue);
            int? idMarca = cbMarca.SelectedIndex == -1 ? (int?)null : Convert.ToInt32(cbMarca.SelectedValue);

            dgvVehiculos.DataSource = vehiculoAD.ObtenerDisponibles(idTipo, idMarca);
            dgvVehiculos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // el id lo ocupamos para solicitar pero no hace falta que el cliente lo vea
            dgvVehiculos.Columns["IdVehiculo"].Visible = false;
            dgvVehiculos.Columns["Precio por día"].DefaultCellStyle.Format = "N2";

            CalcularTotal();
        }

        private void MostrarDeuda()
        {
            decimal deuda = alquilerAD.ObtenerDeuda(SesionUsuario.IdUsuario);

            // se le avisa de una vez para que no intente y le salga el error
            lblDeuda.Text = deuda > 0
                ? "Tiene un saldo pendiente de ₡" + deuda.ToString("N2") + ", no puede solicitar otro alquiler hasta cancelarlo."
                : "";
        }

        // CurrentRow queda vacio hasta que la tabla recibe el foco, la fila seleccionada si esta desde el inicio
        private DataGridViewRow FilaSeleccionada()
        {
            if (dgvVehiculos.SelectedRows.Count > 0)
                return dgvVehiculos.SelectedRows[0];
            return dgvVehiculos.CurrentRow;
        }

        private void CalcularTotal()
        {
            if (dgvVehiculos.Rows.Count == 0)
            {
                lblTotal.Text = "No hay vehículos disponibles con ese filtro";
                return;
            }
            DataGridViewRow fila = FilaSeleccionada();
            if (fila == null)
            {
                lblTotal.Text = "Escoja un vehículo de la lista";
                return;
            }

            int dias = (dtpHasta.Value.Date - dtpDesde.Value.Date).Days;
            decimal precio = Convert.ToDecimal(fila.Cells["Precio por día"].Value);

            // es solo para que el cliente vea cuanto le va a salir, el monto real lo calcula el SP
            lblTotal.Text = dias + " día(s) x ₡" + precio.ToString("N2") + " = ₡" + (dias * precio).ToString("N2");
        }

        private void Filtro_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargarVehiculos();
        }

        private void btnVerTodos_Click(object sender, EventArgs e)
        {
            cbTipo.SelectedIndex = -1;
            cbMarca.SelectedIndex = -1;
            CargarVehiculos();
        }

        private void Fechas_ValueChanged(object sender, EventArgs e)
        {
            // la devolucion siempre tiene que ser por lo menos un dia despues del inicio
            dtpHasta.MinDate = dtpDesde.Value.Date.AddDays(1);
            CalcularTotal();
        }

        private void dgvVehiculos_SelectionChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void btnSolicitar_Click(object sender, EventArgs e)
        {
            DataGridViewRow fila = FilaSeleccionada();
            if (fila == null)
            {
                MessageBox.Show("Debe escoger un vehículo de la lista");
                return;
            }

            int idVehiculo = Convert.ToInt32(fila.Cells["IdVehiculo"].Value);
            string vehiculo = fila.Cells["Marca"].Value + " " + fila.Cells["Modelo"].Value;

            DialogResult confirmar = MessageBox.Show(
                "¿Desea solicitar el " + vehiculo + " del " + dtpDesde.Value.ToString("dd/MM/yyyy") +
                " al " + dtpHasta.Value.ToString("dd/MM/yyyy") + "?\n\n" + lblTotal.Text,
                "Confirmar solicitud", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes)
                return;

            try
            {
                int idAlquiler = alquilerAD.Solicitar(SesionUsuario.IdUsuario, idVehiculo, dtpDesde.Value, dtpHasta.Value);

                MessageBox.Show("Solicitud N° " + idAlquiler + " enviada. El administrador la tiene que aprobar, " +
                    "puede ver como va en Ver Reservas.", "Solicitud enviada",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarVehiculos();
            }
            catch (SqlException ex) when (ex.Number == AlquilerAD.ErrorRegla)
            {
                // son las reglas de negocio del SP (deuda, fechas, carro ya no disponible...)
                MessageBox.Show(ex.Message, "No se pudo solicitar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MostrarDeuda();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

    }
}
