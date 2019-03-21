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
        private List<shapes.Shape> shapes = new List<shapes.Shape>();

        private int ShapesCount = 1500;

        public MainWindow()
        {
            InitializeComponent();

            ShapesCountSlider.Value = ShapesCount;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < ShapesCountSlider.Value; i++)
            {
                switch (random.Next(1))
                {
                    case 0:
                        shapes.Add(new shapes.Circle(canvas, random));
                        break;
                }
            }
        }

        private void RecreateShapes()
        {
            Console.WriteLine($"recreated {ShapesCountSlider.Value} shapes!");

            for (int i = 0; i < shapes.Count; i++)
            {
                shapes[i].randomize(random);
            }
        }

        private void ShapesCountSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            ShapesCountSlider.Value = (int)ShapesCountSlider.Value;
            RecreateShapes();
        }

        private void ShapesCountSlider_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ShapesCountSlider.Value = 1500;
            RecreateShapes();
        }

        private void RecreateShapesButton_Click(object sender, RoutedEventArgs e)
        {
            RecreateShapes();
        }

        private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RecreateShapes();
        }
    }
}
