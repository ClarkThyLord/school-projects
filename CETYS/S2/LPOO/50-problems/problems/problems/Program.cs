using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace problems
{
    class Program
    {
        static void Main(string[] args)
        {
            problem_1(new int[] { 3, 2, 5, 4, 7, 8, 13 });
            problem_2(new int[] { 2, 4, 6, 3, 4, 8 });
        }

        public static int[] problem_1(int[] array)
        {
            Console.WriteLine("PROBLEM #1");
            Console.WriteLine("[{0}]", string.Join(", ", array));

            Array.Sort(array, (INT_1, INT_2) =>
            {
                if (INT_1 % 2 == INT_2 % 2) return 0;
                else if (INT_1 % 2 == 1) return 1;
                else return -1;
            });

            Console.WriteLine("[{0}]", string.Join(", ", array));
            Console.ReadKey();
            Console.Clear();

            return array;
        }

        public static void problem_2(int[] array)
        {
            Console.WriteLine("PROBLEM #2");
            Console.WriteLine("[{0}]", string.Join(", ", array));

            Dictionary<int, int> distances = new Dictionary<int, int>();

            //for (int INT = 0; INT < array.Length; INT++)
            //{
            //if (!distances.ContainsKey(INT)) distances.Add(INT, 0);

            //if (distances[INT] == -1)
            //{
            //    distances[INT] = 0;
            //}
            //else if (distances[INT] > 0)
            //{
            //    Console.WriteLine("la distancia de {0} es {1}", INT, distances[INT]);
            //    distances[INT] = -1;

            //}
            //else
            //{
            //    distances[INT] += 1;
            //}

            //Console.WriteLine(string.Join(", ", distances));
            //}

            foreach (int INT in array)
            {
                if (!distances.ContainsKey(INT))
                {
                    distances.Add(INT, 0);
                    continue;
                }

                foreach (int NUM in distances.Keys)
                {
                    distances[NUM] += 1;
                }

                if (distances[INT] == -1)
                {
                    distances[INT] = 0;
                }
                else if (distances[INT] > 0)
                {
                    Console.WriteLine("la distancia de {0} es {1}", INT, distances[INT]);
                    distances[INT] = -1;
                }

                Console.WriteLine(string.Join(", ", distances));
            }

            Console.ReadKey();
            Console.Clear();
        }
    }
}
