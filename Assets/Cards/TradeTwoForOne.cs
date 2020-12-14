
using System.Collections;

public class TradeTwoForOne : Card
{
    public TradeTwoForOne() : base() { }

    public override IEnumerator CardEffect(Player player, int target, Selection selection, int cardIndex)
    {
        yield return null;
        EveryCowboyForHimself.TradeTwoForOne(player, target, cardIndex);
    }
}