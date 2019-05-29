using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.classes
{
    public class Worker : User
    {
        public double access = 0;

        public Worker(string name, string password) : base(name, password) { }

        public Worker(string name, string password, double access) : this(name, password)
        {
            this.access = access;
        }
    }
}
