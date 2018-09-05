using System;

namespace MP___algorithms.commands.algorithms
{
    public class a_2
    {
        public static double run()
        {
            Console.WriteLine("Ingresar número:");
            double numero = 0;
            while (!double.TryParse(Console.ReadLine(), out numero))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }

            double cubo = Math.Pow(numero, 3);

            Console.WriteLine("{0} ** 2 = {1}", numero, cubo);

            return cubo;
        }
    }
}
