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
    /// Interaction logic for BlackjackSettings.xaml
    /// </summary>
    public partial class BlackjackSettings : UserControl
    {
        public BlackjackSettings()
        {
            InitializeComponent();
        }

        List<int> playersWithHouse = new List<int> { 1, 2, 3, 4, 5 };
        List<int> playersWithoutHouse = new List<int> { 2, 3, 4, 5, 6 };

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ugridPlayerNames.Columns = 3;
            ugridPlayerNames.Rows = 2;
            cmbxPlayerCount.SelectedItem = 2;
            ugridPlayerNames.Children.Add(new TextBox());
            cmbxPlayerCount.ItemsSource = playersWithoutHouse;
        }

        private void cbxWithHouse_CheckChanged(object sender, RoutedEventArgs e)
        {
            if (cbxWithHouse.IsChecked.Value)
            {
                if ((int)cmbxPlayerCount.SelectedItem == 6)
                {
                    cmbxPlayerCount.SelectedItem = 5;
                }
                cmbxPlayerCount.ItemsSource = playersWithHouse;
            }
            if (!cbxWithHouse.IsChecked.Value)
            {
                if ((int)cmbxPlayerCount.SelectedItem == 1)
                {
                    cmbxPlayerCount.SelectedItem = 2;
                }
                cmbxPlayerCount.ItemsSource = playersWithoutHouse;
            }
        }

        private void cmbxPlayerCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ugridPlayerNames.Children.RemoveRange(0, ugridPlayerNames.Children.Count);
            if (cmbxPlayerCount.SelectedItem != null)
            {
                for (int i = 0; i < (int)cmbxPlayerCount.SelectedItem; i++)
                {
                    StackPanel panel = new StackPanel();

                    Label label = new Label();
                    label.Content = $"Player {i + 1}'s name: ";
                    panel.Children.Add(label);

                    TextBox box = new TextBox();
                    box.Margin = new Thickness(10.0);
                    panel.Children.Add(box);

                    ugridPlayerNames.Children.Add(panel);
                }
            }
        }
    }
}
