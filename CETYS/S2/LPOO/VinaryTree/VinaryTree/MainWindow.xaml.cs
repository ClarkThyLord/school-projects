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

        public bool dragging = false;
        public double dragging_speed = 2;
        public Point last_position = new Point();

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
            update_btn.Focus();
        }

        private void canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dragging = true;
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point current_position = e.GetPosition(canvas);

                double x_delta = current_position.X - last_position.X;
                if (x_delta > 0) Canvas.SetLeft(vBinaryTree, Canvas.GetLeft(vBinaryTree) + dragging_speed);
                else if (x_delta < 0) Canvas.SetLeft(vBinaryTree, Canvas.GetLeft(vBinaryTree) - dragging_speed);

                double y_delta = current_position.Y - last_position.Y;
                if (y_delta > 0) Canvas.SetTop(vBinaryTree, Canvas.GetTop(vBinaryTree) + dragging_speed);
                else if (y_delta < 0) Canvas.SetTop(vBinaryTree, Canvas.GetTop(vBinaryTree) - dragging_speed);

                last_position = current_position;
            }
            else last_position = e.GetPosition(canvas);
        }

        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dragging = false;
        }

        private void input_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (input_txt.Text == "Input data...")
            {
                input_txt.Clear();
                input_txt.Foreground = Brushes.Black;
            }
            else input_txt.SelectAll();
        }

        private void input_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (input_txt.Text == "")
            {
                input_txt.Text = "Input data...";
                input_txt.Foreground = Brushes.Gray;
            }
        }

        private void input_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) update();
        }

        private void input_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            update();
        }

        private void center_btn_Click(object sender, RoutedEventArgs e)
        {
            center_binarytree();
        }

        private void update_btn_GotFocus(object sender, RoutedEventArgs e)
        {
            validate_input_txt();
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            update();
        }

        private void help_btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            help_panel.Visibility = help_panel.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        private bool validate_input_txt()
        {
            if (!READY) return false;

            string[] input_txts = input_txt.Text.Split(',');
            foreach (string _input_txt in input_txts)
            {
                if (int.TryParse(_input_txt, out int num_input_txt))
                {
                    input_txt.BorderBrush = input_txt.IsFocused ? SystemColors.ActiveBorderBrush : SystemColors.InactiveBorderBrush;
                }
                else
                {
                    input_txt.BorderBrush = Brushes.Red;
                    return false;
                }
            }

            input_txt.BorderBrush = Brushes.Green;

            return true;
        }

        private int[] get_input_txt()
        {
            if (!READY) return null;

            return input_txt.Text.Split(',').Select(n => int.Parse(n)).ToArray();
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

                input_txt.Text = string.Join(",", values);
                input_txt.Foreground = Brushes.Black;
            }
            else if (validate_input_txt()) vBinaryTree = new VBinaryTree(get_input_txt());
        }
    }
}
