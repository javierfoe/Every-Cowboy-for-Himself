using System.Collections.Generic;

public class Character
{
    public const int Amount = 48;

    public Role Role { get; private set; }
    public CharacterName CharacterName { get; private set; }
    public int HP { get; private set; }
    public readonly List<Card> Hand = new List<Card>();
    public readonly List<Card> Properties = new List<Card>();

    public Character(Role role, CharacterName characterName)
    {
        HP = GetHPCharacter(characterName);
        HP += role == Role.Sheriff ? 1 : 0;
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
