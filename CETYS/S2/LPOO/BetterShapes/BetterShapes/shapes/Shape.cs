using System;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;

namespace BetterShapes.shapes
{
    abstract class Shape
    {
        private static int _id = 0;

        public static int min_size = 10, max_size = 100;

        public int id;
        public string name = "";

        public Canvas canvas;

        private int x = 0;
        public int X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;

                draw();
            }
        }

        private int y = 0;
        public int Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;

                draw();
            }
        }

        private int size = 0;
        public int Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;

                draw();
            }
        }

        private double scale = 1;
        public double Scale
        {
            get
            {
                return this.scale;
            }
            set
            {
                this.scale = value;

                draw();
            }
        }

        private System.Windows.Shapes.Shape _shape;
        public System.Windows.Shapes.Shape shape {
            get {
                return this._shape;
            }
            set {
                this._shape = value;
                this._shape.Fill = this.color;
                this._shape.Opacity = this.Opacity;
            }
        }

        public SolidColorBrush color = new SolidColorBrush();

        private double opacity = 0;
        public double Opacity
        {
            get
            {
                return this.opacity;
            }
            set
            {
                this.opacity = value > 1 ? 1 : (value < 0 ? 0 : value);

                if (this.shape != null) this.shape.Opacity = this.opacity;

                draw();
            }
        }

        public Shape()
        {
            this.id = _id;
            _id++;
        }

        public Shape(Canvas canvas, Random random, bool draw=true) : this()
        {
            this.canvas = canvas;

            randomize(random, draw);
        }

        public Shape(Canvas canvas, int x, int y, int size, int scale=1, Color color=new Color(), double opacity = 1, bool draw=true) : this()
        {
            this.canvas = canvas;

            this.x = x;
            this.y = y;
            this.scale = scale;
            this.size = size;

            this.color.Color = color;
            this.opacity = opacity > 1 ? 1 : (opacity < 0 ? 0 : opacity);

            if (draw) this.draw();
        }

        public void randomize(Random random, bool draw=true)
        {
            this.size = random.Next(min_size, max_size);
            this.x = random.Next(1, (int)this.canvas.ActualWidth - this.size);
            this.y = random.Next(1, (int)this.canvas.ActualHeight - this.size);

            this.color.Color = Color.FromRgb((byte)random.Next(0, 266), (byte)random.Next(0, 266), (byte)random.Next(0, 266));
            this.opacity = random.NextDouble();
            if (this.shape != null) this.shape.Opacity = this.opacity;

            if (draw) this.draw();
        }

        public abstract void draw();
    }
}
