public class ShootCoroutine : ResponseCoroutine
{
    private int misses, dodges, barrelsUsed, barrels;
    private bool dodge;

    public override bool MoveNext()
    {
        WaitForCardResponse responseTimer = Current as WaitForCardResponse;
        if (responseTimer != null)
        {
            currentDecision = responseTimer.Decision;
            switch (responseTimer.Decision)
            {
                case Decision.Avoid:
                    currentDecision = Decision.Pending;
                    dodges++;
                    SetCardResponse(responseTimer);
                    return true;
                case Decision.Barrel:
                    currentDecision = Decision.Pending;
                    barrelsUsed++;
                    Current = new DrawEffectCoroutine(character);
                    return true;
            }
        }
        DrawEffectCoroutine drawEffectCoroutine = Current as DrawEffectCoroutine;
        if (drawEffectCoroutine != null)
        {
            Card drawEffectCard = drawEffectCoroutine.DrawEffectCard;
            dodge = EveryCowboyForHimself.CheckConditionBarrel(drawEffectCard);
            dodges += dodge ? 1 : 0;
            Current = EveryCowboyForHimself.BarrelEffect(character, drawEffectCard, dodge);
            return true;
        }
        if (dodges < misses && currentDecision != Decision.TakeHit)
        {
            Current = new WaitForCardResponse(character);
            return true;
        }
        TakeHit = dodges < misses || currentDecision == Decision.TakeHit;
        return false;
    }

    public ShootCoroutine() { }

    public ShootCoroutine(Player character, int misses = 1) : base(character)
    {
        this.misses = misses;
        this.character = character;
        misses = 1;
        barrels = character.Barrels;
        barrelsUsed = 0;
        dodges = 0;
    }
}