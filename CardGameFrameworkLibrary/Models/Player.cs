using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameFrameworkLibrary.Models
{
    public abstract class Player
    {
        public string Name { get; set; }
        public int Bank { get; set; }
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

        //if bet is larger than their bank they go all  in
        public void PlaceBet(int amount)
        {
            Bank = Bank - amount;
            if (Bank < 0)
            {
                Bank = 0;
            }
        }
        //Getter: Gets the hand value for blackJack
        public int GetHandValue()
        {
            HandValue = 0;
            foreach (var card in CardsInHand)
            {
                HandValue += card.Value;
                if (HandValue > 21 && card.Rank == Rank.ACE)
                {
                    HandValue = HandValue - 10;
                }
            }
            return HandValue;
        }
    }
}
