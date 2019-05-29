using System;
using System.Collections.Generic;
using System.IO;
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
using Bank.classes;
using Newtonsoft.Json;

namespace Bank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<User> users = new List<User>();

        public static string JSON_LOCATION = $@"{Directory.GetCurrentDirectory()}\users.json";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            load_users();

            users_viewlist.ItemsSource = users;
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsLoaded)
            {
                users_viewlist.Items.Refresh();

                save_users();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            save_users();
        }

        private void add_user_Click(object sender, RoutedEventArgs e)
        {
            UserAdd window = new UserAdd();
            window.Owner = this;
            window.Show();
            Hide();
        }

        private void modify_user_Click(object sender, RoutedEventArgs e)
        {
            if (users_viewlist.SelectedItem == null) return;

            UserModify window = new UserModify((User)users_viewlist.SelectedItem);
            window.Owner = this;
            window.Show();
            Hide();
        }

        private void remove_user_Click(object sender, RoutedEventArgs e)
        {
            foreach (User user in users_viewlist.SelectedItems)
            {
                users.Remove(user);
            }
            users_viewlist.Items.Refresh();
        }

        private void users_view_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (users_viewlist.SelectedItem == null) return;

            UserModify window = new UserModify((User)users_viewlist.SelectedItem);
            window.Owner = this;
            window.Show();
            Hide();
        }

        public void save_users()
        {
            using (StreamWriter file = File.CreateText(JSON_LOCATION))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, users);
            }
        }

        public void load_users()
        {
            if (!File.Exists(JSON_LOCATION)) return;

            users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(JSON_LOCATION));
        }

        public void add_user(User user)
        {
            users.Add(user);
        }
    }
}
