using CardGameFrameworkLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Poker.Controller
{
    public class PokerLogic : Game
    {

        public enum Hands
        {
            HIGHCARD = 1,
            PAIR,
            TWOPAIR,
            THREEKIND,
            STRAIGHT,
            FLUSH,
            FULLHOUSE,
            FOURKIND,
            STRAIGHTFLUSH,
            ROYALFLUSH
        }

        public void CheckForWinningHand()
        {
            //Go through each players hands to get a value
            foreach (Player player in Players)
            {
                CheckHands(player);
            }
        }

        private void GetPossibleHands()
        {
            List<Hands> enumList = new List<Hands>();
            foreach (Hands hand in (Hands[])Enum.GetValues(typeof(Hands)))
            {
                enumList.Add(hand);
            }
        }



        private void CheckHands(Player player)
        {
            //Make list out of players cards in hand
            List<Card> cards = player.CardsInHand;

            //Sort Cards by Rank
            cards.Sort((x, y) => x.Value.CompareTo(y.Value));

            //Check  Each possible solution until and return hand value accordingly
            if (HasRoyalFlush(cards))
            {
                player.HandValue = 10;
            }
            else if (HasStraightFlush(cards))
            {
                player.HandValue = 9;
            }
            else if (HasFourKind(cards))
            {
                player.HandValue = 8;
            }
            else if (HasFullHouse(cards))
            {
                player.HandValue = 7;
            }
            else if (HasFlush(cards))
            {
                player.HandValue = 6;
            }
            else if (HasStraight(cards))
            {
                player.HandValue = 5;
            }
            else if (HasThreeKind(cards))
            {
                player.HandValue = 4;
            }
            else if (HasTwoPair(cards))
            {
                player.HandValue = 3;
            }
            else if (HasPair(cards))
            {
                player.HandValue = 2;
            }
            else
            {
                player.HandValue = 1;
            }
        }
        #region High
        //in case of everyone having only a high return all players who have the highest card
        public List<Player> GetHighestWinners()
        {
            //creats List of players
            List<Player> winners = new List<Player>();
            //the highest card someones card needs to have
            int high = HighCard();

            //go through all  the players
            foreach (var player in Players)
            {
                //and all of their cards searching for a card of equal value to high
                ////if the card matches the high add that player to the winners list and search the next player
                foreach (var card in player.CardsInHand)
                {
                    if (card.Value == high)
                    {
                        winners.Add(player);
                        break;
                    }
                }
            }
            return winners;
        }

        public int HighCard()
        {
            int high = 2;
            foreach (var player in Players)
            {
                foreach (var card in player.CardsInHand)
                {
                    if (card.Value > high)
                    {
                        high = card.Value;
                    }
                }
            }
            return high;
        }
        #endregion
        #region Pair
        //Check to see if the cards has one pair (ACE Ace) with any suit
        private bool HasPair(List<Card> cardsInHand)
        {
            //start with one card
            foreach (var card in cardsInHand)
            {
                //this is the number of cards that have the same rank
                int combo = 0;
                //compare it to every other card
                foreach (var compared in cardsInHand)
                {
                    //if the card is the same then add it to the combo
                    if (card.Rank == compared.Rank)
                    {
                        combo++;
                    }
                }
                //after one card has completed it s check on other cards return if there have been 2
                if (combo == 2)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        #region Two Pairs
        //do these cards have two sets of pairs (Ace Ace King King) of any suit
        private bool HasTwoPair(List<Card> cardsInHand)
        {
            //number of pairs found
            int pairsFound = 0;

            //start with one card
            foreach (var card in cardsInHand)
            {
                //this is the number of cards that have the same rank
                int combo = 0;
                //compare it to every other card
                foreach (var compared in cardsInHand)
                {
                    //if the card is the same then add it to the combo
                    if (card.Rank == compared.Rank)
                    {
                        combo++;
                    }
                }
                //after one card has completed it s check on other cards return add to pairs found if pair is found
                if (combo == 2)
                {
                    pairsFound++;
                }
            }
            //returns wether or two pais have been found
            if (pairsFound == 2)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Three of a kind
        //are there three cards that share the same rank (Ace Ace Ace) with  any suit
        private bool HasThreeKind(List<Card> cardsInHand)
        {
            //start with one card
            foreach (var card in cardsInHand)
            {
                //this is the number of cards that have the same rank
                int combo = 0;
                //compare it to every other card
                foreach (var compared in cardsInHand)
                {
                    //if the card is the same then add it to the combo
                    if (card.Rank == compared.Rank)
                    {
                        combo++;
                    }
                }
                //after one card has completed it s check on other cards return if there have been 3
                if (combo == 3)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        #region Straight
        //Do these cards create consecutive ranks w/o wrapping (2,3,4,5,6) (not J,Q,K,A,2) of any suit
        private bool HasStraight(List<Card> cardsInHand)
        {
            //sets the lowest card to be the start value
            int value = cardsInHand[0].Value;

            //how many cards are in a row
            int combo = 0;

            //go through each card to see if the value of the last/lowest value 
            ////if the card is the same add to the combo and up the value
            ///if not break out of the loop
            for (int i = 0; i < cardsInHand.Count; i++)
            {
                if (cardsInHand[0].Value == value)
                {
                    combo++;
                    value++;
                }
                else
                {
                    break;
                }
            }

            //once we are out of the loop return if we hit 5 cards that are straight
            if (combo == 5)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Flush
        //Check if all five cards are the same suit
        private bool HasFlush(List<Card> cardsInHand)
        {
            //List of all suits
            List<Suit> suits = GetSuits();

            //iterate through all the suits
            foreach (var suit in suits)
            {
                //start of a combo
                int combo = 0;

                //go through each card in hand to find
                ////if the card is the same then add it to combo
                foreach (var card in cardsInHand)
                {
                    if (card.Suit == suit)
                    {
                        combo++;
                    }
                }
                // one all the cards have been checked for one suit if all the cards are the same return true
                if (combo == 5)
                {
                    return true;
                }
            }
            return false;
        }
        //Gets all the suits
        private List<Suit> GetSuits()
        {
            return new List<Suit>()
            {
                Suit.CLUB,
                Suit.DIAMOND,
                Suit.HEART,
                Suit.SPADE
            };
        }
        #endregion
        #region Full house
        //is there a pair (Ace, Ace) an three of a kind (King,King,King) of any suit
        private bool HasFullHouse(List<Card> cardsInHand)
        {
            if (HasThreeKind(cardsInHand) && HasPair(cardsInHand))
            {
                return true;
            }
            return false;
        }
        #endregion
        #region 4 of a Kind
        //Are there four kind of any card (4,4,4,4)
        private bool HasFourKind(List<Card> cardsInHand)
        {
            //start with one card
            foreach (var card in cardsInHand)
            {
                //this is the number of cards that have the same rank
                int combo = 0;
                //compare it to every other card
                foreach (var compared in cardsInHand)
                {
                    //if the card is the same then add it to the combo
                    if (card.Rank == compared.Rank)
                    {
                        combo++;
                    }
                }
                //after one card has completed it s check on other cards return if there have been 4
                if (combo == 4)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion 
        #region Straight Flush
        //if the player has both straight and flush then the player has a straight flush
        private bool HasStraightFlush(List<Card> cardsInHand)
        {
            if (HasStraight(cardsInHand) && HasFlush(cardsInHand))
            {
                return true;
            }

            return false;
        }

        #endregion 
        #region RoaylFlushCheck
        //returns if hand is considered a royal flush (10,J,Q,K,A) of one suit
        private bool HasRoyalFlush(List<Card> cards)
        {
            //If the card is a ten then the start of a royal has started
            if (cards[0].Rank == Rank.TEN)
            {
                // Get the suit that all cards must be to start the combo
                Suit startSuit = cards[0].Suit;

                //Gets list of ranks in order
                List<Rank> RoyalFlushRanks = GetRoyalFlushRanks();

                //How many cards are considered part of the combo
                int combo = 0;

                //Go through each card in hand
                ////if the card is the same rank and suit then add to combo
                ///if its not then break out 
                for (int i = 0; i < cards.Count; i++)
                {
                    if (cards[i].Rank == RoyalFlushRanks[i] && cards[i].Suit == startSuit)
                    {
                        combo++;
                    }
                    else
                    {
                        break;
                    }
                }

                //Once out of loop if the combo is complete then return true
                if (combo == 5)
                {
                    return true;
                }
            }
            return false;
        }

        //Returns cards in order according to royal flush ruling
        private List<Rank> GetRoyalFlushRanks()
        {
            return new List<Rank>()
            {
                Rank.TEN,
                Rank.JACK,
                Rank.QUEEN,
                Rank.KING,
                Rank.ACE,
            };
        }
        #endregion
    }
}
