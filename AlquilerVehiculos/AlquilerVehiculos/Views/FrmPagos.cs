using AlquilerVehiculos.Datos;
using AlquilerVehiculos.Utilities;
using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace AlquilerVehiculos
{
    // se cobra lo que deben los clientes (alquiler + multas), se puede pagar por partes
    public partial class FrmPagos : Form
    {
        AlquilerAD alquilerAD = new AlquilerAD();

        public FrmPagos()
        {
            InitializeComponent();
        }

        private void FrmPagos_Load(object sender, EventArgs e)
        {
            try
            {
                cbMetodo.SelectedIndex = 0;
                CargarAlquileres();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void CargarAlquileres()
        {
            // solo los que tienen algo pendiente, los que ya estan al dia no salen
            dgvAlquileres.DataSource = alquilerAD.ObtenerAlquileres(null, null, true);
            FormatoGrid.Aplicar(dgvAlquileres, "Días", "Devuelto el", "Observaciones");

            lblInfo.Text = "Alquileres con saldo pendiente: " + dgvAlquileres.Rows.Count;
            MostrarSaldo();
        }

        private decimal SaldoSeleccionado()
        {
            return Convert.ToDecimal(dgvAlquileres.CurrentRow.Cells["Saldo"].Value);
        }

        private void MostrarSaldo()
        {
            if (dgvAlquileres.CurrentRow == null)
            {
                lblSaldo.Text = "";
                txtMonto.Clear();
                return;
            }

            decimal saldo = SaldoSeleccionado();
            lblSaldo.Text = "Saldo: ₡" + saldo.ToString("N2");

            // se pone el saldo completo de una vez, si paga por partes lo cambia
            txtMonto.Text = saldo.ToString("0.00");
        }

        private void dgvAlquileres_SelectionChanged(object sender, EventArgs e)
        {
            MostrarSaldo();
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (dgvAlquileres.CurrentRow == null)
            {
                MessageBox.Show("Debe escoger un alquiler de la lista");
                return;
            }
            if (!Validaciones.ValidarMonto(txtMonto.Text))
            {
                MessageBox.Show("Debe ingresar un monto válido");
                return;
            }

            decimal monto = decimal.Parse(txtMonto.Text, NumberStyles.Number, CultureInfo.CurrentCulture);
            if (monto > SaldoSeleccionado())
            {
                MessageBox.Show("El monto no puede ser mayor al saldo pendiente");
                return;
            }

            int idAlquiler = Convert.ToInt32(dgvAlquileres.CurrentRow.Cells["N°"].Value);
            string cliente = dgvAlquileres.CurrentRow.Cells["Cliente"].Value.ToString();

            if (MessageBox.Show("¿Registrar pago de ₡" + monto.ToString("N2") + " (" + cbMetodo.Text + ") de " + cliente + "?",
                    "Confirmar pago", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                alquilerAD.RegistrarPago(idAlquiler, monto, cbMetodo.Text);
                MessageBox.Show("Pago registrado correctamente");
                CargarAlquileres();
            }
            catch (SqlException ex) when (ex.Number == AlquilerAD.ErrorRegla)
            {
                MessageBox.Show(ex.Message, "No se pudo registrar el pago", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
