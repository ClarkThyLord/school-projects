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
    /// Interaction logic for Books.xaml
    /// </summary>
    public partial class Books : Window
    {
        public Book book;

        public Books(Book book = null)
        {
            this.book = book;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (book != null) setup(book);
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

            if (book == null) create();
            else edit();

            Close();
        }

        private void cancelGUI_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Cancel?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK) Close();
        }

        public void setup(Book item)
        {
            nameGUI.Text = item.Name;
            descriptionGUI.Text = item.Description;
            priceGUI.Text = item.Price.ToString();
            amountGUI.Text = item.Amount.ToString();

            pagesGUI.Text = book.Pages.ToString();
            languageGUI.Text = book.Language;
            genreGUI.Text = book.Genre;
            authorsGUI.Text = book.Authors;
            editorsGUI.Text = book.Editors;
            publishersGUI.Text = book.Publishers;
            ratingGUI.Text = book.Rating.ToString();
        }

        public bool validate()
        {
            if (nameGUI.Text.Length <= 0) return false;
            else if (descriptionGUI.Text.Length <= 0) return false;
            else if (priceGUI.Text.Length <= 0 || !(double.TryParse(priceGUI.Text, out double price) && (price >= 0))) return false;
            else if (amountGUI.Text.Length <= 0 || !(int.TryParse(amountGUI.Text, out int amount) && (amount >= 0))) return false;

            else if (pagesGUI.Text.Length <= 0 || !(int.TryParse(pagesGUI.Text, out int pages) && (pages > 0))) return false;
            else if (languageGUI.Text.Length <= 0) return false;
            else if (genreGUI.Text.Length <= 0) return false;
            else if (authorsGUI.Text.Length <= 0) return false;
            else if (editorsGUI.Text.Length <= 0) return false;
            else if (publishersGUI.Text.Length <= 0) return false;
            else if (ratingGUI.Text.Length <= 0 || !(double.TryParse(ratingGUI.Text, out double rating) && (rating >= 0 && rating <= 5))) return false;

            return true;
        }

        public void edit()
        {
            book.Name = nameGUI.Text;
            book.Description = descriptionGUI.Text;
            book.Price = double.Parse(priceGUI.Text);
            book.Amount = int.Parse(amountGUI.Text);

            book.Pages = int.Parse(pagesGUI.Text);
            book.Language = languageGUI.Text;
            book.Genre = genreGUI.Text;
            book.Authors = authorsGUI.Text;
            book.Editors = editorsGUI.Text;
            book.Publishers = publishersGUI.Text;
            book.Rating = double.Parse(ratingGUI.Text);
        }

        public void create()
        {
            (Owner as MainWindow).add_product(new Book(nameGUI.Text, descriptionGUI.Text, double.Parse(priceGUI.Text), int.Parse(amountGUI.Text), int.Parse(pagesGUI.Text), languageGUI.Text, genreGUI.Text, authorsGUI.Text, editorsGUI.Text, publishersGUI.Text, double.Parse(ratingGUI.Text)));
        }
    }
}
