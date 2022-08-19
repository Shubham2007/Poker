namespace PokerGame.Contracts
{
    public interface ICloningStrategy<TObject>
    {
        TObject DeepClone(TObject originalObject);
    }
}
