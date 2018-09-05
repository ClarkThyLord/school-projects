using System;

namespace MP___algorithms.commands.algorithms
{
    public class a_8
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
            } else
            {
                salario += salario * 0.12;
            }

            Console.WriteLine("Salario actual: {0}", salario);

            return salario;
        }
    }
}
