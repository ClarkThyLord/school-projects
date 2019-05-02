using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreGeneric.classes
{
    class MoreNode<T> where T : IComparable
    {
        public T value { get; set; }
        public MoreNode<T> link { get; set; }

        public MoreNode(T value)
        {
            this.value = value;
        }

        public MoreNode(T value, MoreNode<T> link)
        {
            this.value = value;
            this.link = link;
        }
    }
}
