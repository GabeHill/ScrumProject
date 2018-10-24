using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameFrameworkLibrary.Models
{
    class Deck
    {
        public List<Card> Cards { get; set; }


        public List<Card> DealCards(int amount)
        {
            Random random = new Random();
            List<Card> DealtCards = new List<Card>();
            for (int i = 0; i < amount; i++)
            {
                int index = random.Next(0, Cards.Count);
                Card card = Cards[index];
                DealtCards.Add(card);
                Cards.Remove(card);
            }
            return DealtCards;
        }
    }
}
