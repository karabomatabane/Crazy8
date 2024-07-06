using Crazy8.Models;

namespace Crazy8.Contracts;

public enum EffectFrequency
{
    SingleTurn,
    WholeRound,
    Persistent
}

public enum EffectType
{
    Attack,
    Transformation
}
public interface IEffect
{
    EffectFrequency Frequency { get; }
    EffectType Type { get; }
    void Execute(Game game);
}