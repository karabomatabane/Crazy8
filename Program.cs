﻿using Crazy8.Contracts;
using Crazy8.Models;

namespace Crazy8;

class Program
{
    static void Main(string[] args)
    {
        Player[] players = new[]
        {
            new Player("Tshiamo"), new Player("Karabo"), new Player("Oratile"), 
            new Player("Fentse"),
            new Player("Thabi")
        };
        Dictionary<string, IEffect?> specialCards = new()
        {
            { "7", new JumpEffect() }, { "8", new CallEffect() }, { "Jack", new ReverseEffect() }
        };

        Game game = new(players, specialCards);
        game.StartGame();
        while (game.Round <= game.TotalRounds)
        {
            // ask player what card they want to play
            Player currentPlayer = players[game.Turn];
            Card? faceUp = game.GetFaceUp();
            if (faceUp == null) continue;
            string sFaceUp = string.IsNullOrEmpty(game.RequiredSuit) ? $"{faceUp.Rank} of {faceUp.Suit}" : game.RequiredSuit;
            Console.WriteLine($"Face up card is: {sFaceUp}");
            game.ProgressGame(currentPlayer.PlayCard());
        }
    }
}