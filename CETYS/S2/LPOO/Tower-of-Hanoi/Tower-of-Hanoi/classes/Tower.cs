using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tower_of_Hanoi.classes
{
    class Tower
    {
        private static int _id = 0;
        public int id { get; } = 0;

        private double x = 0;
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;

                Canvas.SetLeft(shape, x - (width / 2));
            }
        }

        private double y = 0;
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;

                Canvas.SetTop(shape, y - height);
            }
        }

        private double width = 0;
        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        private double height = 0;
        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }
        
        public Polygon shape = new Polygon();

        public List<Ring> rings = new List<Ring>();

        public Tower()
        {
            id = _id;
            _id++;

            shape.Fill = Brushes.IndianRed;
        }

        public void set_pos(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void set_size(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public void draw(Canvas canvas)
        {
            shape.Points.Clear();
            shape.Points.Add(new Point(0, height));
            shape.Points.Add(new Point(0, height * 9 / 10));
            shape.Points.Add(new Point(width * 4 / 10, height * 9 / 10));
            shape.Points.Add(new Point(width * 4 / 10, 0));
            shape.Points.Add(new Point(width * 6 / 10, 0));
            shape.Points.Add(new Point(width * 6 / 10, height * 9 / 10));
            shape.Points.Add(new Point(width, height * 9 / 10));
            shape.Points.Add(new Point(width, height));

            foreach (Ring ring in rings)
            {
                ring.set_pos(canvas.ActualWidth / 2, canvas.ActualHeight / 2);
                ring.draw(canvas);
            }

            if (!canvas.Children.Contains(shape)) canvas.Children.Add(shape);
        }

        public void draw(Canvas canvas, double width, double height, double x, double y)
        {
            Width = width;
            Height = height;
            X = x;
            Y = y;

            draw(canvas);
        }
    }
}
