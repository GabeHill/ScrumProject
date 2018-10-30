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

        public PokerWin(PokerLogic pokerLogic)
        {
            InitializeComponent();
            poker = pokerLogic;
            PlayerInfoSetup();

        }

        private void PlayerInfoSetup()
        {

            PlayerTile CurrentPlayer = new PlayerTile(poker.Players[0]);
            PlayerInfoTiles.Add(CurrentPlayer);
            vb_CurrentPlayer.Child = CurrentPlayer;
            for (int i = 1; i < poker.Players.Count; i++)
            {
                PlayerTile NextPlayer = new PlayerTile(poker.Players[i]);
                PlayerInfoTiles.Add(NextPlayer);
                dp_PlayerBench.Children.Add(NextPlayer);
            }
            foreach (PlayerTile player in dp_PlayerBench.Children)
            {
                player.sp_Cards.Visibility = Visibility.Hidden;
            }
            NextPlayer();
            poker.Deal();

            foreach (PlayerTile info in PlayerInfoTiles)
            {
                foreach (Card card in info.player.CardsInHand)
                {
                    info.sp_Cards.Children.Add(new Image()
                    {
                        Source = new BitmapImage(new Uri(card.ImageSource, UriKind.Absolute)),
                        MaxHeight = 50

                    });
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NextPlayer();
        }

        private void NextPlayer()
        {
            PlayerTile CurrentPlayer = (PlayerTile)vb_CurrentPlayer.Child;
            PlayerTile NextPlayer = (PlayerTile)dp_PlayerBench.Children[0];
            dp_PlayerBench.Children.Remove(NextPlayer);
            vb_CurrentPlayer.Child = null;
            dp_PlayerBench.Children.Add(CurrentPlayer);
            vb_CurrentPlayer.Child = NextPlayer;
            CurrentPlayer.sp_Cards.Visibility = Visibility.Hidden;
            CurrentPlayer.sp_SelectCards.Visibility = Visibility.Hidden;
            NextPlayer.sp_Cards.Visibility = Visibility.Visible;
            NextPlayer.sp_SelectCards.Visibility = Visibility.Visible;
        }
    }
}
