using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoFinal_Ortega_y_Cuevas.commands.general
{
    class exit
    {

        public static string name = "salir";
        public static string description = "salir del programa";

        public static void run(Dictionary<string, Type> commands)
        {
            Console.WriteLine("¿Desea salirse del programa? (y/n)");
            if (Console.ReadLine().ToLower() == "y")
            {
                Environment.Exit(0);
            }
        }
    }
}
