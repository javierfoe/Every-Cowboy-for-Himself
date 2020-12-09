public class YoulGrinnerSkillCoroutine : Enumerator
{
    private int minimumCards, currentPlayer, startingPlayer;
    private Player[] Characters;

    public override bool MoveNext()
    {
        WaitForCardSelection cardSelection = Current as WaitForCardSelection;
        if (cardSelection != null)
        {
            Characters[currentPlayer].RemoveCardHand(cardSelection.Choice);
            Characters[startingPlayer].AddCardHand(cardSelection.ChosenCard);
        }
        currentPlayer = EveryCowboyForHimself.NextPlayerAlive(currentPlayer);
        Player currentPc = Characters[currentPlayer];
        if (startingPlayer != currentPlayer && currentPc.Hand.Count > minimumCards)
        {
            Current = new WaitForCardSelection(currentPc, currentPc.Hand);
            return true;
        }
        return false;
    }

    public YoulGrinnerSkillCoroutine(int player, Player[] Characters)
    {
        this.Characters = Characters;
        startingPlayer = player;
        minimumCards = Characters[player].Hand.Count;
        currentPlayer = player;
    }
}