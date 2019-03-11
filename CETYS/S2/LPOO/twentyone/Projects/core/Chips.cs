using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Projects.core
{
    class Chips
    {
        public Chip[] chips = new Chip[8];

        public int amount { set { update(); } }

        public int rows_distance= 20, chips_distance = 10;
        public Canvas canvas_item { get; } = new Canvas();

        public Chips(int amount=0)
        {
            this.amount = 0;

            int[] values = { 1, 5, 10, 20, 25, 50, 100, 500 };
            for (int i = 0; i < values.Length; i++) chips[i] = new Chip(i, @"assets\chips\chip" + values[i] + ".png");
        }

        public Canvas update(int x = 0, int y = 0, double scale = 1)
        {
            canvas_item.Children.Clear();

            return canvas_item;
        }
    }
}
