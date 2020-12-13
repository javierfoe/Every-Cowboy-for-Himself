using System.Collections;

public class Gatling : Card
{
    public Gatling(Rank rank, Suit suit) : base(CardType.Gatling, rank, suit) { }

    public override IEnumerator CardEffect(Player player, int target, Selection selection, int cardIndex)
    {
        yield return base.CardEffect(player, target, selection, cardIndex);
        yield return EveryCowboyForHimself.Gatling(player.Index, this);
    }
}