using System.Collections;

public class CatBalou : Card
{
    public CatBalou(Rank rank, Suit suit) : base(CardType.CatBalou, rank, suit) { }
    protected CatBalou(CardType cardType, Rank rank, Suit suit) : base(cardType, rank, suit) { }

    public override IEnumerator CardEffect(Player player, int target, Selection selection, int cardIndex)
    {
        yield return StealCard(player, target, selection, cardIndex);
        player.DiscardCardHand(this);
    }

    protected virtual IEnumerator StealCard(Player player, int target, Selection selection, int cardIndex)
    {
        yield return player.CatBalou(target, selection, cardIndex);
    }
}