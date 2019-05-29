using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.classes
{
    public class Withdraw : Transaction
    {
        public string from;

        public Withdraw(DateTime time, double amount, string from) : base(time, amount)
        {
            this.from = from;
        }
    }
}
