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

namespace Projects
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Baraja b;
        Mano m;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Carta c = b.ReparteCarta();
            c.Dibujate(miCanvas, 20, 20);
            m.RecibiriCarta(c);
            lblPuntos.Content = m.SumaMano();

        
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            b = new Baraja();
            m = new Mano();
        }
    }
}
