using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlquilerVehiculos.Models
{

    public class Vehiculo : Master
    {
        public int IdVehiculo { get; set; }
        public int MarcaId { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public decimal PrecioDia { get; set; }
        public bool Estado { get; set; }

        public decimal CalcularAlquiler(int dias)
        {
            return PrecioDia * dias;
        }
    }

}
