using System;
using System.Collections.Generic;

namespace MP___algorithms.commands.algorithms
{
    public class a_16
    {
        public static Dictionary<string, dynamic> run()
        {

            Console.WriteLine("Matrícula:");
            string matricula = Console.ReadLine();

            Console.WriteLine("Carrera: (economia : 1, computacion : 2, administracion : 3, contabilidad : 4)");
            int carrera = 0;
            while (!int.TryParse(Console.ReadLine(), out carrera) && carrera < 5 && carrera > 1)
            {
                Console.WriteLine("Por favor ingrese un número real, entre 1 y 4...");
            }

            Console.WriteLine("Semestres:");
            int semestres = 0;
            while (!int.TryParse(Console.ReadLine(), out semestres) && semestres < 5 && semestres > 1)
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }

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

            string nombre_carrera = "";
            string mensaje = "";
            switch (carrera)
            {
                case 1:
                    nombre_carrera = "Economía";
                    mensaje = (semestres >= 6 && promedio >= 8.8) ? "Aprobado" : "Reprobado";
                    break;
                case 2:
                    nombre_carrera = "Computación";
                    mensaje = (semestres > 6 && promedio > 8.5) ? "Aprobado" : "Reprobado";
                    break;
                case 3:
                    nombre_carrera = "Administración";
                    mensaje = (semestres > 5 && promedio > 8.5) ? "Aprobado" : "Reprobado";
                    break;
                case 4:
                    nombre_carrera = "Contabilidad";
                    mensaje = (semestres > 5 && promedio > 8.5) ? "Aprobado" : "Reprobado";
                    break;
            }
            
            Console.WriteLine("Matrícula: {0}, Carrera: {1}, Status: {3}", matricula, nombre_carrera, mensaje);

            return new Dictionary<string, dynamic>
            {
                {
                    "matricula",
                    matricula
                },
                {
                    "carrera",
                    nombre_carrera
                },
                {
                    "mensaje",
                    mensaje
                }
            };
        }
    }
