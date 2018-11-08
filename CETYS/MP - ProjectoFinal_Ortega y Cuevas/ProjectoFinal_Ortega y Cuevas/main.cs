using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoFinal_Ortega_y_Cuevas
{
    class main
    {
        public static Dictionary<string, Type> commands = new Dictionary<string, Type>();

        static void Main(string[] args)
        {
            header(ConsoleColor.Yellow);

            Type[] typelist = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in typelist)
            {
                if (type.FullName.Contains("commands"))
                {
                    if (type.GetField("name") != null)
                    {
                        commands.Add(type.GetField("name").GetValue(null).ToString().ToLower(), type);
                    }
                    else
                    {
                        commands.Add(type.Name.ToLower(), type);
                    }
                }
            }

            while (true)
            {
                header(ConsoleColor.Yellow);
                Console.WriteLine("¿Qué te gustaría que haga hoy? (? : ayuda)");

                string answer = Console.ReadLine().ToLower();
                
                if (commands.ContainsKey(answer))
                {
                    header(ConsoleColor.Yellow);
                    commands[answer].GetMethod("run").Invoke(null, new object[]{ commands });

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
                else
                {
                    header(ConsoleColor.Yellow);
                    error("No entendí del todo, intente de nuevo...", ConsoleColor.Yellow);
                }
            }
        }

        public static void header(ConsoleColor color = ConsoleColor.White)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("Projecto Final - Alec Ortega 030247 y Christian Cuevas 029794");
            Console.WriteLine("-------------------------------------------------------------");

            Console.ForegroundColor = color;
        }

        public static void error(string msg, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: {0}", msg);

            Console.ReadKey();

            header(color);
        }
    }
}
