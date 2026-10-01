using System;

namespace Tarea_1_Programacion3_Grupo3
{
    // clase padre de los prestamos, implementa la interfaz IPrestamo
    public class Prestamo : IPrestamo
    {
        // monto que se presto
        public double Monto { get; protected set; }
        // lo que falta por pagar con todo e intereses
        public double SaldoPendiente { get; protected set; }
        // plazo del prestamo
        public int Meses { get; protected set; }

        // el cliente pide el prestamo
        public void SolicitarPrestamo(double monto, int meses)
        {
            if (monto <= 0 || meses <= 0)
            {
                Console.WriteLine("Datos del prestamo invalidos");
                return;
            }
            Monto = monto;
            Meses = meses;
            // lo que debe es el monto mas los intereses
            SaldoPendiente = monto + CalcularInteres();
            Console.WriteLine("Prestamo aprobado por " + monto + " a " + meses + " meses. Total a pagar: " + SaldoPendiente);
        }

        // abono al prestamo
        public void PagarPrestamo(double monto)
        {
            if (SaldoPendiente <= 0)
            {
                Console.WriteLine("No tiene prestamo pendiente");
                return;
            }
            if (monto <= 0)
            {
                Console.WriteLine("El monto debe ser mayor a 0");
                return;
            }
            // si paga de mas solo se cobra lo que debe
            if (monto > SaldoPendiente)
            {
                monto = SaldoPendiente;
            }
            SaldoPendiente = SaldoPendiente - monto;
            Console.WriteLine("Pago de " + monto + " realizado. Pendiente: " + SaldoPendiente);
        }

        // interes general 10%, las hijas lo canbian (polimorfismo)
        public virtual double CalcularInteres()
        {
            return Monto * 0.10;
        }
    }

    // prestamo personal, interes mas alto
    public class PrestamoPersonal : Prestamo
    {
        public override double CalcularInteres()
        {
            // 2% por cada mes
            return Monto * 0.02 * Meses;
        }
    }

    // prestamo de casa, interes mas bajo porque es a largo plazo
    public class PrestamoHipotecario : Prestamo
    {
        public override double CalcularInteres()
        {
            // 0.5% por mes
            return Monto * 0.005 * Meses;
        }
    }
}
