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
using BetterRandomFigures.Figures;


namespace BetterRandomFigures
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Random random = new Random();
        public BetterFigure[] figures = {
            new Circle(),
            new Triangle(),
            new BetterRectangle(),
            new Diamond(),
            new Trapeziod()
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Draw()
        {
            for (int i = 0; i < 1500; i++)
            {
                figures[random.Next(0, figures.Length)].draw(canvas,random);
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Draw();
            
        }

        //private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    Draw();
        //}
    }
}
