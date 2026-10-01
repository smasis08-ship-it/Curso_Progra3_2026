using System;

namespace Tarea_1_Programacion3_Grupo3
{
    // clase abstracta base para todas las cuentas del banco
    // aqui van las cosas que tienen en comun la corriente y la de ahorro
    public abstract class CuentaBancaria
    {
        // datos basicos de la cuenta
        public string NumeroCuenta { get; set; }
        public string Titular { get; set; }
        // el saldo es protected set para que solo la clase y las hijas lo puedan cambiar
        public double Saldo { get; protected set; }

        // constructor, recibe el numero, el nombre del dueno y con cuanto se abre la cuenta
        public CuentaBancaria(string numeroCuenta, string titular, double saldoInicial)
        {
            NumeroCuenta = numeroCuenta;
            Titular = titular;
            Saldo = saldoInicial;
        }

        // deposito sin descripcion (sobrecarga 1)
        // recibe solo el monto y llama al otro depositar con un texto por defecto
        public void Depositar(double monto)
        {
            Depositar(monto, "deposito sin descripcion");
        }

        // deposito con descripcion (sobrecarga 2)
        // recibe el monto y una descripcion de para que es el deposito
        public void Depositar(double monto, string descripcion)
        {
            // no se puede depositar algo negativo o en cero
            if (monto <= 0)
            {
                Console.WriteLine("El monto debe ser mayor a 0");
                return;
            }
            Saldo = Saldo + monto;
            Console.WriteLine("Deposito de " + monto + " realizado (" + descripcion + ")");
        }

        // cada cuenta retira diferente por eso es virtual y las hijas lo cambian
        // recibe el monto a sacar y lo resta del saldo si alcanza
        public virtual void Retirar(double monto)
        {
            if (monto <= 0)
            {
                Console.WriteLine("El monto debe ser mayor a 0");
                return;
            }
            // si quiere sacar mas de lo que tiene no se deja
            if (monto > Saldo)
            {
                Console.WriteLine("Fondos insuficientes");
                return;
            }
            Saldo = Saldo - monto;
            Console.WriteLine("Retiro de " + monto + " realizado");
        }

        // muestra el saldo actual de la cuenta
        public void ConsultarSaldo()
        {
            Console.WriteLine("Cuenta: " + NumeroCuenta + " | Titular: " + Titular + " | Saldo: " + Saldo);
        }

        // metodo abstracto, cada tipo de cuenta calcula sus intereses a su manera (polimorfismo)
        // no tiene cuerpo aqui, devuelve cuanto de interes genera la cuenta
        public abstract double CalcularInteres();
    }
}
