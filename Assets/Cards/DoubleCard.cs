public class DoubleCard : Card
{
    private Card first, second;

    public DoubleCard(Card one, Card two) : base()
    {
        first = one;
        second = two;
    }

    public override bool IsSuit(Suit suit)
    {
        return first.IsSuit(suit) && second.IsSuit(suit);
    }
}