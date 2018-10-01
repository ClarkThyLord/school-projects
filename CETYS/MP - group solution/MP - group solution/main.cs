using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP___group_solution
{ 
    class main
    {
        static void Main(string[] args)
        {
            Random ran = new Random();
            int type = 0;
            while (true)
            {
                type = ran.Next(1, 10);
                
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;

                if (type <= 5)
                {
                    Console.WriteLine("RESUELVA PARA UN POLÍGONO DE DOS LADOS");

                    Console.ForegroundColor = ConsoleColor.Magenta;

                    Console.WriteLine("Tamaño del lado #1:");
                    double side_1 = 0;
                    while (!double.TryParse(Console.ReadLine(), out side_1))
                    {
                        Console.WriteLine("Por favor ingrese un número real...");
                    }
                    Console.WriteLine("Tamaño del lado #2:");
                    double side_2 = 0;
                    while (!double.TryParse(Console.ReadLine(), out side_2))
                    {
                        Console.WriteLine("Por favor ingrese un número real...");
                    }
                    
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;

                    if (side_1 == side_2)
                    {
                        cuadrado(side_1, side_2);
                    }
                    else
                    {
                        rectangulo(side_1, side_2);
                    }
                } else
                {
                    Console.WriteLine("RESUELVA PARA UN POLÍGONO DE TRES LADOS");

                    Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.WriteLine("Tamaño del lado #1:");
                    double side_1 = 0;
                    while (!double.TryParse(Console.ReadLine(), out side_1))
                    {
                        Console.WriteLine("Por favor ingrese un número real...");
                    }
                    Console.WriteLine("Tamaño del lado #2:");
                    double side_2 = 0;
                    while (!double.TryParse(Console.ReadLine(), out side_2))
                    {
                        Console.WriteLine("Por favor ingrese un número real...");
                    }
                    Console.WriteLine("Tamaño del lado #3:");
                    double side_3 = 0;
                    while (!double.TryParse(Console.ReadLine(), out side_3))
                    {
                        Console.WriteLine("Por favor ingrese un número real...");
                    }

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;

                    if (side_1 == side_2 && side_1 == side_3)
                    {
                        equilatero(side_1, side_2, side_3);
                    }
                    else if (side_1 == side_2 && side_1 != side_3)
                    {
                        isosceles(side_1, side_2, side_3);
                    }
                    else
                    {
                        escaleno(side_1, side_2, side_3);
                    }
                }

                Console.WriteLine("\nPULSE CUALQUIER TECLA PARA CONTINUAR...");
                Console.ReadKey();

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("¿Continuar? (y/n)");
                string ans = Console.ReadLine().ToLower();
                if (ans == "n")
                {
                    return;
                }
            }
        }

        static void cuadrado(double a, double b)
        {
            Console.WriteLine("CUADRADO POLÍGONO");
            Console.WriteLine("-----------------");

            Console.WriteLine("Superficie: {0}\nPerímetro: {1}", a * a, a * 4);
        }

        static void rectangulo(double a, double b)
        {
            Console.WriteLine("RECTANGULO POLÍGONO");
            Console.WriteLine("-------------------");

            Console.WriteLine("Superficie: {0}\nPerímetro: {1}", a * b, (a * 2) + (b * 2));
        }

        static void equilatero(double a, double b, double c)
        {
            Console.WriteLine("EQUILATERO POLÍGONO");
            Console.WriteLine("-------------------");

            // Un método para calcular el área de un triángulo cuando conoces las longitudes de los tres lados.
            // https://www.mathopenref.com/heronsformula.html

            double p = (a + b + c) / 2;
            Console.WriteLine("Superficie: {0}\nPerímetro: {1}", Math.Sqrt(p * (p - a) * (p - b) * (p - c)), a + b + c);
        }

        static void isosceles(double a, double b, double c)
        {
            Console.WriteLine("ISOSCELES POLÍGONO");
            Console.WriteLine("-----------------");

            // Un método para calcular el área de un triángulo cuando conoces las longitudes de los tres lados.
            // https://www.mathopenref.com/heronsformula.html

            double p = (a + b + c) / 2;
            Console.WriteLine("Superficie: {0}\nPerímetro: {1}", Math.Sqrt(p * (p - a) * (p - b) * (p - c)), a + b + c);
        }

        static void escaleno(double a, double b, double c)
        {
            Console.WriteLine("ESCALENO POLÍGONO");
            Console.WriteLine("-----------------");

            // Un método para calcular el área de un triángulo cuando conoces las longitudes de los tres lados.
            // https://www.mathopenref.com/heronsformula.html

            double p = (a + b + c) / 2;
            Console.WriteLine("Superficie: {0}\nPerímetro: {1}", Math.Sqrt(p * (p - a) * (p - b) * (p - c)), a + b + c);
        }
    }
}
