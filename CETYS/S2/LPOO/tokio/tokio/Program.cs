using System;
using System.Collections.Generic;

namespace tokio
{
    class Program
    {
        static void Main(string[] args)
        {
            problem_1("Hello world!");
            //problem_2("6");
            problem_2("4d6");
            problem_3("edebaCEDecD");
            problem_4("murcielago");
            problem_5("Esta frase tiene cinco palabras!");
            problem_6("cafe", "cae");
            problem_7(new int[] { 12, -1, 3, 0, 4, 4});
            problem_8("14:23");
            //problem_8("01:10");
            problem_9("Abors");
            problem_10("Esta frase tiene cinco palabras!");
            problem_11("01/01");
            problem_12(new int[] { 0, 1, 5, 6, 3 });
            problem_13(new int[] { 0, 1, 5, 6, 3 });
        }

        static void problem_1(string text)
        {
            List<char> letters = new List<char>();

            bool found = false;
            foreach(char c in text)
            {
                char lc = char.ToLower(c);
                if (letters.Contains(lc))
                {
                    found = true;
                    Console.WriteLine("la letra {0} es la primera letra que se repite", lc);
                    break;
                }
                else letters.Add(lc);
            }

            if (!found) Console.WriteLine("no se encontró ninguna carta que se repite");

            Console.ReadKey();
            Console.Clear();
        }

        static int roll_dice(int faces, Random ran)
        {
            int roll = ran.Next(1, faces);
            Console.WriteLine("Roll: {0}", roll);
            return roll;
        }

        static void problem_2(string command)
        {
            int dices = 0;
            int faces = 0;
            int total = 0;
            Random ran = new Random();

            if (command.Length == 1 && int.TryParse(command, out faces))
            {
                total += roll_dice(faces, ran);
            }
            else
            {
                string[] command_parts = command.Split('d');
                if (command_parts.Length == 2 && int.TryParse(command_parts[0], out dices) && int.TryParse(command_parts[1], out faces))
                {
                    for (int i = 0; i < dices; i++)
                    {
                        total += roll_dice(faces, ran);
                    }
                }
                else
                {
                    Console.WriteLine("comando inválido");
                }
            }

            Console.WriteLine("total: {0}", total);

            Console.ReadKey();
            Console.Clear();
        }

        static Dictionary<char, int> problem_3(string text_scores)
        {
            Dictionary<char, int> scores = new Dictionary<char, int>() {
                {
                    'a',
                    0
                },
                {
                    'b',
                    0
                },
                {
                    'c',
                    0
                },
                {
                    'd',
                    0
                },
                {
                    'e',
                    0
                }
            };

            foreach (char c in text_scores)
            {
                if (scores.ContainsKey(char.ToLower(c))) scores[char.ToLower(c)] += char.IsUpper(c) ? -1 : 1;
            }

            foreach (KeyValuePair<char, int> score in scores)
            {
                Console.WriteLine("{0}: {1}", score.Key, score.Value);
            }
            Console.ReadKey();
            Console.Clear();

            return scores;
        }

        static void problem_4(string text)
        {
            string[] vocales = {
                "a",
                "e",
                "i",
                "o",
                "u"
            };

            foreach (string c in vocales)
            {
                text = text.Replace(c, "");
            }

            Console.WriteLine(text);
            Console.ReadKey();
            Console.Clear();
        }

        static void problem_5(string text)
        {
            string[] palabras = text.Split(" ");

            Console.WriteLine("{0} palabras", palabras.Length);
            Console.ReadKey();
            Console.Clear();
        }

        static void problem_6(string first, string second)
        {
            int match = -1;
            if (first.Length - 1 != second.Length) match = 0;
            else
            {
                int fc = 0;
                int removed = 0;
                for (int sc = 0; sc < second.Length; sc++)
                {
                    while (true)
                    {
                        if (second[sc] != first[fc] && removed == 0)
                        {
                            removed = 1;
                            fc++;
                            continue;
                        }
                        else if (second[sc] != first[fc] && removed > 0) match = 0;

                        fc++;
                        break;
                    }

                    if (removed > 0) break;
                }

                if (match != 0) match = 1;
            }

            Console.WriteLine(match == 1 ? true : false);
            Console.ReadKey();
            Console.Clear();
        }

        static int[] problem_7(int[] coeficientes)
        {
            int[] derivadas = new int[coeficientes.Length - 1];

            for (int e = coeficientes.Length - 1; e > -1; e--)
            {
                int coeficiente = coeficientes[e];
                if (coeficiente == 0) continue;

                string temp = "";

                if (coeficiente > 0 && e != coeficientes.Length - 1) temp += " + ";
                else if (coeficiente < 0) temp += " - ";

                if (coeficiente > 1) temp += Math.Abs(coeficiente);
                if (e > 0) temp += "x";
                if (e > 1) temp += string.Format("^{0}", e);

                if (e != 0) derivadas[e - 1] = coeficiente * e;

                Console.Write(temp);
            }
            Console.Write(" = 0");

            Console.WriteLine("\nDerivadas: ");
            foreach (int derivada in derivadas)
            {
                Console.Write(" {0} ", derivada);
            }

            Console.ReadKey();
            Console.Clear();

            return derivadas;
        }


        static void problem_8(string text)
        {
            bool valid = true;
            string[] tiempos = text.Split(":");
            int hora = 0;
            int minuto = 0;

            if (text.Length != 5)
            {
                valid = false;
            } else
            {
                if (!int.TryParse(tiempos[0], out hora) || !int.TryParse(tiempos[1], out minuto))
                {
                    valid = false;
                }
                else
                {
                    if (hora < 0 || hora > 24 || minuto < 0 || minuto >= 60)
                    {
                        valid = false;
                    }
                }
            }

            if (valid)
            {
                string[] horas =
                {
                    "",
                    "Una",
                    "Dos",
                    "Tres",
                    "Cuatro",
                    "Cinco",
                    "Seis",
                    "Ocho",
                    "Nueve",
                    "Diez",
                    "Once",
                    "Doce"
                };

                string[] minutos =
                {
                    "",
                    "uno",
                    "dos",
                    "tres",
                    "cuatro",
                    "cinco",
                    "seis",
                    "siete",
                    "ocho",
                    "nueve",
                    "diez",
                    "once",
                    "doce",
                    "trece",
                    "catorce",
                    "quince",
                    "dieciseis",
                    "diecisiete",
                    "dieciocho",
                    "diecinueve",
                    "veinte",
                    "veintiuno",
                    "veintidos",
                    "veintitres",
                    "veinticuatro",
                    "veinticinco",
                    "veintiseis",
                    "veintisiete",
                    "veintiocho",
                    "veintinueve",
                    "treinta",
                    "treinta y uno",
                    "Treinta Y Dos",
                    "Treinta Y Tres",
                    "Treinta Y Cuatro",
                    "Treinta Y Cinco",
                    "Treinta Y Seis",
                    "Treinta Y Siete",
                    "Treinta Y Ocho",
                    "Treinta Y Nueve",
                    "Cuarenta",
                    "Cuarenta Y Uno",
                    "Cuarenta Y Dos",
                    "Cuarenta Y Tres",
                    "Cuarenta Y Cuatro",
                    "Cuarenta Y Cinco",
                    "Cuarenta Y Seis",
                    "Cuarenta Y Siete",
                    "Cuarenta Y Ocho",
                    "Cuarenta Y Nueve",
                    "Cincuenta",
                    "Cincuenta Y Uno",
                    "Cincuenta Y Dos",
                    "Cincuenta Y Tres",
                    "Cincuenta Y Cuatro",
                    "Cincuenta Y Cinco",
                    "Cincuenta Y Seis",
                    "Cincuenta Y Siete",
                    "Cincuenta Y Ocho",
                    "Cincuenta Y Nueve"
                };

                Console.WriteLine("{3} -> {0} {1} de la {2}.", horas[hora > 12 ? hora - 12 : hora], minutos[minuto].ToLower(), hora > 12 ? "tarde" : "mañana", text);
            }
            else
            {
                Console.WriteLine("Formato inválido!");
            }

            Console.ReadKey();
            Console.Clear();
        }

        static bool problem_9(string text)
        {
            bool ordered = true;

            string ABC = "abcdefghijklmnopqrstuvwxyz";

            int i = -1;
            foreach (char c in text)
            {
                if (ABC.IndexOf(c) >= i)
                {
                    i = ABC.IndexOf(c);
                }
                else
                {
                    ordered = false;
                    break;
                }
            }

            Console.WriteLine("Las letras en el {0} están en orden alfabético -> {1}", text, ordered);
            Console.ReadKey();
            Console.Clear();

            return ordered;
        }

        static void problem_10(string text)
        {
            int promedio = 0;
            string[] palabras = text.Split(" ");
            foreach (string word in palabras)
            {
                promedio += word.Length;
            }

            promedio = promedio / palabras.Length;

            Console.WriteLine("{0} promedio", promedio);
            Console.ReadKey();
            Console.Clear();
        }

        static int problem_11(string date)
        {
            string[] targets = date.Split('/');
            int target_day = 0;
            int target_month = 0;
            if (targets.Length != 2 || !int.TryParse(targets[0], out target_day) || !int.TryParse(targets[1], out target_month)) Console.WriteLine("Formato de fecha no válido!");

            DateTime refrence = new DateTime(2019, 2, 4);
            DateTime target = new DateTime(DateTime.Now.Year, target_month, target_day);
            if (DateTime.Compare(refrence, target) == 1) target = target.AddYears(1);

            Console.WriteLine((target - refrence).TotalDays);
            Console.ReadKey();
            Console.Clear();

            return 365;
        }

        static int problem_12(int[] numeros)
        {
            int numero = 0;
            int real = 0;

            foreach (int n in numeros)
            {
                if (n > numero)
                {
                    real = numero;
                    numero = n;
                }
            }

            Console.WriteLine(real);
            Console.ReadKey();
            Console.Clear();

            return real;
        }

        static int problem_13(int[] numeros)
        {
            List<int> numbers = new List<int>();
            numbers.AddRange(numeros);

            int numero = 0;
            while (true)
            {
                if (numbers.Contains(numero)) numero++;
                else break;
            }

            Console.WriteLine(numero);
            Console.ReadKey();
            Console.Clear();

            return numero;
        }
    }
}
