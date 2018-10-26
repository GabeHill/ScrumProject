using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameFrameworkLibrary.Models
{
    public class Game
    {
        public List<Player> Players { get; set; }
        public Deck Deck { get; set; }
        public int Pot { get; set; }
        public Player Dealer { get; set; }
        public int MyProperty { get; set; }
    }
}


//what do you think about having a model for the game that has a list of players, the deck of cards, the pot, dealer cards and a few others?