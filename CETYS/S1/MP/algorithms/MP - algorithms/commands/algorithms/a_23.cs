using System;
using System.Collections.Generic;

namespace MP___algorithms.commands.algorithms
{
    public class a_23
    {
        public static Dictionary<string, dynamic> run()
        {
            int ID = 0;
            double sueldo = 0;

            Dictionary<int, double> empleados = new Dictionary<int, double>();

            while (true)
            {
                Console.WriteLine("Identificación del empleado:");
                int t_ID = 0;
                while (!int.TryParse(Console.ReadLine(), out t_ID))
                {
                    Console.WriteLine("Por favor ingrese un número real...");
                }

                Console.WriteLine("Sueldo del empleado:");
                double t_sueldo = 0;
                while (!double.TryParse(Console.ReadLine(), out t_sueldo))
                {
                    Console.WriteLine("Por favor ingrese un número real...");
                }

                empleados[t_ID] = t_sueldo;

                Console.WriteLine("¿Continuar? y/n");
                string ans = Console.ReadLine().ToLower();
                if (ans == "n")
                {
                    break;
                } 
            }

            foreach (KeyValuePair<int, double> empleado in empleados)
            {
                if (empleado.Value >= sueldo)
                {
                    ID = empleado.Key;
                    sueldo = empleado.Value;
                }
            }

            Console.WriteLine("Empleado #{0}: ${1}", ID, sueldo);

            return new Dictionary<string, dynamic>
            {
                {
                    "ID",
                    ID
                },
                {
                    "sueldo",
                    sueldo
                }
            };
        }
    }
}
