using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Projects
{
    class Carta
    {
        public string ImageSource { get; }
        public int Valor { get; set; }
        public Carta(string ImageSource, int valor)
        {
            this.ImageSource = ImageSource;
            this.Valor = valor;
        }
        public void Dibujate(Canvas c, int posX, int posY)
        {
           Image i = new Image();
           i.Source = new BitmapImage(new Uri(ImageSource, UriKind.Relative));
           c.Children.Add(i);
            i.Width = 150;
            i.Height = 250;
           Canvas.SetLeft(i, posX);
           Canvas.SetTop(i,posY);
        }
    }
}
