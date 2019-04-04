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
        }
    }
}
