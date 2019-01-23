using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace diagnostico
{
    class Program
    {
        static void Main(string[] args)
        {
            //    problem_1("HOLA");
            //    Console.Clear();
            //    problem_1("HELLO WORLD");
            //    Console.Clear();
            //    problem_1("gerardo del rincon de la macorra");

            problem_5(new int[10]
            {
                1,
                2,
                3,
                6,
                8,
                23,
                6,
                10,
                12,
                123
            });
        }

    static void problem_1 (string text)
        {

            int text_size = text.Length - 1;

            char[] reverse = text.ToCharArray();
            Array.Reverse(reverse);
            string text_reversed = new string(reverse);
                
            Console.WriteLine(text);
            for (int l = 0; l < text_size; l++)
            {
                if (l == 0 || l == text_size) continue;
                Console.Write(text[l].ToString());
                Console.Write(String.Concat(Enumerable.Repeat(" ", text.Length - 2)));
                Console.Write(text_reversed[l].ToString());
                Console.Write("\n");
            }

            Console.WriteLine(text_reversed);

            Console.ReadKey();
        }

        static int[] problem_5 (int[] numbers)
        {
            string results = "";

            for (int i = 0; i < numbers.Length; i++)
            {
                if (i == 0 || i == (numbers.Length - 1)) continue;

                int n = numbers[i];

                if (numbers[i - 1] < numbers[i] && numbers[i + 1] < numbers[i]) results += numbers[i];
            }

            Console.WriteLine(results);

            Console.ReadKey();

            return new int[1] {
                0
            };
        }
    }
}
