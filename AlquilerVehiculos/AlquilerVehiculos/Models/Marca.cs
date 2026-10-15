using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlquilerVehiculos.Models
{
    public class Marca:Master
    {
        public int IdMarca { get; set; }
        public string NombreMarca { get; set; }
        public string Descripcion { get; set; }

    }
}
