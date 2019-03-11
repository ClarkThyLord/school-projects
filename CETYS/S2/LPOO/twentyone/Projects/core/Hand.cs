using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Projects.core
{
    class Hand
    {
        public List<Card> cards { get; } = new List<Card>();

        public int card_distance = 40;
        public Canvas canvas_item { get; } = new Canvas();

        public void add_card(Card card)
        {
            cards.Add(card);

            update();
        }

        public void remove_card(Card card)
        {
            cards.Remove(card);

            update();
        }

        public void clear_cards()
        {
            cards.Clear();

            update();
        }

        public int sum_of_cards()
        {
            return cards.Sum(card => card.value);
        }

        public Canvas update(int x=0, int y=0, double scale=1)
        {
            canvas_item.Children.Clear();

            for (int i = 0; i < cards.Count; i++) canvas_item.Children.Add(cards[i].render(i * card_distance, y, scale));

            return canvas_item;
        }
    }
}
