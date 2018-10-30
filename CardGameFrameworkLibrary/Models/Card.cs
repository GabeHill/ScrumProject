using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CardGameFrameworkLibrary.Models
{
    public enum Suit
    {
        SPADE,
        CLUB,
        HEART,
        DIAMOND
    }

    public enum Rank
    {
        TWO,
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
        EIGHT,
        NINE,
        TEN,
        JACK,
        QUEEN,
        KING,
        ACE
    }

    public class Card
    {
        public Suit Suit { get; set; }
        public int Value { get; set; }
        public Rank Rank { get; set; }
        public string ImageSource { get; set; }
        public bool IsKept { get; set; }



        public BitmapImage GetImage()
        {
            if (ImageSource != null)
            {
                var bitmap = new BitmapImage();
                return bitmap;
            }
            throw new Exception("Image source for this card is invalid");
        }

    }
}
