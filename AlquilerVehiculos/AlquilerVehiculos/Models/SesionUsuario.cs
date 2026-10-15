using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlquilerVehiculos.Models
{
    public static class SesionUsuario
    {
        public static int IdUsuario { get; set; }
        public static string Nombre { get; set; }
        public static string NombreUsuario { get; set; }
        public static string Correo { get; set; }
        public static int IdRol { get; set; }

        // Indica si existe una sesión activa
        public static bool EstaAutenticado
        {
            get { return IdUsuario > 0; }
        }

        public static void IniciarSesion(int idUsuario,string nombre,string correo,int idRol)
        {
            IdUsuario = idUsuario;
            Nombre = nombre;
            Correo = correo;
            IdRol = idRol;
        }

        // Limpiar los datos al cerrar sesión
        public static void CerrarSesion()
        {
            IdUsuario = 0;
            Nombre = null;
            NombreUsuario = null;
            IdRol = 0;
        }
    }
}
