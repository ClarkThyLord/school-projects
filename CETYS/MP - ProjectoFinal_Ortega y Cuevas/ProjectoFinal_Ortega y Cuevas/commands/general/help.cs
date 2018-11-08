using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoFinal_Ortega_y_Cuevas.commands.general
{
    class help
    {

        public static string name = "?";
        public static string description = "menu de ayuda";

        public static void run(Dictionary<string, Type> commands)
        {
            Console.WriteLine("Bienvenido al Menu Principal");
            Console.WriteLine("-------------------------------");

            foreach (KeyValuePair<string, Type> command in commands)
            {
                Console.WriteLine("{0}   -   {1}", command.Value.GetField("name").GetValue(null).ToString(), command.Value.GetField("description").GetValue(null).ToString());
            }
        }
    }
}
