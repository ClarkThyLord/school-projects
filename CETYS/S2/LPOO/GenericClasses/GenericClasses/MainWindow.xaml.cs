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

namespace GenericClasses
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private  Lista<string> stringlist = new Lista<string>();
        private Lista<int> intlist = new Lista<int>();
        private Lista<double> doublelist = new Lista<double>();

        public MainWindow()
        {
            InitializeComponent();

            stringlist.Add(new Nodo<string>("Hello"));
            stringlist.Add("world");
            stringlist.Add("it's");
            stringlist.Add("me");
            stringlist.Add("Chungus!");
            stringlist.Add(":V");
            stringlist.Add("-<");

            Console.WriteLine(stringlist.ToString());

            //stringlist.insert("nope", -1);
            //stringlist.insert("nana", stringlist.size);
            stringlist.insert("UwU", 0);
            stringlist.insert("Big", 5);
            stringlist.insert("|", stringlist.size - 1);

            Console.WriteLine(stringlist.ToString());

            //stringlist.pop(-1);
            //stringlist.pop(stringlist.size);
            stringlist.pop(0);
            stringlist.pop(1);
            stringlist.pop(stringlist.size - 1);

            Console.WriteLine(stringlist.ToString());

            stringlist.remove("world");
            stringlist.remove("Hello");
            stringlist.remove("Big");
            stringlist.remove("|");

            Console.WriteLine(stringlist.ToString());

            Console.WriteLine(stringlist.isEmpty());

            Console.WriteLine(stringlist.count(">:V"));
            Console.WriteLine(stringlist.count(":V"));

            Console.WriteLine(stringlist.contains(">:V"));
            Console.WriteLine(stringlist.contains(":V"));

            stringlist.extends(new string[]
            {
                "|",
                "this",
                "is",
                "new!"
            });

            Console.WriteLine(stringlist.ToString());

            stringlist.reverse();

            Console.WriteLine(stringlist.ToString());

            Console.WriteLine(stringlist.last());

            stringlist.EveryOther();

            Console.WriteLine(stringlist.ToString());

            //stringlist.swap(0, stringlist.size - 1);
            //stringlist.swap(1, stringlist.size - 3);

            //Console.WriteLine(stringlist.ToString());

            Console.WriteLine(stringlist.get(0));
            Console.WriteLine(stringlist.get(3));
            Console.WriteLine(stringlist.get(stringlist.size - 1));

            stringlist.clear();

            Console.WriteLine(stringlist.ToString());
        }
    }
}
