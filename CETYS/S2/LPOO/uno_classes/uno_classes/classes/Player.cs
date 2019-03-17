using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uno_classes.classes
{
    abstract class Player
    {
        public string name;
        public Deck deck { get; }
        public Hand hand { get; }

        public Player(string name, Deck deck)
        {
            this.name = name;
            this.deck = deck;
        }

        public abstract Card draw_card();

        public abstract Card place_card();
    }
}
