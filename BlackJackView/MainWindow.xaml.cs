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
                    Bank = 2000,
                };

                jack.Players.Add(player);
            }

            if (IsHousePlaying)
            {
                jack.Players.Add(new House());
            }

            jack.StartNewTurn(20);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ugridPlayers.Columns = jack.Players.Count;
            for (int i = 0; i < jack.Players.Count; i++)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Fill = Brushes.Aquamarine;
                Border border = new Border();
                border.Padding = new Thickness(5.0);
                border.Child = rectangle;
                ugridPlayers.Children.Add(border);
                MessageBox.Show(jack.Players[i].Name);
            }
        }

        private void btnHit_Click(object sender, RoutedEventArgs e)
        {
            if (!currentPlayer.HasBust && jack.CanDraw(currentPlayer))
            {
                currentPlayer.CardsInHand.Add(jack.GameDeck.DrawCard());
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
            if (sequence >= jack.Players.Count)
            {
                EndRound();
                sequence = 0;
            }
            currentPlayer = jack.Players[sequence];
        }

        private void EndRound()
        {
            jack.GetWinners();
        }

        private void StartRound()
        {
            jack.StartNewTurn(20);
        }
    }
}
