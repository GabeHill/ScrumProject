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
        }

        public void InitializePlayers(List<string> names, bool IsHousePlaying)
        {

            for (int i = 0; i < names.Count; i++)
            {
                Player player = new Player()
                {
                    Name = names[i],
                    Bank = 2000,
                };

                jack.Allplayers.Add(player);
            }

            if (IsHousePlaying)
            {
                jack.Allplayers.Add(new House());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ugridPlayers.Columns = jack.Allplayers.Count;
            for (int i = 0; i < jack.Allplayers.Count; i++)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Fill = Brushes.Aquamarine;
                Border border = new Border();
                border.Padding = new Thickness(5.0);
                border.Child = rectangle;
                ugridPlayers.Children.Add(border);
                MessageBox.Show(jack.Allplayers[i].Name);
            }
        }

        private void btnHit_Click(object sender, RoutedEventArgs e)
        {
            if (!currentPlayer.HasBust && jack.CanDraw(currentPlayer))
            {
                //Draw card jack.Deck
                currentPlayer.GetHandValue();
                if (currentPlayer.HasBust || !jack.CanDraw(currentPlayer))
                {
                    PassTurn();
                }
            }
            else
            {
                PassTurn();
            }
        }

        private void btnPass_Click(object sender, RoutedEventArgs e)
        {
            PassTurn();
        }

        private void PassTurn()
        {
            sequence++;
            if (sequence >= jack.Allplayers.Count)
            {
                EndRound();
                sequence = 0;
            }
            currentPlayer = jack.Allplayers[sequence];
        }

        private void EndRound()
        {
            List<Player> winners = jack.GetWinners();
            foreach (var winner in winners)
            {
                winner.Bank += jack.Pool / winners.Count;
            }
        }

        private void StartRound()
        {
            foreach (var player in jack.Allplayers)
            {
                player.Bank -= 100;
            }
        }
    }
}
