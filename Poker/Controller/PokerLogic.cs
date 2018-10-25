using CardGameFrameworkLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CSC160_ConsoleMenu;

namespace Poker.Controller
{
    public class PokerLogic
    {
        //public List<List<Card>> WinningHands = GenerateWinningHands();

        //Deck to be used (or passed around in code) in game
        public static Deck deck = new Deck();
        public static List<Player> players = new List<Player>();
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
            deck.Cards = GenerateDeck();
            foreach (var card in deck.Cards)
            {
                if (card.ImageSource==null||card.ImageSource=="")
                {
                    Console.WriteLine($"{card.Suit}{card.Value}'s image source is null or empty");
                }
                else
                {
                Console.WriteLine(card.ImageSource);
                }
            }
            ShuffleDeck(deck.Cards);
            players = GeneratePlayers();

            //GenerateWinningHands();
        }

        private List<Card> ShuffleDeck(List<Card> deck)
        {
            Random random = new Random();
            int movingIndex;
            int replacedIndex;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < deck.Count; j++)
                {
                    movingIndex = random.Next(deck.Count);
                    replacedIndex = random.Next(deck.Count);
                    Card tempCard = deck[replacedIndex];
                    deck[replacedIndex] = deck[movingIndex];
                    deck[movingIndex] = tempCard;
                }

            }
            Console.WriteLine();
            return deck;
        }

        private List<Player> GeneratePlayers()
        {
            List<Player> results = new List<Player>();
            int playerNum = CIO.PromptForInt("How many players are there?",2,5);
            for (int i = 0; i < playerNum; i++)
            {
                //Console.WriteLine(i);
                Player player = new Player()
                {
                    Bank = 500,
                    CardsInHand = null,
                    HandValue = 0,
                    HasBust = false,
                    HasFolded = false,
                    Name = CIO.PromptForInput("What is your name?",true),
                };
                if (player.Name == null || player.Name == "")
                {
                    player.Name = $"Player{i+1}";
                }
            }
            return results;
        }

        //private static List<List<Card>> GenerateWinningHands()
        //{
        //    //throw new NotImplementedException();
        //}

        private static List<Card> GenerateDeck()
        {
            //next few lines of code setup variables to get images
            string resourcePath = "../../../CardGameFrameworkLibrary/Resource/";
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
                    string cardName = "";
                    Card card = new Card()
                    {
                        Suit = suitList[color],
                        Rank = rankList[j],
                        Value = (j + 2)

                    };

                    //setting this temp variable to the suitX to match filenames in resource folder
                    cardName = $"{suitList[color].ToString()}{(card.Value)}";
                    foreach (var fileName in fileNames)
                    {
                        if ((fileName).ToLower().Contains((cardName).ToLower()))
                        {
                            card.ImageSource = resourcePath + fileName;
                            //Console.WriteLine(fileName);
                            break;
                        }
                    }
                    //card.ImageSource =
                    newDeck.Add(card);
                }
                color++;
            }
            return newDeck;
        }
    }
}
