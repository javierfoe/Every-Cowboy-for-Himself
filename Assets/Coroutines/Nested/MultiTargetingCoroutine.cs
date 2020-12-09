
using System.Collections;

public abstract class MultiTargetingCoroutine : Enumerator
{
    private int player, next;
    private Card card;
    private bool[] hitPlayers;
    private Player[] players;

    public MultiTargetingCoroutine(Player[] players, int player, Card card)
    {
        this.players = players;
        this.player = player;
        this.card = card;
        next = player;
        hitPlayers = new bool[players.Length];
    }

    public override bool MoveNext()
    {
        if (finished) return false;
        ResponseCoroutine responseCoroutine = Current as ResponseCoroutine;
        if (responseCoroutine != null)
        {
            hitPlayers[next] = responseCoroutine.TakeHit;
        }
        Player pc;
        do
        {
            next = EveryCowboyForHimself.NextPlayerAlive(next);
            pc = players[next];
        } while (pc.IsDead || pc.Immune(card));
        if (next != player)
        {
            Current = CreateResponseCoroutine(pc);
            return true;
        }
        Current = FinishMultiTargeting();
        finished = true;
        return true;
    }

    protected abstract ResponseCoroutine CreateResponseCoroutine(Player player);

    private IEnumerator FinishMultiTargeting()
    {
        int MaxPlayers = players.Length;
        Player aux;
        for (int i = player == MaxPlayers - 1 ? 0 : player + 1; i != player; i = i == MaxPlayers - 1 ? 0 : i + 1)
        {
            aux = players[player];
            if (hitPlayers[i]) yield return players[i].Hit(aux);
        }
        for (int i = player == MaxPlayers - 1 ? 0 : player + 1; i != player; i = i == MaxPlayers - 1 ? 0 : i + 1)
        {
            aux = players[player];
            if (hitPlayers[i]) yield return players[i].Dying(aux);
        }
        for (int i = player == MaxPlayers - 1 ? 0 : player + 1; i != player; i = i == MaxPlayers - 1 ? 0 : i + 1)
        {
            aux = players[player];
            if (hitPlayers[i]) yield return players[i].Die(aux);
        }
    }

}

public class IndiansCoroutine : MultiTargetingCoroutine
{
    public IndiansCoroutine(Player[] players, int player, Card card) : base(players, player, card) { }

    protected override ResponseCoroutine CreateResponseCoroutine(Player player)
    {
        return new IndianCoroutine(player);
    }
}

public class GatlingCoroutine : MultiTargetingCoroutine
{
    public GatlingCoroutine(Player[] players, int player, Card card) : base(players, player, card) { }

    protected override ResponseCoroutine CreateResponseCoroutine(Player player)
    {
        return new ShootCoroutine(player);
    }
}