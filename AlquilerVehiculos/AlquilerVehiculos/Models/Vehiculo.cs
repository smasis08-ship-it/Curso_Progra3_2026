using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlquilerVehiculos.Models
{

    public class Vehiculo
    {
        // Propiedades del vehículo
        public int IdVehiculo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public decimal PrecioDia { get; set; }
        public string Estado { get; set; }

        // Método para calcular el costo del alquiler
        public decimal CalcularAlquiler(int dias)
        {
            return PrecioDia * dias;
        }
    }

}
