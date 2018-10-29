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
        public int Pool { get; set; }
        public Deck Deck { get; set; }
    }
}
