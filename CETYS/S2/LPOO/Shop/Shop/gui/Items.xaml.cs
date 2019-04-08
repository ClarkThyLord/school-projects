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
            if (this.item != null) setup(this.item);

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
            descriptionGUI.Document.Blocks.Clear();
            descriptionGUI.Document.Blocks.Add(new Paragraph(new Run(item.Description)));
        }

        public bool validate()
        {
            if (nameGUI.Text.Length <= 0) return false;
            else if (new TextRange(descriptionGUI.Document.ContentStart, descriptionGUI.Document.ContentEnd).Text.Length <= 0) return false;

            return true;
        }

        public void edit()
        {
            item.Name = nameGUI.Text;
            item.Description = new TextRange(descriptionGUI.Document.ContentStart, descriptionGUI.Document.ContentEnd).Text;
        }

        public void create()
        {
            (Owner as MainWindow).add_product(new Item(nameGUI.Text, new TextRange(descriptionGUI.Document.ContentStart, descriptionGUI.Document.ContentEnd).Text));
        }
    }
}
