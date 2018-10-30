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
