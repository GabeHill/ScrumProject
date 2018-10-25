using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGameFrameworkLibrary.Models;

namespace BlackJackLib
{
    public class BlackJack
    {
        //List of all  players Including House
        public List<Player> Allplayers { get; set; }
        //Current Deck
        public Deck Deck { get; set; }
        //Amount of somone could win
        public int Pool { get; set; }

        //Gets All the players that have no busted
        private void GetWinners()
        {
            List<Player> contenders = new List<Player>();

            foreach (var player in Allplayers)
            {
                if (!player.HasFolded)
                {
                    contenders.Add(player);
                }
            }
            CheckHighest(contenders);
        }
        //Grab all the players who 
        private void CheckHighest(List<Player> players)
        {
            int low = 2;
            bool HasWinner = false;
            while (!HasWinner)
            {
                if (players.Count > 1)
                {
                    Remove(low, players);
                }
                else
                {
                    HasWinner = true;
                }
                low++;
            }
        }

        //Removes player if lower than the low
        public void Remove(int low,List<Player> players)
        {
            foreach (var player in players)
            {
                if (players.Count > 1)
                {
                    if (player.HandValue < low)
                    {
                        players.Remove(player);
                        break;
                    }
                }
            }
        }

        //Does a player have less than five Cards in Hand
        public bool CanDraw(Player player)
        {
            if (player.CardsInHand.Count <= 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}