using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Selection
{
    Nothing,
    Board,
    Hand,
    Properties,
    Weapon
}

public static class EveryCowboyForHimself
{
    private const float eventTime = 1f;

    public static bool FinalDuel => PlayersAlive < 3;

    private static int playerAmount;
    private static int currentTurn = 0;
    private static Player[] players;
    private static Board board;

    public static int PlayersAlive
    {
        get
        {
            int res = 0;
            for (int i = 0; i < playerAmount; i++)
            {
                res += players[i].IsDead ? 0 : 1;
            }
            return res;
        }
    }

    public static void Setup(int players, Role[] roles = null, CharacterName[] allowedCharacters = null)
    {
        //Initialize board
        board = new Board();

        //Set selection of roles
        List<Role> possibleRoles;
        if (roles == null)
        {
            possibleRoles = GetRolesFromPlayers(players);
        }
        else
        {
            possibleRoles = new List<Role>();
            int length = roles.Length;
            for (int i = 0; i < length; i++)
            {
                possibleRoles.Add(roles[i]);
            }
        }

        //Set selection of characters
        List<CharacterName> possibleCharacters = new List<CharacterName>();
        if (allowedCharacters == null)
        {
            for (int i = 0; i < Player.Amount; i++)
            {
                possibleCharacters.Add((CharacterName)i);
            }
        }
        else
        {
            int length = allowedCharacters.Length;
            for (int i = 0; i < length; i++)
            {
                possibleCharacters.Add(allowedCharacters[i]);
            }
        }

        //Choose random characters and roles
        EveryCowboyForHimself.playerAmount = players;
        EveryCowboyForHimself.players = new Player[players];
        int randomCharacter, randomRole;
        CharacterName character;
        Role role;
        for (int i = 0; i < players; i++)
        {
            randomCharacter = Random.Range(0, possibleCharacters.Count);
            randomRole = Random.Range(0, possibleRoles.Count);
            role = possibleRoles[randomRole];
            character = possibleCharacters[randomCharacter];
            possibleCharacters.RemoveAt(randomCharacter);
            possibleRoles.RemoveAt(randomRole);
            EveryCowboyForHimself.players[i] = new Player(role, character, i);
        }
    }

    public static bool IsDying(int player)
    {
        return players[player].IsDying;
    }

    public static int DrawEffectCards(int player)
    {
        return players[player].DrawEffectCards;
    }

    public static bool CheckConditionBarrel(Card card)
    {
        return card.IsSuit(Suit.Hearts);
    }

    public static void EndTurn(int player)
    {
        if (currentTurn != player) return;
        currentTurn = currentTurn < playerAmount - 1 ? currentTurn + 1 : 0;
    }

    public static void PlayerGetsHit(int player, int hits = 1)
    {
        players[player].Hit(hits);
    }

    public static void PlayerDrawsCards(int player, int amount)
    {
        List<Card> drawn = DrawCards(amount);
        players[player].AddCardsHand(drawn);
    }

    public static void PlayerDiscardsCard(int player, int card)
    {
        Card discarded = players[player].RemoveCardHand(card);
        DiscardCard(discarded);
    }

    public static Card DrawCard()
    {
        return board.DrawCard();
    }

    public static List<Card> DrawCards(int amount)
    {
        return board.DrawCards(amount);
    }

    public static void DiscardCard(Card card)
    {
        board.DiscardCard(card);
    }

    public static void EquipPropertyTo(int target, Property p)
    {
        p.EquipProperty(players[target]);
    }

    public static List<int> PlayersInWeaponRange(int player, int weaponRange)
    {
        return PlayersInRange(player, weaponRange, false);
    }

    public static List<int> PlayersInRange(int player, int range, bool includeItself)
    {
        List<int> res = new List<int>();

        TraversePlayers(res, player, range, true, includeItself);
        TraversePlayers(res, player, range, false, includeItself);

        return res;
    }

    public static int NextPlayerAlive(int player)
    {
        Player pc;
        int res = player;
        do
        {
            res++;
            res = res < playerAmount ? res : 0;
            pc = players[res];
        } while (pc.IsDead);
        return res;
    }
    public static void Saloon()
    {
        for (int i = 0; i < playerAmount; i++)
        {
            players[i].RestoreHealth();
        }
    }

    public static IEnumerator CardUsed(Player player, Card card)
    {
        int index = player.Index;
        Player aux;
        for (int i = index, j = 0; j < playerAmount; i = i == playerAmount - 1 ? 0 : i + 1, j++)
        {
            aux = players[i];
            if (!aux.IsDead)
                yield return players[i].UsedCard(index, card);
        }
    }

    public static IEnumerator BarrelEffect(Player target, Card c)
    {
        yield return DrawEffect(target.Index, c);
    }

    public static IEnumerator Shoot(Player player, Player target, int misses = 1)
    {
        ShootCoroutine pimPamPumCoroutine = new ShootCoroutine(target, misses);
        yield return pimPamPumCoroutine;
        if (pimPamPumCoroutine.TakeHit)
        {
            yield return HitPlayer(player, target);
        }
    }
    public static IEnumerator Gatling(int player, Card c)
    {
        yield return new GatlingCoroutine(players, player, c);
    }

    public static IEnumerator Indians(int player, Card c)
    {
        yield return new IndiansCoroutine(players, player, c);
    }

    public static IEnumerator CatBalou(Player player, int target, Selection selection, int cardIndex)
    {
        Player targetPlayer = players[target];
        Card c = null;
        switch (selection)
        {
            case Selection.Hand:
                c = targetPlayer.StealCardFromHand(cardIndex);
                break;
            case Selection.Properties:
                c = targetPlayer.UnequipCard(cardIndex);
                break;
            case Selection.Weapon:
                c = targetPlayer.UnequipWeapon();
                break;
        }
        DiscardCard(c);
        yield return targetPlayer.StolenBy(player);
    }

    public static IEnumerator Panic(Player player, int target, Selection drop, int cardIndex)
    {
        Player targetPlayer = players[target];
        Card c = null;
        switch (drop)
        {
            case Selection.Hand:
                if (target == player.Index)
                {
                    c = null;
                }
                else
                {
                    c = targetPlayer.StealCardFromHand(cardIndex);
                }
                break;
            case Selection.Properties:
                c = targetPlayer.UnequipCard(cardIndex);
                break;
            case Selection.Weapon:
                c = targetPlayer.UnequipWeapon();
                break;
        }
        if (c != null) player.AddCardHand(c);
        yield return targetPlayer.StolenBy(player);
    }

    public static IEnumerator Duel(Player player, int target)
    {
        yield return new DuelCoroutine(player, players[target]);
    }

    public static IEnumerator GeneralStore(int player)
    {
        List<Card> cardChoices = board.DrawCards(PlayersAlive);
        yield return new GeneralStoreCoroutine(players, player, cardChoices);
    }

    public static IEnumerator HitPlayer(Player player, Player target)
    {
        yield return target.GetHitBy(player);
    }

    public static IEnumerator CardResponse(Player character, Card card)
    {
        yield return Event(character + " has avoided the hit with: " + card);
        character.Response();
        DiscardCard(card);
    }

    public static IEnumerator DrawEffect(int player, Card c)
    {
        yield return DiscardDrawEffect(player, c, false);
    }

    public static IEnumerator Event(string eventText)
    {
        yield return new WaitForSeconds(eventTime);
        Debug.Log(eventText);
    }

    public static void PrintStatus()
    {
        Debug.Log(board);
        int length = players.Length;
        for (int i = 0; i < length; i++)
        {
            Debug.Log(players[i]);
        }
    }

    private static int PreviousPlayerAlive(int player)
    {
        Player pc;
        int res = player;
        do
        {
            res--;
            res = res > -1 ? res : playerAmount - 1;
            pc = players[res];
        } while (pc.IsDead);
        return res;
    }

    private static void TraversePlayers(List<int> players, int player, int range, bool forward, bool includeItself, Card c = null)
    {
        int auxRange = 0;
        int next = player;
        Player character;
        bool dead;
        do
        {
            next = forward ? NextPlayerAlive(next) : PreviousPlayerAlive(next);
            character = EveryCowboyForHimself.players[next];
            dead = character.IsDead;
            auxRange += dead ? 0 : 1;
            if (!players.Contains(next) && !dead && (includeItself || next != player) && character.RangeModifier + auxRange < range + 1 && (c == null || c != null && !character.Immune(c))) players.Add(next);
        } while (next != player);
    }

    private static IEnumerator DiscardDrawEffect(int player, Card c, bool discardDrawEffect)
    {
        Player character;
        if (DiscardDrawEffectTriggers(player, discardDrawEffect, out character))
        {
            string eventText = discardDrawEffect ? " adds the discarded card to his hand: " : " adds the draw! effect card to his hand: ";
            yield return Event(character + eventText + c);
            character.AddCardHand(c);
        }
        else
        {
            DiscardCard(c);
        }
    }

    private static bool DiscardDrawEffectTriggers(int player, bool discardDrawEffect, out Player pc)
    {
        pc = null;
        bool res = false;
        Player aux;
        for (int i = player, j = 0; j < playerAmount; i = i == playerAmount - 1 ? 0 : i + 1, j++)
        {
            aux = players[i];
            res |= discardDrawEffect ? aux.EndTurnDiscardPickup(player) : aux.DrawEffectPickup(player);
            pc = pc == null && res ? aux : pc;
        }
        return res;
    }

    private static List<Role> GetRolesFromPlayers(int players)
    {
        List<Role> roles = new List<Role>();
        switch (players)
        {
            case 8:
                roles.Add(Role.Renegade);
                goto case 7;
            case 7:
                roles.Add(Role.Deputy);
                goto case 6;
            case 6:
                roles.Add(Role.Outlaw);
                goto case 5;
            case 5:
                roles.Add(Role.Deputy);
                goto case 4;
            case 4:
                roles.Add(Role.Outlaw);
                roles.Add(Role.Outlaw);
                roles.Add(Role.Renegade);
                roles.Add(Role.Sheriff);
                break;
        }
        return roles;
    }
}
