using AlquilerVehiculos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlquilerVehiculos
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            lblBienvenido.Text = "Bienvenido " + SesionUsuario.Nombre;
            if (SesionUsuario.IdRol == 1)
            {
                menuAdmin.Visible = true;
            }

            else
            { 
                menuAdmin.Visible = false; 
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUsuarios frmUsuarios = new FrmUsuarios();
            frmUsuarios.Show();
        }

        private void rolesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmRoles frmRoles = new FrmRoles();
            frmRoles.Show();
        }

        private void clasificaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmClasificacionVehiculos frmClasificacionVehiculos = new FrmClasificacionVehiculos();
            frmClasificacionVehiculos.Show();
        }

        private void marcasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMarcas frmMarcas = new FrmMarcas();
            frmMarcas.Show();
        }

        private void vehiculosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmVehiculos frmVehiculos = new FrmVehiculos();
            frmVehiculos.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            SesionUsuario.CerrarSesion();
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.FormClosed += (s, args) => this.Close();

            frmLogin.Show();
            this.Hide();
        }
    }
}
