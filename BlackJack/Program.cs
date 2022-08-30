using BlackJack;

GameManager gm = new GameManager();

GameStart:

DrawGetBets();

gm.DealCards();

while (gm.IsRoundOver == false)
{
    if (gm.HasLivePlayers())
    {
        DrawPlayerTurn();
    }

    else if (gm.AreAllPlayersBust() == false)
    {
        DrawDealerTurn();
    }
}

// Final screen
gm.CompareHands();

DrawTable();
DrawWinner();

if (DrawPlayAgain())
{
    Console.Clear();
    goto GameStart;
}

/// <summary>
/// Methods
/// </summary>

void DrawTable()
{
    ShowDealerValues();

    Console.WriteLine();
    Console.WriteLine("-------------------------");
    Console.WriteLine();

    ShowPlayerValues();
}

void ShowDealerValues()
{

    if (gm.HasLivePlayers())
    {
        Console.WriteLine("Dealer Hand: \n");
        Console.WriteLine(gm.Dealer.ShowOneCard());
        Console.WriteLine(gm.Dealer.ShowOneCardValue());
    }

    else
    {
        Console.WriteLine("Dealer Hand: \n");
        Console.WriteLine(gm.Dealer.ShowCards());
        Console.WriteLine(gm.Dealer.ShowValues());
    }

    if (gm.Dealer.Hand.IsBust())
    {
        Console.WriteLine();
        Console.WriteLine("BUSTED");
    }
}

void ShowPlayerValues()
{
    foreach (Player p in gm.Players)
    {
        Console.WriteLine(p.Name + " Hand: \n");
        Console.WriteLine(p.ShowCards());
        Console.WriteLine(p.ShowValues());
        Console.WriteLine();
        Console.WriteLine(p.ShowCash() + " | " + p.ShowBet());

        if (p.Hand.IsBust())
        {
            Console.WriteLine();
            Console.WriteLine("BUSTED");
        }

        Console.WriteLine();
    }
}

void DrawPlayerTurn()
{
    for (int i = 0; i < gm.Players.Count; i++)
    {
        if (gm.Players[i].IsOut == false)
        {
            Console.Clear();

            DrawTable();

            Console.WriteLine();
            Console.WriteLine(gm.Players[i].Name + " - 1. [H]it or 2. [s]tand?");
            
            if (gm.Players[i].Hand.CanSplit())
            {
                Console.WriteLine("Press 3 or P to Split");
            }

            char action = Console.ReadKey().KeyChar;

            gm.GetPlayerAction(action, gm.Players[i]);

            if (action == 'p' || action == 'P' || action == '3')
            {
                // Make i -1, so that on the next iteration, i will be back to 0, and the player will have to determine what to do with all of their split hands
                i = -1;
            }
        }
    }

    Console.Clear();
}

void DrawDealerTurn()
{
    DrawTable();

    if (gm.Dealer.Hand.FinalValue < gm.Dealer.StopValue)
    {
        Console.WriteLine();
        Console.WriteLine("Dealer draws a card...");
        Thread.Sleep(1000);
        gm.HitDealer();
    }

    else
    {
        gm.IsRoundOver = true;
    }

    Console.Clear();
}

void DrawWinner()
{
    foreach (Player p in gm.Players)
    {
        
        Console.WriteLine();

        switch (p.WinCondition)
        {
            case WinCondition.Undetermined:
                Console.WriteLine("I've forgotten to take this case into account... No winner has been determined :)");
                break;
            case WinCondition.Won:
                Console.WriteLine(p.Name + " won this hand!");
                break;
            case WinCondition.Lost:
                Console.WriteLine(p.Name + " lost this hand...");
                break;
            case WinCondition.BlackJack:
                Console.WriteLine(p.Name + " has black jack!");
                break;
            case WinCondition.Bust:
                Console.WriteLine(p.Name + " is bust...");
                break;
            case WinCondition.Draw:
                Console.WriteLine(p.Name + " has drawn with the dealer. No winner can be determined.");
                break;
            default:
                Console.WriteLine("This is the default case... We shouldn't get here. No winner could be determined, because no WinCondition has been set on the player.");
                break;
        }
    }
}

bool DrawPlayAgain()
{
    Console.WriteLine();

    Console.WriteLine("Play another round?");
    if(gm.PlayAgain(Console.ReadKey().KeyChar))
    {
        return true;
    };

    return false;
}

void DrawGetBets()
{
    foreach (Player player in gm.Players)
    {
        MakeBet:
        Console.WriteLine(player.ShowCash());
        Console.WriteLine(player.Name + " - Enter amount to bet: ");

        try
        {
            int betAmount = int.Parse(Console.ReadLine());

            if (player.CanAffordBet(betAmount))
            {
                player.MakeBet(betAmount);
            }
        }
        catch
        {
            Console.Clear();
            Console.WriteLine("Invalid bet amount");
            goto MakeBet;
        }
    }
}