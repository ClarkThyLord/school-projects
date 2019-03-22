using System;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace BetterShapes.shapes
{
    internal class Circle : Shape
    {
        
        private void init(bool visible = true)
        {
            this.shape = new Ellipse();

            if (visible) this.draw();
        }

        public Circle() :
            base()
        {
            init(false);
        }

        public Circle(Canvas canvas, Random random, bool visible=true) :
            base(canvas, random, false)
        {
            init(visible);
        }

        public Circle(Canvas canvas, int x, int y, int size, int scale=1, Color color=new Color(), double opacity=1, bool visible=true) :
            base(canvas, x, y, size, scale, color, opacity, false)
        {
            init(visible);
        }

        public override void draw()
        {
            this.shape.Width = this.Size;
            this.shape.Height = this.Size;

            Canvas.SetLeft(this.shape, this.X);
            Canvas.SetTop(this.shape, this.Y);

            if (!canvas.Children.Contains(this.shape)) canvas.Children.Add(this.shape);
        }
    }
}
