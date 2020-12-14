using System.Collections;

public class Indians : Card
{
    public Indians(Rank rank, Suit suit) : base(CardType.Indians, rank, suit) { }

    public override IEnumerator CardEffect(Player player, int target, Selection selection, int cardIndex)
    {
        yield return base.CardEffect(player, target, selection, cardIndex);
        yield return EveryCowboyForHimself.Indians(player.Index, this);
    }
}