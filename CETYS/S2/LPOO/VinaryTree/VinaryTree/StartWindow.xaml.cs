using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VinaryTree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public Timer timer;
        public MainWindow main;

        public StartWindow()
        {
            InitializeComponent();

            timer = new Timer(1);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                progress.Value += 1;

                if (progress.Value == progress.Maximum)
                {
                    timer.Dispose();

                    main = new MainWindow();
                    main.Owner = this;
                    this.Hide();
                    main.Show();
                }
            }));
        }
    }
}
