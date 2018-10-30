using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGameFrameworkLibrary.Models;

namespace BlackJackLib
{
    public class BlackJack: Game
    {
        public BlackJack()
        {
            Players = new List<Player>();
            Deck = new Deck("Blackjack");
        }

        //Gets All the players that have no busted
        public void GetWinners()
        {
            List<Player> contenders = new List<Player>();

            foreach (var player in Players)
            {
                if (!player.HasBust)
                {
                    contenders.Add(player);
                }
            }
            CheckHighest(contenders);
            EndTurn(contenders);
        }
        //Grabs the highest hand value of all players
        private void CheckHighest(List<Player> players)
        {
            int highestHigh = GetHighestHigh(players);
            Remove(highestHigh, players);
        }

        private void Remove(int highestHigh, List<Player> players)
        {
            foreach (var player in players)
            {
                if (player.GetHandValue() < highestHigh)
                {
                    players.Remove(player);
                    break;
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
            int winnings = Pool / players.Count;
            foreach (var player in players)
            {
                player.Bank += winnings;
            }
        }

        //Players Place Bet and receive new cards
        public void StartNewTurn(int bet)
        {
            foreach (var player in Players)
            {
                player.CardsInHand = (Deck.DealCards(2));
            }
        }
        public void PlaceBet(Player player,int bet)
        {
            player.PlaceBet(bet);
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