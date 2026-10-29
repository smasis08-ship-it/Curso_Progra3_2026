using AlquilerVehiculos.Datos;
using AlquilerVehiculos.Models;
using AlquilerVehiculos.Utilities;
using System;
using System.Data;
using System.Windows.Forms;

namespace AlquilerVehiculos
{
    // pantalla del cliente: todos sus alquileres, con multas, lo pagado y lo que debe
    public partial class FrmHistorial : Form
    {
        AlquilerAD alquilerAD = new AlquilerAD();

        public FrmHistorial()
        {
            InitializeComponent();
        }

        private void FrmHistorial_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = alquilerAD.ObtenerAlquileres(null, SesionUsuario.IdUsuario);
                dgvHistorial.DataSource = dt;
                FormatoGrid.Aplicar(dgvHistorial, "Cliente");

                lblInfo.Text = "Todos mis alquileres: " + dt.Rows.Count;
                lblResumen.Text = "Total pagado: ₡" + FormatoGrid.Sumar(dt, "Pagado").ToString("N2") +
                    "  ·  multas: ₡" + FormatoGrid.Sumar(dt, "Multas").ToString("N2") +
                    "  ·  saldo pendiente: ₡" + FormatoGrid.Sumar(dt, "Saldo").ToString("N2");
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
