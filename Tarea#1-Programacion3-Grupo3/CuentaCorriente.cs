using System;

namespace Tarea_1_Programacion3_Grupo3
{
    // cuenta corriente, esta permite sobregiro hasta un limite
    public class CuentaCorriente : CuentaBancaria
    {
        // cuanto se puede pasar del saldo en negativo
        public double LimiteSobregiro { get; set; }

        // constructor, los primeros 3 datos se mandan al constructor del padre con base
        public CuentaCorriente(string numeroCuenta, string titular, double saldoInicial, double limiteSobregiro)
            : base(numeroCuenta, titular, saldoInicial)
        {
            LimiteSobregiro = limiteSobregiro;
        }

        // se sobreescribe el retiro porque aqui si se puede quedar en negativo
        public override void Retirar(double monto)
        {
            if (monto <= 0)
            {
                Console.WriteLine("El monto debe ser mayor a 0");
                return;
            }
            // se suma el saldo con el sobregiro para saber hasta cuanto puede sacar
            if (monto > Saldo + LimiteSobregiro)
            {
                Console.WriteLine("Supera el limite de sobregiro");
                return;
            }
            Saldo = Saldo - monto;
            Console.WriteLine("Retiro de " + monto + " realizado (cuenta corriente)");
        }

        // la corriente casi no da intereses, le puse 1%
        public override double CalcularInteres()
        {
            // si esta en cero o en negativo no gana nada
            if (Saldo <= 0)
            {
                return 0;
            }
            return Saldo * 0.01;
        }
    }
}
