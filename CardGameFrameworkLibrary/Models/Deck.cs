using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameFrameworkLibrary.Models
{
    public class Deck
    {
        public List<Card> Cards { get; set; }


        public void Shuffle()
        {
            int itr = 7;
            int Picked, Remaining;
            List<Card> Shuffling = new List<Card>();
            Random random = new Random();
            for (int i = 0; i < itr; i++)
            {
                Picked = random.Next(10, 30);
                Shuffling = Cards.GetRange(0, Picked);
                Cards.RemoveRange(0, Picked);

                while(Shuffling.Count != 0)
                {
                    Picked = random.Next(10, Cards.Count - 1);
                    Remaining = random.Next(1, Shuffling.Count / 3 + 1);
                    Cards.InsertRange(Picked, Shuffling.GetRange(0, Remaining));
                    Shuffling.RemoveRange(0, Remaining);
                }
            }
        }


        public Card DrawCard()
        {
            Card card = Cards[Cards.Count - 1];
            Cards.Remove(card);
            return card;
        }

        public List<Card> DealCards(int amount)
        {
            List<Card> DealtCards = new List<Card>();
            for (int i = 0; i < amount; i++)
            {
                DealtCards.Add(Cards[Cards.Count - 1]);
            }
            foreach (Card card in DealtCards)
            {
                Cards.Remove(card);
            }
            return DealtCards;
        }
    }
}
