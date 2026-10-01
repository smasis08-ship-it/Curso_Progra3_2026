namespace Tarea_1_Programacion3_Grupo3
{
    // interfaz con lo que debe tener cualquier prestamo
    public interface IPrestamo
    {
        // pedir un prestamo con el monto y a cuantos meses
        void SolicitarPrestamo(double monto, int meses);
        // hacer un abono al prestamo
        void PagarPrestamo(double monto);
        // devuelve los intereses del prestamo
        double CalcularInteres();
    }
}
