
using System.Collections;

public class Saloon : Card
{
    public Saloon(Rank rank, Suit suit): base(CardType.Saloon, rank, suit) { }

    public override IEnumerator CardEffect(Player player, int target, Selection selection, int cardIndex)
    {
        yield return base.CardEffect(player, target, selection, cardIndex);
        EveryCowboyForHimself.Saloon();
    }
}