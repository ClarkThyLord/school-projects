using System;
using System.Windows;
using System.Collections.Generic;

namespace BetterShapes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random random = new Random();
       
        private int shape_index = 0;
        private List<shapes.Shape> shapes = new List<shapes.Shape>();

        public MainWindow()
        {
            InitializeComponent();

            ShapesCountSlider.Maximum = 10000;
            shape_index = (int)ShapesCountSlider.Maximum - 1;
        }

        private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            redraw();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < ShapesCountSlider.Maximum; i++)
            {
                switch (random.Next(6))
                {
                    case 0:
                        shapes.Add(new shapes.Circle(canvas, random));
                        break;
                    case 1:
                        shapes.Add(new shapes.Triangle(canvas, random));
                        break;
                    case 2:
                        shapes.Add(new shapes.Rectangle(canvas, random));
                        break;
                    case 3:
                        shapes.Add(new shapes.Diamond(canvas, random));
                        break;
                    case 4:
                        shapes.Add(new shapes.Trapezoid(canvas, random));
                        break;
                    case 5:
                        shapes.Add(new shapes.Cross(canvas, random));
                        break;
                }
            }

            redraw();
        }

        private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.OriginalSource is System.Windows.Shapes.Shape && e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                undraw_shape((System.Windows.Shapes.Shape)e.OriginalSource);
            }

            if (e.RightButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                draw_shape((int)e.GetPosition(canvas).X, (int)e.GetPosition(canvas).Y, shapes[shape_index]);
            }
        }

        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.OriginalSource is System.Windows.Shapes.Shape && e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                undraw_shape((System.Windows.Shapes.Shape)e.OriginalSource);
            }

            if (e.RightButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                draw_shape((int)e.GetPosition(canvas).X, (int)e.GetPosition(canvas).Y, shapes[shape_index]);
            }
        }

        private void ShapesCountSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            ShapesCountSlider.Value = (int)ShapesCountSlider.Value;
            redraw();
        }

        private void ShapesCountSlider_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ShapesCountSlider.Value = 1500;
            redraw();
        }

        private void RecreateShapesButton_Click(object sender, RoutedEventArgs e)
        {
            redraw();
        }

        private void draw_shape(int x, int y, shapes.Shape shape)
        {
            shapes[shape_index].X = x;
            shapes[shape_index].Y = y;

            shape_index--;
            if (shape_index < 0) shape_index = (int)ShapesCountSlider.Maximum - 1;
        }

        private void undraw_shape(System.Windows.Shapes.Shape shape)
        {
            canvas.Children.Remove(shape);
        }

        private void redraw()
        {
            if (shapes.Count <= 0) return;

            canvas.Children.Clear();

            for (int i = 0; i < ShapesCountSlider.Value; i++)
            {
                shapes[i].randomize(random);
            }
        }
    }
}
