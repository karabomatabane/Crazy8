using Crazy8.Contracts;
using Crazy8.Models;

namespace Crazy8;

class Program
{
    static async Task Main(string[] args)
    {
        Player[] players = new[]
        {
            new Player("Tshiamo"), new Player("Karabo")
        };
        Dictionary<string, IEffect?> specialCards = new()
        {
            { "7", new JumpEffect() }, { "8", new CallEffect() }, { "Jack", new ReverseEffect() },
            { "2", new AttackEffect() { Magnitude = 1 } }, { "Joker", new AttackEffect() { Magnitude = 2 } }
        };

        Game game = new(players, specialCards);
        game.StartGame();
        while (game.Round <= game.TotalRounds)
        {
            // ask player what card they want to play
            Player currentPlayer = players[game.Turn];
            Card? faceUp = game.GetFaceUp();
            if (faceUp == null) continue;
            string sFaceUp = string.IsNullOrEmpty(game.RequiredSuit)
                ? $"{faceUp.Rank} of {faceUp.Suit}"
                : game.RequiredSuit;
            Console.WriteLine($"Face up card is: {sFaceUp}");
            await game.ProgressGame(currentPlayer.PlayCard());
        }
    }
}