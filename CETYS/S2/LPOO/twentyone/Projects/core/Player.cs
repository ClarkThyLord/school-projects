using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.core
{
    class Player
    {
        public int bet, money;
        public Hand hand = new Hand();
        public Chips chips = new Chips();

        public Player(int money = 500)
        {
            this.money = money;
        }
    }
}
