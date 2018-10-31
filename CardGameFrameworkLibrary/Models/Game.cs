using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameFrameworkLibrary.Models
{
    public abstract class Game
    {
        public List<Player> Players { get; set; }
        public Deck GameDeck { get; set; }
        public int Pot { get; set; }
        public House Dealer { get; set; }
        public Player RaisingPlayer { get; set; }
        public int MinimumBet { get; set; }
        public Phases Phase { get; set; }
    }

    public enum Phases
    {
        FIRSTBETTING,
        DRAWING,
        SECONDBETTING,
        END
    }
}
