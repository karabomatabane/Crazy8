using Crazy8.Constants;

namespace Crazy8.Models;

public class Deck
{
    private List<Card> FaceDown { get; set; }
    public List<Card> FaceUp { get; set; }
    private static Random random = new Random(); 

    public Deck(int size = 52)
    {
        FaceDown = new List<Card>();
        FaceUp = new List<Card>();
        foreach (string suit in Const.Suits)
        {
            foreach (string rank in Const.Ranks)
            {
                FaceDown.Add(new Card(){Suit = suit, Rank = rank});
            }
        }
    }

    /// <summary>
    /// Shuffle the face-down cards
    /// </summary>
    public void Shuffle()
    {
        // Fisher–Yates shuffle
        int n = FaceDown.Count;
        while (n > 1) {  
            n--;  
            int k = random.Next(n + 1);  
            // val at pos n becomes value at pos n and vice-versa
            (FaceDown[k], FaceDown[n]) = (FaceDown[n], FaceDown[k]);
        }
    }

    public void TurnCard()
    {
        if (FaceDown.Count > 0)
        {
            FaceUp.Add(FaceDown[0]);
            FaceDown.RemoveAt(0);
        }
        else
        { 
            // take all the face up cards except the last one
            FaceDown = FaceUp.Take(FaceUp.Count - 1).ToList();
            // shuffle the deck
            Shuffle();
            TurnCard();
        }
    }

    public void AddCard(Card card)
    {
        if (FaceDown.Contains(card) || FaceUp.Contains(card))
        {
            throw new Exception("You cannot have a card that already exists in the deck!\nPanic!!!");
        }
        FaceUp.Add(card);
    }

    /// <summary>
    /// Deal cards from deck
    /// </summary>
    /// <param name="count">Number of cards to deal</param>
    /// <returns>Array of cards <see cref="Card"/></returns>
    public Card[] DealCards(int count)
    {
        Card[] cards = FaceDown.Take(count).ToArray();
        FaceDown.RemoveRange(0, count);
        return cards;
    }
    
    /// <summary>
    /// Deal specific cards
    /// </summary>
    /// <param name="ranks">Array of ranks required</param>
    /// <returns></returns>
    public Card[] DealCards(string[] ranks)
    {
        return ranks.Select(rank => GetCard(rank)!).ToArray();
    }

    private Card? GetCard(string rank)
    {
        Card? card = FaceDown.FirstOrDefault(card => card.Rank == rank);
        int position = card != null ? FaceDown.IndexOf(card) : -1;
        if (position != -1)
        {
            FaceDown.RemoveAt(position);
        }
        return card;
    }

    /// <summary>
    /// Turn all the face-up cards face down 
    /// </summary>
    public void Reset()
    {
        foreach (Card card in FaceUp.ToList())
        {
            FaceDown.Add(card);
            FaceUp.Remove(card);
        }
    }
}