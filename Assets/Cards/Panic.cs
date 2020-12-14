using System.Collections;

public class Panic : CatBalou
{
    public Panic(Rank rank, Suit suit) : base(CardType.Panic, rank, suit) { }

    protected override IEnumerator StealCard(Player player, int target, Selection selection, int cardIndex)
    {
        yield return player.Panic(target, selection, cardIndex);
    }
}