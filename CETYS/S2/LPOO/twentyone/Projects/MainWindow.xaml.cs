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
        private Deck deck = new Deck();
        private House house = new House();
        private Player player = new Player();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            canvas.Children.Add(player.chips.canvas_item);
            canvas.Children.Add(player.hand.canvas_item);

            canvas.Children.Add(house.hand.canvas_item);

            render();
        }

        private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            render();
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

            render();
        }

        private void start(object sender, RoutedEventArgs e)
        {
            if (player.Bet == 0) return;

            player.state = 1;

            betting_controls.Visibility = Visibility.Hidden;
            playing_controls.IsEnabled = true;
            playing_controls.Visibility = Visibility.Visible;

            house.hand.clear_cards();
            house.hand.hidden = true;

            player.hand.clear_cards();
            player.hand.add_card(deck.random_card());

            render();
        }

        private void victor(bool house_won)
        {
            playing_controls.Visibility = Visibility.Hidden;
            end_controls.Visibility = Visibility.Visible;

            house.hand.hidden = false;
            render();

            player.state = 3;

            end_victor.Text = house_won ? "¡Has perdido!" : "¡Has ganado!";
            end_result.Text = house_won ? $"" : $"{player.Bet} X {house.multiplier}";
            end_reward.Text = house_won ? $"-{player.Bet}" : $"{(int)(player.Bet * house.multiplier)}";

            if (house_won)
            {
                player.money -= player.Bet;
                player.Bet = 0;
                house.win();
            } else
            {
                player.money += (int)(player.Bet * house.multiplier);
                player.Bet = 0;
                house.lose();
            }
        }

        private void end(object sender, RoutedEventArgs e)
        {
            player.state = 0;
            player.chips.clear_chips();
            player.hand.clear_cards();

            house.hand.clear_cards();

            end_controls.Visibility = Visibility.Hidden;
            betting_controls.Visibility = Visibility.Visible;

            if (player.money <= 0)
            {
                MessageBoxResult result = MessageBox.Show("Después de perder todo tu dinero, decidiste dejar de jugar...");
                System.Windows.Application.Current.Shutdown();
            }

            render();
        }


        // PLAYING CONTROLS
        private void add(object sender, RoutedEventArgs e)
        {
            player.hand.add_card(deck.random_card());

            int sum_of_cards = player.hand.sum_of_cards();
            if (sum_of_cards == 21) victor(false);
            else if (sum_of_cards > 21) victor(true);

            update();
        }

        private void call(object sender, RoutedEventArgs e)
        {
            player.state = 2;

            update();
        }


        public void update()
        {
            playing_controls.IsEnabled = false;

            // UPDATE HOUSE
            int sum_of_cards = house.hand.sum_of_cards();
            if (sum_of_cards < 17)
            {
                house.hand.add_card(deck.random_card());

                if (player.state == 2) update();
            }
            else if (sum_of_cards == 21) victor(true);
            else if (sum_of_cards > 21) victor(false);

            if (player.state == 2)
            {
                if (player.hand.sum_of_cards() > house.hand.sum_of_cards()) victor(false);
                else victor(true);
                return;
            }

            render();

            playing_controls.IsEnabled = true;
        }

        public void render()
        {
            double scale = ActualWidth / ActualHeight;


            // RENDER PLAYER
            player.chips.render(scale);
            Canvas.SetTop(player.chips.canvas_item, canvas.ActualHeight / 2);

            player.hand.render(scale);
            double pos = canvas.ActualWidth / 2;
            pos -= player.hand.cards.Count > 1 ? player.hand.cards.Count * Hand.card_distance + Card.base_width : Card.base_width / 2;
            pos += player.hand.cards.Count > 1 ? ((player.hand.cards.Count * Hand.card_distance + Card.base_width) / 2) : 0;
            Canvas.SetLeft(player.hand.canvas_item, pos);
            Canvas.SetTop(player.hand.canvas_item, canvas.ActualHeight / 2);


            // RENDER HOUSE
            house.hand.render(scale);
            pos = canvas.ActualWidth / 2;
            pos -= house.hand.cards.Count > 1 ? house.hand.cards.Count * Hand.card_distance + Card.base_width : Card.base_width / 2;
            pos += house.hand.cards.Count > 1 ? ((house.hand.cards.Count * Hand.card_distance + Card.base_width) / 2) : 0;
            Canvas.SetLeft(house.hand.canvas_item, pos);


            // RENDER INFO
            betting_amount.Content = $"${player.Bet} / ${player.money}";
            info.Content = $"Dinero: ${player.money} Apuesta: ${player.Bet} | Suma de Cartas: {player.hand.sum_of_cards()}";
        }
    }
}
