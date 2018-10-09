using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EXAM___pares
{
    class Program
    {
        public static List<int> tipos_de_billetes = new List<int> {
            {
                1
            },
            {
                5
            },
            {
                10
            },
            {
                20
            },
            {
                50
            },
            {
                100
            }
        };

        static void Main(string[] args)
        {
            while (true)
            {
                // INICIO
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Christian Cuevas T029794 y Jorge Velarde T030681");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("*****************");
                Console.WriteLine("Cajero Automático V2.0");
                Console.WriteLine("*****************\n\n");


                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Inserte una cantidad redondeada: (mayor o igual a $15 - menor o igual a $100,000)");
                BigInteger cantidad = new BigInteger();
                while (true)
                {
                    while (!BigInteger.TryParse(Console.ReadLine(), out cantidad))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Por favor ingrese un número real...");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        continue;
                    }

                    Console.ForegroundColor = ConsoleColor.Red;
                    if (cantidad < 15)
                    {
                        Console.WriteLine("Por favor ingrese un número mayor o igual a 15...");
                    }
                    else if (cantidad > 100000)
                    {
                        Console.WriteLine("Por favor ingrese un número menor o igual a 100,000...");
                    }
                    else
                    {
                        break;
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n\n");

                // PROCESSO

                int diferencia = 1;
                BigInteger temp = cantidad;
                string combinacion = "";
                while (true)
                {
                    temp = cantidad;
                    combinacion += String.Format("Posible combinación #{0}:", diferencia);
                    foreach (int billete in tipos_de_billetes.Reverse<int>())
                    {
                        if (billete == 1)
                        {
                            BigInteger denominador = (temp / billete);
                            if (denominador > 0)
                            {
                                combinacion += "\n" + String.Format("{1} billetes de ${0}", billete, denominador);

                                temp -= denominador * billete;
                                if (temp <= 0) break;
                            }
                        }
                        else
                        {
                            BigInteger denominador = (temp / billete) - diferencia;
                            if (denominador > 0)
                            {
                                combinacion += "\n" + String.Format("{1} billetes de ${0}", billete, denominador);

                                temp -= denominador * billete;
                                if (temp <= 0) break;
                            }
                        }
                    }

                    combinacion += "\n\n";
                    diferencia++;
                    if (diferencia == 4) break;
                }

                Console.WriteLine(combinacion);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n{0} escrito en Español con amor ;)\n\n", cantidad);

                //FIN
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("¿Continuar? (y/n)");
                string ans = Console.ReadLine().ToLower();
                if (ans == "n")
                {
                    return;
                }
            }
        }
    }
}
