using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BetterRandomFigures.Figures
{
    class BetterRectangle : BetterFigure
    {
        public override void draw(Canvas canvas, Random random)
        {
            Polygon p = new Polygon();

            int width = random.Next(10, (int)(canvas.ActualWidth * 0.1));
            double height = random.Next(10, (int)(canvas.ActualWidth * 0.1));

            p.Points.Add(new Point(0,0));
            p.Points.Add(new Point(0, height));
            p.Points.Add(new Point(width, height));
            p.Points.Add(new Point(width ,0));

            Canvas.SetLeft(p, random.Next(0, (int)(canvas.ActualWidth - width)));
            Canvas.SetTop(p, random.Next(0, (int)(canvas.ActualHeight - height)));

            canvas.Children.Add(p);
            p.Fill = Brushes.BlanchedAlmond;
            p.Opacity = random.NextDouble();
        }
    }
}
