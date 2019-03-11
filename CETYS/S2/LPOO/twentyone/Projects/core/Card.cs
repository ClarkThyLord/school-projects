using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Projects.core
{
    class Card
    {
        public int value { get; }

        public string image_src { get; }
        public static int base_width = 75, base_height = 125;

        public Card(int value, string image_src)
        {
            this.value = value;

            this.image_src = image_src;
        }

        public Image render(int x=0, int y=0, double scale=1)
        {
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(this.image_src, UriKind.Relative));

            Canvas.SetLeft(image, x);
            Canvas.SetTop(image, y);

            image.Width = base_width * scale;
            image.Height = base_height * scale;

            return image;
        }
    }
}
