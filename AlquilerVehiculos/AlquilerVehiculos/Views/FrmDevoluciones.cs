using AlquilerVehiculos.Datos;
using AlquilerVehiculos.Utilities;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace AlquilerVehiculos
{
    // el admin registra cuando el cliente trae el carro, aca se generan las multas
    public partial class FrmDevoluciones : Form
    {
        AlquilerAD alquilerAD = new AlquilerAD();

        public FrmDevoluciones()
        {
            InitializeComponent();
        }

        private void FrmDevoluciones_Load(object sender, EventArgs e)
        {
            try
            {
                dtpDevolucion.Value = DateTime.Today;
                CargarAlquileres();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void CargarAlquileres()
        {
            // los aprobados son los que el cliente tiene en la calle
            dgvAlquileres.DataSource = alquilerAD.ObtenerAlquileres("Aprobado");
            FormatoGrid.Aplicar(dgvAlquileres, "Multas", "Pagado", "Estado", "Devuelto el");

            lblInfo.Text = "Alquileres activos (aprobados): " + dgvAlquileres.Rows.Count;
            MostrarRetraso();
        }

        // le avisa al admin antes de registrar si va a salir multa por retraso
        private void MostrarRetraso()
        {
            if (dgvAlquileres.CurrentRow == null)
            {
                lblRetraso.Text = "";
                return;
            }

            DateTime fechaPactada = Convert.ToDateTime(dgvAlquileres.CurrentRow.Cells["Hasta"].Value);
            int diasRetraso = (dtpDevolucion.Value.Date - fechaPactada.Date).Days;

            if (diasRetraso > 0)
            {
                lblRetraso.ForeColor = Color.Firebrick;
                lblRetraso.Text = "Va con " + diasRetraso + " día(s) de retraso, se le va a cobrar multa";
            }
            else
            {
                lblRetraso.ForeColor = Color.SeaGreen;
                lblRetraso.Text = "Lo devuelve a tiempo, no hay multa por retraso";
            }
        }

        private void dgvAlquileres_SelectionChanged(object sender, EventArgs e)
        {
            MostrarRetraso();
        }

        private void dtpDevolucion_ValueChanged(object sender, EventArgs e)
        {
            MostrarRetraso();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (dgvAlquileres.CurrentRow == null)
            {
                MessageBox.Show("Debe escoger un alquiler de la lista");
                return;
            }

            decimal montoDanio = nudDanio.Value;

            // si se cobra un daño hay que decir que fue, si no despues nadie sabe por que se cobro
            if (montoDanio > 0 && !Validaciones.ValidarNoVacio(txtDescripcionDanio.Text))
            {
                MessageBox.Show("Debe describir el daño que se va a cobrar");
                return;
            }

            int idAlquiler = Convert.ToInt32(dgvAlquileres.CurrentRow.Cells["N°"].Value);

            if (MessageBox.Show("¿Registrar la devolución del alquiler N° " + idAlquiler + "?\n\n" + lblRetraso.Text +
                    (montoDanio > 0 ? "\nDaños: ₡" + montoDanio.ToString("N2") : ""),
                    "Confirmar devolución", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                int diasRetraso = alquilerAD.RegistrarDevolucion(idAlquiler, dtpDevolucion.Value, montoDanio,
                    txtDescripcionDanio.Text);

                string mensaje = "Devolución registrada.";
                if (diasRetraso > 0)
                    mensaje += "\nSe generó multa por " + diasRetraso + " día(s) de retraso.";
                if (montoDanio > 0)
                    mensaje += "\nSe generó multa por daños y el vehículo pasó a mantenimiento.";
                if (diasRetraso > 0 || montoDanio > 0)
                    mensaje += "\n\nEl saldo se cobra en la pantalla de Pagos.";

                MessageBox.Show(mensaje, "Devolución", MessageBoxButtons.OK, MessageBoxIcon.Information);

                nudDanio.Value = 0;
                txtDescripcionDanio.Clear();
                CargarAlquileres();
            }
            catch (SqlException ex) when (ex.Number == AlquilerAD.ErrorRegla)
            {
                MessageBox.Show(ex.Message, "No se pudo registrar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
