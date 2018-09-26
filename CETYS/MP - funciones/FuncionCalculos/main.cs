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

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Clear();

                Int32 num1, num2;
                int opc;

                Console.WriteLine("Menu Principal de Operaciones Matematicas: ");
                Console.WriteLine("\n");

                Console.Write("Escribe el primer valor númerico: ");
                num1 = Int32.Parse(Console.ReadLine());
                Console.WriteLine("\n");
                Console.Write("Escribe el segundo valor numérico: ");
                num2 = Int32.Parse(Console.ReadLine());
                Console.Clear();

                Console.WriteLine("Menu Principal de Operaciones Matematicas: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n=========================================");
                Console.WriteLine("\n");
                Console.WriteLine("\t1) Número MAYOR de los 2");
                Console.WriteLine("\t2) Múltiplo");
                Console.WriteLine("\t3) Potencia");
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
                            mayor(num1, num2);
                            break;
                        }
                    case 2:
                        {
                            multiplo(num1, num2);
                            break;
                        }
                    case 3:
                        {
                            potencia(num1, num2);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("\tSe ha equivocado de opcón, solo se acepta [1..3}");
                            break;
                        }
                }


                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n¿Cerrar programa? y/n");
                string ans = Console.ReadLine().ToLower();
                if (ans == "y")
                {
                    break;
                }
            }
        }
        // PRIMER FUNCION "NUMERO MAYOR"
        static void mayor(Int32 a, Int32 b)
        {
            if (a>b)
            {
                Console.WriteLine("\tEl número {0} es mayor que {1}", a, b);
            }
            else
            {
                Console.WriteLine("\tEl número {0} es mayor que {1}", b, a);
            }
        }
        //SEGUNDA FUNCION "MULTIPLO DE NUMEROS"
        static void multiplo(Int32 a, Int32 b)
        {
            Int32 w;
            w = (a % b);
            if (w == 0)
            {
                Console.WriteLine("\tEl número {0} es múltiplo de {1}", a, b);
            }
            else
            {
                Console.WriteLine("\tEl número {0} es múltiplo de {1}", b, a);
            }
        }

        //TERCER FUNCION "POTENCIA DE NUMEROS"
        static void potencia(Int32 a, Int32 b)
        {
            double s;
            s = Math.Pow(a, b);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\tEl número {0} se elevó a la potencia {1} resultando en: {2}", a, b, s);
        } 
    }
}
