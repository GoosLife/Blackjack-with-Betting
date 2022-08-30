using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class Bet
    {
        /// <summary>
        /// How much a player has bet on a hand.
        /// </summary>
        public int Amount { get; set; }

        public Bet(int amount)
        {
            Amount = amount;
        }
    }
}
