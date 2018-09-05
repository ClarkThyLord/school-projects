using System;
using System.Collections.Generic;

namespace MP___algorithms.commands.algorithms
{
    public class a_21
    {
        public static Dictionary<string, dynamic> run()
        {
            Console.WriteLine("Christian Moises Cuevas Larin 029794");

            Console.WriteLine("Costo:");
            double costo = 0;
            while (!double.TryParse(Console.ReadLine(), out costo) && costo > 0)
            {
                Console.WriteLine("Por favor ingrese un número real, que no es negativo...");
            }

            Console.WriteLine("Costo: ${0}", costo);

            double total = costo;
            double temp = 0;
            double costo_actual = costo;
            double impuesto = 0;
            double c_impuesto = 0;
            if (costo <= 20)
            {
                temp = costo > 20 ? 20 : costo;
                costo_actual -= temp;
                impuesto = 0.0;
                c_impuesto = temp * 0;
                total += c_impuesto;
                Console.WriteLine("Impuesto: {0}%, Cargo de Impuesto: ${1}", impuesto * 100, c_impuesto);
            }
            if (costo > 20)
            {
                temp = costo > 20 ? 20 : costo;
                costo_actual -= temp;
                impuesto = 0.3;
                c_impuesto = temp * impuesto;
                total += c_impuesto;
                Console.WriteLine("Impuesto: {0}%, Cargo de Impuesto: ${1}", impuesto * 100, c_impuesto);
            }
            if (costo > 40 && costo < 500)
            {
                temp = costo > 460 ? 460 : costo;
                costo_actual -= temp;
                impuesto = 0.4;
                c_impuesto = temp * impuesto;
                total += c_impuesto;
                Console.WriteLine("Impuesto: {0}%, Cargo de Impuesto: ${1}", impuesto * 100, c_impuesto);
            }
            if (costo > 500)
            {
                temp = costo;
                costo_actual -= temp;
                impuesto = 0.5;
                c_impuesto = temp * impuesto;
                total += c_impuesto;
                Console.WriteLine("Impuesto: {0}%, Cargo de Impuesto: ${1}", impuesto * 100, c_impuesto);
            }

            Console.WriteLine("Total: ${0}", total);

            return new Dictionary<string, dynamic>
            {
                {
                    "costo",
                    costo
                },
                {
                    "impuesto",
                    impuesto
                },
                {
                    "carga_impuesto",
                    c_impuesto
                },
                {
                    "total",
                    total
                }
            };
        }
    }
}
