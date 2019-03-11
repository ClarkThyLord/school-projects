using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Projects.core
{
    class Chip
    {
        public int value { get; }

        public string image_src = "";
        public static int base_size = 20;

        public Chip(int value, string image_src)
        {
            this.value = value;

            this.image_src = image_src;
        }

        public Image render(int x = 0, int y = 0, double scale = 1)
        {
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(this.image_src, UriKind.Relative));

            Canvas.SetLeft(image, x);
            Canvas.SetTop(image, y);

            image.Width = image.Height = base_size * scale;
            
            return image;
        }
    }
}
