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

namespace inheritance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<person> people = new List<person>();

        public MainWindow()
        {
            InitializeComponent();
        }

        person add_person(string name = "", string first_name = "", string last_name = "", DateTime date_of_birth = new DateTime())
        {
            person _person = new person(name, first_name, last_name, date_of_birth);

            people.Add(_person);

            return _person;
        }
    }
}
