using System;
using System.Collections.Generic;
using System.Text;

namespace MP___algorithms.commands.general
{
    class help
    {
        public static string name = "?";

        public static string[] lines = new string[]
        {
            "Menú de Ayuda:",
            "**************",
            "?          :   Ayuda",
            "",
            "a_(1-21)   :   Algoritmos",
            "",
            "salir      :   Salir del programa"
        };

        static public string run ()
        {
            Console.WriteLine(string.Join("\n", lines));

            return "1";
        }
    }
}
