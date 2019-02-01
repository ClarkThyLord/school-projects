using System;

namespace Tarjetazo
{
    class Program
    {
        static double SALDO = 0;
        static double INTERES_ANUAL = 0;
        static double INTERES_MENSUAL = 0;

        static void Main(string[] args)
        {
            //input_values();
            input_values(5000, 2, 12);

            Console.Clear();

            int ans = 0;
            while (true)
            {
                while (true)
                {
                    Console.WriteLine("SALDO: {0} INTERES MENSUAL: {2}% INTERES ANUAL: {1}%", SALDO, INTERES_MENSUAL, INTERES_ANUAL);
                    Console.WriteLine("***");
                    Console.WriteLine("1) Modificar valores");
                    Console.WriteLine("2) Problema 1");
                    Console.WriteLine("3) Problema 2");
                    Console.WriteLine("4) Problema 3");
                    Console.WriteLine("5) Salir");
                    Console.WriteLine("***");
                    Console.Write("¿Que te gustaría hacer?");
                    if (!int.TryParse(Console.ReadLine(), out ans))
                    {
                        error("No entendí eso, inténtalo de nuevo!");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }

                    if (ans > 0 && ans < 6) break;
                    else
                    {
                        error("No entendí eso, inténtalo de nuevo!");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }
                }

                Console.Clear();

                switch (ans)
                {
                    case 1:
                        input_values();
                        break;
                    case 2:
                        problem_1();
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

        struct monthly_charge
        {
            public double minimo_mensual;
            public double interes_pagado;
            public double pago_a_capital;
            public double saldo_restante;
        }

        static monthly_charge monthly_payment (double saldo, double interes_mensual, double interes_anual)
        {
            monthly_charge charge = new monthly_charge();

            charge.minimo_mensual = Math.Round((interes_mensual * 0.01) * saldo, 2);
            charge.interes_pagado = Math.Round(((interes_anual * 0.01) / 12) * saldo);
            charge.pago_a_capital = Math.Round(charge.minimo_mensual - charge.interes_pagado);
            charge.saldo_restante = Math.Round(saldo - charge.pago_a_capital);

            return charge;
        }

        static void problem_1(int years = 10)
        {
            double pago_total = 0;
            double saldo_restante = SALDO;
            for (int y = 0; y < years; y++)
            {
                for (int m = 0; m < 12; m++)
                {
                    monthly_charge charge = monthly_payment(saldo_restante, INTERES_MENSUAL, INTERES_ANUAL);

                    pago_total += charge.minimo_mensual;
                    saldo_restante = charge.saldo_restante;

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("AÑO: {3} MES: {4}/12\nMINIMO MENSUAL: ${0}\nPAGO A CAPITAL: ${1}\nSALDO RESTATE: ${2}\n---", charge.minimo_mensual, charge.pago_a_capital, charge.saldo_restante, y + 1, m + 1);
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("DESPUES DE {2} AÑO(S)...\nPAGO EN TOTAL: ${0}\nSALDO RESTANTE: ${1}", pago_total, saldo_restante, years);

            Console.ReadKey();
        }

        static void problem_2()
        {
            Console.ReadKey();
        }

        static void problem_3()
        {
            Console.ReadKey();
        }
    }
}
