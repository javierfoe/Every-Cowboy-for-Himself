using System.Collections;

public class Missed : Card
{
    public Missed(Rank rank, Suit suit) : base(CardType.Missed, rank, suit) { }

    public override IEnumerator CardEffect(Player player, int target, Selection selection, int cardIndex) { yield return null; }
}