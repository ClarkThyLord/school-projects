using System;
using System.IO;
using System.Reflection;

namespace MP___algorithms
{
    class main
    {
        static public string source_path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName).FullName;

        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles(source_path + "/commands/");
            string[] directories = Directory.GetDirectories(source_path + "/commands/");

            foreach (string directory in directories)
            {
                Console.WriteLine(directory);
            }

            Assembly mscorlib = typeof(string).Assembly;
            foreach (Type type in mscorlib.GetTypes())
            {
                Console.WriteLine(type.FullName);
            }

            Console.WriteLine("Hola mundo!\n¿Qué te gustaría que haga hoy?");
            while (true)
            {
                string answer = Console.ReadLine().ToLower();
                switch (answer)
                {
                    case "?":
                        Console.WriteLine("Menú de Ayuda:\n**************\nsalir   :   Salir del programa.");
                        break;
                    case "salir":
                        Console.WriteLine("¡Adiós!");
                        return;
                    default:
                        Console.WriteLine("No entendí del todo, intente de nuevo...");
                        break;
                }
            }
        }
    }
}
