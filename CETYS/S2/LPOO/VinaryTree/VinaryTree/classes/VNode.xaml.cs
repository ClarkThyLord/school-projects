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
                else
                {
                    Line link = new Line();
                    link.Stroke = lesserLink;

                    link.X1 = MinWidth / 2;
                    link.Y1 = MinHeight / 2;
                    link.X2 = -60 + MinWidth / 2;
                    link.Y2 = 60 + MinHeight / 2;

                    link.StrokeThickness = 2;
                    canvas.Children.Add(link);
                }

                this.left = value;

                // Setup left node relative to this nodes canvas
                Canvas.SetRight(this.left, 60);
                Canvas.SetTop(this.left, 60);

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
                else
                {
                    Line link = new Line();
                    link.Stroke = greaterLink;

                    link.X1 = MinWidth / 2;
                    link.Y1 = MinHeight / 2;
                    link.X2 = 60 + MinWidth / 2;
                    link.Y2 = 60 + MinHeight / 2;

                    link.StrokeThickness = 2;
                    canvas.Children.Add(link);
                }

                this.right = value;

                // Setup right node relative to this nodes canvas
                Canvas.SetRight(this.right, -60);
                Canvas.SetTop(this.right, 60);

                // Add new right node to this nodes canvas
                canvas.Children.Add(this.right);
            }
        }

        public static Brush defaultFill = Brushes.White;
        public static Brush defaultStroke = Brushes.Gray;

        public static Brush focusedFill = Brushes.LightGray;
        public static Brush focusedStroke = Brushes.White;

        public static Brush lesserLink = Brushes.Blue;
        public static Brush greaterLink = Brushes.Red;

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
            ellipse.Fill = defaultFill;
            ellipse.Stroke = defaultStroke;
            value_txt.Content = value.ToString();
        }

        private void ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
            ellipse.Fill = focusedFill;
            ellipse.Stroke = focusedStroke;
            ellipse.Margin = new Thickness(-Math.Abs(MinWidth / MinHeight * 3));
        }

        private void ellipse_MouseLeave(object sender, MouseEventArgs e)
        {
            ellipse.Fill = defaultFill;
            ellipse.Stroke = defaultStroke;
            ellipse.Margin = new Thickness(0);
        }
    }
}
