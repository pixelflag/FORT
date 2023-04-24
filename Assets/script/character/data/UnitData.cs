public class UnitData
{
    public BattleParameters[] playerLevelData;
    public BattleParameters[] buddiesLevelData;
    public BattleParameters[] enemyLevelData;
    public WeaponParameters[] weaponData;

    public UnitData()
    {
        playerLevelData = new BattleParameters[]
        {
            // HP, AT, EXP
            new BattleParameters(300,50,50,1000),
        };

        buddiesLevelData = new BattleParameters[]
        {
            // HP, AT, EXP
            new BattleParameters(100, 20, 20, 1000),
        };

        enemyLevelData = new BattleParameters[]
        {
            // HP, AT, EXP
            new BattleParameters(100,10,10,10),
            new BattleParameters(100,10,10,10),
            new BattleParameters(100,10,10,10),
            new BattleParameters(100,10,10,10),
            new BattleParameters(100,10,10,10),
            new BattleParameters(100,10,10,10),
            new BattleParameters(100,10,10,10),
            new BattleParameters(100,10,10,10),
            new BattleParameters(100,10,10,10),
            new BattleParameters(100,10,10,10),
            new BattleParameters(100,10,10,10),
            new BattleParameters(100,10,10,10),
            new BattleParameters(100,10,10,10),
            new BattleParameters(100,10,10,10),
            new BattleParameters(100,10,10,10),
            new BattleParameters(100,10,10,10),
        };

        weaponData = new WeaponParameters[]
        {
            // name, Type, skin, element, power
            new WeaponParameters("Sword",   WeaponType.Sword,   0, ElementType.None, 10,false),
            new WeaponParameters("Spear",   WeaponType.Spear,   0, ElementType.None, 10,false),
            new WeaponParameters("Bow",     WeaponType.Bow,     0, ElementType.None, 10,true),
            new WeaponParameters("FireRod", WeaponType.Rod,     0, ElementType.Fire, 10,true),
            new WeaponParameters("IceRod",  WeaponType.Rod,     1, ElementType.Ice,  10,true),
            new WeaponParameters("Axe",     WeaponType.Axe,     0, ElementType.None, 10,false),
            new WeaponParameters("Hammer",  WeaponType.Hammer,  0, ElementType.None, 10,false),
            new WeaponParameters("Sickle",  WeaponType.Sickle,  0, ElementType.None, 10,false),
        };
    }
}

// バトルシステムの詳細はこれから考える。

// 戦闘共通パラメータ
public struct BattleParameters
{
    public int maxLife;
    public int attack;
    public int defense;
    public int exp;

    public BattleParameters(int maxLife, int attack, int defense, int exp)
    {
        this.maxLife = maxLife;
        this.attack = attack;
        this.defense = defense;
        this.exp = exp;
    }
}

// 武器データ
public struct WeaponParameters
{
    public string name;
    public WeaponType type;
    public int skinID;
    public ElementType element;
    public int power;
    public bool isFire;

    public WeaponParameters(string name, WeaponType type, int skinID, ElementType element, int power, bool isFire)
    {
        this.name = name;
        this.type = type;
        this.skinID = skinID;
        this.element = element;
        this.power = power;
        this.isFire = isFire;
    }
}

public enum WeaponType
{
    None,
    Sword,
    Spear,
    Bow,
    Rod,
    Axe,
    Hammer,
    Sickle
}

public enum ElementType
{
    None,
    Fire,
    Ice,
    Thunder,
    Stone,
    Wind,
    Water,
    Holy,
    Dark,
}