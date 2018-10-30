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

namespace PokerView
{
    /// <summary>
    /// Interaction logic for PlayerTile.xaml
    /// </summary>
    public partial class PlayerTile : UserControl
    {
        public Player player;
        public PlayerTile(Player player)
        {
            this.player = player;
            DataContext = player;
            InitializeComponent();
        }
    }
}
