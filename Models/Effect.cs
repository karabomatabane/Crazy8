using Crazy8.Contracts;

namespace Crazy8.Models;

public class ReverseEffect : IEffect
{
    public void Execute(Game game)
    {
        game.Clockwise = !game.Clockwise;
    }
}