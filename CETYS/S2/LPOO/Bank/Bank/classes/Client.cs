using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.classes
{
    public class Client : User
    {
        public double money = 0;
        public List<Transaction> transactions = new List<Transaction>();

        public Client(string name, string password) : base(name, password) { }

        public Client(string name, string password, double money) : this(name, password)
        {
            this.money = money;
        }
    }
}
