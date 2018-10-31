using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGameFrameworkLibrary.Models;

namespace BlackJackLib
{
    public class BlackJack : Game
    {
        public int Pool { get; set; }

        public BlackJack()
        {
            Players = new List<Player>();
            GameDeck = new Deck("Blackjack");
        }

        //Gets All the players that have no busted
        public void GetWinners()
        {
            List<Player> contenders = new List<Player>();

            foreach (var player in Players)
            {
                player.Bank -= player.Bet;
                if (!player.HasBust)
                {
                    contenders.Add(player);
                }
            }

            int winningAmount = GetHighestHigh(contenders);
            foreach (var player in contenders)
            {
                if (player.GetHandValue() == winningAmount)
                {
                    if (player.GetHandValue() == 21)
                    {
                        player.Bank += player.Bet * 3;
                    }
                    else if (player.CardsInHand.Count == 5)
                    {
                        player.Bank += player.Bet * 5;
                    }
                    else
                    {
                        player.Bank += player.Bet * 2;
                    }

                }
            }

            //CheckHighest(contenders);
            //EndTurn(contenders);
        }
        //Grabs the highest hand value of all players
        private void CheckHighest(List<Player> players)
        {
            int highestHigh = GetHighestHigh(players);
            Remove(highestHigh, players);
        }

        private void Remove(int highestHigh, List<Player> players)
        {
            bool isRemoving = true;
            while (isRemoving)
            {
                int total = players.Count;
                foreach (var player in players)
                {
                    if (player.GetHandValue() < highestHigh)
                    {
                        players.Remove(player);
                        break;
                    }
                }
                if (total == players.Count)
                {
                    isRemoving = false;
                }
            }
        }

        private int GetHighestHigh(List<Player> players)
        {
            int high = 2;
            foreach (var player in players)
            {
                if (player.GetHandValue() > high)
                {
                    high = player.HandValue;
                }
                if (high == 21)
                {
                    return high;
                }
            }
            return high;
        }

        //Give Players Their winnings
        public void EndTurn(List<Player> players)
        {
            int winnings = Pot / players.Count;
            foreach (var player in players)
            {
                player.Bank += winnings;
            }
        }

        //Players Place Bet and receive new cards
        public void StartNewTurn()
        {
            foreach (var player in Players)
            {
                player.CardsInHand = (GameDeck.DealCards(2));
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