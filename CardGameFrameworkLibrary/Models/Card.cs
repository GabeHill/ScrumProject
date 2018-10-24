using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameFrameworkLibrary.Models
{
    public enum Suit
    {
        SPADE,
        CLUB,
        HEART,
        DIAMOND
    }

    public class Card
    {
        public Suit Suit { get; set; }
        public int Value { get; set; }
        public int Rank { get; set; }
        public string ImageSource { get; set; }

    }
}
