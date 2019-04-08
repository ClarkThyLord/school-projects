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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shop.gui;
using Shop.products;

namespace Shop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Window itemsWindow = new Items();
        public Window booksWindow = new Books();
        public Window moviesWindow = new Movies();

        private List<Product> products = new List<Product>();

        public static string JSON_LOCATION = $@"{Directory.GetCurrentDirectory()}\products.json";

        public MainWindow()
        {
            load_products();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            itemsWindow.Owner = this;
            booksWindow.Owner = this;
            moviesWindow.Owner = this;
        }

        private void searchGUI_GotFocus(object sender, RoutedEventArgs e)
        {
            searchGUI.Text = "";
            searchGUI.Foreground = Brushes.Black;
        }

        private void searchGUI_LostFocus(object sender, RoutedEventArgs e)
        {
            searchGUI.Text = "Search products...";
            searchGUI.Foreground = Brushes.Gray;
        }

        public void save_products()
        {
            using (StreamWriter file = File.CreateText(JSON_LOCATION))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, products);
            }
        }

        public void load_products()
        {
            if (!File.Exists(JSON_LOCATION)) return;

            try { foreach (JObject item in JArray.Parse(File.ReadAllText(JSON_LOCATION))) products.Add(Product.from_json(item)); }
            catch (Exception e)
            {
                MessageBoxResult result = MessageBox.Show("Saved products have been corrupted, creating a new batch!",
                                          "Confirmation",
                                          MessageBoxButton.OKCancel,
                                          MessageBoxImage.Question);

                if (result == MessageBoxResult.OK) File.Delete(JSON_LOCATION);
                else Application.Current.Shutdown();
            }
        }
    }
}
