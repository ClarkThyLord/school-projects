using System;
using System.Collections.Generic;

namespace MP___algorithms.commands.algorithms
{
    public class a_26
    {
        public static void run()
        {
            Console.WriteLine("¿Cuántos grados máximo?");

            int max = 0;
            while (true)
            {
                while (!int.TryParse(Console.ReadLine(), out max))
                {
                    Console.WriteLine("Por favor ingrese un número real...");
                    continue;
                }

                if (max <= 0)
                {
                    Console.WriteLine("Por favor inserte un valor mayor a 0...");
                    continue;
                }

                break;
            }

            double[] grados = new double[max];

            for (int i = 0; i < max; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Insertar grado #{0}:", i + 1);

                Console.ForegroundColor = ConsoleColor.Blue;
                double grado = 0;
                while (!double.TryParse(Console.ReadLine(), out grado))
                {
                    Console.WriteLine("Por favor ingrese un número real...");
                }

                grados[i] = grado;
            }

            double grados_total = 0;
            foreach (double grado in grados)
            {
                grados_total += grado;
            }

            double media = grados_total / grados.Length;

            double varianza = 0;
            foreach (double grado in grados)
            {
                varianza += Math.Pow(grado, 2);
            }
            varianza = varianza / grados.Length;
            
            double number_1 = 0;
            double frecuencia_1 = 0;
            double number_2 = 0;
            double frecuencia_2 = 0;
            Array.Sort(grados);
            foreach (double grado in grados)
            {
                if (number_2 != grado)
                {
                    if (frecuencia_2 > frecuencia_1)
                    {
                        number_1 = number_2;
                        frecuencia_1 = frecuencia_2;
                    }

                    number_2 = grado;
                    frecuencia_2 = 1;
                } else
                {
                    frecuencia_2 += 1;
                }
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nMedia Aritmética de Grados: {0}", Math.Round(media, 2));
            Console.WriteLine("Varianza de Grados: {0}", Math.Round(varianza, 2));
            Console.WriteLine("Varianza Estándar de Grados: {0}", Math.Round(Math.Sqrt(varianza - Math.Pow(media, 2)), 2));
            Console.WriteLine("Moda de Grados: {0}", Math.Round(number_1, 2));
        }
    }
}
