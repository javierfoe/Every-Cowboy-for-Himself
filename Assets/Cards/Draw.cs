using System.Collections;

public abstract class Draw : Card
{
    private int numberToDraw;

    protected Draw(CardType cardType, Rank rank, Suit suit, int numberToDraw) : base(cardType, rank, suit)
    {
        this.numberToDraw = numberToDraw;
    }

    public override IEnumerator CardEffect(Player player, int target, Selection selection, int cardIndex)
    {
        yield return base.CardEffect(player, target, selection, cardIndex);
        yield return player.DrawFromCard(numberToDraw);
    }
}

public class Stagecoach : Draw
{
    public Stagecoach(Rank rank, Suit suit) : base(CardType.Stagecoach, rank, suit, 2) { }
}

public class WellsFargo : Draw
{
    public WellsFargo(Rank rank, Suit suit) : base(CardType.Stagecoach, rank, suit, 3) { }
}