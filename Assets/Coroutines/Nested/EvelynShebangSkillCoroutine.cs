using System.Collections.Generic;

public class EvelynShebangSkillCoroutine : Enumerator
{
    private Player player;
    private List<int> availablePlayers;
    private int cards;

    public override bool MoveNext()
    {
        WaitForClickChoice waitForPhaseOne = Current as WaitForClickChoice;
        if (waitForPhaseOne != null)
        {
            cards--;
            switch (waitForPhaseOne.Decision)
            {
                case Decision.Deck:
                    player.DrawCards();
                    break;
                case Decision.Player:
                    Player target = waitForPhaseOne.Player;
                    int targetPlayer = target.Index;
                    availablePlayers.Remove(targetPlayer);
                    Current = EveryCowboyForHimself.Shoot(player, target);
                    return true;
            }
        }
        if (availablePlayers.Count > 0 && cards > 0)
        {
            Current = new WaitForClickChoice(player);
            return true;
        }
        if (cards > 0) player.DrawCards(cards);
        return false;
    }

    public EvelynShebangSkillCoroutine(Player player, int cards)
    {
        this.player = player;
        this.cards = cards;
        availablePlayers = player.PlayersInWeaponRange();
    }
}
