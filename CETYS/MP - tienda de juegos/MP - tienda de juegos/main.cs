using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP___tienda_de_juegos
{
    class main
    {
        public static string[] names;
        public static string[] descriptions;

        public struct product
        {
            public int id;
            public string name;
            public string description;
            public string count;
        }

        public struct discount
        {
            public int id;
            public int product_id;
            public double amount;
        }

        public static product[] products;
        public static discount[] discounts;

        static void Main(string[] args)
        {
            // LOAD JSONS
            using (System.IO.StreamReader r = new StreamReader("./db/products/names.json"))
            {
                string json = r.ReadToEnd();
                names = JsonConvert.DeserializeObject<string[]>(json);
            }
            using (System.IO.StreamReader r = new StreamReader("./db/products/descriptions.json"))
            {
                string json = r.ReadToEnd();
                descriptions = JsonConvert.DeserializeObject<string[]>(json);
            }

            while (true)
            {
                header();
                Console.WriteLine("¿Qué te gustaría que haga hoy? (? : ayuda )");

                Console.ForegroundColor = ConsoleColor.Green;
                string answer = Console.ReadLine().ToLower();

                switch (answer)
                {
                    case "?":
                        ayuda();
                        break;
                    case "6":
                        return;
                    default:
                        error("No entendí del todo, intente de nuevo...");
                        Console.ReadKey();
                        continue;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\nPulse cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        static void header(ConsoleColor color = ConsoleColor.White)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Christian Moises Cuevas Larin 029794");
            Console.WriteLine("====================================");
            Console.WriteLine("TIENDA DE JUEGOS V1.0");
            Console.WriteLine("************************************");
            Console.ForegroundColor = color;
        }

        static void error(string error, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ForegroundColor = color;
        }

        static void ayuda()
        {
            header(ConsoleColor.Blue);
            Console.WriteLine("************************************");
            Console.WriteLine("Menú de Ayuda");
            Console.WriteLine("************************************");
            Console.WriteLine("?   :   ayuda");
            Console.WriteLine("");
            Console.WriteLine("OPCIONES DE GESTION");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("1   :   lista de productos");
            Console.WriteLine("2   :   auto crear productos");
            Console.WriteLine("3   :   crear productos manualmente");
            Console.WriteLine("");
            Console.WriteLine("OPCIONES DE TIENDA");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("4   :   abrir la tienda, y manejar automáticamente");
            Console.WriteLine("5   :   abrir la tienda");
            Console.WriteLine("");
            Console.WriteLine("6   :   salir del programa");
        }
    }
}
