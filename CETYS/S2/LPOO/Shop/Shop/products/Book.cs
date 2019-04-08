using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.products
{
    internal class Book : Product
    {
        public Book(string name = null, string description = null) : base(Types.Book, name, description) { }
    }
}
