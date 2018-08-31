using System;
using System.Collections.Generic;

namespace MP___algorithms
{
    class main
    {
        static public Dictionary<string, Func<string>> commands = new Dictionary<string, Func<string>>
        {
            {
                "?",
                help
            },
            {
                "salir",
                exit
            }
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Hola mundo!\n¿Qué te gustaría que haga hoy?");
            while (true)
            {
                string answer = Console.ReadLine().ToLower();

                //switch (answer)
                //{
                //    case "?":
                //        Console.WriteLine("Menú de Ayuda:\n**************\n?   : Ayuda\n\nsalir   :   Salir del programa");
                //        break;
                //    case "salir":
                //        Console.WriteLine("¡Adiós!");
                //        return;
                //    default:
                //        Console.WriteLine("No entendí del todo, intente de nuevo...");
                //        break;
                //}

                if (commands.ContainsKey(answer))
                {
                    commands[answer]();
                }
                else
                {
                    Console.WriteLine("No entendí del todo, intente de nuevo...");
                }
            }
        }

        static public string help()
        {
            Console.WriteLine("Menú de Ayuda:\n**************\n?   : Ayuda\n\nsalir   :   Salir del programa");

            return "1";
        }

        static public string exit()
        {
            System.Environment.Exit(1);

            return "1";
        }
    }
}
