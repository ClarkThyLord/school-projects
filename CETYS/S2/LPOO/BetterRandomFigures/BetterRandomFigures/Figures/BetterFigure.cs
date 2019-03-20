using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace BetterRandomFigures.Figures
{
    public abstract class BetterFigure
    {
        public abstract void draw(Canvas canvas, Random random);
    }
}
