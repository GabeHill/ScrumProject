using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameFrameworkLibrary.Models
{
    public class Player
    {
        public int Bank { get; set; }
        public int Bet { get; set; }
        public string Name { get; set; }
        public List<Card> CardsInHand { get; set; }
        public bool HasFolded { get; set; }
        public bool HasBust
        {
            get { return Bust(); }
        }
        public int HandValue { get; set; }

        private bool Bust()
        {
            GetHandValue();
            if (HandValue > 21)
            {
                return true;
            }
            return false;
        }

        public void GetHandValue()
        {
            HandValue = 0;
            foreach (var card in CardsInHand)
            {
                HandValue += card.Value;
            }
        }
    }
}
