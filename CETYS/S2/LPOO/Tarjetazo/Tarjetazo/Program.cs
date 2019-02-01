using System;

namespace Tarjetazo
{
    class Program
    {
        static double SALDO = 0;
        static double INTERES_MENSUAL = 0;
        static double INTERES_ANUAL = 0;

        static void Main(string[] args)
        {
            //input_values();
            input_values(5000, 2, 18);

            Console.Clear();

            int ans = 0;
            while (true)
            {
                while (true)
                {
                    header();
                    Console.WriteLine("1) Modificar valores");
                    Console.WriteLine("2) Problema 1");
                    Console.WriteLine("3) Problema 2");
                    Console.WriteLine("4) Problema 3");
                    Console.WriteLine("5) Salir");
                    Console.WriteLine("***");
                    Console.Write("¿Que te gustaría hacer? ");
                    if (int.TryParse(Console.ReadLine(), out ans) && ans > 0 && ans < 6) break;
                    error("No entendí eso, inténtalo de nuevo!");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }

                Console.Clear();
                header();

                switch (ans)
                {
                    case 1:
                        input_values();
                        break;
                    case 2:
                        problem_1();
                        //problem_1(1);
                        //problem_1(100);
                        break;
                    case 3:
                        problem_2();
                        break;
                    case 4:
                        problem_3();
                        break;
                    case 5:
                        System.Environment.Exit(1);
                        break;
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
            }
        }

        static void header ()
        {
            Console.WriteLine("SALDO: {0} INTERES MENSUAL: {1}% INTERES ANUAL: {2}%\n***", SALDO, INTERES_MENSUAL, INTERES_ANUAL);
        }

        static void error (string msg, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = color;
        }

        static void input_values (double saldo = 0, double interes_mensual = 0, double interes_anual = 0)
        {
            if (saldo > 0) SALDO = saldo;
            else
            {
                Console.Write("SALDO: ");
                while (!double.TryParse(Console.ReadLine(), out SALDO))
                {
                    error("¡Eso no es válido!");
                }
            }

            if (interes_mensual > 0) INTERES_MENSUAL = interes_mensual;
            else
            {
                Console.Write("INTERES MENSUAL: ");
                while (!double.TryParse(Console.ReadLine(), out INTERES_MENSUAL))
                {
                    error("¡Eso no es válido!");
                }
            }

            if (interes_anual > 0) INTERES_ANUAL = interes_anual;
            else
            {
                Console.Write("INTERES ANUAL: ");
                while (!double.TryParse(Console.ReadLine(), out INTERES_ANUAL))
                {
                    error("¡Eso no es válido!");
                }
            }
        }

        static void problem_1(int years = 10)
        {
            double pago_total = 0;
            double saldo_restante = SALDO;
            for (int y = 0; y < years; y++)
            {
                for (int m = 0; m < 12; m++)
                {
                    double minimo_mensual = Math.Round((INTERES_MENSUAL * 0.01) * saldo_restante, 2);
                    double interes_pagado = Math.Round(((INTERES_ANUAL * 0.01) / 12) * saldo_restante);
                    double pago_a_capital = Math.Round(minimo_mensual - interes_pagado);
                    saldo_restante = Math.Round(saldo_restante - pago_a_capital);

                    pago_total += minimo_mensual;

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("AÑO: {3}/{5} MES: {4}/12\nMINIMO MENSUAL: ${0}\nPAGO A CAPITAL: ${1}\nSALDO RESTATE: ${2}\n---", minimo_mensual, pago_a_capital, saldo_restante, y + 1, m + 1, years);
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("DESPUES DE {2} AÑO(S)...\nPAGO EN TOTAL: ${0}\nSALDO RESTANTE: ${1}", pago_total, saldo_restante, years);

            Console.ReadKey();
        }

        static void problem_2(int years = 1)
        {
            double saldo = SALDO;
            
            double pago_minimo_fijo = (Math.Ceiling((SALDO / (years * 12)) * 0.01) / 0.01) * 2;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("PAGO MINIMO FIJO: {0}", pago_minimo_fijo);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("MESES PARA PAGAR SALDO: {0}/12", Math.Ceiling(SALDO / pago_minimo_fijo));

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("---");
            for (int y = 0; y < years; y++)
            {
                for (int m = 0; m < 12; m++)
                {
                    saldo = Math.Round(saldo * ((INTERES_ANUAL * 0.01) / 12 + 1) - pago_minimo_fijo, 2);

                    if (saldo < 0) Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("MES: {0}/12 : SALDO: {1}", m + 1, saldo);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            Console.WriteLine("---");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("SALDO FINAL DESPUES DE {1} AÑO(S) : {0}", saldo, years);

            Console.ReadKey();
        }

        static void problem_3()
        {
            Console.ReadKey();
        }
    }
}
