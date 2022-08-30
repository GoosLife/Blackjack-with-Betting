using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    internal class Card
    {
        public Suit Suit { get; private set; }
        public int Value { get; private set; }
        public int PointValue { get; private set; }
        public string Name { get; private set; }

        public Card(Suit suit, int value)
        {
            Suit = suit;
            Value = value;
            PointValue = (value < 10) ? value : 10;

            if (Value == 1 || Value == 11 ||
                Value == 12 || Value == 13)
            {
                Name = GetName(Value);
            }
            else
                Name = Value.ToString();
        }
        private string GetName(int value)
        {
            switch (value)
            {
                case 1:
                    return "A";
                case 11:
                    return "J";
                case 12:
                    return "Q";
                case 13:
                    return "K";
                default:
                    throw new ArgumentOutOfRangeException("Attempted to get name for card that held a value that doesn't denote an ace or a face card.");
            }
        }

        public override string ToString()
        {
            string suitChar;

            switch (Suit)
            {
                case Suit.Hearts:
                    suitChar = "♥";
                    break;
                case Suit.Diamonds:
                    suitChar = "♦";
                    break;
                case Suit.Clubs:
                    suitChar = "♣";
                    break;
                case Suit.Spades:
                    suitChar = "♠";
                    break;
                default:
                    throw new ArgumentException("Attempted to write to console a card that doesn't have a suit.");
            }

            return Name + suitChar;
        }

        public static int operator +(Card a, Card b) => a.PointValue + b.PointValue;
    }
}
