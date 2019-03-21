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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 1500; i++)
            {
                switch (random.Next(1))
                {
                    case 0:
                        shapes.Add(new shapes.Circle(canvas, random));
                        break;
                }
            }
        }
    }
}
