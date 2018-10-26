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
        public List<Player> Allplayers { get; private set; }
        //Current Deck
        public Deck Deck { get; private set; }
        //Amount of somone could win
        public int Pool { get; private set; }

        //Gets All the players that have no busted
        private void GetWinners()
        {
            List<Player> contenders = new List<Player>();

            foreach (var player in Allplayers)
            {
                if (!player.HasBust)
                {
                    contenders.Add(player);
                }
            }
            CheckHighest(contenders);
        }
        //Grabs the highest hand value of all players
        private int GetHighestNumber(List<Player> players)
        {
            int high = 1;
            foreach (var player in players)
            {
                if (player.HandValue > high)
                {
                    high = player.HandValue;
                }
            }
            return high;
        }
        //Remove all players who have a lower hand value than a given int
        private void CheckHighest(List<Player> players)
        {
            int high = GetHighestNumber(players);
            foreach (var player in players)
            {
                if (player.HandValue < high)
                {
                    players.Remove(player);
                    break;
                }
            }
        }

        //Give Players Their winnings
        public void EndTurn(List<Player> players)
        {
            int winnings = Pool / players.Count;
            foreach (var player in players)
            {
                if (player.GetType() == typeof(Human))
                {
                    Human human = player as Human;
                    human.Bank += winnings;
                }
            }
        }

        //Players Place Bet and receive new cards
        public void StartNewTurn()
        {
            foreach (var player in Allplayers)
            {
                player.CardsInHand = (Deck.DealCards(2));
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