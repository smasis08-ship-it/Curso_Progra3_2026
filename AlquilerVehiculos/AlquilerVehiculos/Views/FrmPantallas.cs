using AlquilerVehiculos.Datos;
using AlquilerVehiculos.Models;
using AlquilerVehiculos.Utilities;
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
    public partial class FrmPantallas : Form
    {
        RolAD rolAD = new RolAD();
        public int IdRolSelected = 0;
        public FrmPantallas()
        {
            InitializeComponent();
        }

        private void FrmPantallas_Load(object sender, EventArgs e)
        {
            try
            {
                CargarRoles();
                //CargarPantallas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }


        }

        private void CargarRoles()
        {
            DataTable dt = rolAD.ObtenerRoles();
            CargarRoles(dt);
        }
        private void CargarPantallas(int IdRol)
        {
            DataTable dt = rolAD.ObtenerPantallas();
            DataTable dtSelected = rolAD.ObtenerPantallasxRol(IdRol);
            bool selected = false;

            // Limpiamos los controles anteriores
            flpPantalla.Controls.Clear();
            foreach (DataRow fila in dt.Rows)
            {
                foreach (DataRow fila2 in dtSelected.Rows)
                {
                    if (Convert.ToInt32(fila["IdPantalla"]) == Convert.ToInt32(fila2["IdPantalla"]))
                        selected = true;
                }
                CargarPantallasSeleccionadas(fila, IdRol, selected);
                selected = false;
            }

        }


        private void CargarRoles(DataTable dtRoles)
        {
            // Limpiamos los controles anteriores
            flpRoles.Controls.Clear();

            // Recorremos los roles del DataTable
            foreach (DataRow fila in dtRoles.Rows)
            {
                
                CheckBox chk = new CheckBox();

                // Nombre del rol
                chk.Text = fila["NombreRol"].ToString();

                // Guardamos el IdRol
                chk.Tag = Convert.ToInt32(fila["IdRol"]);

                if ((int)chk.Tag != 1)
                {
                    // Propiedades del CheckBox
                    chk.AutoSize = true;
                    chk.Margin = new Padding(10, 5, 10, 5);
                    chk.CheckedChanged += ChkRol_CheckedChanged;

                    // Agregamos el CheckBox al panel
                    flpRoles.Controls.Add(chk);
                }
                    
            }
        }



        private void ChkRol_CheckedChanged(object sender, EventArgs e)
        {
            // Obtenemos el CheckBox que cambió
            CheckBox chk = (CheckBox)sender;

            // Obtenemos el identificador del rol
            int idRol = Convert.ToInt32(chk.Tag);

            IdRolSelected = idRol;

            // Verificamos si fue marcado o desmarcado
            if (chk.Checked)
            {
                // Desmarcamos los demás CheckBox
                foreach (Control control in flpRoles.Controls)
                {
                    if (control is CheckBox)
                    {
                        CheckBox otroChk = (CheckBox)control;

                        if (otroChk != chk)
                        {
                            otroChk.Checked = false;
                        }
                    }
                }

                // Cargamos las pantallas del rol seleccionado
                CargarPantallas(idRol);
            }
            else
            {
                // Solo limpiamos si no queda ningún rol marcado
                bool haySeleccionado = false;

                foreach (Control control in flpRoles.Controls)
                {
                    if (control is CheckBox &&
                        ((CheckBox)control).Checked)
                    {
                        haySeleccionado = true;
                        break;
                    }
                }

                if (!haySeleccionado)
                {
                    flpPantalla.Controls.Clear();
                }
            }
        }

        private void ChkPantalla_CheckedChanged(object sender, EventArgs e)
        {
            // Obtenemos el CheckBox que cambió
            CheckBox chk = (CheckBox)sender;

            // Obtenemos el identificador del rol
            int idPantalla = Convert.ToInt32(chk.Tag);

            // Verificamos si fue marcado o desmarcado
            if (chk.Checked)
            {
                rolAD.InsertarPantallaRol(IdRolSelected,idPantalla);
            }
            else
            {
                rolAD.BorrarPantallaRol(IdRolSelected, idPantalla);
            }
        }


        private void CargarPantallasSeleccionadas(DataRow dtPantalla, int idPantalla, bool check)
        {
            CheckBox chk = new CheckBox();
            // Nombre del rol
            chk.Text = dtPantalla["Nombre"].ToString();

            // Guardamos el IdRol
            chk.Tag = Convert.ToInt32(dtPantalla["IdPantalla"]);

            // Propiedades del CheckBox
            chk.AutoSize = true;
            chk.Margin = new Padding(10, 5, 10, 5);

            chk.Checked = check;

            chk.CheckedChanged += ChkPantalla_CheckedChanged;

                // Agregamos el CheckBox al panel
                flpPantalla.Controls.Add(chk);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public bool InsertarRol(Rol rol)
        {

            int resultado = 0;
            resultado = rolAD.Insertar(rol);
            if (resultado > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void flpPantalla_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
