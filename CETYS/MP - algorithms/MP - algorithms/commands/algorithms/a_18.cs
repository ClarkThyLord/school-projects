using System;

namespace MP___algorithms.commands.algorithms
{
    public class a_18
    {
        public static string run()
        {
            Console.WriteLine("Numero:");
            double numero = 0;
            while (!double.TryParse(Console.ReadLine(), out numero))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }

            string mensaje = "";
            if (numero >= 1)
            {
                mensaje = "positivo";
            } else if (numero <= -1)
            {
                mensaje = "negativo";
            } else
            {
                mensaje = "nulo";
            }

            Console.WriteLine("El numero {0} es un {1}", numero, mensaje);

            return mensaje;
        }
    }
}
