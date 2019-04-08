using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.products
{
    internal class Item : Product
    {
        public Item(Types type, string name = null, string description = null) : base(type, name, description) { }
    }
}
