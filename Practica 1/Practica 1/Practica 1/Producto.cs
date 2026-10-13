using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_1
{
    public abstract class Producto
    {
        // propiedades del producto
        public string Nombre { get; set; }
        public decimal Precio { get; set; }

        // constructor de el producto
        public Producto(string nombre, decimal precio)
        {
            Nombre = nombre;
            Precio = precio;
        }

        // retorna el precio orginal del producto 
        public decimal CalcularPrecio()
        {
            return Precio;
        }
    }
}
