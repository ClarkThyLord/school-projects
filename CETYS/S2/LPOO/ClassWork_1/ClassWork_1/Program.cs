using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassWork_1
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static void work_1 ()
        {
            float SALDO = 0;
            while (float.TryParse(Console.ReadLine(), out SALDO))
            {
                Console.WriteLine("¡El saldo no es válido!");
            }
        }
    }
}
