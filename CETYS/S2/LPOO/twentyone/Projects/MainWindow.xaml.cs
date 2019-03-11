using Projects.core;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Projects
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private House house = new House();
        private Player player = new Player();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            canvas.Children.Add(player.chips.canvas_item);
            canvas.Children.Add(player.hand.canvas_item);

            update();
        }

        private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            update();
        }


        // BETTING CONTROLS
        private void bet(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int amount = (e.RightButton == MouseButtonState.Pressed) ? -1 : 1;
            switch (((StackPanel)sender).Name)
            {
                case "bet_1":
                    amount *= 1;
                    break;
                case "bet_5":
                    amount *= 5;
                    break;
                case "bet_10":
                    amount *= 10;
                    break;
                case "bet_20":
                    amount *= 20;
                    break;
                case "bet_25":
                    amount *= 25;
                    break;
                case "bet_50":
                    amount *= 50;
                    break;
                case "bet_100":
                    amount *= 100;
                    break;
                case "bet_500":
                    amount *= 500;
                    break;
            }

            if ((amount > 0 && player.Bet + amount <= player.money) || (amount < 0 && player.Bet + amount >= 0)) player.Bet += amount;

            update();
        }

        private void empezar(object sender, RoutedEventArgs e)
        {
            if (player.Bet == 0) return;

            betting_controls.Visibility = Visibility.Hidden;
            playing_controls.Visibility = Visibility.Visible;

            update();
        }


        // PLAYING CONTROLS
        private void add(object sender, RoutedEventArgs e)
        {

        }

        private void call(object sender, RoutedEventArgs e)
        {

        }

        private void finish(object sender, RoutedEventArgs e)
        {

        }


        public void update()
        {
            double scale = canvas.ActualWidth / 800;

            betting_amount.Content = $"${player.Bet} / ${player.money}";
            info.Content = $"Dinero: ${player.money} Apuesta: ${player.Bet} | Suma de Cartas: {player.hand.sum_of_cards()}";

            player.chips.update(scale);
            Canvas.SetTop(player.chips.canvas_item, (canvas.ActualHeight / 2));
        }
    }
}
