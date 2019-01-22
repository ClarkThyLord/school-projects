using System;

namespace MP___algorithms.commands.algorithms
{
    public class a_6
    {
        public static double run()
        {
            Console.WriteLine("Salario:");
            double salario = 0;
            while (!double.TryParse(Console.ReadLine(), out salario))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }

            if (salario < 1000)
            {
                salario += salario * 0.15;
            }

            Console.WriteLine("Salario actual: {0}", salario);

            return salario;
        }
    }
}
