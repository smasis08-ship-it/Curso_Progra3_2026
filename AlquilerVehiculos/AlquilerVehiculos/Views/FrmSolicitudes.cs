using AlquilerVehiculos.Datos;
using AlquilerVehiculos.Utilities;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AlquilerVehiculos
{
    // el admin ve las solicitudes que hicieron los clientes y las aprueba o rechaza
    public partial class FrmSolicitudes : Form
    {
        AlquilerAD alquilerAD = new AlquilerAD();

        public FrmSolicitudes()
        {
            InitializeComponent();
        }

        private void FrmSolicitudes_Load(object sender, EventArgs e)
        {
            try
            {
                CargarSolicitudes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void CargarSolicitudes()
        {
            dgvSolicitudes.DataSource = alquilerAD.ObtenerAlquileres("Pendiente");

            // en una solicitud todavia no hay multas, pagos ni devolucion, no tiene sentido mostrarlos
            FormatoGrid.Aplicar(dgvSolicitudes, "Multas", "Pagado", "Saldo", "Estado", "Devuelto el", "Observaciones");

            lblInfo.Text = "Solicitudes pendientes de aprobar: " + dgvSolicitudes.Rows.Count;
        }

        private void Responder(bool aprobar)
        {
            if (dgvSolicitudes.CurrentRow == null)
            {
                MessageBox.Show("Debe escoger una solicitud de la lista");
                return;
            }

            int idAlquiler = Convert.ToInt32(dgvSolicitudes.CurrentRow.Cells["N°"].Value);
            string cliente = dgvSolicitudes.CurrentRow.Cells["Cliente"].Value.ToString();
            string accion = aprobar ? "aprobar" : "rechazar";

            if (MessageBox.Show("¿Desea " + accion + " la solicitud N° " + idAlquiler + " de " + cliente + "?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                alquilerAD.Responder(idAlquiler, aprobar, txtObservaciones.Text);

                MessageBox.Show(aprobar
                    ? "Solicitud aprobada, el vehículo quedó como alquilado."
                    : "Solicitud rechazada.");

                txtObservaciones.Clear();
                CargarSolicitudes();
            }
            catch (SqlException ex) when (ex.Number == AlquilerAD.ErrorRegla)
            {
                // por ejemplo si el carro ya se lo aprobaron a otro cliente
                MessageBox.Show(ex.Message, "No se pudo " + accion, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CargarSolicitudes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void btnAprobar_Click(object sender, EventArgs e)
        {
            Responder(true);
        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {
            Responder(false);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
