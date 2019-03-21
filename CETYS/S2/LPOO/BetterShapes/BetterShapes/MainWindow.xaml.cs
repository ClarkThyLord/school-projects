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
                switch (random.Next(1))
                {
                    case 0:
                        shapes.Add(new shapes.Circle(canvas, random));
                        break;
                }
            }

            canvas.MouseMove += Canvas_MouseMove;
        }

        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.OriginalSource is System.Windows.Shapes.Shape && e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                canvas.Children.Remove((System.Windows.Shapes.Shape)e.OriginalSource);
            }

            if (e.RightButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                shapes[shape_index].X = (int)e.GetPosition(canvas).X - shapes[shape_index].Size / 2;
                shapes[shape_index].Y = (int)e.GetPosition(canvas).Y - shapes[shape_index].Size / 2;

                shape_index--;
                if (shape_index < 0) shape_index = (int)ShapesCountSlider.Maximum - 1;
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
