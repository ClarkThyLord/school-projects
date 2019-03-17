using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace uno_classes.classes
{
    class Game
    {
        public Canvas canvas;
        public Deck deck { get; } = new Deck();
        public Player[] players { get; }
        public Card last_card { get; }
        public int multiplier { get; } = 0;

        public Game(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public void start() { }

        public void turn () { }

        public void end () { }

        public void reset() { }

        public void draw() { }
    }
}
