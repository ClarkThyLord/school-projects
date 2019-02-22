using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace palindromo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(problem("racecar"));
            Console.WriteLine(problem("mam"));
            Console.ReadKey();
        }

        static bool problem(string text, string reversed="", int index=0, bool recursive = false)
        {
            bool result = true;

            if (reversed == "")
            {
                char[] treversed = text.ToArray();
                Array.Reverse(treversed);
                reversed = string.Concat(treversed);
            }
            
            if (text.Length <= 0 || index == text.Length || text[index] != reversed[index]) result = false;

            if (result && index < text.Length) problem(text, reversed, index + 1, true);

            return result;
        }
    }
}
