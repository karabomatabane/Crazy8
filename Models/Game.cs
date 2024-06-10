using Crazy8.Contracts;

namespace Crazy8.Models;

public class Game
{
    private Player[] Players { get; set; }
    private Deck Deck { get; set; }
    public int Round { get; private set; }
    public int TotalRounds { get; }
    public int Turn { get; private set; }
    public int Step { get; set; } = 1; // default step is 1 
    public bool Clockwise { get; set; } = true; // default direction is clockwise
    private List<IEffect?> ActiveEffects { get; set; } // list of active effects
    private Dictionary<string, IEffect?> SpecialCards { get; set; }
    public string? RequiredSuit { get; set; }

    public Game(Player[] players, Dictionary<string, IEffect?> specialCards)
    {
        Players = players;
        SpecialCards = specialCards;
        Deck = new Deck();
        Round = 0;
        TotalRounds = Players.Length - 1;
        ActiveEffects = new List<IEffect?>();
    }

    public void StartGame()
    {
        Deck.Shuffle();
        DealCards(5);
        while (true)
        {
            Deck.TurnCard();
            Card? card = GetFaceUp();
            if (card == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("You shouldn't be here without a card!\nPanic!!!");
            }
            if (SpecialCards.TryGetValue(card.Rank, out IEffect? effect) && effect != null)
            {
                Deck.Reset();
                Deck.Shuffle();
            }
            else
            {
                break;
            }
        }
        Round = 1;
    }

    public void ProgressGame(Card playerChoice)
    {
        /*    Manages the turn-based logic of the game, allowing each player to take their turn.
              Applies any special card effects if a special card is played.
             Checks game conditions (e.g., if a player has exhausted all their cards).
             Determines if the round has ended and prepares for the next round if needed.*/
        Deck.AddCard(playerChoice);
        ApplySpecialCard(playerChoice.Rank);
        SetNext();
        if (!Players[Turn].HasCards()) Round++;
        if (Round >= TotalRounds)
            EndGame();
        Console.Clear();
    }

    public Card? GetFaceUp()
    {
        return Deck.FaceUp.Count > 0 ? Deck.FaceUp[^1] : null;
    }

    private void EndGame()
    {
        // TODO: End the game
    }

    private void DealCards(int count)
    {
        foreach (Player player in Players)
        {
            player.Hand = Deck.DealCards(count);
        }
    }

    private void ApplySpecialCard(string cardRank)
    {
        if (!SpecialCards.TryGetValue(cardRank, out IEffect? effect) || effect == null) return;
        effect.Execute(this);
            
        // Add effect to active effects if not single-turn
        if (effect.Frequency != EffectFrequency.SingleTurn)
        {
            ActiveEffects.Add(effect);
        }
    }

    private void SetNext()
    {
        int n = Players.Length - 1;
        if (Clockwise)
        {
            Turn = (Turn + Step + n) % n;
        }
        else
        {
            Turn = (Turn + n - Step) % n;
        }
        
        // reset to default
        Step = 1;
    }
}