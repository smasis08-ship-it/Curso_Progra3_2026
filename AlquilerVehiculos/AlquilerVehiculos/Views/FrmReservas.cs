using AlquilerVehiculos.Datos;
using AlquilerVehiculos.Models;
using AlquilerVehiculos.Utilities;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AlquilerVehiculos
{
    // pantalla del cliente: lo que tiene pendiente de aprobar y lo que tiene alquilado ahorita
    public partial class FrmReservas : Form
    {
        AlquilerAD alquilerAD = new AlquilerAD();

        public FrmReservas()
        {
            InitializeComponent();
        }

        private void FrmReservas_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = alquilerAD.ObtenerAlquileres(null, SesionUsuario.IdUsuario);

                // aca solo lo que esta "vivo", lo rechazado y lo devuelto sale en el historial
                DataView reservas = new DataView(dt, "Estado IN ('Pendiente', 'Aprobado')", "", DataViewRowState.CurrentRows);
                dgvReservas.DataSource = reservas;
                FormatoGrid.Aplicar(dgvReservas, "Cliente", "Devuelto el");

                lblInfo.Text = reservas.Count == 0
                    ? "No tiene solicitudes pendientes ni alquileres en curso"
                    : "Solicitudes pendientes y alquileres en curso: " + reservas.Count;

                decimal deuda = alquilerAD.ObtenerDeuda(SesionUsuario.IdUsuario);
                lblDeuda.ForeColor = deuda > 0 ? Color.Firebrick : Color.SeaGreen;
                lblDeuda.Text = deuda > 0
                    ? "Saldo pendiente: ₡" + deuda.ToString("N2")
                    : "Está al día, no tiene saldo pendiente";
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
