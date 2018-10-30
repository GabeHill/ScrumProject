﻿using System;
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
            Allplayers = new List<Player>();
            Deck = new Deck("Blackjack");
        }
        //List of all  players Including House
        public List<Player> Allplayers { get; set; }
        //Current Deck
        public Deck Deck { get; set; }
        //Amount of somone could win
        public int Pool { get; set; }

        //Gets All the players that have no busted
        public List<Player> GetWinners()
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

            return contenders;
        }
        //Grabs the highest hand value of all players
        private int GetHighestNumber(List<Player> players)
        {
            int highestHigh = GetHighestHigh(players);
            Remove(highestHigh, players);
        }

        private int GetHighestHigh(List<Player> players)
        {
            int high = 2;
            foreach (var player in players)
            {
                player.GetHandValue();
                if (player.HandValue > high)
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
        public void StartNewTurn()
        {
            foreach (var player in Players)
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