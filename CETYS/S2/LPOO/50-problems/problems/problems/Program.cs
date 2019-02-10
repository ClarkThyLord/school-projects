using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace problems
{
    class Program
    {
        static string VOCALS = "aeiou";

        static void Main(string[] args)
        {
            problem_45();
        }

        static void error(string msg, ConsoleColor return_color = ConsoleColor.White)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = return_color;
        }

        static int[] problem_1(int[] array)
        {
            int[] ordered = new int[array.Length];

            int par = 0;
            int impar = array.Length - 1;
            foreach (int n in array)
            {
                if (n % 2 == 0)
                {
                    ordered[par] = n;
                    par++;
                }
                else
                {
                    ordered[impar] = n;
                    impar--;
                }
            }

            Console.WriteLine("PROBLEM #1");
            Console.WriteLine("Entrada: [{0}]", string.Join(", ", array));
            Console.WriteLine("Salida:  [{0}]", string.Join(", ", ordered));

            return ordered;
        }

        static void problem_2(int[] array)
        {
            Console.WriteLine("PROBLEM #2");
            Console.WriteLine("Entrada: [{0}]", string.Join(", ", array));
            Console.WriteLine("Salida:");
            
            Dictionary<int, int> distances = new Dictionary<int, int>();
            for (int i = 0; i < array.Length; i++)
            {
                int n = array[i];
                if (distances.ContainsKey(n))
                {
                    Console.WriteLine("la distancia de {0} es {1}", n, i - distances[n]);
                    distances.Remove(n);
                }
                else distances.Add(n, i);

                if (i == array.Length - 1)
                {
                    foreach (KeyValuePair<int, int> distance in distances)
                    {
                        Console.WriteLine("la distancia de {0} es {1}", distance.Key, i - distance.Value);
                    }
                }
            }
        }

        static double problem_3(int[] array)
        {
            double average = 0;

            int min = 0, max = 0;
            for (int i = 0; i < array.Length; i++)
            {
                int n = array[i];

                if (i == 0) min = max = n;
                else if (n > max) max = n;
                else if (n < min) min = n;
            }

            foreach (int n in array)
            {
                if (n == min || n == max) continue;
                else average += n;
            }

            average /= array.Length;

            Console.WriteLine("PROBLEM #3");
            Console.WriteLine("Entrada: [{0}]", string.Join(", ", array));
            Console.WriteLine("Salida: {0}", average);

            return average;
        }

        static int[] problem_4(int min, int max, int amount)
        {
            int[] randoms = new int[amount];

            Random random = new Random();
            for (int i = 0; i < amount; i++)
            {
                randoms[i] = random.Next(min, max);
            }

            Console.WriteLine("PROBLEM #4");
            Console.WriteLine("Entrada: min : {0} - max: {1} - amount: {2}", min, max, amount);
            Console.WriteLine("Salida: {0}", string.Join(", ", randoms));

            return randoms;
        }

        static string problem_5(int size)
        {
            string piramid = "";

            for (int s = 1; s <= size; s++)
            {
                piramid += String.Concat(Enumerable.Repeat("*", s)) + "\n";
            }

            for (int s = size - 1; s > 0; s--)
            {
                piramid += String.Concat(Enumerable.Repeat("*", s)) + "\n";
            }

            Console.WriteLine("PROBLEM #5");
            Console.WriteLine("Entrada: size - {0}", size);
            Console.WriteLine("Salida:\n{0}", piramid);

            return piramid;
        }

        static string problem_6(string text)
        {
            char[] chars = text.ToCharArray();
            Array.Reverse(chars);
            string reversed = string.Concat(chars);
            
            Console.WriteLine("PROBLEM #6");
            Console.WriteLine("Entrada: {0}", text);
            Console.WriteLine("Salida: {0}", reversed);

            return reversed;
        }

        static int problem_7(string text)
        {
            int occurrences = 0;
            foreach (char c in text)
            {
                if (c == text[0]) occurrences++;
            }

            Console.WriteLine("PROBLEM #7");
            Console.WriteLine("Entrada: {0}", text);
            Console.WriteLine("Salida: {0}", occurrences);

            return occurrences;
        }

        static string problem_8(string text)
        {
            string word = text.Remove(0, 1).Insert(text.Length - 1, text[0].ToString() + (VOCALS.IndexOf(text[0]) == -1 ? "oy" : "y"));

            Console.WriteLine("PROBLEM #8");
            Console.WriteLine("Entrada: {0}", text);
            Console.WriteLine("Salida: {0}", word);

            return word;
        }

        static int[] problem_9(int[] numbers_1, int[] numbers_2)
        {
            List<int> common = new List<int>();

            foreach (int n in numbers_1)
            {
                if (!common.Contains(n) && numbers_2.Contains(n)) common.Add(n);
            }

            Console.WriteLine("PROBLEM #9");
            Console.WriteLine("Entrada: {0} - {1}", string.Join(", ", numbers_1), string.Join(", ", numbers_2));
            Console.WriteLine("Salida: {0}", string.Join(", ", common));

            return common.ToArray();
        }

        static void problem_10(int num = 0)
        {
            if (num == 0)
            {
                while (true)
                {
                    Console.WriteLine("Inserte un número entero positivo:");
                    if (!int.TryParse(Console.ReadLine(), out num) || num < 1)
                    {
                        Console.Clear();
                        error("no es un número entero positivo válido");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else break;
                }
                
                Console.Clear();
            }

            List<int> used = new List<int>();

            Console.WriteLine("PROBLEM #10");
            Console.WriteLine("Entrada: {0}", num);
            Console.WriteLine("Salida:");
            for (int n = 1; n < num; n++)
            {
                if (!used.Contains(n) && num % n == 0)
                {
                    used.Add(num / n);
                    Console.WriteLine("{0} x {1} = {2}", n, num / n, num);
                }
            }
        }

        static void problem_45()
        {
            int ans = -1;
            while (true)
            {
                Console.WriteLine("Opciones:");
                Console.WriteLine("salida             :   0");
                Console.WriteLine("problema #1 - 44   :   1 - 44");
                Console.WriteLine("***");
                
                Console.WriteLine("Que te gustaría hacer?");
                if (!int.TryParse(Console.ReadLine(), out ans)) {
                    Console.Clear();
                    error("no es una opción válida");
                }
                else
                {
                    Console.Clear();
                    switch (ans)
                    {
                        case 0:
                            return;
                        case 1:
                            problem_1(new int[] { 3, 2, 5, 4, 7, 8 });
                            break;
                        case 2:
                            problem_2(new int[] { 2, 4, 6, 3, 4, 8 });
                            break;
                        case 3:
                            problem_3(new int[] { 10, 1, 80, 2, 3, 4 });
                            break;
                        case 4:
                            problem_4(10, 100, 20);
                            break;
                        case 5:
                            problem_5(4);
                            break;
                        case 6:
                            problem_6("raborper oreiuq on");
                            break;
                        case 7:
                            problem_7("reposteria");
                            break;
                        case 8:
                            problem_8("comer");
                            problem_8("ella");
                            break;
                        case 9:
                            problem_9(new int[] { 2, 4, 6, 8, 10, 12 }, new int[] { 3, 6, 9, 12 });
                            break;
                        case 10:
                            problem_10();
                            break;
                        default:
                            error("no es una opción válida");
                            break;
                    }
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
