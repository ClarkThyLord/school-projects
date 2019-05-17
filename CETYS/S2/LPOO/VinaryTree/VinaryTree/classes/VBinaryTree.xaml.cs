using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VinaryTree.classes;

namespace VinaryTree.classes
{
    /// <summary>
    /// Interaction logic for VBinaryTree.xaml
    /// </summary>
    public partial class VBinaryTree : UserControl
    {
        private VNode root;
        private VNode Root
        {
            get
            {
                return root;
            }
            set
            {
                // Remove old root VNode from this tree's canvas
                canvas.Children.Remove(root);

                // Set new root VNode
                root = value;

                // Add new root VNode to this tree's canvas
                canvas.Children.Add(root);
            }
        }

        public VBinaryTree() { InitializeComponent(); }

        private VBinaryTree(VNode node) : this() { Add(node); }

        public VBinaryTree(int value) : this(new VNode(value)) { }

        public VBinaryTree(int[] values) : this() { foreach (int value in values) Add(value); }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            // Once we're able to access the canvas add root node to it
            if (root != null) Root = root;
        }

        private void Add(VNode node)
        {
            VNode current = Root;
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

        public void Add(int value) { Add(new VNode(value)); }
    }
}
