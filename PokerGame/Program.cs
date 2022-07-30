using PokerGame.Poker;
using System;

namespace PokerGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to poker club: ");
            Console.WriteLine("Please enter the number of players");
            int numberOfPlayers = Convert.ToInt32(Console.ReadLine());

            PokerTable table = new(numberOfPlayers);
            table.StartGame();

            Console.ReadKey();
        }
    }
}
