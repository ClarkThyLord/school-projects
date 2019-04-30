using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreGeneric.classes
{
    class MoreStack<T> where T : IComparable
    {
        private MoreNode<T> root { get; set; }

        protected int size = 0;
        public int Size { get { return size; } }


        public void Push(MoreNode<T> MoreNode)
        {
            if (root == null) root = MoreNode;
            else
            {
                _last().link = MoreNode;
            }
            size++;
        }

        public void Push(T valor)
        {
            Push(new MoreNode<T>(valor));
        }

        public T Pop()
        {
            if (size == 0) new NullReferenceException("Empty Queue");

            MoreNode<T> popped = root;
            root = root.link;
            size--;

            return popped.value;
        }

        private MoreNode<T> _last()
        {
            MoreNode<T> last = root;
            for (int i = 0; i < size - 1; i++) last = last.link;
            return last;
        }

        public override string ToString()
        {
            string result = "";
            MoreNode<T> current = root;

            for (int i = 0; i < size; i++)
            {
                result += $"{current.value}" + (current.link == null ? "" : ",");
                current = current.link;
            }

            return result;
        }
    }
}
