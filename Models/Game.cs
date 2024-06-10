namespace Crazy8.Models;

public class Game
{
    private Player[] Players { get; set; }
    private Deck Deck { get; set; }
    private int Round { get; set; }
    private int TotalRounds { get; }
    private int Turn { get; set; }
    private bool Clockwise { get; set; }
    public Dictionary<string, Action> SpecialCards { get; set; }

    public Game(Player[] players, Dictionary<string, Action> specialCards)
    {
        Players = players;
        SpecialCards = specialCards;
        Deck = new Deck();
        Round = 0;
        TotalRounds = Players.Length - 1;
    }

    public void StartGame()
    {
        DealCards(5);
        Round = 1;
    }

    public void ProgressGame(Card faceUp)
    {
        /*    Manages the turn-based logic of the game, allowing each player to take their turn.
              Applies any special card effects if a special card is played.
             Checks game conditions (e.g., if a player has exhausted all their cards).
             Determines if the round has ended and prepares for the next round if needed.*/
        faceUp.ApplyEffect();
        SetNext();
        Round++;
        if (Round >= TotalRounds)
            EndGame();
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

    private void SetNext()
    {
        int n = Players.Length - 1;
        if (Clockwise)
        {
            Turn = (Turn + 1 + n) % n;
        }
        else
        {
            Turn = (Turn + n - 1) % n;
        }
    }
}