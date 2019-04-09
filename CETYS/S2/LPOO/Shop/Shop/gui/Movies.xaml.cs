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
    /// Interaction logic for Movies.xaml
    /// </summary>
    public partial class Movies : Window
    {
        public Movie movie;

        public Movies(Movie movie = null)
        {
            this.movie = movie;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.movie != null) setup(this.movie);
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

            if (movie == null) create();
            else edit();

            Close();
        }

        private void cancelGUI_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Cancel?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK) Close();
        }

        public void setup(Movie movie)
        {
            nameGUI.Text = movie.Name;
            descriptionGUI.Text = movie.Description;
            priceGUI.Text = movie.Price.ToString();
            amountGUI.Text = movie.Amount.ToString();

            lengthGUI.Text = movie.Length.ToString();
            languageGUI.Text = movie.Language;
            genreGUI.Text = movie.Genre;
            actorsGUI.Text = movie.Actors;
            directorsGUI.Text = movie.Directors;
            producersGUI.Text = movie.Producers;
            studioGUI.Text = movie.Studio;
            ratingGUI.Text = movie.Rating.ToString();
        }

        public bool validate()
        {
            if (nameGUI.Text.Length <= 0) return false;
            else if (descriptionGUI.Text.Length <= 0) return false;
            else if (priceGUI.Text.Length <= 0 || !(double.TryParse(priceGUI.Text, out double price) && (price >= 0))) return false;
            else if (amountGUI.Text.Length <= 0 || !(int.TryParse(amountGUI.Text, out int amount) && (amount >= 0))) return false;

            else if (lengthGUI.Text.Length <= 0 || !(double.TryParse(lengthGUI.Text, out double length) && (length >= 0))) return false;
            else if (languageGUI.Text.Length <= 0) return false;
            else if (genreGUI.Text.Length <= 0) return false;
            else if (actorsGUI.Text.Length <= 0) return false;
            else if (directorsGUI.Text.Length <= 0) return false;
            else if (producersGUI.Text.Length <= 0) return false;
            else if (studioGUI.Text.Length <= 0) return false;
            else if (ratingGUI.Text.Length <= 0 || !(double.TryParse(ratingGUI.Text, out double rating) && (rating >= 0 && rating <= 5))) return false;

            return true;
        }

        public void edit()
        {
            movie.Name = nameGUI.Text;
            movie.Description = descriptionGUI.Text;
            movie.Price = double.Parse(priceGUI.Text);
            movie.Amount = int.Parse(amountGUI.Text);

            movie.Length = double.Parse(lengthGUI.Text);
            movie.Language = languageGUI.Text;
            movie.Genre = genreGUI.Text;
            movie.Actors = actorsGUI.Text;
            movie.Directors = directorsGUI.Text;
            movie.Producers = producersGUI.Text;
            movie.Studio = studioGUI.Text;
            movie.Rating = double.Parse(ratingGUI.Text);
        }

        public void create()
        {
            (Owner as MainWindow).add_product(new Movie(nameGUI.Text, descriptionGUI.Text, double.Parse(priceGUI.Text), int.Parse(amountGUI.Text), double.Parse(lengthGUI.Text), languageGUI.Text, genreGUI.Text, actorsGUI.Text, directorsGUI.Text, producersGUI.Text, studioGUI.Text, double.Parse(ratingGUI.Text)));
        }
    }
}
