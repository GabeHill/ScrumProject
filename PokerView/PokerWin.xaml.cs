using CardGameFrameworkLibrary.Models;
using Poker.Controller;
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
using System.Windows.Shapes;

namespace PokerView
{
    /// <summary>
    /// Interaction logic for PokerView.xaml
    /// </summary>
    public partial class PokerWin : Window
    {
        PokerLogic poker;
        List<PlayerTile> PlayerInfoTiles = new List<PlayerTile>();
        Player CurrentPlayer;
        private int FoldCount;

        public PokerWin(PokerLogic pokerLogic)
        {
            InitializeComponent();
            poker = pokerLogic;
            PlayerInfoSetup();
            FoldCount = 0;

        }

        private void PlayerInfoSetup()
        {

            PlayerTile CurrentTile = new PlayerTile(poker.Players[0]);
            CurrentPlayer = CurrentTile.player;
            PlayerInfoTiles.Add(CurrentTile);
            vb_CurrentPlayer.Child = CurrentTile;
            for (int i = 1; i < poker.Players.Count; i++)
            {
                PlayerTile NextTile = new PlayerTile(poker.Players[i]);
                PlayerInfoTiles.Add(NextTile);
                dp_PlayerBench.Children.Add(NextTile);
            }
            foreach (PlayerTile player in dp_PlayerBench.Children)
            {
                player.sp_Cards.Visibility = Visibility.Hidden;
            }
            if (CurrentTile.player.Equals(poker.Dealer))
            {
                NextPlayer();
            }
            poker.Deal();

            foreach (PlayerTile info in PlayerInfoTiles)
            {
                foreach (Card card in info.player.CardsInHand)
                {
                    Image image = new Image()
                    {
                        Source = new BitmapImage(new Uri(card.ImageSource, UriKind.Absolute)),
                        MaxHeight = 50,


                    };
                    image.MouseDown += card.ToggleSelected;
                    image.MouseDown += ToggleOpacity;
                    
                    info.sp_Cards.Children.Add(image);
                }
            }
        }

        private void ToggleOpacity(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image)sender;
            image.Opacity = image.Opacity == 1 ? 0.7 : 1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NextPlayer();
        }

        private void NextPlayer()
        {
            MoveTiles();
            IsBetChecked();
            if (IsBettingPhase())
            {
                btnCheck.IsEnabled = true;
                btnBet.IsEnabled = true;
                btnFold.IsEnabled = true;
                tbBet.IsEnabled = true;
            }
            if (IsDrawingPhase())
            {
                btnCheck.IsEnabled = false;
                btnBet.IsEnabled = false;
                btnFold.IsEnabled = false;
                tbBet.IsEnabled = false;
            }

            if (FoldCount == poker.Players.Count)
            {
                throw new Exception("All players have folded and this needs to be handled");
            }

            if (CurrentPlayer.HasFolded)
            {
                NextPlayer();
            }
        }

        private void Discard()
        {
            List<Card> temp = new List<Card>();
            foreach (Card card in CurrentPlayer.CardsInHand)
            {
                temp.AddRange(CurrentPlayer.CardsInHand);
                if (!card.IsKept)
                {
                    temp.Remove(card);
                }
            }
            CurrentPlayer.CardsInHand = temp;
        }

        private void Drawing()
        {
            while (CurrentPlayer.CardsInHand.Count <= 5)
            {
                CurrentPlayer.CardsInHand.Add(poker.GameDeck.DrawCard());
            }
            //draw number of cards depending on number discarded(amount)
            throw new NotImplementedException();
        }

        private bool IsBettingPhase()
        {
            if (CurrentPlayer.Equals(poker.RaisingPlayer) && poker.Phase == Phases.DRAWING)
            {
                poker.Phase = Phases.BETTING;
                return true;
            }
            return false;
        }

        private bool IsDrawingPhase()
        {
            if (CurrentPlayer.Equals(poker.RaisingPlayer) || poker.Phase == Phases.DRAWING)
            {
                poker.Phase = Phases.DRAWING;
                return true;
            }
            return false;
        }

        private void IsBetChecked()
        {
            if (poker.MinimumBet > 0)
            {
                btnCheck.IsEnabled = false;
            }
            else
            {
                btnCheck.IsEnabled = true;
            }
        }

        private void MoveTiles()
        {
            PlayerTile CurrentTile = (PlayerTile)vb_CurrentPlayer.Child;
            PlayerTile NextTile = (PlayerTile)dp_PlayerBench.Children[0];
            dp_PlayerBench.Children.Remove(NextTile);
            vb_CurrentPlayer.Child = null;
            dp_PlayerBench.Children.Add(CurrentTile);
            vb_CurrentPlayer.Child = NextTile;
            CurrentTile.sp_Cards.Visibility = Visibility.Hidden;
            NextTile.sp_Cards.Visibility = Visibility.Visible;
            CurrentPlayer = NextTile.player;
        }

        private void Fold(object sender, RoutedEventArgs e)
        {
            CurrentPlayer.HasFolded = true;
            NextPlayer();
            FoldCount++;
        }

        private void PlaceBet(object sender, RoutedEventArgs e)
        {

            int bet = int.Parse(tbBet.Text);
            poker.SetBet(CurrentPlayer, bet);
            NextPlayer();

        }



        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid grid = (Grid)sender;
            grid.Focus();
        }

        private void tbBet_LostFocus(object sender, MouseEventArgs e)
        {
            if (poker != null)
            {
                TextBox textBox = (TextBox)sender;
                try
                {
                    if (int.Parse(textBox.Text) < poker.MinimumBet)
                    {
                        textBox.Text = poker.MinimumBet.ToString();
                    }
                }
                catch
                {
                    textBox.Text = poker.MinimumBet.ToString();
                }
            }
        }
    }
}
