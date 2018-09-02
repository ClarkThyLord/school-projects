using System;

namespace MP___algorithms.commands.algorithms
{
    public class a_7
    {
        public static double run()
        {
            Console.WriteLine("Costo total:");
            double precio = 0;
            while (!double.TryParse(Console.ReadLine(), out precio))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }

            Console.WriteLine("Cantidad pagada:");
            double pago = 0;
            while (!double.TryParse(Console.ReadLine(), out pago))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }

            double cambio = precio - pago;

            Console.WriteLine((cambio >= 0) ? "Cantidad adeudada: {0}" : "Cambio: {1}", cambio, Math.Abs(cambio));

            return cambio;
        }
    }
}
