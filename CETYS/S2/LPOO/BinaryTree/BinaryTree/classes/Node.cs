using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree.classes
{
    class Node
    {
        private int value;
        public int Value
        {
            set
            {
                this.value = value;
            }
            get
            {
                return this.value;
            }
        }

        public Node Left = null; // Less then
        public Node Right = null; // Greater then

        public Node(int Value)
        {
            this.Value = Value;
        }

        public Node(int Value, Node Left = null, Node Right = null) : this(Value)
        {
            this.Left = Left;
            this.Right = Right;
        }

        public Node(int Value, int? Left = null, int? Right = null) : this(Value, Left == null ? null : new Node((int)Left), Right == null ? null : new Node((int)Right)) { }
    }
}
