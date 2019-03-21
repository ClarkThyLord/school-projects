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

            this.shape.Fill = this.color;
            this.shape.Opacity = this.Opacity;

            canvas.Children.Add(this.shape);

            if (draw) this.draw();
        }

        public Circle() : base()
        {
            init(false);
        }

        public Circle(Canvas canvas, Random random) : base(canvas, random)
        {
            init();
        }

        public Circle(Canvas canvas, int x, int y, int size, int scale=1, Color color=new Color()) : base(canvas, x, y, size, scale, color)
        {
            init();
        }

        public override void draw()
        {
            this.shape.Width = this.Size * this.Scale;
            this.shape.Height = this.Size * this.Scale;

            Canvas.SetLeft(this.shape, this.X * this.Scale);
            Canvas.SetTop(this.shape, this.Y * this.Scale);
        }
    }
}
