using System;
using System.Collections.Generic;

namespace MP___algorithms.commands.algorithms
{
    public class a_2
    {
        public static double run()
        {

            Console.WriteLine("Matrícula:");
            string matricula = Console.ReadLine();
            
            List<double> calificaciones = new List<double>();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Calificación {0}:", i + 1);

                double ans = 0;
                while (!double.TryParse(Console.ReadLine(), out ans))
                {
                    Console.WriteLine("Por favor ingrese un número real...");
                }

                calificaciones.Add(ans);
            }

            double suma = 0;
            foreach (double calificacion in calificaciones)
            {
                suma += calificacion;
            }

            double promedio = suma / 5;

            Console.WriteLine("Matrícula: {0}, Promedio: {1}", matricula, promedio);

            return promedio;
        }
    }
}
