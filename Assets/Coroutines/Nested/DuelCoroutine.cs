public class DuelCoroutine : Enumerator
{
    private Player player, target, next;
    private int pimPamPumsTarget;
    private Decision decision;

    public override bool MoveNext()
    {
        if (finished) return false;
        WaitForCardResponse responseCoroutine = Current as WaitForCardResponse;
        if (responseCoroutine != null)
        {
            decision = responseCoroutine.Decision;
            if (decision == Decision.Avoid)
            {
                Current = EveryCowboyForHimself.Event(next + " keeps dueling.");
                if (next == target)
                {
                    pimPamPumsTarget++;
                }
                return true;
            }
            else
            {
                Current = EveryCowboyForHimself.Event(next + " loses the duel.");
                return true;
            }
        }
        if (decision != Decision.TakeHit)
        {
            next = next == player ? target : player;
            //TODO next.EnablePimPamPumsDuelResponse();
            Current = new WaitForCardResponse(next);
            return true;
        }
        else
        {
            target.FinishResponse(pimPamPumsTarget);
            Current = EveryCowboyForHimself.HitPlayer(player, next);
            finished = true;
            return true;
        }
    }

    public DuelCoroutine(Player player, Player target)
    {
        this.player = player;
        this.target = target;
        next = player;
        decision = Decision.Pending;
    }
}