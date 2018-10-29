using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameFrameworkLibrary.Models
{
    public abstract class Player
    {
        public List<Card> CardsInHand { get; set; }
        public bool HasFolded { get; set; }
        public bool HasBust { get; set; }
        public int Bank { get; set; }
        public int HandValue { get; set; }
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
            int total = 0;
            foreach (var card in CardsInHand)
            {
                total += card.Value;
                if (total > 21 && card.Rank == Rank.ACE)
                {
                    total = total - 10;
                }
            }
            return total;
        }
    }
}
