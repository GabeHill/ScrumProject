using BlackJackLib;
using CardGameFrameworkLibrary.Models;
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

namespace BlackJackView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlackJack jack;
        int sequence;
        Player currentPlayer;

        public MainWindow(List<string> playerNamesInput, bool withHouse)
        {
            InitializeComponent();
            jack = new BlackJack();
            InitializePlayers(playerNamesInput, withHouse);
            currentPlayer = jack.Players[0];
        }

        public void InitializePlayers(List<string> names, bool IsHousePlaying)
        {

            for (int i = 0; i < names.Count; i++)
            {
                Human player = new Human()
                {
                    Name = names[i],
                    Bank = 20,
                    Bet = 1,
                    CardsInHand = new List<Card>(),
                };

                jack.Players.Add(player);
            }

            if (IsHousePlaying)
            {
                jack.Players.Add(new House());
            }



            jack.StartNewTurn(20);
        }

        private void PopulatePlayerGrid()
        {
            foreach (var player in jack.Players)
            {
                PlayerInfo playerInfo = new PlayerInfo(player);
                switch (jack.Players.IndexOf(player))
                {
                    case 0:
                        playerInfo.pnlMain.Background = Brushes.PaleVioletRed;
                        break;
                    case 1:
                        playerInfo.pnlMain.Background = Brushes.Aquamarine;
                        break;
                    case 2:
                        playerInfo.pnlMain.Background = Brushes.Yellow;
                        break;
                    case 3:
                        playerInfo.pnlMain.Background = Brushes.LawnGreen;
                        break;
                    case 4:
                        playerInfo.pnlMain.Background = Brushes.Lavender;
                        break;
                    case 5:
                        playerInfo.pnlMain.Background = Brushes.Orange;
                        break;
                }
                playerInfo.UpdateView();
                Border border = new Border();
                border.Padding = new Thickness(5.0);
                border.Child = playerInfo;
                ugridPlayers.Children.Add(border);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ugridPlayers.Columns = 2;
            ugridPlayers.Rows = 3;
            PopulatePlayerGrid();
            lblTurnIndicator.Content = $"{jack.Players[0].Name}'s turn!";
            foreach (var card in jack.Players[0].CardsInHand)
            {
                pnlCardDisply.Children.Add(new Image { Source = new BitmapImage(new Uri(card.ImageSource)) });
            }
        }

        private void btnHit_Click(object sender, RoutedEventArgs e)
        {
            btnBet1.IsEnabled = false;
            btnBet1.Content = "Can't bet anymore sry";
            btnBet5.IsEnabled = false;
            btnBet5.Content = "Can't bet anymore sry";
            btnBet10.IsEnabled = false;
            btnBet10.Content = "Can't bet anymore sry";
            if (!currentPlayer.HasBust && jack.CanDraw(currentPlayer))
            {
                currentPlayer.CardsInHand.Add(jack.Deck.DrawCard());
                currentPlayer.GetHandValue();
                if (currentPlayer.HasBust || !jack.CanDraw(currentPlayer))
                {
                    UpdateViews();
                    btnHit.IsEnabled = false;
                    btnHit.Content = (currentPlayer.HasBust) ? "Busted!" : "Can't draw any more";
                }
            }
            else
            {
                btnHit.IsEnabled = false;
                btnHit.Content = (currentPlayer.HasBust) ? "Busted!" : "Can't draw any more";
            }
            pnlCardDisply.Children.RemoveRange(0, pnlCardDisply.Children.Count);
            foreach (var card in jack.Players[sequence].CardsInHand)
            {
                pnlCardDisply.Children.Add(new Image { Source = new BitmapImage(new Uri(card.ImageSource)) });
            }
            UpdateViews();
        }

        private void btnPass_Click(object sender, RoutedEventArgs e)
        {
            PassTurn();
        }

        private void PassTurn()
        {
            btnHit.IsEnabled = true;
            btnHit.Content = "Hit";

            pnlCardDisply.Children.RemoveRange(0, pnlCardDisply.Children.Count);
            foreach (var card in jack.Players[sequence].CardsInHand)
            {
                pnlCardDisply.Children.Add(new Image { Source = new BitmapImage(new Uri(card.ImageSource)) });
            }
            sequence++;
            if (sequence >= jack.Players.Count)
            {
                EndRound();
                sequence = 0;
                foreach (var player in jack.Players)
                {
                    player.HasBust = false;
                }
                jack.Deck = new Deck("Blackjack");
            }

            pnlCardDisply.Children.RemoveRange(0, pnlCardDisply.Children.Count);
            foreach (var card in jack.Players[sequence].CardsInHand)
            {
                pnlCardDisply.Children.Add(new Image { Source = new BitmapImage(new Uri(card.ImageSource)) });
            }
            currentPlayer = jack.Players[sequence];
            lblTurnIndicator.Content = $"{jack.Players[sequence].Name}'s turn!";

            btnBet1.IsEnabled = true;
            btnBet1.Content = "Bet $1";
            btnBet5.IsEnabled = true;
            btnBet5.Content = "Bet $5";
            btnBet10.IsEnabled = true;
            btnBet10.Content = "Bet $10";
        }

        private void EndRound()
        {
            int winningAmount = jack.TakeHouseTurn();
            jack.GetWinners(winningAmount);
            foreach (var player in jack.Players)
            {
                player.CardsInHand = new List<Card>();
            }
            ShowHouseHand();
            StartRound();
            UpdateViews();
        }

        private void ShowHouseHand()
        {
            lblTurnIndicator.Content = "House's turn";

            pnlCardDisply.Children.RemoveRange(0, pnlCardDisply.Children.Count);
            foreach (var card in jack.House.CardsInHand)
            {
                pnlCardDisply.Children.Add(new Image { Source = new BitmapImage(new Uri(card.ImageSource)) });
            }
            if (jack.House.HandValue == 0)
            {
                MessageBox.Show("The house busted");
            }
            else
            {
                MessageBox.Show($"The house got {jack.House.HandValue}");
            }
        }

        private void StartRound()
        {
            List<int> playersToRemove = new List<int>();
            foreach (var player in jack.Players)
            {
                if (player.Bank <= -50)
                {
                    playersToRemove.Add(jack.Players.IndexOf(player));
                }
            }
            foreach (int i in playersToRemove)
            {
                jack.Players.RemoveAt(i);
            }
            jack.StartNewTurn(20);
        }

        private void UpdateViews()
        {
            for (int i = 0; i < jack.Players.Count; i++)
            {
                Border border = (Border)ugridPlayers.Children[i];
                PlayerInfo info = (PlayerInfo)border.Child;
                info.UpdateView();
            }
        }

        private void btnBet1_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.Bet = 1;
            UpdateViews();
        }

        private void btnBet5_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.Bet = 5;
            UpdateViews();
        }

        private void btnBet10_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.Bet = 10;
            UpdateViews();
        }
    }
}
