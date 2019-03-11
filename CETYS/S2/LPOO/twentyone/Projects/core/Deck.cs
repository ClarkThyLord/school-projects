using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.core
{
    class Deck
    {
        private Random random = new Random();
        public Card[] cards { get; } = new Card[52];

        public Deck()
        {
            string[] types = { "clubs", "hearts", "spades", "diamonds" };
            string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "ace", "jack", "queen", "king" };

            for (int i = 0; i < cards.Length; i++)
            {
                int value = 0;
                if (i % 13 == 9) value = 11;
                else if (i % 13 > 9) value = 10;
                else value = (i % 13) + 2;

                cards[i] = new Card(value, @"assets\cards\" + values[i % 13] + "_of_" + types[i / 13] + ".png");
            }
        }

        public Card random_card()
        {
            return cards[random.Next(cards.Length)];
        }
    }
}
