using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreGeneric.classes
{
    class MoreQueue<T> where T : IComparable
    {
        private MoreNode<T> root { get; set; }

        protected int size = 0;
        public int Size { get { return size; } }


        public void Push(MoreNode<T> MoreNode)
        {
            if (root == null) root = MoreNode;
            else
            {
                MoreNode.link = root;
                root = MoreNode;
            }
            size++;
        }

        public void Push(T valor)
        {
            Push(new MoreNode<T>(valor));
        }

        public T Pop()
        {
            MoreNode<T> popped = null;

            if (size == 0) new NullReferenceException("Empty Queue");
            if (size == 1)
            {
                popped = root;
                root = null;
            }
            else if (size >= 2)
            {
                MoreNode<T> actual = root;
                for (int i = 0; i < size - 1; i++)
                {
                    if (i == size - 2)
                    {
                        popped = actual.link;
                        actual.link = null;
                        break;
                    }
                    actual = actual.link;
                }
            }

            size--;

            return popped.value;
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
