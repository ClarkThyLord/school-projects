using System;
using System.Collections.Generic;

namespace MP___algorithms.commands.algorithms
{
    public class a_20
    {
        public static double run()
        {
            Console.WriteLine("Categoría de Trabajo:");
            int cat_trabajo = 0;
            while (!int.TryParse(Console.ReadLine(), out cat_trabajo))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }

            Console.WriteLine("Salario:");
            double salario = 0;
            while (!double.TryParse(Console.ReadLine(), out salario))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }

            Console.WriteLine("Extra Horas:");
            double ext_horas = 0;
            while (!double.TryParse(Console.ReadLine(), out ext_horas))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }

            double ext_paga = 0;
            switch (cat_trabajo)
            {
                case 1:
                    ext_paga = 30;
                    break;
                case 2:
                    ext_paga = 38;
                    break;
                case 3:
                    ext_paga = 50;
                    break;
                case 4:
                    ext_paga = 70;
                    break;
            }

            double ext_salario = ext_horas * ext_paga;

            double total_salario = salario + ext_salario;
            
            Console.WriteLine("Sueldo Base: {0}, Categoría: {1}, Horas Trabajadas: {2}, Costo de Hora Extra: {3}, Sueldo Total: {4}", salario, cat_trabajo, ext_horas, ext_paga, total_salario);

            return total_salario;
        }
    }
}
