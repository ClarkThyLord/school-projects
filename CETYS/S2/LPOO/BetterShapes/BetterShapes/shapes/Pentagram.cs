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
    class Pentagram : Shape
    {
        private void init(bool draw = true)
        {
            this.shape = new Polyline();
            this.shape.Fill = null;
            this.shape.Stroke = this.color;

            if (draw) this.draw();
        }

        public Pentagram() :
            base()
        {
            init(false);
        }

        public Pentagram(Canvas canvas, Random random, bool draw = true) :
            base(canvas, random, false)
        {
            init(draw);
        }

        public Pentagram(Canvas canvas, int x, int y, int size, int scale = 1, Color color = new Color(), double opacity = 1, bool draw = true) :
            base(canvas, x, y, size, scale, color, opacity, false)
        {
            init(draw);
        }

        public override void draw()
        {
            Polyline p = (Polyline)this.shape;
            p.Points.Clear();

            p.Points.Add(new Point(this.Size / 2, 0));
            p.Points.Add(new Point(this.Size * 9 / 10, this.Size));
            p.Points.Add(new Point(0, this.Size * 4/10));
            p.Points.Add(new Point(this.Size, this.Size * 4/10));
            p.Points.Add(new Point(this.Size * 1 / 10, this.Size));
            p.Points.Add(new Point(this.Size / 2, 0));

            Canvas.SetLeft(this.shape, this.X * this.Scale);
            Canvas.SetTop(this.shape, this.Y * this.Scale);

            if (!canvas.Children.Contains(this.shape)) canvas.Children.Add(this.shape);
        }
    }
}
