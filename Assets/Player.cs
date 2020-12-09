using System.Collections;
using System.Collections.Generic;

public class Player
{
    public const int Amount = 48;

    public readonly List<Card> Hand = new List<Card>();
    public readonly List<Card> Properties = new List<Card>();
    public bool IsDying => HP < 1;
    public bool HasCards => Hand.Count > 0;
    public int WeaponRange => Weapon.Range + Scope;
    public int DrawEffectCards { get; protected set; }
    public int Scope { get; protected set; }
    public int RangeModifier { get; protected set; }
    public int Barrels { get; protected set; }
    public int Index { get; private set; }
    public bool IsDead { get; private set; }
    public int HP { get; private set; }
    public Card Weapon { get; private set; }
    public Role Role { get; private set; }
    public CharacterName CharacterName { get; private set; }

    private int maxHP, index;

    public Player(Role role, CharacterName characterName, int index)
    {
        Index = index;
        Role = role;
        CharacterName = characterName;
        HP = GetHPCharacter(characterName);
        HP += role == Role.Sheriff ? 1 : 0;
        maxHP = HP;
        Weapon = Card.Colt45;
    }

    public void RestoreHealth(int amount)
    {
        HP += amount;
        HP = HP > maxHP ? maxHP : HP;
    }

    public void Hit(int amount)
    {
        HP -= amount;
    }

    public void DrawCards(int amount = 1)
    {
        List<Card> cards = EveryCowboyForHimself.DrawCards(amount);
        AddCardsHand(cards);
    }

    public void AddCardHand(Card card)
    {
        Hand.Add(card);
    }

    public void AddCardsHand(List<Card> drawn)
    {
        Hand.AddRange(drawn);
    }

    public Card RemoveCardHand(int index)
    {
        return RemoveCardList(index, Hand);
    }

    public void EquipCard(Card card)
    {
        Properties.Add(card);
    }

    public Card UnequipCard(int index)
    {
        return RemoveCardList(index, Properties);
    }

    public Card EquipWeapon(Card card)
    {
        Card res = null;
        if (Weapon != Card.Colt45)
        {
            res = Weapon;
        }
        Weapon = card;
        return res;
    }

    public Card UnequipWeapon()
    {
        return EquipWeapon(Card.Colt45);
    }

    public List<int> PlayersInWeaponRange()
    {
        List<int> result = EveryCowboyForHimself.PlayersInWeaponRange(Index, WeaponRange);
        return result;
    }

    public void FinishResponse(int pimPamPumsUsed = 1)
    {
        for (int i = 0; i < pimPamPumsUsed; i++) CardUsedOutOfTurn();
        CheckNoCards();
    }

    public void Response()
    {
        FinishResponse();
    }

    public void CheckNoCards()
    {
        if (!HasCards)
        {
            NoCardTrigger();
        }
    }

    public IEnumerator GetHitBy(Player player, int amount = 1)
    {
        yield return Hit(player, amount);
        yield return Dying(player, amount);
        yield return Die(player);
    }

    public IEnumerator Hit(Player attacker, int amount = 1)
    {
        if (attacker != null && attacker != this)
        {
            yield return EveryCowboyForHimself.Event(this + " has been hit by " + attacker);
        }
        else
        {
            yield return EveryCowboyForHimself.Event(this + " loses " + amount + " hit points.");
        }
        HP -= amount;
    }

    public IEnumerator Dying(Player attacker, int amount = 1)
    {
        if (!IsDead)
        {
            if (IsDying)
            {
                yield return new WaitForDying(this);
            }
            if (!IsDying)
            {
                for (int i = 0; i < amount; i++) HitTrigger(attacker);
            }
        }
    }
    public IEnumerator Die(Player killer)
    {
        if (!IsDead && IsDying)
        {
            IsDead = true;
            yield return DieTrigger(killer);
        }
    }

    public virtual bool Immune(Card c) { return false; }

    public virtual bool EndTurnDiscardPickup(int player) { return false; }

    public virtual bool DrawEffectPickup(int player) { return false; }

    protected virtual void CardUsedOutOfTurn() { }

    protected virtual void NoCardTrigger() { }

    protected virtual IEnumerator HitTrigger(Player attacker) { yield return null; }

    protected virtual IEnumerator DieTrigger(Player attacker) { yield return null; }

    private Card RemoveCardList(int index, List<Card> cards)
    {
        Card card = cards[index];
        cards.RemoveAt(index);
        return card;
    }

    private int GetHPCharacter(CharacterName name)
    {
        int hp;
        switch (name)
        {
            case CharacterName.ApacheKid:
            case CharacterName.DonBell:
            case CharacterName.ElenaFuente:
            case CharacterName.ElGringo:
            case CharacterName.PaulRegret:
            case CharacterName.PixiePete:
            case CharacterName.SeanMallory:
            case CharacterName.TerenKill:
                hp = 3;
                break;
            case CharacterName.GaryLooter:
            case CharacterName.TucoFranziskaner:
                hp = 5;
                break;
            case CharacterName.BigSpencer:
                hp = 9;
                break;
            default:
                hp = 4;
                break;
        }
        return hp;
    }

    public override string ToString()
    {
        string result = "Character: " + CharacterName;
        result += "\nRole: " + Role;
        result += "\nHP: " + HP;
        result += "\nCards: ";
        int length = Hand.Count;
        for (int i = 0; i < length; i++)
        {
            result += "\n\t" + Hand[i];
        }
        result += "\nProperties: ";
        length = Properties.Count;
        for (int i = 0; i < length; i++)
        {
            result += "\n\t" + Properties[i];
        }
        return result;
    }
}

public enum Role
{
    Sheriff,
    Deputy,
    Outlaw,
    Renegade
}

public enum CharacterName
{
    AnnieVersary,
    ApacheKid,
    BartCassidy,
    BelleStar,
    BigSpencer,
    BillNoface,
    BlackFlower,
    BlackJack,
    CalamityJanet,
    ChuckWengam,
    ColoradoBill,
    Dobletiro,
    DocHoliday,
    DonBell,
    ElenaFuente,
    ElGringo,
    EvelynShebang,
    FlintWestwood,
    GaryLooter,
    GregDigger,
    HenryBlock,
    HerbHunter,
    JesseJones,
    JohnnyKisch,
    JohnPain,
    JoseDelgado,
    Jourdonnais,
    KitCarlson,
    LeeVanKliff,
    LemonadeJim,
    LuckyDuke,
    MadamYto,
    MollyStark,
    PatBrennan,
    PaulRegret,
    PedroRamirez,
    PixiePete,
    RoseDoolan,
    SeanMallory,
    SidKetchum,
    SlabTheKiller,
    SuzyLafayette,
    TequilaJoe,
    TerenKill,
    TucoFranziskaner,
    VultureSam,
    WillyTheKid,
    YoulGrinner
}
