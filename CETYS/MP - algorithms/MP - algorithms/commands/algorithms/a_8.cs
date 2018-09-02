using System;

namespace MP___algorithms.commands.algorithms
{
    public class a_8
    {
        public static double run()
        {
            Console.WriteLine("Galones:");
            double galones = 0;
            while (!double.TryParse(Console.ReadLine(), out galones))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }

            double precio = (galones * 3.785) * 8.20;

            Console.WriteLine("Precio: {0}", precio);

            return precio;
        }
    }
}
