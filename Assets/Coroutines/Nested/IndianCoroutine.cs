public class IndianCoroutine : ResponseCoroutine
{
    private bool first = true;

    public override bool MoveNext()
    {
        if (first)
        {
            first = false;
            return true;
        }
        WaitForCardResponse responseTimer = Current as WaitForCardResponse;
        if (responseTimer != null)
        {
            currentDecision = responseTimer.Decision;
            switch (responseTimer.Decision)
            {
                case Decision.Avoid:
                    SetCardResponse(responseTimer);
                    return true;
            }
        }
        TakeHit = currentDecision == Decision.TakeHit;
        //TODO Character.DisableCards();
        return false;
    }

    public IndianCoroutine(Player player) : base(player)
    {
        Current = new WaitForCardResponse(player);
    }
}
