using System;
using System.Collections.Generic;
using System.Text;

namespace MP___algorithms.commands.general
{
    class exit
    {
        public static string name = "salir";

        static public string run()
        {
            Console.WriteLine("¡Adiós!");

            System.Environment.Exit(1);

            return "1";
        }
    }
}
