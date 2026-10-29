using AlquilerVehiculos.Datos;
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

        RolAD rolAD = new RolAD();

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            lblBienvenido.Text = "Bienvenido " + SesionUsuario.Nombre;

            try
            {
                AplicarPermisos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los permisos: " + ex.Message);
            }
        }

        // lo que se ve en el menu depende de las pantallas que tenga marcadas el rol (FrmPantallas)
        // se usa Available y no Visible porque Visible da false mientras el menu esta cerrado
        private void AplicarPermisos()
        {
            // el admin ve todo, a el no se le configuran permisos
            if (SesionUsuario.IdRol == 1)
            {
                menuAdmin.Available = true;
                return;
            }

            HashSet<string> permitidas = new HashSet<string>(
                rolAD.ObtenerPantallasxRol(SesionUsuario.IdRol).AsEnumerable()
                    .Select(fila => fila["Nombre"].ToString()));

            // cada opcion con el mismo nombre que tiene en la tabla Pantallas
            usuariosToolStripMenuItem.Available = permitidas.Contains("Usuarios");
            rolesToolStripMenuItem1.Available = permitidas.Contains("Roles");
            clasificaciónToolStripMenuItem.Available = permitidas.Contains("Clasificación de vehículos");
            vehiculosToolStripMenuItem1.Available = permitidas.Contains("Vehículos");
            marcasToolStripMenuItem.Available = permitidas.Contains("Marcas");
            solicitudesToolStripMenuItem.Available = permitidas.Contains("Solicitudes de alquiler");
            devolucionesToolStripMenuItem.Available = permitidas.Contains("Devoluciones");
            pagosToolStripMenuItem.Available = permitidas.Contains("Pagos");
            reportesToolStripMenuItem.Available = permitidas.Contains("Reportes");

            // los submenus solo salen si tienen algo adentro
            rolesToolStripMenuItem.Available = usuariosToolStripMenuItem.Available || rolesToolStripMenuItem1.Available;
            vehiculosToolStripMenuItem.Available = clasificaciónToolStripMenuItem.Available
                || vehiculosToolStripMenuItem1.Available || marcasToolStripMenuItem.Available;
            alquileresToolStripMenuItem.Available = solicitudesToolStripMenuItem.Available
                || devolucionesToolStripMenuItem.Available || pagosToolStripMenuItem.Available;
            menuAdmin.Available = rolesToolStripMenuItem.Available || vehiculosToolStripMenuItem.Available
                || alquileresToolStripMenuItem.Available || reportesToolStripMenuItem.Available;

            // las 3 opciones grandes de la pantalla principal (las del cliente)
            MostrarOpcion(panel2, label2, permitidas.Contains("Buscar vehículo"));
            MostrarOpcion(panel3, label3, permitidas.Contains("Reservas"));
            MostrarOpcion(panel4, label4, permitidas.Contains("Historial de alquiler"));
        }

        private void MostrarOpcion(Panel icono, Label texto, bool permitido)
        {
            icono.Visible = permitido;
            texto.Visible = permitido;
        }

        private void solicitudesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSolicitudes frmSolicitudes = new FrmSolicitudes();
            frmSolicitudes.Show();
        }

        private void devolucionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDevoluciones frmDevoluciones = new FrmDevoluciones();
            frmDevoluciones.Show();
        }

        private void pagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPagos frmPagos = new FrmPagos();
            frmPagos.Show();
        }

        private void reportesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReportes frmReportes = new FrmReportes();
            frmReportes.Show();
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            FrmBuscarVehiculo frmBuscarVehiculo = new FrmBuscarVehiculo();
            frmBuscarVehiculo.Show();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            FrmReservas frmReservas = new FrmReservas();
            frmReservas.Show();
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            FrmHistorial frmHistorial = new FrmHistorial();
            frmHistorial.Show();
        }
    }
}
