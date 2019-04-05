using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tower_of_Hanoi.classes
{
    class Ring
    {
        private static int _id = 0;
        public static int used = 0;

        public int id { get; } = 0;
        
        public static Brush[] colors = new Brush[]
        {
            Brushes.AliceBlue,
            Brushes.Beige,
            Brushes.BlueViolet,
            Brushes.DarkBlue,
            Brushes.DarkRed,
            Brushes.DeepSkyBlue,
            Brushes.ForestGreen,
            Brushes.Goldenrod
        };

        private double x;
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;

                Canvas.SetLeft(shape, x);
            }
        }

        private double y;
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;

                Canvas.SetTop(shape, y);
            }
        }

        public Rectangle shape = new Rectangle();

        public Ring()
        {
            id = _id;
            _id++;

            shape.Fill = colors[id % 8];
        }

        public void set_pos(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void draw(Canvas canvas)
        {
            shape.Width = Math.Abs(canvas.ActualWidth - (canvas.ActualWidth * 1 / 10 * id));
            shape.Height = canvas.Height * 1 / 10;

            if (!canvas.Children.Contains(shape)) canvas.Children.Add(shape);
        }
    }
}
