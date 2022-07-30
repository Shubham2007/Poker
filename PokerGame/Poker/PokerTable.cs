using System;
using System.Collections.Generic;

namespace PokerGame.Poker
{
    class PokerTable
    {
        private readonly int _totalPlayers;
        private Deck _deck;
        private Dealer _dealer;
        private List<Player> _players;

        public PokerTable(int totalPlayers)
        {
            ValidateNumberOfPlayers(totalPlayers);
            _totalPlayers = totalPlayers;
            InitializeTable();
        }        

        public void StartGame()
        {
            List<Card> cardsOnTable = new Bet(_players, _dealer).StartBetting();

            // Analyse Results...
        }

        private void InitializeTable()
        {
            // Creates a deck of complete 52 cards
            _deck = new();

            // Creates a dealer with a deck of cards
            _dealer = new(_deck);

            DistributeCardsToPlayers();
        }

        private void DistributeCardsToPlayers()
        {
            _players = GetAllPlayers();

            DistributeCards(_players);          
        }

        private void DistributeCards(List<Player> players)
        {
            foreach(Player player in players)
            {
                player.RecieveCard(_dealer.DealCard());
                player.RecieveCard(_dealer.DealCard());
            }
        }

        private List<Player> GetAllPlayers()
        {
            List<Player> players = new();

            for(int player = 0; player < _totalPlayers; player++)
            {
                players.Add(GetPlayer());
            }

            return players;
        }

        private static Player GetPlayer()
            => new();

        private static void ValidateNumberOfPlayers(int totalPlayers)
        {
            if (totalPlayers < 2 || totalPlayers > 5)
                throw new ArgumentException("Table must allow only 2-5 players");
        }
    }
}
