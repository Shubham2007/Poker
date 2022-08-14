namespace PokerGame.Poker.Interfaces
{
    interface IDeck
    {
        void ShuffleCards();
        Card GetCard();
    }
}
