using System;
using System.Timers;
using System.Windows;
using System.Collections.Generic;

namespace BetterShapes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int shapes_count = 100;
        public int animation_rate = 10;
        public bool animate_shapes = true;

        private Random random = new Random();
       
        private int shape_index = 0;
        private List<shapes.Shape> shapes = new List<shapes.Shape>();

        private Timer timer;

        public MainWindow()
        {
            InitializeComponent();

            ShapesCountSlider.Maximum = shapes_count;
            shape_index = (int)ShapesCountSlider.Maximum - 1;
            AnimateShapes.IsChecked = animate_shapes;
            AnimateShapesSpeed.Value = animation_rate;
        }

        private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            redraw();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < ShapesCountSlider.Maximum; i++)
            {
                switch (random.Next(7))
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
                    case 6:
                        shapes.Add(new shapes.Pentagram(canvas, random));
                        break;
                }
            }

            redraw();

            timer = new Timer(animation_rate);
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            animate();
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

        private void CanvasMarginSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            CanvasMarginSlider.Value = (int)CanvasMarginSlider.Value;
            redraw();
        }

        private void CanvasMarginSlider_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CanvasMarginSlider.Value = 10;

            redraw();
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

        private void AnimateShapes_Checked(object sender, RoutedEventArgs e)
        {
            animate_shapes = (bool)AnimateShapes.IsChecked;
        }

        private void AnimateShapesSpeed_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            AnimateShapesSpeed.Value = 10;
            animation_rate = 10;
            timer.Interval = animation_rate;
            AnimateShapesSpeedLabel.Text = $"Speed of Shapes: {animation_rate}";
        }

        private void AnimateShapesSpeed_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            animation_rate = (int)AnimateShapesSpeed.Value;
            timer.Interval = animation_rate;
            AnimateShapesSpeedLabel.Text = $"Speed of Shapes: {animation_rate}";
        }

        private void RecreateShapesButton_Click(object sender, RoutedEventArgs e)
        {
            redraw();
        }

        private void draw_shape(int x, int y, shapes.Shape shape)
        {
            x -= shape.Size / 2;
            if (x < 0) x += Math.Abs(x);
            else if (x + shape.Size > canvas.ActualWidth) x -= (int)Math.Abs(canvas.ActualWidth - (x + shape.Size));

            y -= shape.Size / 2;
            if (y < 0) y += Math.Abs(y);
            else if (y + shape.Size > canvas.ActualHeight) y -= (int)Math.Abs(canvas.ActualHeight - (y + shape.Size));

            shape.Visible = false;
            shape.X = x;
            shape.Y = y;
            shape.Visible = true;

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

            CanvasMarginLabel.Text = $"Canvas Margin: {CanvasMarginSlider.Value}";
            ShapesCountLabel.Text = $"Amount of Shapes: {ShapesCountSlider.Value}";
            AnimateShapesSpeedLabel.Text = $"Speed of Shapes: {animation_rate}";

            canvas.Children.Clear();

            for (int i = 0; i < ShapesCountSlider.Maximum; i++)
            {
                if (i < ShapesCountSlider.Value)
                {
                    shapes[i].randomize(random, (int)CanvasMarginSlider.Value);
                    shapes[i].Visible = true;
                }
                else
                {
                    shapes[i].Visible = false;
                }
            }
        }

        private void animate()
        {
            try
            {
                if (animate_shapes)
                {
                    for (int i = 0; i < shapes_count; i++)
                    {
                        if (shapes[i].Visible)
                        {
                            this.Dispatcher.Invoke((Action)(() =>
                            {
                                shapes[i].animate();
                            }));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("catched error on animation!");
            }
            
        }
    }
}
