using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace uno_classes.classes
{
    abstract class Hand
    {
        public List<Card> cards;
        public Canvas canvas { get; }

        public abstract int sum();

        public abstract Canvas draw(float scale);
    }
}
