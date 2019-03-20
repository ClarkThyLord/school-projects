using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BetterRandomFigures.Figures
{
    public class Circle : BetterFigure
    {

        public override void draw(Canvas canvas, Random random)
        {
            int radius = random.Next(10, (int)(canvas.ActualWidth * 0.1));  
            Ellipse c = new Ellipse();
            c.Width =  radius;
            c.Height =  radius;
            Canvas.SetLeft(c, random.Next(0, (int)(canvas.ActualWidth - c.Width)));
            Canvas.SetTop(c, random.Next(0, (int)(canvas.ActualHeight - c.Height)));
           
            canvas.Children.Add(c);
            c.Fill = Brushes.HotPink;
            c.Opacity = random.NextDouble();
        }
    }
}
