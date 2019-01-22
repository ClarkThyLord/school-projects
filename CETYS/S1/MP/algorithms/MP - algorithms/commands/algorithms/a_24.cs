using System;
using System.Collections.Generic;

namespace MP___algorithms.commands.algorithms
{
    public class a_24
    {
        public static Dictionary<string, dynamic> run()
        {
            Console.WriteLine("Monto:");
            double monto = 0;
            while (!double.TryParse(Console.ReadLine(), out monto))
            {
                Console.WriteLine("Por favor ingrese un número real...");
            }
            
            double descuento = 0;
            if (monto <= 500)
            {
                descuento = 0;
            } else if (500 < monto && monto <= 1000)
            {
                descuento = 0.05;
            } else if (1000 < monto && monto <= 7500)
            {
                descuento = 0.11;
            } else if (7500 < monto && monto <= 15000)
            {
                descuento = 0.18;
            } else
            {
                descuento = 0.25;
            }

            double i_descuento  = monto * descuento;
            double total = monto - i_descuento;

            Console.WriteLine("Monto: ${0}\nDescuento: {1}%\nImporte de Descuento:${2}\nTotal: ${3}", monto, (descuento * 100), i_descuento, total);

            return new Dictionary<string, dynamic>
            {
                {
                    "monto",
                    monto
                },
                {
                    "impuesto",
                    descuento
                },
                {
                    "c_impuesto",
                    i_descuento
                },
                {
                    "total",
                    total
                }
            };
        }
    }
}
