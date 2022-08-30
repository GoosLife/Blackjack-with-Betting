using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    enum WinCondition
    {
        Undetermined,
        Won,
        Lost,
        BlackJack,
        Bust,
        Draw
    }
    internal class Player
    {
        public Hand Hand { get; set; }
        /// <summary>
        /// Whether or not the player is still in the game or not. A player is out of the game when they stand or are bust.
        /// </summary>
        public bool IsOut { get; set; }
        public string Name { get; set; }
        public WinCondition WinCondition { get; set; }
        public Bet Bet { get; set; }
        public int Cash { get; set; } = 100;

        public Player(string name = "Player")
        {
            WinCondition = WinCondition.Undetermined;
            Hand = new Hand();
            IsOut = false;
            Name = name;
            Bet = new Bet(0);
        }

        public void ResetPlayer()
        {
            WinCondition = WinCondition.Undetermined;
            Hand = new Hand();
            IsOut = false;
            Bet.Amount = 0;
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

        public string ShowBet()
        {
            return "Bet: " + Bet.Amount;
        }

        public string ShowCash()
        {
            return "Cash: " + Cash;
        }

        public bool CanAffordBet(int amount)
        {
            return amount <= Cash;
        }

        public void MakeBet(int amount)
        {
            Cash -= amount;
            Bet.Amount = amount;
        }
    }
}
