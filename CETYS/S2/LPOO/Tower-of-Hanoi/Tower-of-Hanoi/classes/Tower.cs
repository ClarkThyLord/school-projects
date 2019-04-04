using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Tower_of_Hanoi.classes
{
    class Tower
    {
        private static int _id = 0;
        public int id { get; } = 0;

        public List<Ring> rings = new List<Ring>();

        public Canvas canvas = new Canvas();

        public double x = 0;
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;

                Canvas.SetLeft(canvas, x);
            }
        }

        public double y = 0;
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;

                Canvas.SetTop(canvas, y);
            }
        }

        public double size = 100;
        public double Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;

                draw();
            }
        }

        public Tower()
        {
            id = _id;
            _id++;
        }

        public void draw()
        {

        }
    }
}
