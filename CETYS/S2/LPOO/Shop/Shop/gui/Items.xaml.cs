using Shop.products;
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

namespace Shop.gui
{
    /// <summary>
    /// Interaction logic for Items.xaml
    /// </summary>
    public partial class Items : Window
    {
        public Item item;

        public Items(Item item=null)
        {
            this.item = item;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (item != null) setup(item);
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
            Owner.Show();
        }

        private void submitGUI_Click(object sender, RoutedEventArgs e)
        {
            if (!validate()) return;

            if (MessageBox.Show("Submit?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel) return;

            if (item == null) create();
            else edit();

            Close();
        }

        private void cancelGUI_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Cancel?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK) Close();
        }

        public void setup(Item item)
        {
            nameGUI.Text = item.Name;
            descriptionGUI.Text = item.Description;
            priceGUI.Text = item.Price.ToString();
            amountGUI.Text = item.Amount.ToString();

            weightGUI.Text = item.Weight.ToString();
            dimensionsGUI.Text = item.Dimensions;
            usageGUI.Text = item.Usage;
            producersGUI.Text = item.Producers;
        }

        public bool validate()
        {
            if (nameGUI.Text.Length <= 0) return false;
            else if (descriptionGUI.Text.Length <= 0) return false;
            else if (priceGUI.Text.Length <= 0 || !(double.TryParse(priceGUI.Text, out double price) && (price >= 0))) return false;
            else if (amountGUI.Text.Length <= 0 || !(int.TryParse(amountGUI.Text, out int amount) && (amount >= 0))) return false;

            else if (weightGUI.Text.Length <= 0 || !double.TryParse(weightGUI.Text, out double weight)) return false;
            else if (dimensionsGUI.Text.Length <= 0) return false;
            else if (usageGUI.Text.Length <= 0) return false;
            else if (producersGUI.Text.Length <= 0) return false;

            return true;
        }

        public void edit()
        {
            item.Name = nameGUI.Text;
            item.Description = descriptionGUI.Text;
            item.Price = double.Parse(priceGUI.Text);
            item.Amount = int.Parse(amountGUI.Text);

            item.Weight = double.Parse(weightGUI.Text);
            item.Dimensions = dimensionsGUI.Text;
            item.Usage = usageGUI.Text;
            item.Producers = producersGUI.Text;
        }

        public void create()
        {
            (Owner as MainWindow).add_product(new Item(nameGUI.Text, descriptionGUI.Text, double.Parse(priceGUI.Text), int.Parse(amountGUI.Text), double.Parse(weightGUI.Text), dimensionsGUI.Text, usageGUI.Text, producersGUI.Text));
        }
    }
}
