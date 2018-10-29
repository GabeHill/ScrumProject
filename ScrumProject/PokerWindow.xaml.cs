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

        //PokerGameLogic game

        public PokerWindow()
        {
            InitializeComponent();
            PlayerInfoSetup();

        }

        private void PlayerInfoSetup()
        {
            vb_CurrentPlayer.Child = new PlayerInfo(1);
            for (int i = 1; i < players; i++)
            {
                dp_PlayerBench.Children.Add(new PlayerInfo(i + 1));
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
