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
                if (string.IsNullOrWhiteSpace(names[i]))
                {
                    names[i] = $"Player {i + 1}";
                }

                Human player = new Human()
                {
                    Name = names[i],
                    Bank = 1000,
                    Bet = 20,
                    CardsInHand = new List<Card>(),
                };

                jack.Players.Add(player);
            }

            if (IsHousePlaying)
            {
                jack.Players.Add(new House());
            }
            jack.StartNewTurn();
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
            DrawHand();
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
                currentPlayer.CardsInHand.Add(jack.GameDeck.DrawCard());
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
                UpdateViews();
                btnHit.IsEnabled = false;
                btnHit.Content = (currentPlayer.HasBust) ? "Busted!" : "Can't draw any more";
            }
            DrawHand();
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

            sequence++;
            if (sequence >= jack.Players.Count - 1)
            {
                currentPlayer = jack.Players[sequence];
                if (currentPlayer.GetType() == typeof(House))
                {
                    while (currentPlayer.GetHandValue() < 17)
                    {
                        currentPlayer.CardsInHand.Add(jack.GameDeck.DrawCard());
                    }
                }
                EndRound();
                sequence = 0;
                jack.GameDeck = new Deck("Blackjack");
            }

            currentPlayer = jack.Players[sequence];
            DrawHand();
            lblTurnIndicator.Content = $"{jack.Players[sequence].Name}'s turn!";

            btnBet1.IsEnabled = true;
            btnBet1.Content = "Bet $20";
            btnBet5.IsEnabled = true;
            btnBet5.Content = "Bet $50";
            btnBet10.IsEnabled = true;
            btnBet10.Content = "Bet $100";
        }

        private void EndRound()
        {
            jack.GetWinners();
            ShowHouseHand();
            StartRound();
            UpdateViews();
            DrawHand();
        }

        private void ShowHouseHand()
        {
            lblTurnIndicator.Content = "House's turn";

            DrawHand();
            MessageBox.Show($"The house got {currentPlayer.GetHandValue()}");
        }

        private void DrawHand()
        {
            pnlCardDisply.Children.RemoveRange(0, pnlCardDisply.Children.Count);
            foreach (var card in currentPlayer.CardsInHand)
            {
                Image image = new Image()
                {
                    Source = new BitmapImage(new Uri(card.ImageSource)),
                    MaxHeight = 200,
                    MaxWidth = 100
                    
                };
                pnlCardDisply.Children.Add(image);
            }
        }

        private void StartRound()
        {
            List<int> playersToRemove = new List<int>();
            foreach (var player in jack.Players)
            {
                if (player.Bank <= 0)
                {
                    playersToRemove.Add(jack.Players.IndexOf(player));
                }
            }
            foreach (int i in playersToRemove)
            {
                jack.Players.RemoveAt(i);
            }
            jack.StartNewTurn();
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
            currentPlayer.Bet = 20;
            UpdateViews();
        }

        private void btnBet5_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.Bet = 50;
            UpdateViews();
        }

        private void btnBet10_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer.Bet = 100;
            UpdateViews();
        }
    }
}
