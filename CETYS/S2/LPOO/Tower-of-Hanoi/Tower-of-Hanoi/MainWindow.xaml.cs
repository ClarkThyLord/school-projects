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
using Tower_of_Hanoi.classes;

namespace Tower_of_Hanoi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Tower[] towers = new Tower[]
        {
            new Tower(),
            new Tower(),
            new Tower()
        };

        private Ring[] rings = new Ring[]
        {
            new Ring(),
            new Ring(),
            new Ring(),
            new Ring(),
            new Ring(),
            new Ring(),
            new Ring(),
            new Ring()
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            setup();
            draw();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            draw();
        }

        private int get_rings_amount()
        {
            return rings_amount.SelectedIndex + 3;
        }

        private void setup()
        {
            Ring.used = get_rings_amount();

            foreach (Tower tower in towers) tower.rings.Clear();
            for (int i = 0; i < Ring.used; i++) towers[0].rings.Add(rings[i]);
        }

        private void draw()
        {
            for (int i = 0; i < towers.Length; i++) towers[i].draw(canvas, canvas.ActualWidth * 2 / 10, canvas.ActualHeight / 2, canvas.ActualWidth * 2 / 8 + (canvas.ActualWidth * 2 / 8 * i), canvas.ActualHeight * 7 / 8);
        }
    }
}
