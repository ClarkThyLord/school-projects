using System;

namespace MP___algorithms.commands.algorithms
{
    public class a_1
    {
        public static double run()
        {
            Console.WriteLine("Valor de A:");
            double A = 0;
            while (!double.TryParse(Console.ReadLine(), out A))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }

            Console.WriteLine("Valor de B:");
            double B = 0;
            while (!double.TryParse(Console.ReadLine(), out B))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }

            double C = Math.Pow(A + B, 2) / 3;

            Console.WriteLine("(({0} + {1}) ** 2) / 3 = {2} ", A, B, C);

            return C;
        }
    }
}
