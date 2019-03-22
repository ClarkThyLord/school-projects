using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BetterShapes.shapes
{
    class Diamond : Shape
    {
        private void init(bool visible = true)
        {
            this.shape = new Polygon();

            if (visible) this.draw();
        }

        public Diamond() :
            base()
        {
            init(false);
        }

        public Diamond(Canvas canvas, Random random, bool visible = true) :
            base(canvas, random, false)
        {
            init(visible);
        }

        public Diamond(Canvas canvas, int x, int y, int size, int scale = 1, Color color = new Color(), double opacity = 1, bool visible = true) :
            base(canvas, x, y, size, scale, color, opacity, false)
        {
            init(visible);
        }
        public override void draw()
        {
            Polygon p = (Polygon)this.shape;
            p.Points.Clear();

            p.Points.Add(new Point(this.Size / 2, 0));
            p.Points.Add(new Point(this.Size, this.Size / 2));
            p.Points.Add(new Point(this.Size/ 2, this.Size));
            p.Points.Add(new Point(0, this.Size/ 2));

            Canvas.SetLeft(this.shape, this.X * this.Scale);
            Canvas.SetTop(this.shape, this.Y * this.Scale);

            if (!canvas.Children.Contains(this.shape)) canvas.Children.Add(this.shape);
        }
    }
}
