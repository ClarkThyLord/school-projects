using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.core
{
    class Player
    {
        private int bet;
        public int Bet
        {
            get
            {
                return this.bet;
            }
            set
            {
                this.bet = value;
                chips.Amount = value;
            }
        }
        public int money;
        public Hand hand = new Hand();
        public Chips chips = new Chips();

        public int state = 0;

        public Player(int money = 500)
        {
            this.money = money;
        }
    }
}
