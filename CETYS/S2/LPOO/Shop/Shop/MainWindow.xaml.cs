using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
        private List<Product> products = new List<Product>();

        public static string JSON_LOCATION = $@"{Directory.GetCurrentDirectory()}\products.json";

        public MainWindow()
        {
            load_products();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ProductModifyGUI.Visibility = Visibility.Hidden;
            ProductRemoveGUI.Visibility = Visibility.Hidden;

            productsGUI.ItemsSource = products;
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                Width = MinWidth;
                Height = MinHeight;
                Left = (SystemParameters.PrimaryScreenWidth / 2) - (Width / 2);
                Top = (SystemParameters.PrimaryScreenHeight / 2) - (Height / 2);

                searchGUI.Focus();
                productsGUI.UnselectAll();

                update_productsGUI();
                save_products();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            save_products();
        }

        private void ItemAddGUI_Click(object sender, RoutedEventArgs e)
        {
            Window itemsWindow = new Items();
            itemsWindow.Owner = this;
            itemsWindow.Show();
            Hide();
        }

        private void BookAddGUI_Click(object sender, RoutedEventArgs e)
        {
            Window booksWindow = new Books();
            booksWindow.Owner = this;
            booksWindow.Show();
            Hide();
        }

        private void MovieAddGUI_Click(object sender, RoutedEventArgs e)
        {
            Window moviesWindow = new Movies();
            moviesWindow.Owner = this;
            moviesWindow.Show();
            Hide();
        }

        private void ProductModifyGUI_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProductRemoveGUI_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoginGUI_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogoutGUI_Click(object sender, RoutedEventArgs e)
        {

        }

        private void productsResetGUI_Click(object sender, RoutedEventArgs e)
        {
            reset_products();
        }

        private void searchGUI_GotFocus(object sender, RoutedEventArgs e)
        {
            if (searchGUI.Text == "Search products...")
            {
                searchGUI.Text = "";
                searchGUI.Foreground = Brushes.Black;
                if (productsGUI != null) productsGUI.ItemsSource = products;
            }
        }

        private void searchGUI_LostFocus(object sender, RoutedEventArgs e)
        {
            if (searchGUI.Text == "")
            {
                searchGUI.Text = "Search products...";
                searchGUI.Foreground = Brushes.Gray;
                if (productsGUI != null) productsGUI.ItemsSource = products;
            }
        }

        private void searchGUI_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchGUI.Text.Length > 0 && searchGUI.Text != "Search products...") search_listGUI(searchGUI.Text);
            else if (productsGUI != null) productsGUI.ItemsSource = products;

        }

        private void searchGUI_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                add_product(new Book());
                if (searchGUI.Text.Length > 0 && searchGUI.Text != "Search products...") search_listGUI(searchGUI.Text);
                else if (productsGUI != null) productsGUI.ItemsSource = products;
            }
        }

        private void searchGoGUI_Click(object sender, RoutedEventArgs e)
        {
            if (searchGUI.Text.Length > 0 && searchGUI.Text != "Search products...") search_listGUI(searchGUI.Text);
            else if (productsGUI != null) productsGUI.ItemsSource = products;
        }

        private void productsGUI_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (productsGUI.SelectedItems.Count > 0)
            {
                ProductModifyGUI.Visibility = Visibility.Visible;
                ProductRemoveGUI.Visibility = Visibility.Visible;
            }
            else
            {
                ProductModifyGUI.Visibility = Visibility.Hidden;
                ProductRemoveGUI.Visibility = Visibility.Hidden;
            }
        }

        private void productsGUI_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (productsGUI.SelectedItem != null)
            {
                Window window = null;
                Product product = (Product)productsGUI.SelectedItem;

                switch (product.Type)
                {
                    case Product.Types.Item:
                        window = new Items((Item)product);
                        break;
                    case Product.Types.Book:
                        window = new Books((Book)product);
                        break;
                    case Product.Types.Movie:
                        window = new Movies((Movie)product);
                        break;
                }

                if (window != null)
                {
                    window.Owner = this;
                    window.Show();
                    Hide();
                }
            }
        }

        public void update_productsGUI()
        {
            productsGUI.Items.Refresh();
        }

        public Product get_product(int index)
        {
            return products.Find(product => product.id == index);
        }

        public void add_product(Product product)
        {
            products.Add(product);
            update_productsGUI();
            save_products();
        }

        public void remove_product(Product product)
        {
            products.Remove(product);
            update_productsGUI();
            save_products();
        }

        public void reset_products()
        {
            if (MessageBox.Show("Are you sure you want to reset Shop?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                products.Clear();
                update_productsGUI();
                save_products();
            }
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
                if (MessageBox.Show("Saved Shop products have been corrupted, creating a new batch!", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK) File.Delete(JSON_LOCATION);
                else Application.Current.Shutdown();
            }
        }

        public void search_listGUI(string search_term)
        {
            if (search_term.Length <= 0) return;

            productsGUI.ItemsSource = products.FindAll(product => product.ToString().Contains(search_term));
        }
    }
}
