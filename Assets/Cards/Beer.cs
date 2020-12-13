using System.Collections;

public class Beer : Card
{
    public Beer(Rank rank, Suit suit) : base(CardType.Beer, rank, suit) { }

    public override IEnumerator CardEffect(Player player, int target, Selection drop, int cardIndex)
    {
        yield return base.CardEffect(player, target, drop, cardIndex);
        player.HealFromBeer();
    }
}