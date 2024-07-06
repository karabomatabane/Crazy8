﻿using Crazy8.Constants;
using Crazy8.Contracts;

namespace Crazy8.Models;

public class ReverseEffect : IEffect
{
    public EffectFrequency Frequency { get; } = EffectFrequency.Persistent;
    public EffectType Type { get; } = EffectType.Transformation;
    public void Execute(Game game)
    {
        game.Clockwise = !game.Clockwise;
    }
}

public class JumpEffect : IEffect
{
    public EffectFrequency Frequency { get; } = EffectFrequency.SingleTurn;
    public EffectType Type { get; } = EffectType.Transformation;
    public void Execute(Game game)
    {
        game.Step = 2;
    }
}

public class AttackEffect : IEffect
{
    public EffectFrequency Frequency { get; } = EffectFrequency.SingleTurn;
    public EffectType Type { get; } = EffectType.Transformation;
    public int Magnitude { get; init; } = 1;
    public void Execute(Game game)
    {
        game.Attacks += Magnitude;
    }
}

public class CallEffect : IEffect
{
    public EffectType Type { get; } = EffectType.Transformation;
    public EffectFrequency Frequency { get; } = EffectFrequency.SingleTurn;
    public void Execute(Game game)
    {
        game.RequiredSuit = PromptPlayerForSuit(game.GetFaceUp()!.Suit);
    }

    private string PromptPlayerForSuit(string defaultSuit)
    {
        Console.WriteLine($"Pick a suit:\n0. Hearts\n1. Diamonds\n2. Clubs\n3. Spades\nPress 'Enter' " +
                          $"to select default: {defaultSuit}");
        Console.Write("Choice (0-3): ");
        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            return Const.Suits[choice];
        }

        return defaultSuit;
    }
}