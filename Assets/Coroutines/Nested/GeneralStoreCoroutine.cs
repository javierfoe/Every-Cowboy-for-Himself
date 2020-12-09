using System.Collections.Generic;

public class GeneralStoreCoroutine : Enumerator
{
    private Player[] players;
    private int nextPlayer, pendingPlayers;
    private bool lastCard;

    public List<Card> CardChoices { get; set; }
    public Card LastCard { get { return CardChoices[0]; } }

    public override bool MoveNext()
    {
        WaitForCardSelection generalStoreTimer = Current as WaitForCardSelection;
        if (generalStoreTimer != null)
        {
            CardChoices = generalStoreTimer.NotChosenCards;
            players[nextPlayer].AddCardHand(generalStoreTimer.ChosenCard);
            nextPlayer = EveryCowboyForHimself.NextPlayerAlive(nextPlayer);
            pendingPlayers--;
            return true;
        }
        bool res = pendingPlayers > 1;
        if (res)
        {
            Current = new WaitForCardSelection(players[nextPlayer], CardChoices);
            return true;
        }
        if (!res && !lastCard)
        {
            lastCard = true;
            players[nextPlayer].AddCardHand(LastCard);
            return true;
        }
        return res;
    }

    public GeneralStoreCoroutine(Player[] players, int start, List<Card> cards)
    {
        this.players = players;
        lastCard = false;
        pendingPlayers = cards.Count;
        CardChoices = cards;
        nextPlayer = start;
    }
}