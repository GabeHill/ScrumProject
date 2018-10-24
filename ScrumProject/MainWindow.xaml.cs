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

namespace ScrumProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnPoker_Click(object sender, RoutedEventArgs e)
        {
            //Name this whatever the poker window is called
            //PokerWindow poker = new PokerWindow();
            //poker.Show();
            Close();
        }

        private void btnBlackjack_Click(object sender, RoutedEventArgs e)
        {
            //Name this whatever the blackjack window is called
            //BlackjackWindow blackjack = new BlackjackWindow();
            //blackjack.Show();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblGame.Height = lblGame.Width;
            tblGame.Height = tblGame.Width;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            lblGame.Height = lblGame.Width;
            tblGame.Height = tblGame.Width;
        }
    }
}
