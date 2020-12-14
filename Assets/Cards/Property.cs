using System.Collections;

public abstract class Property : Card
{
    public Property(CardType cardType, Rank rank, Suit suit) : base(cardType, rank, suit) { }
    protected Property(CardType cardType) : base(cardType) { }

    public override IEnumerator CardEffect(Player player, int target, Selection selection, int cardIndex)
    {
        player.RemoveCardHand(this);
        EveryCowboyForHimself.EquipPropertyTo(target, this);
        yield return EquipTrigger(player);
    }

    public virtual void EquipProperty(Player player)
    {
        player.EquipCard(this);
        AddPropertyEffect(player);
    }

    public virtual void AddPropertyEffect(Player player) { }

    public virtual void RemovePropertyEffect(Player player) { }

    protected IEnumerator EquipTrigger(Player player)
    {
        yield return player.EquipTrigger(this);
    }
}