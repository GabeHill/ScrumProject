using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameFrameworkLibrary.Models
{
    public class Player : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }

        private int bank;
        public int Bank
        {
            get
            {
                return bank;
            }
            set
            {
                if (bank != value)
                {
                    bank = value;
                    OnPropertyChanged("Bank");
                }
            }
        }
        private int bet;
        public int Bet
        {
            get
            {
                return bet;
            }
            set
            {
                if (bet != value)
                {
                    bet = value;
                    OnPropertyChanged("Bet");
                }
            }
        }
        public string Name { get; set; }
        public List<Card> CardsInHand { get; set; }
        public bool HasFolded { get; set; }
        public bool HasBust { get; set; }
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
        //Note: ONLY FOR BLACKJACK. Does not return accurate
        //values for other games
        public int GetHandValue()
        {
            HandValue = 0;
            int numOfAces = 0;
            foreach (var card in CardsInHand)
            {
                HandValue += card.Value;
                if (card.Rank == Rank.ACE)
                {
                    numOfAces++;
                }
            }
            if (HandValue > 21)
            {
                for (int i = 0; i < numOfAces; i++)
                {
                    if (HandValue > 21)
                    {
                        HandValue = HandValue - 10;
                    }
                }
                if (HandValue > 21)
                {
                    HasBust = true;
                }
            }
            return HandValue;
        }
    }
}
