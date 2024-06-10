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
        while (game.Round < game.TotalRounds)
        {
            // ask player what card they want to play
            Player currentPlayer = players[game.Turn];
            Card? faceUp = game.GetFaceUp();
            if (currentPlayer.Hand == null || faceUp == null) continue;
            string sFaceUp = game.RequiredSuit ?? $"{faceUp.Rank} of {faceUp.Suit}";
            Console.WriteLine($"Face up card is: {sFaceUp}");
            Console.WriteLine($"{currentPlayer.Name}'s turn\n====================\nPick card from:\n" +
                              ListCards(currentPlayer.Hand) + "\n====================");
            Console.Write("Your choice: ");
            if(int.TryParse(Console.ReadLine(), out int choice))
            {
                game.ProgressGame(currentPlayer.Hand[choice]);
            }
        }
    }

    private static string ListCards(Card[] cards)
    {
        string output = "";
        for (int i = 0; i < cards.Length; i++)
        {
            output += $"{i}. {cards[i].Rank} of {cards[i].Suit}\n";
        }

        return output;
    }
}