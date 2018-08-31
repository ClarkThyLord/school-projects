using System;
using System.IO;
using System.Reflection;
using MP___algorithms.commands.algorithms;

namespace MP___algorithms
{
    class main
    {
        static public string source_path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName).FullName;

        static void Main(string[] args)
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in types)
            {
                Console.WriteLine("{0}:", type.FullName.ToString());
            }

            Console.WriteLine("Hola mundo!\n¿Qué te gustaría que haga hoy?");
            while (true)
            {
                string answer = Console.ReadLine().ToLower();
                switch (answer)
                {
                    case "?":
                        Console.WriteLine("Menú de Ayuda:\n**************\n?   : Ayuda\n\nsalir   :   Salir del programa");
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
