﻿public class WaitForCardResponse : WaitForDecision
{
    public Card ResponseCard { get; private set; }

    public WaitForCardResponse(Player player) : base(player, Decision.TakeHit) { }

    public override void MakeDecisionCard(Decision decision, Card card)
    {
        base.MakeDecisionCard(decision, card);
        ResponseCard = card;
    }
}