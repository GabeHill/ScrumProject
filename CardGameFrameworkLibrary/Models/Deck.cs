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


        /*public List<Card> DealCards(int amount)
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
        }*/
    }
}
