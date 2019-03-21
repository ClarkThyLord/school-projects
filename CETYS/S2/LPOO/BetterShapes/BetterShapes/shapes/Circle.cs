using System;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace BetterShapes.shapes
{
    internal class Circle : Shape
    {
        private void init(bool draw = true)
        {
            this.shape = new Ellipse();

            if (draw) this.draw();
        }

        public Circle() :
            base()
        {
            init(false);
        }

        public Circle(Canvas canvas, Random random, bool draw=true) :
            base(canvas, random, false)
        {
            init(draw);
        }

        public Circle(Canvas canvas, int x, int y, int size, int scale=1, Color color=new Color(), double opacity=1, bool draw=true) :
            base(canvas, x, y, size, scale, color, opacity, false)
        {
            init(draw);
        }

        public override void draw()
        {
            this.shape.Width = this.Size * this.Scale;
            this.shape.Height = this.Size * this.Scale;

            Canvas.SetLeft(this.shape, this.X * this.Scale);
            Canvas.SetTop(this.shape, this.Y * this.Scale);

            if (!canvas.Children.Contains(this.shape)) canvas.Children.Add(this.shape);
        }
    }
}
