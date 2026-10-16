using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlquilerVehiculos.Utilities
{
    public class Validaciones
    {
        // Valida el formato de un correo electrónico
        public static bool ValidarCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return false;

            string patron = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(correo, patron);
        }

        public static bool ValidarMonto(string monto)
        {
            // Validamos que no esté vacío
            if (string.IsNullOrWhiteSpace(monto))
                return false;

            decimal valor;

            // Verificamos que sea un número decimal
            bool esValido = decimal.TryParse(
                monto,
                NumberStyles.Number,
                CultureInfo.CurrentCulture,
                out valor);

            // Validamos que el monto sea mayor que cero
            return esValido && valor > 0;
        }
        public static bool ValidarNoVacio(string monto)
        {
            // Validamos que no esté vacío
            if (string.IsNullOrWhiteSpace(monto))
                return false;
            else
                return true;
        }
    }
}
