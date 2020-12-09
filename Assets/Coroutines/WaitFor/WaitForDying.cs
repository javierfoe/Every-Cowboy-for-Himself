using System;

public class WaitForDying : WaitForDecision
{
    private Func<bool> dying;

    public override bool MoveNext()
    {
        bool res = dying();
        Finished(res);
        return res;
    }

    protected override void Finished()
    {
        WaitForController.StopMainCorutine();
    }

    public WaitForDying(Player player) : base(player)
    {
        dying = () => base.MoveNext() && player.IsDying;
        WaitForController.DyingCorutine = this;
    }
}
