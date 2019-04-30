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
using MoreGeneric.classes;

namespace MoreGeneric
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MoreStack<int> moreStack = new MoreStack<int>();
            moreStack.Push(1);
            moreStack.Push(2);
            moreStack.Push(3);
            moreStack.Pop();

            Console.WriteLine(moreStack.ToString());

            MoreQueue<int> moreQueue = new MoreQueue<int>();
            moreQueue.Push(1);
            moreQueue.Push(2);
            moreQueue.Push(3);
            moreQueue.Pop();

            Console.WriteLine(moreQueue.ToString());
        }
    }
}
