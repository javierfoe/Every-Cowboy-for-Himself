public class WaitForClickChoice : WaitForDecision
{
    public int ChosenPlayer
    {
        get; private set;
    }

    public int CardIndex
    {
        get; private set;
    }

    public override bool MoveNext()
    {
        bool res = base.MoveNext();
        Finished(res);
        return res;
    }

    public WaitForClickChoice(Player player, Decision decision) : base(player, decision) { }

    public WaitForClickChoice(Player player) : this(player, Decision.Deck) { }

    public override void MakeDecisionPhaseOne(Decision phaseOneOption, int chosenPlayer = -1, int card = -1)
    {
        base.MakeDecisionCard(phaseOneOption);
        ChosenPlayer = chosenPlayer;
        CardIndex = card;
    }
}