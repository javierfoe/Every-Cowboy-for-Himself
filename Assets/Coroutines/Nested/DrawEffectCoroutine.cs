using System.Collections.Generic;

public class DrawEffectCoroutine : Enumerator
{
    public Card DrawEffectCard { get; private set; }

    private List<Card> drawnCards;
    private Player player;
    private int currentCard, maxCards;
    private bool drawEffectStarted;

    public override bool MoveNext()
    {
        WaitForCardSelection chooseCardTimer = Current as WaitForCardSelection;
        if (chooseCardTimer != null && chooseCardTimer.ChosenCard != null)
        {
            DrawEffectCard = chooseCardTimer.ChosenCard;
            drawnCards = chooseCardTimer.Cards;
        }
        if (drawnCards != null && !drawEffectStarted)
        {
            currentCard = 0;
            maxCards = drawnCards.Count;
            drawEffectStarted = true;
        }
        if (drawEffectStarted && currentCard < maxCards)
        {
            Current = EveryCowboyForHimself.DrawEffect(player.Index, drawnCards[currentCard++]);
            return true;
        }
        if (maxCards > 0 && currentCard == maxCards) return false;
        return true;
    }

    public DrawEffectCoroutine(Player player)
    {
        this.player = player;

        int drawEffectCards = player.DrawEffectCards;

        if (drawEffectCards < 2)
        {
            DrawEffectCard = EveryCowboyForHimself.DrawCard();
            drawnCards = new List<Card>();
            drawnCards.Add(DrawEffectCard);
        }
        else
        {
            Current = new WaitForCardSelection(player, drawEffectCards);
        }
    }
}