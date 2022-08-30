using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class Dealer
    {
        public Hand Hand { get; set; }

        /// <summary>
        /// The value at or above which the dealer must stop.
        /// </summary>
        public int StopValue { get; set; }
        public WinCondition WinCondition { get; set; }

        public Dealer(int stopValue = 16)
        {
            StopValue = stopValue;

            Hand = new Hand();
        }

        public void ResetDealer()
        {
            Hand = new Hand();
        }

        public string ShowOneCard()
        {
            return Hand.Cards[0].ToString() + " ??"; 
        }

        public string ShowOneCardValue()
        {
            string result = "";

            if (Hand.Cards[0].PointValue == 1)
            {
                result = "1/11";
            }
            else
            {
                result = "Value: " + Hand.Cards[0].PointValue.ToString();
            }

            return result;
        }

        public string ShowCards()
        {
            string result = "";

            foreach (Card c in Hand.Cards)
            {
                result += c.ToString() + " ";
            }

            return result;
        }

        public string ShowValues()
        {
            string result = "Value: ";

            for (int i = 0; i < Hand.PossibleValues.Count; i++)
            {
                if (i == 0)
                    result += Hand.PossibleValues[i].ToString();
                else
                    result += '/' + Hand.PossibleValues[i].ToString();
            }

            return result;
        }
    }
}
