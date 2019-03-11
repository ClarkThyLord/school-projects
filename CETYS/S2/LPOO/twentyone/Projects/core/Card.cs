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

        public Image image { get; }

        public Card(int value, string image_src)
        {
            this.value = value;

            this.image_src = image_src;

            image = new Image();
            image.Source = new BitmapImage(new Uri(this.image_src, UriKind.Relative));
            update();
        }

        public Image update(int x=0, int y=0, double scale=1)
        {
            Canvas.SetLeft(image, x);
            Canvas.SetTop(image, y);

            image.Width = base_width * scale;
            image.Height = base_height * scale;

            return image;
        }
    }
}
