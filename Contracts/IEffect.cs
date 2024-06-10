using Crazy8.Models;

namespace Crazy8.Contracts;

public interface IEffect
{
    void Execute(Game game);
}