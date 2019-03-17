using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace uno_classes.classes
{
    abstract class Game
    {
        public Canvas canvas;
        public Deck deck { get; }
        public Player[] players { get; }
        public Card last_card { get; }
        public int multiplier { get; } = 0;

        public Game(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public abstract void start();

        public abstract void turn();

        public abstract void end();

        public abstract void reset();

        public abstract void draw();
    }
}
