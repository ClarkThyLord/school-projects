using System;

namespace MP___algorithms.commands.algorithms
{
    public class a_5
    {
        public static string run()
        {
            Console.WriteLine("Calificación:");
            double calificacion = 0;
            while (!double.TryParse(Console.ReadLine(), out calificacion))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }

            string mensaje = (calificacion > 8) ? "Aprobado" : "Reprobado";

            Console.WriteLine(mensaje);

            return mensaje;
        }
    }
}
