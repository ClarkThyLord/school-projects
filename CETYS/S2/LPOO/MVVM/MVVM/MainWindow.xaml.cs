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

namespace MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void set_image(Image image)
        {
            // SET IMAGE TO INTERFACE
        }

        private void image_picker_Click(object sender, RoutedEventArgs e)
        {
            Logic.process_image(image_picker.Content);
        }

        public void set_name(string name)
        {
            // SET IMAGE TO INTERFACE
        }

        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {
            Logic.process_name(name.Text);
        }

        public void set_info(String info)
        {
            // SET INFO TO INTERFACE
        }

        private void info_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Logic.process_info(info_txt.Text);
        }

        public void set_date_of_birth(DateTime date)
        {
            // SET DATE OF BIRTH
        }

        private void date_of_birth_picker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Logic.process_date_of_birth(date_of_birth_picker.SelectedDate);
        }

        public void set_date_of_death(DateTime date)
        {
            // SET DATE OF DEATH
        }

        private void date_of_death_picker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Logic.process_date_of_death(date_of_death_picker.SelectedDate);
        }
    }
}
