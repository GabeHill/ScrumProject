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
    /// Interaction logic for PlayerInfo.xaml
    /// </summary>
    public partial class PlayerInfo : UserControl
    {
        public Player player;

        public PlayerInfo(Player p)
        {
            InitializeComponent();
            player = p;
        }

        public void UpdateView()
        {
            lblName.Content = $"Player: {player.Name}";
            lblBank.Content = $"${player.Bank}";
            lblBet.Content = $"${player.Bet}";

            pnlCards.Children.RemoveRange(0, pnlCards.Children.Count);
            foreach (var card in player.CardsInHand)
            {
                Image image = new Image();
                image.Source = new BitmapImage(new Uri("pack://application:,,,/CardGameFrameworkLibrary;component/Resource/CARDBACK.png"));
                image.IsMouseDirectlyOverChanged += ImageMouseOverChanged;
                image.MaxHeight = 50;
                image.MaxWidth = 50;
                pnlCards.Children.Add(image);
            }
        }

        private void ImageMouseOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Image i = (Image)sender;
            if (i.IsMouseDirectlyOver)
            {
                i.Source = new BitmapImage(new Uri(player.CardsInHand[pnlCards.Children.IndexOf(i)].ImageSource));
            }
            else
            {
                i.Source = new BitmapImage(new Uri("pack://application:,,,/CardGameFrameworkLibrary;component/Resource/CARDBACK.png"));
            }
        }
    }
}
