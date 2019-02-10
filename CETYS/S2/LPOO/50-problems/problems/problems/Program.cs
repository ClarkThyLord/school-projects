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
            Console.WriteLine("Entrada: min : {0} - max : {1} - amount : {2}", min, max, amount);
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

        static string[] problem_11(string text)
        {
            string[] chars = new string[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                chars[i] = text[i].ToString();
            }
            
            Console.WriteLine("PROBLEM #11");
            Console.WriteLine("Entrada: {0}", text);
            Console.WriteLine("Salida: {0}", string.Join(", ", chars));

            return chars;
        }

        static string problem_12(string[] chars)
        {
            string text = string.Concat(chars);

            Console.WriteLine("PROBLEM #12");
            Console.WriteLine("Entrada: {0}", string.Join(", ", chars));
            Console.WriteLine("Salida: {0}", text);

            return text;
        }

        static int problem_13(int powers, int min, int max)
        {
            int sum = 0;

            for (int e = min; e <= max; e++)
            {
                sum += (int)Math.Pow(powers, e);
            }

            Console.WriteLine("PROBLEM #13");
            Console.WriteLine("Entrada: poderes : {0} - min : {1} - max : {2}", powers, min, max);
            Console.WriteLine("Salida: {0}", sum);

            return sum;
        }

        static bool problem_14(int number)
        {
            bool divisible = true;

            foreach (char n in number.ToString())
            {
                int num = int.Parse(n.ToString());
                if (number % num != 0)
                {
                    divisible = false;
                    break;
                }
            }

            Console.WriteLine("PROBLEM #14");
            Console.WriteLine("Entrada: {0}", number);
            Console.WriteLine("Salida: {0}", divisible);

            return divisible;
        }

        static double[] problem_15(int faces, int dices, int rolls)
        {
            Random random = new Random();
            double[] sums = new double[faces];

            for (int r = 0; r < rolls; r++)
            {
                for (int d = 0; d < dices; d++)
                {
                    sums[d] += random.Next(1, faces);
                }
            }

            Console.WriteLine("PROBLEM #15");
            Console.WriteLine("Entrada: faces : {0} - dices : {1} - rolls : {2}", faces, dices, rolls);
            Console.WriteLine("Salida:");
            for (int d = 0; d < dices; d++)
            {
                Console.WriteLine("{0} : {1}%", d + 1, Math.Round(sums[d] / rolls, 4));
            }

            return sums;
        }

        static int problem_16(int number_1, int number_2, int number_3)
        {
            int sum = 0;

            if (number_1 != 13)
            {
                sum += number_1;
                if (number_2 != 13)
                {
                    sum += number_2;
                    if (number_3 != 13)
                    {
                        sum += number_3;
                    }
                }
            }

            Console.WriteLine("PROBLEM #16");
            Console.WriteLine("Entrada: numero #1 : {0} - numero #2 : {1} - numero #3 : {2}", number_1, number_2, number_3);
            Console.WriteLine("Salida: {0}", sum);

            return sum;
        }

        static int problem_17(string text)
        {
            int occurrences = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (i == 0) continue;
                else if (char.ToLower(text[i]) == 's' && char.ToLower(text[i - 1]) == 's') occurrences++;
            }

            Console.WriteLine("PROBLEM #17");
            Console.WriteLine("Entrada: {0}", text);
            Console.WriteLine("Salida: {0}", occurrences);

            return occurrences;
        }

        static int problem_18(int divisible_1, int divisible_2, int min, int max)
        {
            int sum = 0;

            for (int n = min; n <= max; n++)
            {
                if (n % divisible_1 == 0 || n % divisible_2 == 0) sum += n;
            }

            Console.WriteLine("PROBLEM #18");
            Console.WriteLine("Entrada: divisible #1 : {0} - divisible #2 : {1} - min : {2} - max : {3}", divisible_1, divisible_2, min, max);
            Console.WriteLine("Salida: {0}", sum);

            return sum;
        }

        static int problem_19(int goal)
        {
            int sum = 0;

            for (int n = 1; n <= goal; n++)
            {
                sum += n + (n - 1);
            }

            Console.WriteLine("PROBLEM #19");
            Console.WriteLine("Entrada: goal : {0}", goal);
            Console.WriteLine("Salida: {0}", sum);

            return sum;
        }

        static int problem_20(int number)
        {
            int sum = 0;

            for (int n = 0; n < number; n++)
            {
                sum += n;
            }

            Console.WriteLine("PROBLEM #20");
            Console.WriteLine("Entrada: {0}", number);
            Console.WriteLine("Salida: {0}", sum);

            return sum;
        }

        static int problem_21(int number)
        {
            int sum = 0;

            for (int n = 1; n <= number; n++)
            {
                if (number % n == 0) sum += n;
            }

            Console.WriteLine("PROBLEM #21");
            Console.WriteLine("Entrada: {0}", number);
            Console.WriteLine("Salida: {0}", sum);

            return sum;
        }

        static double[] problem_22(double[] numbers, double power)
        {
            double[] elevated = new double[numbers.Length];

            for (int i = 0; i < numbers.Length; i++)
            {
                elevated[i] = Math.Pow(numbers[i], power);
            }

            Console.WriteLine("PROBLEM #18");
            Console.WriteLine("Entrada: numbers : {0} - power : {1}", string.Join(", ", numbers), power);
            Console.WriteLine("Salida: {0}", string.Join(", ", elevated));

            return elevated;
        }

        static double[] problem_23(double[] numbers)
        {
            double[] elevated = new double[numbers.Length];

            for (int i = 0; i < numbers.Length; i++)
            {
                elevated[i] = Math.Pow(numbers[i], i);
            }

            Console.WriteLine("PROBLEM #23");
            Console.WriteLine("Entrada: {0}", string.Join(", ", numbers));
            Console.WriteLine("Salida: {0}", string.Join(", ", elevated));

            return elevated;
        }

        static double problem_24(double[] numbers)
        {
            double sum = 0;

            bool active = false;
            foreach (double number in numbers)
            {
                if (!active && number == 5) active = true;
                else if (active) sum += number;
            }

            Console.WriteLine("PROBLEM #24");
            Console.WriteLine("Entrada: {0}", string.Join(", ", numbers));
            Console.WriteLine("Salida: {0}", sum);

            return sum;
        }

        static bool problem_25(double[] numbers)
        {
            bool duplicate = false;

            for (int i = 0; i < numbers.Length; i++)
            {
                for (int si = i + 1; si < numbers.Length; si++)
                {
                    if (numbers[i] == numbers[si]) duplicate = true;
                    if (duplicate) break;
                }
                if (duplicate) break;
            }

            Console.WriteLine("PROBLEM #25");
            Console.WriteLine("Entrada: {0}", string.Join(", ", numbers));
            Console.WriteLine("Salida: {0}", duplicate);

            return duplicate;
        }

        static double problem_26(double[] numbers)
        {
            double root_of_average = Math.Sqrt(numbers.Average());

            Console.WriteLine("PROBLEM #26");
            Console.WriteLine("Entrada: [{0}]", string.Join(", ", numbers));
            Console.WriteLine("Salida: {0}", Math.Round(root_of_average, 2));

            return root_of_average;
        }

        static int problem_27(int divisible, double[] numbers)
        {
            int occurrences = 0;

            foreach (double number in numbers)
            {
                if (number % divisible == 0) occurrences++;
            }

            Console.WriteLine("PROBLEM #27");
            Console.WriteLine("Entrada: divisible : {0} - numbers : [{1}]", divisible, string.Join(", ", numbers));
            Console.WriteLine("Salida: {0}", occurrences);

            return occurrences;
        }

        static double[] problem_28(double[] numbers_1, double[] numbers_2)
        {
            List<double> common = new List<double>();

            foreach (double n in numbers_1)
            {
                if (!common.Contains(n) && numbers_2.Contains(n)) common.Add(n);
            }

            Console.WriteLine("PROBLEM #28");
            Console.WriteLine("Entrada: {0} - {1}", string.Join(", ", numbers_1), string.Join(", ", numbers_2));
            Console.WriteLine("Salida: {0}", string.Join(", ", common));

            return common.ToArray();
        }

        static string problem_29(int size)
        {
            string ramp = "";

            for (int s = size; s > 0; s--)
            {
                ramp += String.Concat(Enumerable.Repeat("*", s)) + "\n";
                s--;
            }

            Console.WriteLine("PROBLEM #29");
            Console.WriteLine("Entrada: size - {0}", size);
            Console.WriteLine("Salida:\n{0}", ramp);

            return ramp;
        }
        
        static int problem_30(string text, string key)
        {
            int occurrences = 0;

            string temp = text;
            int index = temp.IndexOf(key);
            while (index != -1)
            {
                occurrences++;
                temp = temp.Remove(index, key.Length);
                index = temp.IndexOf(key);
            }

            Console.WriteLine("PROBLEM #30");
            Console.WriteLine("Entrada: key : {0} - text : {1}", key, text);
            Console.WriteLine("Salida: {0}", occurrences);

            return occurrences;
        }

        static int[] problem_31(int divisible, int[] numbers)
        {
            int[] valued = new int[numbers.Length];

            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] % divisible == 0) valued[i] = numbers[i];
                else
                {
                    int temp = numbers[i];
                    do temp++; while (temp % 2 != 0);
                    valued[i] = temp;
                }
            }

            Console.WriteLine("PROBLEM #31");
            Console.WriteLine("Entrada: divisible : {0} - numbers : {1}", divisible, string.Join(", ", numbers));
            Console.WriteLine("Salida: {0}", string.Join(", ", valued));

            return valued;
        }

        static void problem_32(int pesos)
        {
            Console.WriteLine("PROBLEM #32");
            Console.WriteLine("Entrada: {0}", pesos);
            Console.WriteLine("Salida:");

            int fix = 0;
            while (true)
            {
                int temp = pesos;

                int fits = (temp / 10) - fix;
                if (fits > 0)
                {
                    temp -= fits * 10;
                    Console.WriteLine("$10 : #{0}", fits);
                }

                fits = (temp / 5) - (fix % 2 == 0 ? fix / 2 : 0);
                if (fits > 0)
                {
                    temp -= fits * 5;
                    Console.WriteLine("$5 : #{0}", fits);
                }
                

                fits = (temp / 2) - (fix % 3 == 0 ? fix / 3 : 0);
                if (fits > 0)
                {
                    temp -= fits * 2;
                    Console.WriteLine("$2 : #{0}", fits);
                }

                if (temp > 0) Console.WriteLine("$1 : #{0}", temp);

                Console.WriteLine("---");

                if (temp == pesos) break;
                else fix++;
            }
        }

        static double[] problem_33(double sum, double[] numbers)
        {
            double[] sums = new double[numbers.Length];

            for (int i = 0; i < numbers.Length; i++)
            {
                sums[i] = numbers[i] + sum;
            }

            Console.WriteLine("PROBLEM #33");
            Console.WriteLine("Entrada: sum : {0} - numbers : {1}", sum, string.Join(", ", numbers));
            Console.WriteLine("Salida: {0}", string.Join(", ", sums));

            return sums;
        }

        static double[] problem_34(int[] numbers)
        {
            double[] averages = new double[2];

            double pars = 0;
            double impars = 0;
            foreach (int number in numbers)
            {
                if (number % 2 == 0)
                {
                    averages[0] += number;
                    pars++;
                } else
                {
                    averages[1] += number;
                    impars++;
                }
            }

            averages[0] /= pars;
            averages[1] /= impars;

            Console.WriteLine("PROBLEM #34");
            Console.WriteLine("Entrada: {0}", string.Join(", ", numbers));
            Console.WriteLine("Salida: pares : {0} - nones : {1}", averages[0], averages[1]);

            return averages;
        }

        static string problem_35(string text)
        {
            string modified = "";

            foreach (char letter in text)
            {
                if (VOCALS.IndexOf(letter) == -1) modified += letter;
            }

            Console.WriteLine("PROBLEM #35");
            Console.WriteLine("Entrada: {0}", text);
            Console.WriteLine("Salida: {0}", modified);

            return modified;
        }

        static DateTime problem_36()
        {
            Random random = new Random();
            DateTime date = new DateTime(random.Next(1900, 2099), random.Next(1, 12), random.Next(1, 31));

            Console.WriteLine("PROBLEM #36");
            //Console.WriteLine("Entrada: {0}", text);
            Console.WriteLine("Salida: {0}", date);

            return date;
        }

        static int problem_37(int[] numbers)
        {
            int occurrences = 0;

            foreach (int number in numbers) if (number == 0) occurrences++;

            Console.WriteLine("PROBLEM #37");
            Console.WriteLine("Entrada: {0}", string.Join(", ", numbers));
            Console.WriteLine("Salida: {0}", occurrences);

            return occurrences;
        }

        static bool problem_38(int[] numbers)
        {
            bool real = true;

            for (int i = 0; i < numbers.Length; i++)
            {
                if (i == 0 || i == numbers.Length - 1) continue;
                else if (numbers[i] + numbers[i - 1] != numbers[i + 1])
                {
                    real = false;
                    break;
                }
            }

            Console.WriteLine("PROBLEM #38");
            Console.WriteLine("Entrada: {0}", string.Join(", ", numbers));
            Console.WriteLine("Salida: {0}", real);

            return real;
        }

        static int problem_39(int[] numbers)
        {
            int sum = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                int n = numbers[i];
                if (i < 5) sum += n;
                else sum -= n;
            }

            Console.WriteLine("PROBLEM #39");
            Console.WriteLine("Entrada: {0}", string.Join(", ", numbers));
            Console.WriteLine("Salida: {0}", sum);

            return sum;
        }

        static int problem_40(int number_1 = 0, int number_2 = 0)
        {
            if (number_1 == 0)
            {
                while (true)
                {
                    Console.WriteLine("Inserte un número:");
                    if (!int.TryParse(Console.ReadLine(), out number_1))
                    {
                        Console.Clear();
                        error("no es un número válido");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else break;
                }

                Console.Clear();
            }

            if (number_2 == 0)
            {
                while (true)
                {
                    Console.WriteLine("Inserte un número:");
                    if (!int.TryParse(Console.ReadLine(), out number_2))
                    {
                        Console.Clear();
                        error("no es un número válido");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else break;
                }

                Console.Clear();
            }

            int lcd = 2;
            for (; lcd < ((number_1 < number_2) ? number_1 : number_2); lcd++)
            {
                if (number_1 % lcd == 0 && number_2 % lcd == 0) break;
            }

            Console.WriteLine("PROBLEM #40");
            Console.WriteLine("Entrada: numero #1 : {0} - numero #2 : {1}", number_1, number_2);
            Console.WriteLine("Salida: {0}", lcd);

            return lcd;
        }
        
        static int[] problem_41()
        {
            Random random = new Random();

            int _7_11 = 0;
            int _2_3_12 = 0;

            for (int r = 0; r < 1000; r++)
            {
                int sum = 0;
                for (int d = 0; d < 2; d++)
                {
                    sum += random.Next(1, 6);
                }

                if (sum == 7 || sum == 11) _7_11 += 1;
                else if (sum == 2 || sum == 3 || sum == 12) _2_3_12 += 1;
            }

            Console.WriteLine("PROBLEM #41");
            //Console.WriteLine("Entrada:");
            Console.WriteLine("Salida: 7 o 11 : #{0} - 2 o 3 o 12 : #{1}", _7_11, _2_3_12);

            return new int[] { _7_11, _2_3_12 };
        }

        static int problem_42()
        {
            int number = 1;

            while (true)
            {
                bool valid = true;
                for (int n = 1; n <= 10; n++)
                {
                    if (number % n != 0)
                    {
                        valid = false;
                        break;
                    }
                }
                if (valid) break;
                else number++;
            }

            Console.WriteLine("PROBLEM #42");
            //Console.WriteLine("Entrada:");
            Console.WriteLine("Salida: {0}", number);

            return number;
        }

        static double problem_43(double[] numbers)
        {
            double sum_1 = 0;
            double sum_2 = 0;
            foreach (double number in numbers)
            {
                sum_1 += Math.Pow(number, 2);
                sum_2 += number;
            }

            sum_2 = Math.Pow(sum_2, 2);

            double difference = sum_2 - sum_1;

            Console.WriteLine("PROBLEM #43");
            Console.WriteLine("Entrada: {0}", string.Join(", ", numbers));
            Console.WriteLine("Salida: {0}", difference);

            return difference;
        }

        static int problem_44(string text_1, string text_2)
        {
            int differences = 0;

            for (int i = 0; i < text_1.Length; i++)
            {
                if (text_1[i] != text_2[i]) differences++;
            }

            Console.WriteLine("PROBLEM #44");
            Console.WriteLine("Entrada: text #1 : {0} - text #2 : {1}", text_1, text_2);
            Console.WriteLine("Salida: {0}", differences);

            return differences;
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
                        case 11:
                            problem_11("Hola mundo");
                            break;
                        case 12:
                            problem_12(new string[] { "H", "o", "l", "a", " ", "m", "u", "n", "d", "o" });
                            break;
                        case 13:
                            problem_13(2, 2, 2050);
                            break;
                        case 14:
                            problem_14(12);
                            problem_14(45);
                            break;
                        case 15:
                            problem_15(6, 5, 1000);
                            break;
                        case 16:
                            problem_16(4, 2, 3);
                            problem_16(4, 2, 13);
                            problem_16(4, 13, 3);
                            problem_16(13, 2, 3);
                            break;
                        case 17:
                            problem_17("Mississippi");
                            problem_17("essscobass");
                            break;
                        case 18:
                            problem_18(3, 5, 1, 1000);
                            break;
                        case 19:
                            problem_19(20);
                            break;
                        case 20:
                            problem_20(15);
                            break;
                        case 21:
                            problem_21(12);
                            problem_21(5);
                            break;
                        case 22:
                            problem_22(new double[] { 1, 2, 3, 4, 10 }, 2);
                            break;
                        case 23:
                            problem_23(new double[] { 1, 2, 3, 4, 10 });
                            break;
                        case 24:
                            problem_24(new double[] { 2, 5, 1, 4, 7 });
                            problem_24(new double[] { 12, 6, 8, 7, 5 });
                            problem_24(new double[] { 5, 7, 1, 2 });
                            break;
                        case 25:
                            problem_25(new double[] { 2, 8, 4, 5, 2, 4 });
                            problem_25(new double[] { 1, 2, 3, 4, 5, 6 });
                            break;
                        case 26:
                            problem_26(new double[] { 2, 8, 4, 5, 2, 4 });
                            break;
                        case 27:
                            problem_27(7, new double[] { 21, 3, 7, 12, 8, 100, 70 });
                            break;
                        case 28:
                            problem_28(new double[] { 9, 5, 2, 4 }, new double[] { 1, 9, 2, 6 });
                            break;
                        case 29:
                            problem_29(10);
                            break;
                        case 30:
                            problem_30("abnabaababa", "ba");
                            break;
                        case 31:
                            problem_31(3, new int[] { 2, 7, 9, 11, 16 });
                            break;
                        case 32:
                            problem_32(100);
                            break;
                        case 33:
                            problem_33(3, new double[] { 2, 7, 9, 11, 16 });
                            break;
                        case 34:
                            problem_34(new int[] { 2, 7, 9, 11, 16 });
                            break;
                        case 35:
                            problem_35("hello world!");
                            break;
                        case 36:
                            problem_36();
                            break;
                        case 37:
                            problem_37(new int[] { 0, 2, 7, 9, 0, 0, 11, 16 });
                            break;
                        case 38:
                            problem_38(new int[] { 2, 2, 4, 6, 10, 16 });
                            problem_38(new int[] { 1, 1, 6, 8 });
                            break;
                        case 39:
                            problem_39(new int[] { 2, 5, 4, 7, 8, 9, 1, 3, 4, 5 });
                            break;
                        case 40:
                            problem_40();
                            break;
                        case 41:
                            problem_41();
                            break;
                        case 42:
                            problem_42();
                            break;
                        case 43:
                            problem_43(new double[] { 1, 2, 3, 4, 5 });
                            break;
                        case 44:
                            problem_44("hola", "hell");
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
