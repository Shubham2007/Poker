using System;
using System.Collections.Generic;
using System.Linq;

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

        public delegate void Winners(IReadOnlyList<PlayerWinnigPriority> winners);

        public event Winners GetWinners; 

        public void StartGame()
        {
            List<Card> finalCardsOnTable = new Bet(_players, _dealer).StartBetting();

            // Analyse Results...
            BetResult betResult = new PokerHandEvaluator(finalCardsOnTable, _players).Evaluate();

            IReadOnlyList<PlayerWinnigPriority> winners = DecideWinner(betResult);
            RaiseWinnersEvent(winners); // Notify all subscribers about winners on current table
        }

        private void RaiseWinnersEvent(IReadOnlyList<PlayerWinnigPriority> winners)
        {
            GetWinners?.Invoke(winners);
        }

        private static List<PlayerWinnigPriority> DecideWinner(BetResult betResult)
        {
            List<PlayerWinnigPriority> playerWinnigs = betResult.PlayerWinnigPriorities
                .GroupBy(x => x.WinningPriority)
                .Select(x => x.ToList())
                .OrderByDescending(x => x.Select(x => x.WinningPriority))
                .First()
                .GroupBy(x => x.TotalValueOfCards)
                .Select(x => x.ToList())
                .OrderByDescending(x => x.Select(x => x.TotalValueOfCards))
                .First();

            return playerWinnigs;
        }

        private void InitializeTable()
        {
            // Creates a deck of complete 52 cards
            _deck = new();

            // Creates a dealer with a deck of cards
            _dealer = new(_deck);
            _dealer.ShuffleCards();

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

            // Generate player from id 0
            for(int playerId = 0; playerId < _totalPlayers; playerId++)
            {
                players.Add(GetPlayer(playerId));
            }

            return players;
        }

        private static Player GetPlayer(int playerId)
            => new(playerId);

        private static void ValidateNumberOfPlayers(int totalPlayers)
        {
            if (totalPlayers < 2 || totalPlayers > 5)
                throw new ArgumentException("Table must allow only 2-5 players");
        }
    }
}
