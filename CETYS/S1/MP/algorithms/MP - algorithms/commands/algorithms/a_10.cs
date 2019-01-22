using System;

namespace MP___algorithms.commands.algorithms
{
    public class a_10
    {
        public static double run()
        {
            Console.WriteLine("Identificación del Trabajo: (1-4)");
            int ans = 0;
            while (!int.TryParse(Console.ReadLine(), out ans) && ans < 5 && ans >= 1)
            {
                Console.WriteLine("Por favor ingrese un número real, entre 1 e 4...");
            }

            Console.WriteLine("Salario:");
            double salario = 0;
            while (!double.TryParse(Console.ReadLine(), out salario))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }
            
            switch (ans)
            {
                case 1:
                    salario *= 0.15;
                    break;
                case 2:
                    salario *= 0.10;
                    break;
                case 3:
                    salario *= 0.8;
                    break;
                case 4:
                    salario *= 0.7;
                    break;
            }

            Console.WriteLine("Salario actual: {0}", salario);

            return salario;
        }
    }
}
