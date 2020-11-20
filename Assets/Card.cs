public class Card
{
    public Suit Suit { get; private set; }
    public Rank Rank { get; private set; }
    public CardType Type { get; private set; }

    public Card(CardType type)
    {
        Type = type;
    }

    public Card(CardType type, Rank rank, Suit suit)
    {
        Suit = suit;
        Rank = rank;
        Type = type;
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