using System;

namespace MP___algorithms.commands.algorithms
{
    public class a_16
    {
        public static bool run()
        {
            Console.WriteLine("Valor de P:");
            double P = 0;
            while (!double.TryParse(Console.ReadLine(), out P))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }

            Console.WriteLine("Valor de Q:");
            double Q = 0;
            while (!double.TryParse(Console.ReadLine(), out Q))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }

            bool resultado = Math.Pow(P, 3) + Math.Pow(Q, 4) - (2 * Math.Pow(P, 2)) < 680;

            Console.WriteLine("{0} ** 3 + {1} ** 4 - (2 ** {0} ** 2) < 680 => {2}", P , Q, resultado);

            return resultado;
        }
    }
}
