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
using System.Windows.Shapes;
using Bank.classes;

namespace Bank
{
    /// <summary>
    /// Interaction logic for UserModify.xaml
    /// </summary>
    public partial class UserModify : Window
    {
        private User user;

        public UserModify(User user)
        {
            this.user = user;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            name_input.Text = user.name;
            password_input.Password = user.password;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Owner.Show();
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to modify user?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
            {
                user.name = name_input.Text;
                user.password = password_input.Password;

                Close();
            }
        }
    }
}
