using System;
using System.Collections.Generic;

namespace MP___algorithms.commands.algorithms
{
    public class a_13
    {
        public static Dictionary<string, double> run()
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

            Console.WriteLine("Valor de C:");
            double C = 0;
            while (!double.TryParse(Console.ReadLine(), out C))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }
            
            double x1 = (-B + Math.Sqrt(Math.Pow(B, 2) - 4 * A * C)) / 2 * A;
            double x2 = (-B - Math.Sqrt(Math.Pow(B, 2) - 4 * A * C)) / 2 * A;

            Console.WriteLine("(-{1} + Math.Root(Math.Pow({1}, 2) - 4 * {0} * {2})) / 2 * {0} = {3} = x1\n(-{1} - Math.Root(Math.Pow({1}, 2) - 4 * {0} * {2})) / 2 * {0} = {4} = x2", A, B, C, x1, x2);

            return new Dictionary<string, double>
            {
                {
                    "x1",
                    x1
                },
                {
                    "x2",
                    x2
                }
            };
        }
    }
}
