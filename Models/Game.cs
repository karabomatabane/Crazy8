namespace Crazy8.Models;

public class Game
{
    public Player[] Players { get; private set; }
    public Deck Deck { get; set; }
    private int Round { get; set; }
    private int TotalRounds { get; set; }
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

    public void ProgressGame()
    {
        /*    Manages the turn-based logic of the game, allowing each player to take their turn.
              Applies any special card effects if a special card is played.
             Checks game conditions (e.g., if a player has exhausted all their cards).
             Determines if the round has ended and prepares for the next round if needed.*/
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
            player.Hand = new Card[count];
        }
    }
}