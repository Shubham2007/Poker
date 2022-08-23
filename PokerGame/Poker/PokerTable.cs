using PokerGame.Extensions;
using PokerGame.Poker.Interfaces;
using System;
using System.Collections.Generic;

namespace PokerGame.Poker
{
    class PokerTable : IPokerTable
    {
        private int _totalPlayers;
        private IDealer _dealer;
        private List<Player> _players;
        private readonly IPokerHandEvaluator _pokerHandEvaluator;
        private readonly IBet _bet;

        public PokerTable(IPokerHandEvaluator pokerHandEvaluator, IBet bet, IDealer dealer)
        {           
            _dealer = dealer;            
            _pokerHandEvaluator = pokerHandEvaluator;
            _bet = bet;          
        }

        public delegate void Winners(IReadOnlyList<PlayerWinnigPriority> winners);

        public event Winners GetWinners; 

        public void StartGame(int totalPlayers)
        {
            ValidateNumberOfPlayers(totalPlayers);
            _totalPlayers = totalPlayers;
            InitializeTable();

            List<Card> finalCardsOnTable = _bet.StartBetting(_players, _dealer);

            // Analyse Results...
            BetResult betResult = _pokerHandEvaluator.Evaluate(finalCardsOnTable, _players);

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
                .FirstMaxGroupedItem(x => x.WinningPriority)
                .FirstMaxGroupedItem(x => x.TotalValueOfCards);

            return playerWinnigs;
        }

        private void InitializeTable()
        {
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
                _ = player.RecieveCard(_dealer.DealCard()).RecieveCard(_dealer.DealCard());
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
