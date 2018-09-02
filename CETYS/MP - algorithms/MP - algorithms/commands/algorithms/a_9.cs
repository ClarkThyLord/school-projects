using System;

namespace MP___algorithms.commands.algorithms
{
    public class a_9
    {
        public static double run()
        {
            Console.WriteLine("Cantidad de sonidos del cricket por minuto:");
            double grillo = 0;
            while (!double.TryParse(Console.ReadLine(), out grillo))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }

            double temperatura = (grillo / 4) + 40;

            Console.WriteLine("Temperatura: {0}", temperatura);

            return temperatura;
        }
    }
}
