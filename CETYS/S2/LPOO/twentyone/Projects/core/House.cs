﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.core
{
    class House
    {
        private int wins = 0;
        public double multiplier;
        
        public Hand hand = new Hand();

        public House(double multiplier = 1.25)
        {
            this.multiplier = multiplier;
        }

        public void win()
        {
            wins += 1;
            update();
        }

        public void lose()
        {
            wins -= 1;
            update();
        }

        private void update()
        {
            if (wins > 0) multiplier += 0.15;
            else if (multiplier > 1.1) multiplier -= 0.1;
        }
    }
}
