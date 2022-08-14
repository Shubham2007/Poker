namespace PokerGame.Poker.Interfaces
{
    public interface IDeck
    {
        void ShuffleCards();
        Card GetCard();
    }
}
