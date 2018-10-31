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
        public Deck Deck { get; set; }
        public int Pool { get; set; }

        public Player House { get; set; }

        public BlackJack()
        {
            Players = new List<Player>();
            Deck = new Deck("Blackjack");
        }

        //Gets All the players that have no busted
        public void GetWinners(int winningAmount)
        {
            List<Player> contenders = new List<Player>();

            foreach (var player in Players)
            {
                if (!player.HasBust)
                {
                    contenders.Add(player);
                }
                else
                {
                    player.Bank -= player.Bet;
                }
            }
            foreach (var player in contenders)
            {
                if (player.HandValue > winningAmount)
                {
                    if (player.HandValue == 21)
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
                else if (player.HandValue == winningAmount)
                {
                    //Do nothing
                }
                else
                {
                    player.Bank -= player.Bet;
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
        public void PlaceBet(Player player, int bet)
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

        public int TakeHouseTurn()
        {
            int houseHandValue = 0;
            House = new Player();
            House.CardsInHand = new List<Card>();
            while (houseHandValue < 17)
            {
                House.CardsInHand.Add(Deck.DrawCard());
                houseHandValue = House.GetHandValue();
            }
            if (houseHandValue > 21)
            {
                houseHandValue = 0;
            }
            return houseHandValue;
        }
    }
}