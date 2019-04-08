﻿using System;
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
        private List<Product> products = new List<Product>();

        public static string JSON_LOCATION = $@"{Directory.GetCurrentDirectory()}\products.json";

        public MainWindow()
        {
            load_products();
            InitializeComponent();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                Width = MinWidth;
                Height = MinHeight;
                Left = (SystemParameters.PrimaryScreenWidth / 2) - (Width / 2);
                Top = (SystemParameters.PrimaryScreenHeight / 2) - (Height / 2);
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

        private void searchGUI_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                add_product(new Item(Product.Types.Item));
            }
        }

        public Product get_product(int index)
        {
            return products.Find(product => product.id == index);
        }

        public void add_product(Product product)
        {
            products.Add(product);
            productsGUI.Items.Add(product);
            save_products();
        }

        public void remove_product(Product product)
        {
            products.Remove(product);
            productsGUI.Items.Remove(product);
            save_products();
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
    }
}
