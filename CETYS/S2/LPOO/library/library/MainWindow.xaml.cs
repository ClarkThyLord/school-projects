using library.classes;
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

namespace library
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        worker CURRENT_USER = new worker(-1, "BOB", "THE", "BUILDER", DateTime.Now, 3);
        database DB = new database();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void GUI_clear()
        {
            user_name.Text = "";
            user_first_name.Text = "";
            user_last_name.Text = "";
            user_date_of_birth.SelectedDate = DateTime.Now;
            user_worker_access.Text = "0";

            book_name.Text = "";
            book_author.Text = "";
            book_category.Text = "";
            book_rating.Text = "0";
        }

        public string get_view()
        {
            ComboBoxItem SelectedItem = (ComboBoxItem)GUIview.SelectedItem;

            return SelectedItem.Content.ToString();
        }

        public void change_view(string view)
        {
            switch (view)
            {
                case "worker":
                    GUIuser.Visibility = Visibility.Visible;
                    GUIworker.Visibility = Visibility.Visible;
                    GUIbook.Visibility = Visibility.Hidden;
                    break;
                case "client":
                    GUIuser.Visibility = Visibility.Visible;
                    GUIworker.Visibility = Visibility.Hidden;
                    GUIbook.Visibility = Visibility.Hidden;
                    break;
                case "book":
                    GUIuser.Visibility = Visibility.Hidden;
                    GUIworker.Visibility = Visibility.Hidden;
                    GUIbook.Visibility = Visibility.Visible;
                    break;
            }

            GUI_clear();
        }

        private void GUIview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            change_view(get_view());
        }

        private void GUIsubmit_Click(object sender, RoutedEventArgs e)
        {
            switch (get_view())
            {
                case "worker":
                    DB.add_worker(user_name.Text, user_first_name.Text, user_last_name.Text, (user_date_of_birth.SelectedDate.HasValue ? (DateTime)user_date_of_birth.SelectedDate : DateTime.Now), int.Parse(user_worker_access.Text), CURRENT_USER);
                    break;
                case "client":
                    DB.add_client(user_name.Text, user_first_name.Text, user_last_name.Text, (user_date_of_birth.SelectedDate.HasValue ? (DateTime)user_date_of_birth.SelectedDate : DateTime.Now), CURRENT_USER);
                    break;
                case "book":
                    DB.add_book(book_name.Text, book_author.Text, book_category.Text, int.Parse(book_rating.Text), CURRENT_USER);
                    break;
            }

            GUI_clear();
        }
    }
}
