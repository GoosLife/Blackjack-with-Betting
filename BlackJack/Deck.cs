using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class Deck
    {
        //public Card[] Cards { get; set; }
        public List<Card> Cards { get; set; }

        public Deck()
        {
            Cards = new List<Card>();

            foreach (Suit suit in (Suit[]) Enum.GetValues(typeof(Suit)))
            {
                for (int i = 1; i <= 13; i++)
                {
                    Card c = new Card(suit, i);
                    Cards.Add(c);
                }
            }
        }

        public void ResetDeck()
        {
            Cards.Clear();

            foreach (Suit suit in (Suit[])Enum.GetValues(typeof(Suit)))
            {
                for (int i = 1; i <= 13; i++)
                {
                    Card c = new Card(suit, i);
                    Cards.Add(c);
                }
            }
        }

        public Card DrawCard()
        {
            // Generate random card to draw
            Random r = new Random();
            int drawnCard = r.Next(0, Cards.Count);

            // Get card to draw
            Card c = Cards[drawnCard];

            // Remove it from deck
            Cards.RemoveAt(drawnCard);

            // Return drawn card
            return c;
        }

        public override string ToString()
        {
            string output = "";

            foreach (Card c in Cards)
            {
                output += c.ToString();
                output += '\n';
            }

            output += '\n';

            return output;
        }
    }
}
