using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uno_classes.classes
{
    class Player
    {
        public string name;
        public Deck deck { get; }
        public Hand hand { get; } = new Hand();

        public Player(string name, Deck deck)
        {
            this.name = name;
            this.deck = deck;
        }

        public Card draw_card() { return null; }

        public Card place_card() { return null; }
    }
}
