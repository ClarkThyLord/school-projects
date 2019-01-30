using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassWork_1
{
    class Program
    {
        static double SALDO_DE_TARJETA = 0, TASA_DE_INTERES_ANNUAL = 0, PORCENTAJE_DEL_PAGO_MINIMO = 0;

        static void Main(string[] args)
        {
            input_values();

            int ans = 0;
            while (true)
            {
                Console.Clear();

                Console.WriteLine("SALDO DE TARJETA: {0} - TASA DE INTERES ANNUAL: {1} - PORCENTAJE DEL PAGO MINIMO: {2}", SALDO_DE_TARJETA, TASA_DE_INTERES_ANNUAL, PORCENTAJE_DEL_PAGO_MINIMO);
                Console.WriteLine("***\nMENU\n---");
                Console.WriteLine("1) cambiar valores");
                Console.WriteLine("2) 10 años");
                Console.WriteLine("3) ");
                Console.WriteLine("4) salir");

                Console.WriteLine("---\nQue te gustaría hacer?");
                while (true)
                {
                    while (!int.TryParse(Console.ReadLine(), out ans))
                    {
                        Console.WriteLine("¡No es válido!");
                    }

                    if (ans > 0 && ans < 5) break;
                    else Console.WriteLine("¡No es válido!");
                }

                Console.Clear();

                switch (ans)
                {
                    case 1:
                        input_values();
                        break;
                    case 2:
                        work_1();
                        break;
                    case 4:
                        System.Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("No entiendo, inténtalo de nuevo...");
                        break;
                }
            }
        }

        static void input_values()
        {
            Console.Write("SALDO DE TARJETA: ");
            while (!double.TryParse(Console.ReadLine(), out SALDO_DE_TARJETA))
            {
                Console.WriteLine("¡No es válido!");
            }

            Console.Write("\nTASA DE INTERES ANNUAL: ");
            while (!double.TryParse(Console.ReadLine(), out TASA_DE_INTERES_ANNUAL))
            {
                Console.WriteLine("¡No es válido!");
            }

            Console.Write("\nPORCENTAJE DEL PAGO MINIMO: ");
            while (!double.TryParse(Console.ReadLine(), out PORCENTAJE_DEL_PAGO_MINIMO))
            {
                Console.WriteLine("¡No es válido!");
            }
        }

        static void work_1 ()
        {
            double temp = SALDO_DE_TARJETA;
            for (int y = 0; y < 10; y++)
            {
                for (int m = 0; m < 12; m++)
                {
                    Console.WriteLine("Pago mensual:");
                    double min_charge = temp * (PORCENTAJE_DEL_PAGO_MINIMO * 0.01);
                    temp -= min_charge;
                    Console.WriteLine("Pago minimo: {0}", min_charge);
                    Console.WriteLine("Saldo restante: {0}", temp);
                    Console.WriteLine("***");
                }

                double annual_charge = temp * (TASA_DE_INTERES_ANNUAL * 0.01);
                temp += annual_charge;
                Console.WriteLine("Incremento annual: {0}", annual_charge);
                Console.WriteLine("Saldo restante: {0}", temp);
                Console.WriteLine("---");
            }

            Console.ReadKey();
        }
    }
}
