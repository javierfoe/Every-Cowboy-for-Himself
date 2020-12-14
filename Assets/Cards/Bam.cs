
using System.Collections;

public class Bam : Card
{
    public Bam(Rank rank, Suit suit) : base(CardType.Bam, rank, suit) { }
    protected Bam() : base(CardType.Bam) { }

    public override IEnumerator CardEffect(Player player, int target, Selection selection, int cardIndex)
    {
        yield return base.CardEffect(player, target, selection, cardIndex);
        yield return Shoot(player, target);
    }

    protected virtual IEnumerator Shoot(Player player, int target)
    {
        yield return player.Shoot(target);
        player.Shot();
    }
}