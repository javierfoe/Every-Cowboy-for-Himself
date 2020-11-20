using System.Collections.Generic;

public class Character
{
    public const int Amount = 48;

    public Role Role { get; private set; }
    public CharacterName CharacterName { get; private set; }
    public int HP { get; private set; }
    public readonly List<Card> Hand = new List<Card>();
    public readonly List<Card> Properties = new List<Card>();

    private int maxHP;

    public Character(Role role, CharacterName characterName)
    {
        Role = role;
        CharacterName = characterName;
        HP = GetHPCharacter(characterName);
        HP += role == Role.Sheriff ? 1 : 0;
        maxHP = HP;
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
        for(int i = 0; i < length; i++)
        {
            result += "\n\t" + Hand[i];
        }
        result += "\nProperties: ";
        length = Properties.Count;
        for(int i = 0; i < length; i++)
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
