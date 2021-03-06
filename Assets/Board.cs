﻿using System.Collections.Generic;

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
        if (Deck.Count < 1)
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
        deck.Add(new Bam(Rank.Ace, Suit.Spades));
        deck.Add(new Bam(Rank.Ace, Suit.Hearts));
        deck.Add(new Bam(Rank.Queen, Suit.Hearts));
        deck.Add(new Bam(Rank.King, Suit.Hearts));
        for (int i = 1; i < 14; i++)
            deck.Add(new Bam((Rank)i, Suit.Diamonds));
        for (int i = 2; i < 10; i++)
            deck.Add(new Bam((Rank)i, Suit.Clubs));

        //Missed
        for (int i = 2; i < 9; i++)
            deck.Add(new Missed((Rank)i, Suit.Spades));
        for (int i = 10; i < 14; i++)
            deck.Add(new Missed((Rank)i, Suit.Clubs));
        deck.Add(new Missed(Rank.Ace, Suit.Clubs));

        //Beer
        for (int i = 6; i < 12; i++)
            deck.Add(new Beer((Rank)i, Suit.Hearts));

        //Panic
        deck.Add(new Panic(Rank.Ace, Suit.Hearts));
        deck.Add(new Panic(Rank.Queen, Suit.Hearts));
        deck.Add(new Panic(Rank.Eight, Suit.Diamonds));
        deck.Add(new Panic(Rank.Jack, Suit.Hearts));

        //Cat Balou
        deck.Add(new CatBalou(Rank.King, Suit.Hearts));
        deck.Add(new CatBalou(Rank.Nine, Suit.Diamonds));
        deck.Add(new CatBalou(Rank.Ten, Suit.Diamonds));
        deck.Add(new CatBalou(Rank.Jack, Suit.Diamonds));

        //Duel
        deck.Add(new Duel(Rank.Jack, Suit.Spades));
        deck.Add(new Duel(Rank.Queen, Suit.Diamonds));
        deck.Add(new Duel(Rank.Eight, Suit.Clubs));

        //Stagecoach
        deck.Add(new Stagecoach(Rank.Nine, Suit.Spades));
        deck.Add(new Stagecoach(Rank.Nine, Suit.Spades));

        //General Store
        deck.Add(new GeneralStore(Rank.Queen, Suit.Spades));
        deck.Add(new GeneralStore(Rank.Nine, Suit.Clubs));

        //Indians
        deck.Add(new Indians(Rank.Ace, Suit.Diamonds));
        deck.Add(new Indians(Rank.King, Suit.Diamonds));

        //Saloon
        deck.Add(new Saloon(Rank.Five, Suit.Hearts));

        //Gatling
        deck.Add(new Gatling(Rank.Ten, Suit.Hearts));

        //Wells Fargo
        deck.Add(new WellsFargo(Rank.Three, Suit.Hearts));

        //Schofield
        deck.Add(new Schofield(Rank.King, Suit.Spades));
        deck.Add(new Schofield(Rank.Jack, Suit.Clubs));
        deck.Add(new Schofield(Rank.Queen, Suit.Clubs));

        //Volcanic
        deck.Add(new Volcanic(Rank.Ten, Suit.Spades));
        deck.Add(new Volcanic(Rank.Ten, Suit.Clubs));

        //Carabine
        deck.Add(new Carabine(Rank.Ace, Suit.Clubs));

        //Winchester
        deck.Add(new Winchester(Rank.Eight, Suit.Spades));

        //Remington
        deck.Add(new Remington(Rank.King, Suit.Clubs));

        //Jail
        deck.Add(new Jail(Rank.Ten, Suit.Spades));
        deck.Add(new Jail(Rank.Jack, Suit.Spades));
        deck.Add(new Jail(Rank.Four, Suit.Hearts));

        //Mustang
        deck.Add(new Mustang(Rank.Eight, Suit.Hearts));
        deck.Add(new Mustang(Rank.Nine, Suit.Hearts));

        //Barrel
        deck.Add(new Barrel(Rank.Queen, Suit.Spades));
        deck.Add(new Barrel(Rank.King, Suit.Spades));

        //Scope
        deck.Add(new Scope(Rank.Ace, Suit.Spades));

        //Dynamite
        deck.Add(new Dynamite(Rank.Two, Suit.Hearts));

        return deck;
    }
}
