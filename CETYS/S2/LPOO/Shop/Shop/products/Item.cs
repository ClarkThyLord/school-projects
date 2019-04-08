using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.products
{
    internal class Item : Product
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

        public Item(string name = null, string description = null) : base(Types.Item, name, description) { }

        public Item(string name, string description, double weight, string dimensions, string usage, string producers) : this(name, description)
        {
            Weight = weight;
            Dimensions = dimensions;
            Usage = usage;
            Producers = producers;
        }
    }
}
