using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            PROBLEMS.Items.Add("Problem #1");
            PROBLEMS.Items.Add("Problem #2");
            PROBLEMS.Items.Add("Problem #3");
            PROBLEMS.Items.Add("Problem #4");
            PROBLEMS.Items.Add("Problem #5");
            PROBLEMS.Items.Add("Problem #7");
            PROBLEMS.Items.Add("Problem #8");
            PROBLEMS.Items.Add("Problem #9");
            PROBLEMS.Items.Add("Problem #10");

            PROBLEMS.SelectedIndex = 0;
            PROBLEMS.SelectionChanged += PROBLEMS_SelectionChanged;

            //Console.WriteLine(string.Join(", ", problem_1(100)));
            //Console.WriteLine(problem_2(120));
            //Console.WriteLine(problem_3("hhoaolla "));
            //Console.WriteLine(string.Join(", ", problem_4("#4C8ED53")));
            //Console.WriteLine(problem_7("hello world, this is a test!"));
            //Console.WriteLine(problem_8("XaDBACFdE"));
            //Console.WriteLine(problem_9("Hello world!"));
            //Console.WriteLine(problem_10(problem_9("Hello world!")));
        }

        private void PROBLEMS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            input.Text = "";
            output.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (input.Text.Length == 0)
            {
                MessageBox.Show("entrada inválida");
                return;
            }

            switch (PROBLEMS.Items.GetItemAt(PROBLEMS.SelectedIndex))
            {
                case "Problem #1":
                    int itemp = 0;
                    if (!int.TryParse(input.Text, out itemp))
                    {
                        MessageBox.Show("entrada inválida");
                        return;
                    }
                    output.Text = string.Join(", ", problem_1(itemp));
                    break;
                case "Problem #2":
                    double dtemp = 0;
                    if (!double.TryParse(input.Text, out dtemp))
                    {
                        MessageBox.Show("entrada inválida");
                        return;
                    }
                    output.Text = string.Format("{0}", problem_2(dtemp));
                    break;
                case "Problem #3":
                    output.Text = string.Format("{0}", problem_3(input.Text));
                    break;
                case "Problem #4":
                    output.Text = "rgb(" + string.Join(", ", problem_4(input.Text)) + ")";
                    break;
                case "Problem #5":
                    output.Text = string.Format("{0}", problem_5(input.Text));
                    break;
                case "Problem #7":
                    output.Text = problem_7(input.Text);
                    break;
                case "Problem #8":
                    output.Text = problem_8(input.Text);
                    break;
                case "Problem #9":
                    output.Text = problem_9(input.Text);
                    break;
                case "Problem #10":
                    output.Text = problem_10(input.Text);
                    break;
                default:
                    MessageBox.Show("¡algo salió horriblemente mal!");
                    break;
            }
        }

        public static List<int> problem_1 (int max)
        {
            List<int> primos = Enumerable.Range(2, max - 1).ToList<int>();

            for (int n = 0; n < primos.Count(); n++)
            {
                for (int nu = n + 1; nu < primos.Count(); nu++)
                {
                    if (primos[nu] % primos[n] == 0)
                    {
                        primos.Remove(primos[nu]);
                        nu--;
                    }
                }
            }

            return primos;
        }

        public static double problem_2 (double miles)
        {
            double fine = 0;

            if (miles > 65)
            {
                fine += 100;

                double temp = miles - 60;
                fine += (temp > 25 ? temp : 25) * 5;
            }

            if (miles > 90)
            {
                fine += 200;

                double temp = miles - 115;
                fine +=  temp * 10;
            }

            return fine;
        }

        public bool problem_3 (string text)
        {
            if (text.Length == 0) return true;

            int max = 0;
            int now = 0;
            bool first = true;
            while (text.Length > 0)
            {
                now = 0;
                char temp = text[0];
                while (text.IndexOf(temp) != -1)
                {
                    text = text.Remove(text.IndexOf(temp), 1);
                    now += 1;
                }
                Console.WriteLine("{0} : {1}", temp, now);
                if (!first && now != max) return false;
                else if (first)
                {
                    first = false;
                    max = now;
                }
            }

            return true;
        }

        public int[] problem_4 (string hex_color)
        {
            int[] rgb = new int[3];
            string hex = "0123456789abcdef";

            if (hex_color[0] == '#') hex_color = hex_color.Remove(0, 1);

            for (int c = 0; c < hex_color.Length; c++)
            {
                if (c >= 6) break;
                rgb[(c / 2)] += (int)(Math.Pow(16, ((double)c / (double)2) % 1 == 0 ? 1 : 0) * hex.IndexOf(char.ToLower(hex_color[c])));
            }

            return rgb;
        }

        public static bool problem_5 (string upc)
        {
            int temp = 0;
            int a = 0, b = 0;
            
            for (int n = 0; n < upc.Length - 1; n++)
            {
                if (!int.TryParse(char.ToString(upc[n]), out temp)) return false;
                else if (((double)n / (double)2) % 1 == 0)
                {
                    b += temp;
                }
                else
                {
                    a += temp;
                }
            }

            a = ((a * 3) + b) % 10;
            
            if (a == 0 || !int.TryParse(char.ToString(upc[11]), out temp)) return false;
            else if (10 - a == temp) return true;
            else return false;
        }

        public string problem_7 (string enunciado)
        {
            string result = "";
            List<string> words = new List<string>();

            for (int c = 0; c < enunciado.Length; c++)
            {
                if (c == 0 || (enunciado[c - 1] == ' ' && enunciado[c] != ' '))
                {
                    result += char.ToUpper(enunciado[c]); // THIS ISN'T A string FUNCTION >_>
                }
                else result += enunciado[c];
            }

            // Could I of used this? :v
            //return string.Join(" ", words);
            return result;
        }

        public static string problem_8 (string text)
        {
            return String.Concat(text.OrderBy(c => char.ToLower(c)));
        }

        public static string problem_9 (string text)
        {
            string coded = "";

            foreach (char w in text)
            {
                coded += (char)(w * 2);
            }

            return coded;
        }

        public static string problem_10 (string code)
        {
            string decoded = "";

            foreach (char w in code)
            {
                decoded += (char)(w / 2);
            }

            return decoded;
        }
    }
}
