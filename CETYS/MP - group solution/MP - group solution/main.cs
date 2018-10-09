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
                type = ran.Next(2, 8);
                
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                
                Console.WriteLine("RESUELVA PARA UN POLÍGONO DE {0} LADOS", type);
                
                Console.ForegroundColor = ConsoleColor.Magenta;

                double lado_normal = 0;
                List<double> lados = new List<double>();
                double apotema = 0;
                if (type < 5)
                {
                    lados = integra_lados(type);
                }
                else
                {
                    Console.WriteLine("Lado de la figura regular:");
                    while (!double.TryParse(Console.ReadLine(), out lado_normal))
                    {
                        Console.WriteLine("Por favor ingrese un número real...");
                    }

                    Console.WriteLine("Apotema:");
                    while (!double.TryParse(Console.ReadLine(), out apotema))
                    {
                        Console.WriteLine("Por favor ingrese un número real...");
                    }
                } 

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                switch (type)
                {
                    case 2:
                        if (lados[0] == lados[1])
                        {
                            cuadrado(lados[0], lados[1]);
                        }
                        else
                        {
                            rectangulo(lados[0], lados[1]);
                        }
                        break;
                    case 3:
                        if (lados[0] == lados[1] && lados[0] == lados[2])
                        {
                            equilatero(lados[0], lados[1], lados[2]);
                        }
                        else if (lados[0] == lados[1] && lados[0] != lados[2])
                        {
                            isosceles(lados[0], lados[1], lados[2]);
                        }
                        else
                        {
                            escaleno(lados[0], lados[1], lados[2]);
                        }
                        break;
                    case 4:
                        trapezio(lados);
                        break;
                    case 5:
                        pentagono(lado_normal);
                        break;
                    case 6:
                        hexagono(lado_normal, apotema);
                        break;
                    case 7:
                        heptagono(lado_normal, apotema);
                        break;
                    case 8:
                        octagono(lado_normal, apotema);
                        break;
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

        static List<double> integra_lados(int numero_de_lados)
        {
            List<double> lados = new List<double>();

            for (int i = 0; i < numero_de_lados; i++)
            {
                Console.WriteLine("Tamaño del lado #{0}:", i + 1);
                double side = 0;
                while (!double.TryParse(Console.ReadLine(), out side))
                {
                    Console.WriteLine("Por favor ingrese un número real...");
                }

                lados.Add(side);
            }

            return lados;
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
        
        static void pentagono(double a)
        {
            Console.WriteLine("CUADRADO POLÍGONO");
            Console.WriteLine("-----------------");

            double h = a / 1.45;

            Console.WriteLine("Superficie: {0}\nPerímetro: {1}", ((a * 5) * h) / 2, a * 5);
        }

        static void trapezio(List<double> lados)
        {
            Console.WriteLine("POLIGONO TRAPEZOIDE");
            Console.WriteLine("-------------------");

            //A = Base MAYOR B = Base Menor C = Lado 
            double a = lados.Max();
            double b = lados.Min();
            lados.Remove(a);
            lados.Remove(b);
            double c = lados.Max();

            double n = (a - b) / 2;
            double h = Math.Pow(c, 2) - Math.Pow(n, 2);
            h = Math.Pow(h, (1 / 2));
            double area = (a + b * h) / 2;

            Console.WriteLine("Superficie: {0}\nPerimetro: {1}", area, (a + b + (c * 2)));
        }

        static void hexagono(double a, double apotema)
        {
            Console.WriteLine("HEXAGONO");
            Console.WriteLine("-----------------");

            double perimetro = 6 * a;
            Console.WriteLine("Superficie: {0}\nPerimetro: {1}", ((perimetro * apotema) / 2), perimetro);

        }

        static void heptagono(double a, double apotema)
        {
            Console.WriteLine("HEPTÁGONO");
            Console.WriteLine("-----------------");

            double p = a * 7;
            Console.WriteLine("Superficie: {0}\nPerímetro: {1}", (p * apotema) / 2, p);
        }

        static void octagono(double a, double apotema)
        {
            Console.WriteLine("OCTAGONO");
            Console.WriteLine("-----------------");

            Console.WriteLine("Superficie: {0}\nPerimetro: {1}", (4 * a * apotema), (8 * a));
        }
    }
}
