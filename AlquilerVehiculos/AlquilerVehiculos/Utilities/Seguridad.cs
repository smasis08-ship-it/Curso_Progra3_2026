using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SistemaAlquilerVehiculos.Utilities
{
    public class Seguridad
    {
        // Genera una contraseña protegida (Hash y Salt) para guardarla en la base de datos.
        public void CrearHash(string clave,
            out byte[] hash, out byte[] salt)
        {
            // Generamos un Salt aleatorio de 16 bytes
            salt = new byte[16];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Generamos el Hash de la contraseña
            using (var pbkdf2 = new Rfc2898DeriveBytes(
                clave, salt, 100000, HashAlgorithmName.SHA256))
            {
                hash = pbkdf2.GetBytes(32);
            }
        }

        // Valida la contraseña ingresada contra el Hash y Salt almacenados en SQL Server.
        public bool ValidarClave(string clave,
            byte[] hashGuardado, byte[] salt)
        {
            // Generamos el Hash de la clave ingresada
            byte[] hashIngresado;

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                clave, salt, 100000, HashAlgorithmName.SHA256))
            {
                hashIngresado = pbkdf2.GetBytes(32);
            }

            // Verificamos que tengan el mismo tamaño
            if (hashIngresado.Length != hashGuardado.Length)
            {
                return false;
            }

            // Comparamos los bytes de ambos Hash
            int diferencia = 0;

            for (int i = 0; i < hashIngresado.Length; i++)
            {
                diferencia |= hashIngresado[i] ^ hashGuardado[i];
            }

            // Si no hay diferencias, la contraseña es correcta
            return diferencia == 0;
        }
    }
}
