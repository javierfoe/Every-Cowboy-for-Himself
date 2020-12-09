public abstract class ResponseCoroutine : Enumerator
{
    protected Player character;
    protected Decision currentDecision;

    public bool TakeHit { get; protected set; }

    public override abstract bool MoveNext();

    protected void SetCardResponse(WaitForCardResponse timer)
    {
        Current = EveryCowboyForHimself.CardResponse(character, timer.ResponseCard);
    }

    public ResponseCoroutine() { }

    public ResponseCoroutine(Player character)
    {
        this.character = character;
    }
}
