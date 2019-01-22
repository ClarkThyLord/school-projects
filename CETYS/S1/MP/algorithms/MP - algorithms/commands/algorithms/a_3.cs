using System;
using System.Collections.Generic;

namespace MP___algorithms.commands.algorithms
{
    public class a_3
    {
        public static Dictionary<string, double> run()
        {
            Console.WriteLine("Base:");
            double b = 0;
            while (!double.TryParse(Console.ReadLine(), out b))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }

            Console.WriteLine("Altura:");
            double h = 0;
            while (!double.TryParse(Console.ReadLine(), out h))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }

            double area = (b * h) / 2;

            double perimetro = b + (Math.Sqrt(Math.Pow(b, 2) + Math.Pow(h, 2)) * 2);

            Console.WriteLine("Area: {0}, Perimetro: {1}", area, perimetro);

            return new Dictionary<string, double>
            {
                {
                    "area",
                    area
                },
                {
                    "perimetro",
                    perimetro
                }
            };
        }
    }
}
