using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncionCalculos
{
    class main
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Christian Moises Cuevas Larin 029794\nHola mundo!");


            int opc;
            Int32 num1 = 0, num2 = 0;
            Boolean continuar = false;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Clear();

                Console.WriteLine("Menu Principal de Operaciones Matematicas: ");
                Console.WriteLine("\n");

                if (!continuar)
                {
                    Console.Write("Escribe el primer valor númerico: ");
                    num1 = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("\n");
                }

                Console.Write("Escribe el segundo valor numérico: ");
                num2 = Int32.Parse(Console.ReadLine());
                Console.Clear();

                Console.WriteLine("Menu Principal de Operaciones Matematicas: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n=========================================");
                Console.WriteLine("\n");
                Console.WriteLine("\t1) Número MAYOR");
                Console.WriteLine("\t2) Múltiplo");
                Console.WriteLine("\t3) Potencia");
                Console.WriteLine("\t4) Suma");
                Console.WriteLine("\t5) Resta");
                Console.WriteLine("\n");
                Console.WriteLine("\t6) Salir");
                Console.WriteLine("\n=========================================");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n");
                
                Console.Write("\tDigitar la opción deseada [1..3]: ");
                opc = int.Parse(Console.ReadLine());
                Console.WriteLine("\n");

                switch (opc)
                {
                    case 1:
                        {
                            num1 = mayor(num1, num2);
                            break;
                        }
                    case 2:
                        {
                            num1 = multiplo(num1, num2);
                            break;
                        }
                    case 3:
                        {
                            num1 = potencia(num1, num2);
                            break;
                        }
                    case 4:
                        {
                            num1 = suma(num1, num2);
                            break;
                        }
                    case 5:
                        {
                            num1 = resta(num1, num2);
                            break;
                        }
                    case 6:
                        {
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("\tSe ha equivocado de opcón, solo se acepta [1..6]");
                            break;
                        }
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("¿continuar las operaciones con resultado? (y/n)");
                string ans = Console.ReadLine().ToLower();
                if (ans == "y")
                {
                    continuar = true;
                } else
                {
                    continuar = false;
                }
            }
        }
        // PRIMER FUNCION "NUMERO MAYOR"
        static Int32 mayor(Int32 a, Int32 b)
        {
            if (a>b)
            {
                Console.WriteLine("\tEl número {0} es mayor que {1}", a, b);

                return a;
            }
            else
            {
                Console.WriteLine("\tEl número {0} es mayor que {1}", b, a);

                return a;
            }
        }
        //SEGUNDA FUNCION "MULTIPLO DE NUMEROS"
        static Int32 multiplo(Int32 a, Int32 b)
        {
            Int32 w;
            w = (a % b);
            if (w == 0)
            {
                Console.WriteLine("\tEl número {0} es múltiplo de {1}", a, b);

                return a;
            }
            else
            {
                Console.WriteLine("\tEl número {0} es múltiplo de {1}", b, a);

                return b;
            }
        }

        //TERCER FUNCION "POTENCIA DE NUMEROS"
        static Int32 potencia(Int32 a, Int32 b)
        {
            double s;
            s = (Int32)Math.Pow(a, b);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\tEl número {0} se elevó a la potencia {1} resultando en: {2}", a, b, s);

            return (Int32)s;
        }

        //TERCER FUNCION "SUMA DE NUMEROS"
        static Int32 suma(Int32 a, Int32 b)
        {
            double s = (Int32)a + b;
            Console.WriteLine("{0} + {1} = {2}", a, b, s);

            return (Int32)s;
        }

        //TERCER FUNCION "RESTA DE NUMEROS"
        static Int32 resta(Int32 a, Int32 b)
        {
            double s = (Int32)a - b;
            Console.WriteLine("{0} - {1} = {2}", a, b, s);

            return (Int32)s;
        }
    }
}
