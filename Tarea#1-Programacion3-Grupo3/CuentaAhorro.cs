using System;

namespace Tarea_1_Programacion3_Grupo3
{
    // cuenta de ahorro, da mas interes pero tiene limite de retiros
    public class CuentaAhorro : CuentaBancaria
    {
        // tasa en decimal, por ejemplo 0.05 es un 5%
        public double TasaInteres { get; set; }
        // lleva la cuenta de cuantos retiros se han hecho
        private int retirosHechos = 0;
        private const int MaxRetiros = 3;

        // constructor, igual que la corriente pero aqui se pasa la tasa
        public CuentaAhorro(string numeroCuenta, string titular, double saldoInicial, double tasaInteres)
            : base(numeroCuenta, titular, saldoInicial)
        {
            TasaInteres = tasaInteres;
        }

        // solo deja hacer 3 retiros, despues ya no
        public override void Retirar(double monto)
        {
            if (retirosHechos >= MaxRetiros)
            {
                Console.WriteLine("Ya llego al maximo de retiros permitidos");
                return;
            }
            // se guarda el saldo y se usa el retirar del padre que ya valida todo
            double saldoAntes = Saldo;
            base.Retirar(monto);
            // si el saldo cambio es porque si se hizo el retiro
            if (Saldo != saldoAntes)
            {
                retirosHechos++;
            }
        }

        // aqui el interes depende de la tasa que se le ponga
        public override double CalcularInteres()
        {
            return Saldo * TasaInteres;
        }
    }
}
