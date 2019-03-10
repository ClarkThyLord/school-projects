using System;
using System.Windows;

namespace Projects
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool init = false;

        Baraja b;
        Mano m;

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            b = new Baraja();
            m = new Mano();

            init = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!init) return;

            m.RecibiriCarta(b.ReparteCarta());

            render();
        }

        private void miCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!init) return;

            render();
        }

        public void render()
        {
            if (!init) return;

            miCanvas.Children.Clear();
            
            m.Dibujate(miCanvas, (int)miCanvas.ActualWidth / 2, (int)miCanvas.ActualHeight / 2, miCanvas.ActualWidth / 800);

            lblPuntos.Content = m.SumaMano();
        }
    }
}
