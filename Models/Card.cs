using Crazy8.Contracts;

namespace Crazy8.Models;

public class Card
{
    public string Suit { get; set; }
    public string Rank { get; set; }
    public IEffect? Effect { get; set; }

    public void Flip()
    {
        //TODO: flips the card face up
    }

    public void ApplyEffect(Game game)
    {
        Effect?.Execute(game);
    }
}