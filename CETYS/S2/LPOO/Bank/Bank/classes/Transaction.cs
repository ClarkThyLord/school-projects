using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.classes
{
    public abstract class Transaction
    {
        public DateTime time;
        public double amount;

        public Transaction(DateTime time, double amount)
        {
            this.time = time;
            this.amount = amount;
        }
    }
}
