using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EveryCowboyForHimself
{
    private const float eventTime = 1f;

    private static int players;
    private static int currentTurn = 0;
    private static Player[] characters;
    private static Board board;

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
        EveryCowboyForHimself.players = players;
        characters = new Player[players];
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
            characters[i] = new Player(role, character, i);
        }
    }

    public static bool IsDying(int player)
    {
        return characters[player].IsDying;
    }

    public static int DrawEffectCards(int player)
    {
        return characters[player].DrawEffectCards;
    }

    public static bool CheckConditionBarrel(Card card)
    {
        return card.Suit == Suit.Hearts;
    }

    public static void EndTurn(int player)
    {
        if (currentTurn != player) return;
        currentTurn = currentTurn < players - 1 ? currentTurn + 1 : 0;
    }

    public static void PlayerGetsHit(int player, int hits = 1)
    {
        characters[player].Hit(hits);
    }

    public static void PlayerDrawsCards(int player, int amount)
    {
        List<Card> drawn = DrawCards(amount);
        characters[player].AddCardsHand(drawn);
    }

    public static void PlayerDiscardsCard(int player, int card)
    {
        Card discarded = characters[player].RemoveCardHand(card);
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
            res = res < players ? res : 0;
            pc = characters[res];
        } while (pc.IsDead);
        return res;
    }

    public static IEnumerator BarrelEffect(Player target, Card c, bool dodge)
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
        int length = characters.Length;
        for (int i = 0; i < length; i++)
        {
            Debug.Log(characters[i]);
        }
    }

    private static int PreviousPlayerAlive(int player)
    {
        Player pc;
        int res = player;
        do
        {
            res--;
            res = res > -1 ? res : players - 1;
            pc = characters[res];
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
            character = characters[next];
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
        for (int i = player, j = 0; j < players; i = i == players - 1 ? 0 : i + 1, j++)
        {
            aux = characters[i];
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
