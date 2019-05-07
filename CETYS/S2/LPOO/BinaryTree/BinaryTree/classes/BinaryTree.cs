using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree.classes
{
    class BinaryTree
    {
        private Node Root = null;

        public BinaryTree() { }

        public BinaryTree(Node node)
        {
            this.Root = node;
        }

        public BinaryTree(int value) : this(new Node(value)) { }

        public void Add(Node node) {
            Node current = Root;
            while (true)
            {
                if (current == null)
                {
                    Root = node;
                    return;
                }
                else if (node.Value == current.Value) return;
                else if (node.Value < current.Value)
                {
                    if (current.Left == null)
                    {
                        current.Left = node;
                        return;
                    }
                    else
                    {
                        current = current.Left;
                        continue;
                    }
                }
                else if (node.Value > current.Value)
                {
                    if (current.Right == null)
                    {
                        current.Right = node;
                        return;
                    }
                    else
                    {
                        current = current.Right;
                        continue;
                    }
                }
            }
        }

        public void Add(int value) { Add(new Node(value)); }

        public bool Contains(int value)
        {
            Node current = Root;
            while (true)
            {
                if (current == null) return false;
                else if (value == current.Value) return true;
                else if (value < current.Value)
                {
                    if (current.Left == null) return false;
                    else
                    {
                        current = current.Left;
                        continue;
                    }
                }
                else if (value > current.Value)
                {
                    if (current.Right == null) return false;
                    else
                    {
                        current = current.Right;
                        continue;
                    }
                }
            }
        }
    }
}
