using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class GameManager
    {
        public List<Player> Players { get; private set; }
        public Dealer Dealer { get; private set; }
        public Deck Deck { get; private set; }
        public bool IsRoundOver { get; set; }

        public GameManager()
        {
            // Create one player (default)
            Player p = new Player();
            Players = new List<Player>();
            Players.Add(p);

            Dealer = new Dealer();

            Deck = new Deck();

            IsRoundOver = false;
        }

        public void DealCards()
        {
            Dealer.Hand.AddCard(Deck.DrawCard());
            Dealer.Hand.AddCard(Deck.DrawCard());

            if (Dealer.Hand.FinalValue == 21)
            {
                Dealer.WinCondition = WinCondition.BlackJack;
            }

            foreach (Player p in Players)
            {
                p.Hand.AddCard(Deck.DrawCard());
                p.Hand.AddCard(Deck.DrawCard());

                if (p.Hand.FinalValue == 21)
                {
                    p.IsOut = true;
                    
                    if (Dealer.WinCondition == WinCondition.BlackJack)
                    {
                        p.WinCondition = WinCondition.Draw;
                    }
                    else
                    {
                        p.WinCondition = WinCondition.BlackJack;
                    }
                }
            }
        }

        public void HitPlayer(Player p)
        {
            p.Hand.AddCard(Deck.DrawCard());

            if (p.Hand.IsBust())
            {
                p.IsOut = true;
                p.WinCondition = WinCondition.Bust;
            }
        }

        public void SplitPlayer(Player originalHand)
        {
            originalHand.Name = "Hand 1";
            Player splitHand = new Player("Hand 2");

            Players.Add(splitHand);

            splitHand.Hand.AddCard(originalHand.Hand.Cards[0]);
            splitHand.Hand.AddCard(Deck.DrawCard());

            originalHand.Hand.Cards[0] = Deck.DrawCard();
        }

        public void HitDealer()
        {
            Dealer.Hand.AddCard(Deck.DrawCard());

            if (Dealer.Hand.IsBust())
            {
                Dealer.WinCondition = WinCondition.Bust;
                foreach (Player p in Players)
                {
                    if (p.WinCondition != WinCondition.Bust)
                    {
                        p.WinCondition = WinCondition.Won;
                    }
                }
                return;
            }

            if (Dealer.Hand.PossibleValues[0] > Dealer.StopValue)
            {
                CompareHands();
            }
        }

        public void Stand(Player p)
        {
            p.IsOut = true;
        }

        public void CompareHands()
        {
            Dealer.Hand.GetFinalValue();

            foreach (Player p in Players)
            {
                if (p.WinCondition == WinCondition.Undetermined)
                {
                    p.Hand.GetFinalValue();

                    if (p.Hand.IsBust())
                    {
                        p.WinCondition = WinCondition.Bust;
                        return;
                    }

                    if (p.Hand.FinalValue == Dealer.Hand.FinalValue)
                    {
                        p.WinCondition = WinCondition.Draw;
                        return;
                    }

                    if (p.Hand.FinalValue > Dealer.Hand.FinalValue)
                    {
                        p.WinCondition = WinCondition.Won;
                    }
                    else
                    {
                        p.WinCondition = WinCondition.Lost;
                    }
                }

                PayPlayers();
            }
        }

        public bool GetPlayerAction(char c, Player p)
        {
            switch(c)
            {
                case 'h':
                case 'H':
                case '1':
                    HitPlayer(p);
                    return true;
                case 's':
                case 'S':
                case 't':
                case 'T':
                case '2':
                    Stand(p);
                    return true;
                case 'p':
                case 'P':
                case '3':
                    // If the hand can't split, return false
                    if (!p.Hand.CanSplit())
                    {
                        return false;
                    }
                    // else, split the hand
                    SplitPlayer(p);
                    return true;
                default:
                    return false;
            }
        }

        public bool HasLivePlayers()
        {
            foreach (Player p in Players)
            {
                if (!p.IsOut)
                {
                    return true;
                }
            }

            return false;
        }

        public bool AreAllPlayersBust()
        {
            foreach (Player p in Players)
            {
                if (p.Hand.IsBust() == false)
                {
                    return false;
                }
            }

            IsRoundOver = true;
            return true;
        }

        public bool PlayAgain(char c)
        {
            switch (c)
            {
                case 'y':
                case 'Y':
                case '1':
                    ResetRound();
                    return true;
                default:
                    return false;
            }
        }

        public void ResetRound()
        {
            IsRoundOver = false;
            Dealer.ResetDealer();

            // Remove additional hands (if any)
            if (Players.Count > 1)
                Players.RemoveRange(1, Players.Count - 1);

            Players[0].ResetPlayer();

            Deck.ResetDeck();
        }

        /// <summary>
        /// Handles the transaction between the 
        /// </summary>
        public void PayPlayers()
        {
            foreach (Player p in Players)
            {
                switch (p.WinCondition)
                {
                    case WinCondition.Won:
                        p.Cash += p.Bet.Amount * 2;
                        break;
                    case WinCondition.BlackJack:
                        p.Cash += (int)Math.Floor(p.Bet.Amount * 1.5);
                        break;
                    case WinCondition.Draw:
                        p.Cash += p.Bet.Amount;
                        break;
                }
            }
        }
    }
}
