using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP___menu.programas.estadistico
{
    class estadistico
    {
        public static void run ()
        {
            List<Dictionary<string, dynamic>> empleados = new List<Dictionary<string, dynamic>>();

            Console.WriteLine("Christian Moises Cuevas Larin 029794 ~ Estadisticas");

            while (true)
            {
                Console.WriteLine("Empleado #{0}", empleados.Count + 1);

                Console.WriteLine("Sueldo:");
                double sueldo = 0;
                while (!double.TryParse(Console.ReadLine(), out sueldo))
                {
                    Console.WriteLine("Por favor ingrese un número real...");
                }

                Console.WriteLine("Sexo: (f: femenino, m : masculino)");
                string sexo = "";
                while (true)
                {
                    sexo = Console.ReadLine().ToLower();

                    if (sexo == "f" || sexo == "m")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Por favor inserte un género válido, f: femenino, m : masculino...");
                        continue;
                    }
                }

                Console.WriteLine("Edad:");
                double edad = 0;
                while (!double.TryParse(Console.ReadLine(), out edad))
                {
                    Console.WriteLine("Por favor ingrese un número real...");
                }

                empleados.Add(new Dictionary<string, dynamic>
                {
                    {
                        "sueldo",
                        sueldo
                    },
                    {
                        "sexo",
                        sexo
                    },
                    {
                        "edad",
                        edad
                    }
                });

                Console.WriteLine("¿Continuar insertando empleados? y/n");
                string ans = Console.ReadLine().ToLower();
                if (ans == "y")
                {
                    continue;
                }
                else
                {
                    break;
                }
            }

            int mujeres_t = 0;
            int mujeres_sueldo_mas_500 = 0;
            int hombres_t = 0;
            int hombres_sueldo_menos_400 = 0;
            int mujeres_edad_mayor_20_menor_28 = 0;
            int hombres_edad_mayor_18_menor_25 = 0;
            double mujeres_nomina = 0;
            double hombres_nomina = 0;
            double total_nomina = 0;

            foreach (Dictionary<string, dynamic> empleado in empleados)
            {
                if (empleado["sexo"] == "f")
                {
                    mujeres_t += 1;
                    if (empleado["sueldo"] > 500)
                    {
                        mujeres_sueldo_mas_500 += 1;
                    }
                    if (empleado["edad"] > 20 && empleado["edad"] < 28)
                    {
                        mujeres_edad_mayor_20_menor_28 += 1;
                    }
                    mujeres_nomina += empleado["sueldo"];
                } else
                {
                    hombres_t += 1;
                    if (empleado["sueldo"] < 400)
                    {
                        hombres_sueldo_menos_400 += 1;
                    }
                    if (empleado["edad"] > 18 && empleado["edad"] < 25)
                    {
                        hombres_edad_mayor_18_menor_25 += 1;
                    }
                    hombres_nomina += empleado["sueldo"];
                }

                total_nomina += empleado["sueldo"];
            }

            Console.WriteLine("Mujeres Trabajadoras: {0}", mujeres_t);
            Console.WriteLine("Mujeres Sueldo Mayor a $500MX: {0}", mujeres_sueldo_mas_500);
            Console.WriteLine("Hombres Trabajadores: {0}", hombres_t);
            Console.WriteLine("Hombres Sueldo Menor a $400MX: {0}", hombres_sueldo_menos_400);
            Console.WriteLine("Porcentaje de Mujeres Dentro de la Fuerza de Trabajo: {0}% / 100%", (mujeres_t / (double)(mujeres_t + hombres_t)) * 100);
            Console.WriteLine("Porcentaje de Hombres Dentro de la Fuerza de Trabajo: {0}% / 100%", (hombres_t / (double)(mujeres_t + hombres_t)) * 100);
            Console.WriteLine("Mujeres Edad Mayor a 20 y Menor a 28: {0}", mujeres_edad_mayor_20_menor_28);
            Console.WriteLine("Hombres Edad Mayor a 18 y Menor a 25: {0}", hombres_edad_mayor_18_menor_25);
            Console.WriteLine("Nomina de Mujeres Trabajadoras: ${0}MX", mujeres_nomina);
            Console.WriteLine("Nomina de Hombres Trabajadores: ${0}MX", hombres_nomina);
            Console.WriteLine("Nomina Total de la Empresa: ${0}MX", total_nomina);
            Console.ReadKey();
        }
    }
}
