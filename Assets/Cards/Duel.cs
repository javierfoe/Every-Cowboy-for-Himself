using System.Collections;

public class Duel : Card
{
    public Duel(Rank rank, Suit suit) : base(CardType.Duel, rank, suit) { }

    public override IEnumerator CardEffect(Player player, int target, Selection selection, int cardIndex)
    {
        yield return base.CardEffect(player, target, selection, cardIndex);
        yield return EveryCowboyForHimself.Duel(player, target);
    }
}