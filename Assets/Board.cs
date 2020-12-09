using System.Collections.Generic;

public class Board
{
    public readonly List<Card> Deck = new List<Card>();
    public readonly List<Card> DiscardStack = new List<Card>();

    public Card TopDiscardStack => DiscardStack[DiscardStack.Count - 1];

    public Board()
    {
        List<Card> deck = GenerateBasicDeck();
        AddCardsToDeck(deck);
    }

    public Board(List<Card> deck)
    {
        AddCardsToDeck(deck);
    }

    public Card DrawCard()
    {
        if(Deck.Count < 1)
        {
            AddCardsToDeck(DiscardStack);
            DiscardStack.Clear();
        }
        Card result = Deck[0];
        Deck.RemoveAt(0);
        return result;
    }

    public List<Card> DrawCards(int amount)
    {
        List<Card> result = new List<Card>();
        Card aux;
        for (int i = 0; i < amount; i++)
        {
            aux = DrawCard();
            result.Add(aux);
        }
        return result;
    }

    public void DiscardCard(Card card)
    {
        DiscardStack.Add(card);
    }

    public void DiscardCards(List<Card> cards)
    {
        DiscardStack.AddRange(cards);
    }

    public override string ToString()
    {
        return "Deck: " + Deck.Count + " Discard: " + DiscardStack.Count;
    }

    private void AddCardsToDeck(List<Card> cards)
    {
        int random;
        Card card;
        while (cards.Count > 0)
        {
            random = UnityEngine.Random.Range(0, cards.Count);
            card = cards[random];
            cards.RemoveAt(random);
            Deck.Add(card);
        }
    }

    private List<Card> GenerateBasicDeck()
    {
        List<Card> deck = new List<Card>();

        //Bams
        deck.Add(new Card(CardType.Bam, Rank.Ace, Suit.Spades));
        deck.Add(new Card(CardType.Bam, Rank.Ace, Suit.Hearts));
        deck.Add(new Card(CardType.Bam, Rank.Queen, Suit.Hearts));
        deck.Add(new Card(CardType.Bam, Rank.King, Suit.Hearts));
        for (int i = 1; i < 14; i++)
            deck.Add(new Card(CardType.Bam, (Rank)i, Suit.Diamonds));
        for (int i = 2; i < 10; i++)
            deck.Add(new Card(CardType.Bam, (Rank)i, Suit.Clubs));

        //Missed
        for (int i = 2; i < 9; i++)
            deck.Add(new Card(CardType.Missed, (Rank)i, Suit.Spades));
        for (int i = 10; i < 14; i++)
            deck.Add(new Card(CardType.Missed, (Rank)i, Suit.Clubs));
        deck.Add(new Card(CardType.Missed, Rank.Ace, Suit.Clubs));

        //Beer
        for (int i = 6; i < 12; i++)
            deck.Add(new Card(CardType.Beer, (Rank)i, Suit.Hearts));

        //Panic
        deck.Add(new Card(CardType.Panic, Rank.Ace, Suit.Hearts));
        deck.Add(new Card(CardType.Panic, Rank.Jack, Suit.Hearts));
        deck.Add(new Card(CardType.Panic, Rank.Queen, Suit.Hearts));
        deck.Add(new Card(CardType.Panic, Rank.Eight, Suit.Diamonds));

        //Cat Balou
        deck.Add(new Card(CardType.CatBalou, Rank.King, Suit.Hearts));
        deck.Add(new Card(CardType.CatBalou, Rank.Nine, Suit.Diamonds));
        deck.Add(new Card(CardType.CatBalou, Rank.Ten, Suit.Diamonds));
        deck.Add(new Card(CardType.CatBalou, Rank.Jack, Suit.Diamonds));

        //Duel
        deck.Add(new Card(CardType.Duel, Rank.Jack, Suit.Spades));
        deck.Add(new Card(CardType.Duel, Rank.Queen, Suit.Diamonds));
        deck.Add(new Card(CardType.Duel, Rank.Eight, Suit.Clubs));

        //Stagecoach
        deck.Add(new Card(CardType.Stagecoach, Rank.Nine, Suit.Spades));
        deck.Add(new Card(CardType.Stagecoach, Rank.Nine, Suit.Spades));

        //General Store
        deck.Add(new Card(CardType.GeneralStore, Rank.Queen, Suit.Spades));
        deck.Add(new Card(CardType.GeneralStore, Rank.Nine, Suit.Clubs));

        //Indians
        deck.Add(new Card(CardType.Indians, Rank.Ace, Suit.Diamonds));
        deck.Add(new Card(CardType.Indians, Rank.King, Suit.Diamonds));

        //Saloon
        deck.Add(new Card(CardType.Saloon, Rank.Five, Suit.Hearts));

        //Gatling
        deck.Add(new Card(CardType.Gatling, Rank.Ten, Suit.Hearts));

        //Wells Fargo
        deck.Add(new Card(CardType.WellsFargo, Rank.Three, Suit.Hearts));

        //Schofield
        deck.Add(new Card(CardType.Schofield, Rank.King, Suit.Spades));
        deck.Add(new Card(CardType.Schofield, Rank.Jack, Suit.Clubs));
        deck.Add(new Card(CardType.Schofield, Rank.Queen, Suit.Clubs));

        //Volcanic
        deck.Add(new Card(CardType.Volcanic, Rank.Ten, Suit.Spades));
        deck.Add(new Card(CardType.Volcanic, Rank.Ten, Suit.Clubs));

        //Carabine
        deck.Add(new Card(CardType.Carabine, Rank.Ace, Suit.Clubs));

        //Winchester
        deck.Add(new Card(CardType.Winchester, Rank.Eight, Suit.Spades));

        //Remington
        deck.Add(new Card(CardType.Remington, Rank.King, Suit.Clubs));

        //Jail
        deck.Add(new Card(CardType.Jail, Rank.Ten, Suit.Spades));
        deck.Add(new Card(CardType.Jail, Rank.Jack, Suit.Spades));
        deck.Add(new Card(CardType.Jail, Rank.Four, Suit.Hearts));

        //Mustang
        deck.Add(new Card(CardType.Mustang, Rank.Eight, Suit.Hearts));
        deck.Add(new Card(CardType.Mustang, Rank.Nine, Suit.Hearts));

        //Barrel
        deck.Add(new Card(CardType.Barrel, Rank.Queen, Suit.Spades));
        deck.Add(new Card(CardType.Barrel, Rank.King, Suit.Spades));

        //Scope
        deck.Add(new Card(CardType.Scope, Rank.Ace, Suit.Spades));

        //Dynamite
        deck.Add(new Card(CardType.Dynamite, Rank.Two, Suit.Hearts));

        return deck;
    }
}
