using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameFrameworkLibrary.Models
{
    public class Human: Player
    {
        public int Bank { get; set; }
        public string Name { get; set; }

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
