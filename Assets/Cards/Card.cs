using System.Collections;

public abstract class Card
{
    public static Weapon Colt45 = new Colt45();

    public bool IsBrown { get; protected set; }
    public Suit Suit { get; private set; }
    public Rank Rank { get; private set; }
    public CardType CardType { get; private set; }

    public virtual Card Original => null;

    public Card(CardType type)
    {
        CardType = type;
    }

    public Card(CardType type, Rank rank, Suit suit)
    {
        Suit = suit;
        Rank = rank;
        CardType = type;
    }

    public Card ConvertTo<T>() where T : Card, new()
    {
        if (this is T) return this;
        Card converted = new T();
        return new ConvertedCard(this, converted);
    }

    public IEnumerator CardUsed(Player player)
    {
        Card usedCard = RetrieveUsedCard();
        yield return EveryCowboyForHimself.CardUsed(player, usedCard);
    }

    public virtual bool Is<T>() where T : Card
    {
        return this is T;
    }

    public virtual bool IsSuit(Suit suit)
    {
        return Suit == suit;
    }

    public virtual void BeginCardDrag(Player player) { }

    public virtual IEnumerator PlayCard(Player player, int target, Selection drop, int cardIndex)
    {
        yield return CardEffect(player, target, drop, cardIndex);
        yield return CardUsed(player);
        player.FinishCardUsed();
    }

    public virtual IEnumerator CardEffect(Player player, int target, Selection drop, int cardIndex)
    {
        player.DiscardCardHand(this);
        yield return null;
    }

    protected virtual Card RetrieveUsedCard() { return this; }

    public override string ToString()
    {
        return CardType + " " + Rank + " " + Suit;
    }
}

public enum Suit
{
    Spades,
    Clubs,
    Hearts,
    Diamonds
}

public enum Rank
{
    Ace = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13
}

public enum CardType
{
    Bam,
    Barrel,
    Beer,
    Carabine,
    CatBalou,
    Colt45,
    Duel,
    Dynamite,
    Gatling,
    GeneralStore,
    Indians,
    Jail,
    Missed,
    Mustang,
    Panic,
    Remington,
    Saloon,
    Schofield,
    Scope,
    Stagecoach,
    Volcanic,
    WellsFargo,
    Winchester
}