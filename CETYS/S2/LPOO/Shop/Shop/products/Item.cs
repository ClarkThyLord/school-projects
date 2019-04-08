using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.products
{
    public class Item : Product
    {
        private double weight = 0;
        public double Weight
        {
            get { return weight; }
            set { weight = value < 0 ? 0 : value; }
        }
        public string Dimensions
        {
            get;
            set;
        } = "";

        public string Usage
        {
            get;
            set;
        } = "";

        public string Producers
        {
            get;
            set;
        } = "";

        public Item(string name="", string description="", double price=0, int amount=0) : base(Types.Item, name, description) { }

        public Item(string name, string description, double price, int amount, double weight, string dimensions, string usage, string producers) : this(name, description, price, amount)
        {
            Weight = weight;
            Dimensions = dimensions;
            Usage = usage;
            Producers = producers;
        }
    }
}
