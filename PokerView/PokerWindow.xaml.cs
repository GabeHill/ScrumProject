using CardGameFrameworkLibrary.Models;
using ScrumProject.User_Controls;
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

namespace ScrumProject
{
    /// <summary>
    /// Interaction logic for PokerWindow.xaml
    /// </summary>
    public partial class PokerWindow : Window
    {
        private List<string> pokerNames;
        private bool isChecked;
        Deck deck;

        //PokerGameLogic game

        public PokerWindow(List<string> pokerNames, bool isChecked)
        {
            this.pokerNames = pokerNames;
            this.isChecked = isChecked;
            InitializeComponent();
            GameSetup();
            PlayerInfoSetup();
        }

        private void GameSetup()
        {
            deck = new Deck("Poker");
            deck.Shuffle();

        }

        private void PlayerInfoSetup()
        {
            PlayerInfo player1 = new PlayerInfo(pokerNames[0]);
            player1.card1.Source = deck.DrawCard().GetImage();
            player1.card2.Source = deck.DrawCard().GetImage();
            player1.card3.Source = deck.DrawCard().GetImage();
            player1.card4.Source = deck.DrawCard().GetImage();
            player1.card5.Source = deck.DrawCard().GetImage();
            vb_CurrentPlayer.Child = player1;
            for (int i = 1; i < pokerNames.Count; i++)
            {
                PlayerInfo nextPlayer = new PlayerInfo(pokerNames[i]);
                nextPlayer.card1.Source = deck.DrawCard().GetImage();
                nextPlayer.card2.Source = deck.DrawCard().GetImage();
                nextPlayer.card3.Source = deck.DrawCard().GetImage();
                nextPlayer.card4.Source = deck.DrawCard().GetImage();
                nextPlayer.card5.Source = deck.DrawCard().GetImage();
                dp_PlayerBench.Children.Add(nextPlayer);
            }
            foreach (PlayerInfo player in dp_PlayerBench.Children)
            {
                player.sp_Cards.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NextPlayer();
        }

        private void NextPlayer()
        {
            PlayerInfo CurrentPlayer = (PlayerInfo) vb_CurrentPlayer.Child;
            PlayerInfo NextPlayer = (PlayerInfo) dp_PlayerBench.Children[0];
            dp_PlayerBench.Children.Remove(NextPlayer);
            vb_CurrentPlayer.Child = null;
            dp_PlayerBench.Children.Add(CurrentPlayer);
            vb_CurrentPlayer.Child = NextPlayer;
            CurrentPlayer.sp_Cards.Visibility = Visibility.Hidden;
            NextPlayer.sp_Cards.Visibility = Visibility.Visible;
        }
    }
}
