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
        int playerNum = 6;
        List<string> playerNames;
        bool housePlaying;
        public MainWindow(List<string> playerNamesInput, bool withHouse)
        {
            InitializeComponent();
            playerNames = playerNamesInput;
            playerNum = playerNamesInput.Count;
            housePlaying = withHouse;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ugridPlayers.Columns = playerNum;
            for (int i = 0; i < playerNum; i++)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Fill = Brushes.Aquamarine;
                Border border = new Border();
                border.Padding = new Thickness(5.0);
                border.Child = rectangle;
                ugridPlayers.Children.Add(border);
                MessageBox.Show(playerNames[i]);
            }
        }

        private void btnHit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPass_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
