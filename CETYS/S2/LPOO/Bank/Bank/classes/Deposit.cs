using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.classes
{
    public class Deposit : Transaction
    {
        public string to;

        public Deposit(DateTime time, double amount, string to) : base(time, amount)
        {
            this.to = to;
        }
    }
}
