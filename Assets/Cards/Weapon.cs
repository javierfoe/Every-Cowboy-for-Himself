public abstract class Weapon : Property
{
    public bool InfiniteShooting { get; protected set; } = false;
    public int Range { get; private set; }

    protected Weapon(CardType cardType, int range) : base(cardType)
    {
        Range = range;
    }

    protected Weapon(CardType cardType, Rank rank, Suit suit, int range) : base(cardType, rank, suit)
    {
        Range = range;
    }

    public override void EquipProperty(Player player)
    {
        player.EquipWeapon(this);
    }

    public virtual bool Shoot() { return false; }
}

public class Carabine : Weapon
{
    public Carabine(Rank rank, Suit suit) : base(CardType.Carabine, rank, suit, 4) { }
}

public class Colt45 : Weapon
{
    public Colt45() : base(CardType.Colt45, 1) { }
}

public class Remington : Weapon
{
    public Remington(Rank rank, Suit suit) : base(CardType.Remington, rank, suit, 3) { }
}

public class Schofield : Weapon
{
    public Schofield(Rank rank, Suit suit) : base(CardType.Schofield, rank, suit, 2) { }
}

public class Volcanic : Weapon
{
    public Volcanic(Rank rank, Suit suit) : base(CardType.Volcanic, rank, suit, 1)
    {
        InfiniteShooting = true;
    }
}

public class Winchester : Weapon
{
    public Winchester(Rank rank, Suit suit) : base(CardType.Winchester, rank, suit, 2) { }
}