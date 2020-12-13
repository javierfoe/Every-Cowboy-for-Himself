using System.Collections;

public class ConvertedCard : Card
{
    private Card original, converted;

    public override Card Original => original;

    public ConvertedCard(Card original, Card converted) : base(original.CardType, original.Rank, original.Suit)
    {
        this.original = original;
        this.converted = converted;
    }

    public override void BeginCardDrag(Player player)
    {
        converted.BeginCardDrag(player);
    }

    public override IEnumerator CardEffect(Player player, int target, Selection drop, int cardIndex)
    {
        yield return converted.CardEffect(player, target, drop, cardIndex);
    }

    public override IEnumerator CardUsed(Player pc)
    {
        pc.UsedSkillCard();
        yield return original.CardUsed(pc);
    }

    public override bool Is<T>()
    {
        if (typeof(T) == typeof(ConvertedCard)) return true;
        return converted.Is<T>();
    }

    public override string ToString()
    {
        return original + " as " + converted;
    }
}