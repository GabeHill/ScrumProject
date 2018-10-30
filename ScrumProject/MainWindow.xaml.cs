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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> games = new List<string> { "Poker", "Blackjack" };
            lbxGames.ItemsSource = games;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            switch ((string)lbxGames.SelectedItem)
            {
                case "Poker":
                    PokerSettings pokerSettings = (PokerSettings)settings;
                    List<string> pokerNames = new List<string>();
                    for (int i = 0; i < (int)pokerSettings.cmbxPlayerCount.SelectedItem; i++)
                    {
                        var nameHolder = (StackPanel)pokerSettings.ugridPlayerNames.Children[i];
                        var name = (TextBox)nameHolder.Children[1];
                        pokerNames.Add(name.Text);
                    }
                    PokerWindow pokerWindow = new PokerWindow(pokerNames, (bool)pokerSettings.cbxWithHouse.IsChecked);

                    pokerWindow.Show();
                    Close();
                    break;
                case "Blackjack":
                    BlackjackSettings blackjackSettings = (BlackjackSettings)settings;
                    List<string> blackjackNames = new List<string>();
                    for (int i = 0; i < (int)blackjackSettings.cmbxPlayerCount.SelectedItem; i++)
                    {
                        var nameHolder = (StackPanel)blackjackSettings.ugridPlayerNames.Children[i];
                        var name = (TextBox)nameHolder.Children[1];
                        blackjackNames.Add(name.Text);
                    }
                    BlackJackView.MainWindow blackjackWindow = new BlackJackView.MainWindow(blackjackNames, (bool)blackjackSettings.cbxWithHouse.IsChecked);
                    blackjackWindow.Show();
                    Close();
                    break;
            }
        }

        UserControl settings;
        private void lbxGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            grdMain.Children.Remove(settings);
            if ((string)lbxGames.SelectedItem == "Poker")
            {
                settings = new PokerSettings();

            }
            if ((string)lbxGames.SelectedItem == "Blackjack")
            {
                settings = new BlackjackSettings();
            }
            grdMain.Children.Add(settings);
            Grid.SetColumn(settings, 1);
        }
    }
}
