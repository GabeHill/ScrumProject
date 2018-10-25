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
        public string Name { get; set; }
        public List<Card> CardsInHand
        { get; set; }
        public bool HasFolded { get; set; }
        public bool HasBust { get; set; }
        public int HandValue
        {
            get { return GetHandValue(); }
        }

        //Getter: Gets the hand value
        private int GetHandValue()
        {
            int total = 0;
            foreach (var card in CardsInHand)
            {
                total += card.Value;
            }
            return total;
        }

        //Place a bet and remove that amount from his "bank"
        public void PlaceBet(int amount)
        {
            Bank = Bank - amount;
            if (Bank < 0)
            {
                Bank = 0;
            }
        }
    }
}
