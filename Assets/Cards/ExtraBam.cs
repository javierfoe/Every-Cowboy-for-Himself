using System.Collections;

public class ExtraBam : Bam
{
    public ExtraBam() : base() { }

    protected override IEnumerator Shoot(Player player, int target)
    {
        yield return player.Shoot(target);
    }
}