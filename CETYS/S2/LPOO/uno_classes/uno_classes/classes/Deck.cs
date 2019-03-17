using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uno_classes.classes
{
    abstract class Deck
    {
        public Card[] cards { get; }

        public Deck() { }

        public abstract void resort();

        public abstract void return_card();

        public abstract Card draw_card();
    }
}
