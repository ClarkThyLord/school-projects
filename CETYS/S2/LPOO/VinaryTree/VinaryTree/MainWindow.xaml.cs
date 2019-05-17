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
using System.Windows.Shapes;
using VinaryTree.classes;

namespace VinaryTree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool READY = false;

        private VBinaryTree vbinarytree;
        public VBinaryTree vBinaryTree
        {
            get
            {
                return vbinarytree;
            }
            set
            {
                if (vbinarytree != null) canvas.Children.Remove(vbinarytree);

                vbinarytree = value;

                canvas.Children.Add(vbinarytree);
                center_binarytree();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            READY = true;

            update(new int[] {
                11,
                20,
                4,
                5,
                -45,
                33,
                100,
                31,
                332,
                123
            });
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            center_binarytree();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Owner.Close();
        }

        private void canvas_MouseEnter(object sender, MouseEventArgs e)
        {
            view.Focus();
        }

        private void input_GotFocus(object sender, RoutedEventArgs e)
        {
            if (input.Text == "Input data...")
            {
                input.Clear();
                input.Foreground = Brushes.Black;
            }
            else input.SelectAll();
        }

        private void input_LostFocus(object sender, RoutedEventArgs e)
        {
            if (input.Text == "")
            {
                input.Text = "Input data...";
                input.Foreground = Brushes.Gray;
            }
        }

        private void input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) update();
        }

        private void view_GotFocus(object sender, RoutedEventArgs e)
        {
            validate_input();
        }

        private void view_Click(object sender, RoutedEventArgs e)
        {
            update();
        }

        private bool validate_input()
        {
            if (!READY) return false;

            string[] inputs = input.Text.Split(',');
            foreach (string _input in inputs)
            {
                if (int.TryParse(_input, out int num_input))
                {
                    input.BorderBrush = input.IsFocused ? SystemColors.ActiveBorderBrush : SystemColors.InactiveBorderBrush;
                }
                else
                {
                    input.BorderBrush = Brushes.Red;
                    return false;
                }
            }

            input.BorderBrush = Brushes.Green;

            return true;
        }

        private int[] get_input()
        {
            if (!READY) return null;

            return input.Text.Split(',').Select(n => int.Parse(n)).ToArray();
        }

        public void center_binarytree()
        {
            if (!READY) return;

            // Center it horizontally and a little bit bellow the top of the canvas
            Canvas.SetLeft(vBinaryTree, canvas.ActualWidth / 2 - 25);
            Canvas.SetTop(vBinaryTree, 75);
        }

        public void update(int[] values = null)
        {
            if (!READY) return;
            
            if (values != null)
            {
                vBinaryTree = new VBinaryTree(values);

                input.Text = string.Join(",", values);
                input.Foreground = Brushes.Black;
            }
            else if (validate_input()) vBinaryTree = new VBinaryTree(get_input());
        }
    }
}
