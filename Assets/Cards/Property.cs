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

public class Barrel : Property
{
    public Barrel(Rank rank, Suit suit) : base(CardType.Barrel, rank, suit) { }

    public override void AddPropertyEffect(Player player)
    {
        player.EquipBarrel();
    }

    public override void RemovePropertyEffect(Player player)
    {
        player.UnequipBarrel();
    }
}

public class Dynamite : Property
{
    public Dynamite(Rank rank, Suit suit) : base(CardType.Dynamite, rank, suit) { }

    public override void AddPropertyEffect(Player player)
    {
        player.EquipDynamite();
    }

    public override void RemovePropertyEffect(Player player)
    {
        player.UnequipDynamite();
    }
}

public class Jail : Property
{
    public Jail(Rank rank, Suit suit) : base(CardType.Jail, rank, suit) { }

    public override void AddPropertyEffect(Player player)
    {
        player.EquipJail();
    }

    public override void RemovePropertyEffect(Player player)
    {
        player.UnequipJail();
    }
}

public class Mustang : Property
{
    public Mustang(Rank rank, Suit suit) : base(CardType.Mustang, rank, suit) { }

    public override void AddPropertyEffect(Player player)
    {
        player.EquipMustang();
    }

    public override void RemovePropertyEffect(Player player)
    {
        player.UnequipMustang();
    }
}

public class Scope : Property
{
    public Scope(Rank rank, Suit suit) : base(CardType.Scope, rank, suit) { }

    public override void AddPropertyEffect(Player player)
    {
        player.EquipScope();
    }

    public override void RemovePropertyEffect(Player player)
    {
        player.UnequipScope();
    }
}