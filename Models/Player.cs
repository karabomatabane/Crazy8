using System.Diagnostics;

namespace Crazy8.Models;

public class Player
{
    public Player(string name)
    {
        PlayerId = "GUID";
        Name = name;
    }

    public string PlayerId { get; set; }
    public string Name { get; set; }
    public Card[]? Hand { get; set; }

    public void PlayCard(Card choice)
    {
        // TODO: play card
    }

    public void PickCard()
    {
        // TODO: pick card from deck
    }

    public bool HasCards()
    {
        Debug.Assert(Hand != null, nameof(Hand) + " != null");
        return Hand.Length > 0;
    }
}