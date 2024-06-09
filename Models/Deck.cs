namespace Crazy8.Models;

public class Deck
{
    private List<Card> FaceDown { get; set; }
    public List<Card> FaceUp { get; set; }
    
    string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
    string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", 
        "Jack", "Queen", "King", "Ace" };

    public Deck(int size = 52)
    {
        FaceDown = new List<Card>();
        FaceUp = new List<Card>();
        foreach (string suit in suits)
        {
            Card card = new() { Suit = suit };
            foreach (string rank in ranks)
            {
                card.Rank = rank;
                FaceDown.Add(card);
            }
        }
    }

    public void Shuffle()
    {
        // TODO: join the two piles and shuffle them
    }

    public Card[] DealCards(int count)
    {
        return FaceDown.Take(count).ToArray();
    }
}