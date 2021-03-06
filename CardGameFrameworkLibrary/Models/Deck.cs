﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameFrameworkLibrary.Models
{
    public class Deck
    {
        public List<Card> Cards { get; set; }


        public Deck(string gameType)
        {
            Cards = GenerateDeck(gameType);
            Shuffle();
        }

        public void Shuffle()
        {
            {
                Random random = new Random();
                int movingIndex;
                int replacedIndex;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < Cards.Count; j++)
                    {
                        movingIndex = random.Next(Cards.Count);
                        replacedIndex = random.Next(Cards.Count);
                        Card tempCard = Cards[replacedIndex];
                        Cards[replacedIndex] = Cards[movingIndex];
                        Cards[movingIndex] = tempCard;
                    }
                }
            }

        }
       

        public Card DrawCard()
        {
            Card card = Cards[Cards.Count - 1];
            Cards.Remove(card);
            return card;
        }

        public List<Card> DealCards(int amount)
        {
            List<Card> DealtCards = new List<Card>();
            for (int i = 0; i < amount; i++)
            {
                DealtCards.Add(DrawCard());
            }
            return DealtCards;
        }

        private List<Card> GenerateDeck(string gameType)
        {
            //next few lines of code setup variables to get images
            string resourcePath = "../../../CardGameFrameworkLibrary/Resource/";
            string path = "pack://application:,,,/CardGameFrameworkLibrary;component/Resource/";
            string[] cardImagePath = Directory.GetFiles(resourcePath);
            List<string> fileNames = new List<string>();
            foreach (string file in cardImagePath)
            {
                fileNames.Add(Path.GetFileName(file));

            }

            List<Card> newDeck = new List<Card>();

            //These two lists hold respective enum values to later easily iterate over in loop
            List<Suit> suitList = new List<Suit>();
            List<Rank> rankList = new List<Rank>();

            //sets each list values
            foreach (Suit suit in (Suit[])Enum.GetValues(typeof(Suit)))
            {
                suitList.Add(suit);
            }
            foreach (Rank rank in (Rank[])Enum.GetValues(typeof(Rank)))
            {
                rankList.Add(rank);
            }

            //1st loop
            //iterate 4 times (4(suits)*13(ranks) == 52(cards))
            //2nd loop
            //creates an instance of card 13 times
            int color = 0;
            for (int i = 0; i < 4; i++)
            {
                //
                for (int j = 0; j < 13; j++)
                {
                    Card card = null;
                    string cardName = "";
                    switch (gameType)
                    {
                        case "Poker":
                            card = new Card()
                            {
                                Suit = suitList[color],
                                Rank = rankList[j],
                                Value = (j + 2),
                                IsSelected = false

                            };
                            break;
                        case "Blackjack":
                            card = new Card()
                            {
                                Suit = suitList[color],
                                Rank = rankList[j],
                                Value = (j + 2),
                                IsSelected = false
                            };
                            if(card.Value > 10)
                            {
                                card.Value = 10;
                            }
                            if(card.Rank == Rank.ACE)
                            {
                                card.Value = 11;
                            }
                            break;
                        default:
                            card = new Card();
                            break;

                    }
                    //setting this temp variable to the suitX to match filenames in resource folder
                    cardName = $"{card.Suit}{card.Rank}";
                    foreach (var fileName in fileNames)
                    {
                        if (fileName.Contains(cardName))
                        {
                            card.ImageSource = path + fileName;
                            
                            break;
                        }
                    }
                    newDeck.Add(card);
                }
                color++;
            }
            return newDeck;
        }
    }
}
