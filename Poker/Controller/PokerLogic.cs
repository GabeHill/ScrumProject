﻿using CardGameFrameworkLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Controller
{
    public class PokerLogic
    {
        //public List<List<Card>> WinningHands = GenerateWinningHands();
        //public List<Card> Deck = 
        public void Setup()
        {
            //standard deck of 52
            //(from high to low) Ace, King, Queen, Jack, 10, 9, 8, 7, 6, 5, 4, 3, 2, Ace
            //ace can be high or low but usually high
            //no suit is higher than another
            //poker hands are 5 cards

            //Hands are ranked:
            //five of a kind - only possible with wild cards.(No wild cards here)
            //royal flush - (A-K-Q-J-T) with ace high
            //straight flush - (5-6-7-8-9) same suit
            //four of a kind - four cards of the same rank(5-5-5-5)
            //full house - three of a kind and a pair, such as K-K-K-5-5
            //flush - same suit(J-8-5-3-2)
            //straight - 5 cards in order
            //three of a kind - 3 cards of any rank matched with two cards of different ranks(Q-Q-Q-4-2)
            //two pair - 4 cards, two 2 pair that are matched with a 5th card (J-J-3-3-9)
            //pair - 2 matching cards with 3 additonal non-matching cards (J-J-K-4-8)
            //high card - Highest value card in your hand
            GenerateDeck();
            //GenerateWinningHands();
        }

        //private static List<List<Card>> GenerateWinningHands()
        //{
        //    //throw new NotImplementedException();
        //}

        private static void GenerateDeck()
        {
            for (int i = 0; i < 51; i++)
            {
                for (int j = 0; j < 12; j++)
                {

                    Card card = new Card()
                    {
                        Suit = Suit.CLUB,
                        
                        
                    };

                }
            }
        }
    }
}