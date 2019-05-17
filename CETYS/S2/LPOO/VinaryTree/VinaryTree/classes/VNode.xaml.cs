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

namespace VinaryTree.classes
{
    /// <summary>
    /// Interaction logic for VNode.xaml
    /// </summary>
    public partial class VNode : UserControl
    {
        private int value;
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                value_txt.Content = value.ToString();
            }
        }

        private VNode left;
        public VNode Left
        {
            get
            {
                return left;
            }
            set
            {
                // If there is a left node remove it
                if (this.left != null) canvas.Children.Remove(this.left);

                this.left = Left;

                // Setup left node relative to this nodes canvas
                Canvas.SetRight(this.left, -30);
                Canvas.SetTop(this.left, 30);

                // Add new left node to this nodes canvas
                canvas.Children.Add(this.left);
            }
        }

        private VNode right;
        public VNode Right
        {
            get
            {
                return right;
            }
            set
            {
                // If there is a right node remove it
                if (this.right != null) canvas.Children.Remove(this.right);

                this.right = Right;

                // Setup right node relative to this nodes canvas
                Canvas.SetRight(this.right, 30);
                Canvas.SetTop(this.right, 30);

                // Add new right node to this nodes canvas
                canvas.Children.Add(this.right);
            }
        }

        public VNode()
        {
            InitializeComponent();
        }

        public VNode(int value) : this()
        {
            this.value = value;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            // Once we're able to access the value_txt change it's content to this nodes value
            value_txt.Content = value.ToString();
        }

        private void ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
            ellipse.Margin = new Thickness(-Math.Abs(MinWidth / MinHeight * 3));
        }

        private void ellipse_MouseLeave(object sender, MouseEventArgs e)
        {
            ellipse.Margin = new Thickness(0);
        }
    }
}
