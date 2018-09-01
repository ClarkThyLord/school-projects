using System;
using System.Collections.Generic;
using System.Reflection;

namespace MP___algorithms
{
    class main
    {
        //static public Dictionary<string, Func<string>> commands = new Dictionary<string, Func<string>>
        //{
        //    {
        //        "?",
        //        MP___algorithms.commands.general.help
        //    },
        //    {
        //        "salir",
        //        exit
        //    }
        //};

        static public Dictionary<string, Type> commands = new Dictionary<string, Type>();

        static void Main(string[] args)
        {
            Type[] typelist = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in typelist)
            {
                Console.WriteLine(type);

                if (type.FullName.Contains("help"))
                {
                    //object instance = Activator.CreateInstance(type);
                    //dynamic d_instance = (dynamic)instance;

                    //Console.ReadKey();

                    //Console.WriteLine(d_instance.run());

                    type.GetMethod("run").Invoke(null, null);
                }
            }

            Console.ReadKey();

            //Console.WriteLine("Hola mundo!\n¿Qué te gustaría que haga hoy?");
            //while (true)
            //{
            //    string answer = Console.ReadLine().ToLower();

            //    if (commands.ContainsKey(answer))
            //    {
            //        commands[answer]();
            //    }
            //    else
            //    {
            //        Console.WriteLine("No entendí del todo, intente de nuevo...");
            //    }
            //}
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
