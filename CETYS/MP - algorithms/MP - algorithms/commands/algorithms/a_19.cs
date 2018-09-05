using System;

namespace MP___algorithms.commands.algorithms
{
    public class a_19
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
            if (numero == 0)
            {
                mensaje = "nulo";
            }
            else if (numero % 2 == 0)
            {
                mensaje = "par";
            }
            else
            {
                mensaje = "impar";
            }

            Console.WriteLine("El numero {0} es un {1}", numero, mensaje);

            return mensaje;
        }
    }
}
