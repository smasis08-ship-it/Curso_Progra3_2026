using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_1_Programacion3_Grupo3
{
    internal class Program
    {
        // lista donde se guardan las cuentas creadas
        static List<CuentaBancaria> cuentas = new List<CuentaBancaria>();
        // el prestamo actual, empieza en null porque no se ha pedido ninguno
        static IPrestamo prestamo = null;

        static void Main(string[] args)
        {
            // unas cuentas de prueba para no tener que crearlas cada vez
            cuentas.Add(new CuentaCorriente("100", "Juan Perez", 50000, 20000));
            cuentas.Add(new CuentaAhorro("200", "Maria Lopez", 100000, 0.05));

            // el menu se repite hasta que escoja 0
            int opcion = -1;
            while (opcion != 0)
            {
                Console.WriteLine();
                Console.WriteLine("===== SISTEMA BANCARIO =====");
                Console.WriteLine("1. Crear cuenta");
                Console.WriteLine("2. Depositar");
                Console.WriteLine("3. Retirar");
                Console.WriteLine("4. Consultar saldo");
                Console.WriteLine("5. Ver intereses de las cuentas");
                Console.WriteLine("6. Solicitar prestamo");
                Console.WriteLine("7. Pagar prestamo");
                Console.WriteLine("0. Salir");
                Console.Write("Opcion: ");

                // si escribe letras o algo raro se queda en -1
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    opcion = -1;
                }

                switch (opcion)
                {
                    case 1: CrearCuenta(); break;
                    case 2: Depositar(); break;
                    case 3: Retirar(); break;
                    case 4: ConsultarSaldo(); break;
                    case 5: VerIntereses(); break;
                    case 6: SolicitarPrestamo(); break;
                    case 7: PagarPrestamo(); break;
                    case 0: Console.WriteLine("Saliendo..."); break;
                    default: Console.WriteLine("Opcion invalida"); break;
                }
            }
        }

        // pide los datos y crea una cuenta corriente o de ahorro segun lo que escoja
        static void CrearCuenta()
        {
            Console.Write("Tipo (1 = Corriente, 2 = Ahorro): ");
            string tipo = Console.ReadLine();
            Console.Write("Numero de cuenta: ");
            string numero = Console.ReadLine();
            Console.Write("Titular: ");
            string titular = Console.ReadLine();
            double saldo = LeerNumero("Saldo inicial: ");

            if (tipo == "1")
            {
                double limite = LeerNumero("Limite de sobregiro: ");
                cuentas.Add(new CuentaCorriente(numero, titular, saldo, limite));
            }
            else if (tipo == "2")
            {
                double tasa = LeerNumero("Tasa de interes (ej 0.05): ");
                cuentas.Add(new CuentaAhorro(numero, titular, saldo, tasa));
            }
            else
            {
                Console.WriteLine("Tipo invalido");
                return;
            }
            Console.WriteLine("Cuenta creada");
        }

        // busca la cuenta y le deposita
        static void Depositar()
        {
            CuentaBancaria cuenta = BuscarCuenta();
            if (cuenta == null) return;

            double monto = LeerNumero("Monto: ");
            Console.Write("Descripcion (enter para dejar vacio): ");
            string desc = Console.ReadLine();

            // aqui se usa la sobrecarga, si no puso descripcion llama al otro metodo
            if (string.IsNullOrWhiteSpace(desc))
            {
                cuenta.Depositar(monto);
            }
            else
            {
                cuenta.Depositar(monto, desc);
            }
        }

        // busca la cuenta y hace el retiro
        static void Retirar()
        {
            CuentaBancaria cuenta = BuscarCuenta();
            if (cuenta == null) return;
            double monto = LeerNumero("Monto: ");
            // dependiendo del tipo de cuenta se usa el retirar de cada una
            cuenta.Retirar(monto);
        }

        // muestra los datos y el saldo de una cuenta
        static void ConsultarSaldo()
        {
            CuentaBancaria cuenta = BuscarCuenta();
            if (cuenta != null)
            {
                cuenta.ConsultarSaldo();
            }
        }

        // recorre todas las cuentas y cada una calcula su interes diferente
        static void VerIntereses()
        {
            foreach (CuentaBancaria c in cuentas)
            {
                Console.WriteLine("Cuenta " + c.NumeroCuenta + " (" + c.GetType().Name + ") interes: " + c.CalcularInteres());
            }
        }

        // crea el prestamo segun el tipo y lo solicita
        static void SolicitarPrestamo()
        {
            Console.Write("Tipo (1 = Personal, 2 = Hipotecario): ");
            string tipo = Console.ReadLine();

            // la variable es de tipo IPrestamo pero guarda cualquiera de los dos
            if (tipo == "1")
            {
                prestamo = new PrestamoPersonal();
            }
            else if (tipo == "2")
            {
                prestamo = new PrestamoHipotecario();
            }
            else
            {
                Console.WriteLine("Tipo invalido");
                return;
            }

            double monto = LeerNumero("Monto: ");
            int meses = (int)LeerNumero("Meses: ");
            prestamo.SolicitarPrestamo(monto, meses);
        }

        // abona al prestamo que ya se pidio
        static void PagarPrestamo()
        {
            if (prestamo == null)
            {
                Console.WriteLine("Primero debe solicitar un prestamo");
                return;
            }
            double monto = LeerNumero("Monto a pagar: ");
            prestamo.PagarPrestamo(monto);
        }

        // busca la cuenta por el numero
        static CuentaBancaria BuscarCuenta()
        {
            Console.Write("Numero de cuenta: ");
            string numero = Console.ReadLine();
            foreach (CuentaBancaria c in cuentas)
            {
                if (c.NumeroCuenta == numero)
                {
                    return c;
                }
            }
            // si llega aqui es que no existe
            Console.WriteLine("No se encontro la cuenta");
            return null;
        }

        // pide un numero hasta que lo escrban bien
        static double LeerNumero(string mensaje)
        {
            double valor;
            Console.Write(mensaje);
            while (!double.TryParse(Console.ReadLine().Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out valor))
            {
                Console.Write("Numero invalido, intente de nuevo: ");
            }
            return valor;
        }
    }
}
