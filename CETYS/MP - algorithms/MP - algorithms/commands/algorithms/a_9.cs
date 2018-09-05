using System;

namespace MP___algorithms.commands.algorithms
{
    public class a_9
    {
        public static double run()
        {
            Console.WriteLine("Ejecutar modelo: (1-4)");
            int ans = 0;
            while (!int.TryParse(Console.ReadLine(), out ans) || ans < 1 || ans > 4)
            {
                Console.WriteLine("Por favor ingrese un número real, entre 1 e 4...");
            }

            Console.WriteLine("Valor:");
            double valor = 0;
            while (!double.TryParse(Console.ReadLine(), out valor))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }

            double resultado = 0;
            switch (ans)
            {
                case 1:
                    resultado = 100 * valor;

                    Console.WriteLine("100 * {0} = {1}", valor, resultado);
                    break;
                case 2:
                    resultado = Math.Pow(100, valor);

                    Console.WriteLine("100 ** {0} = {1}", valor, resultado);
                    break;
                case 3:
                    resultado = 100 / valor;

                    Console.WriteLine("100 / {0} = {1}", valor, resultado);
                    break;
                case 4:
                    resultado = 0;
                    break;
            }

            return resultado;
        }
    }
}
