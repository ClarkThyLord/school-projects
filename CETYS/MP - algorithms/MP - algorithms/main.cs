using System;
using System.IO;
using System.Reflection;

namespace MP___algorithms
{
    class main
    {
        static void Main(string[] args)
        {
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
