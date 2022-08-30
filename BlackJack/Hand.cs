using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class Hand
    {
        public List<Card> Cards { get; set; }
        public List<int> PossibleValues { get; set; }
        public int FinalValue { get; set; }

        public Hand()
        {
            Cards = new List<Card>();
            PossibleValues = new List<int>();
        }

        public void AddCard(Card c)
        {
            Cards.Add(c);

            CalculateValues();

            GetFinalValue();
        }

        public bool IsBust()
        {
            return PossibleValues[0] > 21;
        }

        public void CalculateValues()
        {
            PossibleValues.Clear();

            int valueAcesHigh = 0;
            int valueAcesLow = 0;

            foreach(Card c in Cards)
            {
                if (c.Value == 1)
                {
                    valueAcesHigh += 11;
                    valueAcesLow += 1;
                }
                else
                {
                    valueAcesHigh += c.PointValue;
                    valueAcesLow += c.PointValue;
                }
            }

            if (valueAcesHigh == 21 || valueAcesLow == 21)
            {
                // BLACKJACK!
                // If this is a player hand, player automatically wins.
                // IF this is a dealer hand, player will keep playing.
                PossibleValues.Add(21);
                return;
            }
            
            // If these values are the same, no aces are present and we only have to add one of the values.
            if (valueAcesHigh == valueAcesLow)
            {
                PossibleValues.Add(valueAcesHigh);
                return;
            }

            // If the aces at 11 would cause the hand to go bust, there is no reason to include the option.
            else if (valueAcesHigh > 21)
            {
                PossibleValues.Add(valueAcesLow);
                return;
            }

            // If the values are different, add both possible values to the list of possible values.
            else
            {
                PossibleValues.Add(valueAcesLow);
                PossibleValues.Add(valueAcesHigh);
            }
        }

        public void GetFinalValue()
        {
            if (PossibleValues.Count > 1)
            {
                FinalValue = (PossibleValues[0] > PossibleValues[1] ? PossibleValues[0] : PossibleValues[1]);
            }
            else
                FinalValue = PossibleValues[0];
        }

        public bool CanSplit()
        {
            return Cards[0].Name == Cards[1].Name && Cards.Count == 2;
        }
    }
}
