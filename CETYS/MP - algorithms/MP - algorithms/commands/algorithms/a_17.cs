using System;
using System.Collections.Generic;

namespace MP___algorithms.commands.algorithms
{
    public class a_17
    {
        public static List<double> run()
        {
            List<double> numeros = new List<double>();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Número {0}:", i + 1);

                double numero = 0;
                while (!double.TryParse(Console.ReadLine(), out numero))
                {
                    Console.WriteLine("Por favor ingrese un número real...");
                }

                numeros.Add(numero);
            }

            numeros.Sort();

            Console.WriteLine("Números en Orden: {0}", string.Join(",", numeros));

            return numeros;
        }
    }
}
