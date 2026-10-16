using AlquilerVehiculos.Datos;
using AlquilerVehiculos.Models;
using SistemaAlquilerVehiculos.Utilities;
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
    public partial class FrmContraseñaUsuarios : Form
    {
        //Instancias requeridas para las AD
        MarcaAD marcaAD = new MarcaAD();
        VehiculoAD vehiculoAD = new VehiculoAD();
        UsuarioAD usuarioAD = new UsuarioAD();
        ClasificacionVehiculoAD clasificacionAD = new ClasificacionVehiculoAD();


        //Instancias de listas requeridas en la vista
        List<Usuario> lUsuarios = new List<Usuario>();
        List<ClasificacionVehiculo> lClasificaciones = new List<ClasificacionVehiculo>();
        public FrmContraseñaUsuarios()
        {
            InitializeComponent();
        }

        private void FrmVehiculos_Load(object sender, EventArgs e)
        {
            try
            {
                //Carga lista de usuarios
                CargarUsuarios();

                cbUsuario.DisplayMember = "Nombre";
                cbUsuario.ValueMember = "IdUsuario";
                cbUsuario.DataSource = lUsuarios;

                // No seleccionar ningun usuario inicialmente
                cbUsuario.SelectedIndex = -1;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
            

        }

        private void CargarUsuarios()
        {
            DataTable dt = usuarioAD.ObtenerUsuarios();
            lUsuarios = dt.AsEnumerable()
                .Select(fila => new Usuario
                {
                    IdUsuario = Convert.ToInt32(fila["IdUsuario"]),
                    Nombre = fila["Nombre"].ToString()
                })
                .ToList();
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

       

        public bool InsertarMarca(Vehiculo vehiculo)
        {
            //Insertar marca
            int resultado = 0;
            resultado = vehiculoAD.Insertar(vehiculo);
            if (resultado > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                //Valida campos requeridos
                if (cbUsuario.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar un usuario");
                    return;
                }
                if (txtPassword.Text.Trim() == "")
                {
                    MessageBox.Show("Debe asignar alguna contraseña");
                    return;
                }
                Seguridad seguridad = new Seguridad();
                byte[] hash = null;
                byte[] salt = null;
                seguridad.CrearHash(txtPassword.Text, out hash, out salt);
                var respuesta = usuarioAD.AsignarPassword(int.Parse(cbUsuario.SelectedValue.ToString()), hash, salt);
                if (respuesta > 0)
                {
                    MessageBox.Show("Contraseña asignada correctamente");
                }
                else
                {
                    MessageBox.Show("No fue posible asignar la contraseña");
                }
            }
            catch (Exception ex) { 
            MessageBox.Show("Error: " +ex.Message);
            }
            
        }
    }
}
