using UnityEngine;

public class WaitFor : Enumerator
{
    private const float responseTime = 10f;
    private float time, maxTime;
    private int frame;

    public Player Player { get; private set; }
    public bool Response { get; set; }
    public bool TimeUp { get; private set; }

    public override bool MoveNext()
    {
        int currentFrame = Time.frameCount;
        if (frame != currentFrame)
        {
            frame = currentFrame;
            time += Time.deltaTime;
        }
        bool timer = time < maxTime;
        TimeUp = !timer;
        Finished(timer);
        return timer;
    }

    protected void Finished(bool value)
    {
        if (finished || value) return;
        Finished();
        finished = true;
    }

    protected virtual void Finished() { }

    protected WaitFor(Player player, float maxTime = responseTime, bool turn = false)
    {
        Player = player;
        frame = -1;
        this.maxTime = maxTime;
        if (!turn) WaitForController.MainCorutine = this;
        time = 0;
    }

    public void StopCorutine()
    {
        time = maxTime;
        Finished(false);
    }

    public virtual void MakeDecisionCardIndex(int card) { }
    public virtual void MakeDecisionCard(Decision decision, Card card) { }
    public virtual void MakeDecisionPhaseOne(Decision phaseOneOption, int player = -1, int card = -1) { }
}
