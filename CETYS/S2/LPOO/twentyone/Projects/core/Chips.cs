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

        private int amount;
        public int Amount
        {
            get
            {
                return this.amount;
            }
            set
            {
                this.amount = value;
            }
        }

        public int rows_distance= 20, chips_distance = 10;
        public Canvas canvas_item { get; } = new Canvas();

        public Chips(int amount=0)
        {
            this.amount = 0;

            int[] values = { 500, 100, 50, 25, 20, 10, 5, 1 };
            for (int i = 0; i < values.Length; i++) chips[i] = new Chip(values[i], @"assets\chips\chip" + values[i] + ".png");
        }

        public Canvas update(double scale = 1)
        {
            canvas_item.Children.Clear();

            int _amount = this.amount;
            for (int i = 0; i < chips.Length; i++)
            {
                if (_amount / chips[i].value >= 1)
                {
                    for (int n = 0; n < _amount / chips[i].value; n++)
                    {
                        canvas_item.Children.Add(chips[i].render((int)((chips_distance * (n + 1)) * scale), (int)((rows_distance * (i + 1)) * scale), scale));
                    }

                    _amount -= chips[i].value * (_amount / chips[i].value);
                }
            }

            return canvas_item;
        }
    }
}
