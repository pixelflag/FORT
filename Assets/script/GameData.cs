public class GameData
{
    public static GameData instance;

    public UnitData[] unitData;
    public UnitData GetUnitData (UnitType type) => unitData[(int)type];

    public WeaponData[] weaponData;

    public GameData()
    {
        instance = this;

        unitData = new UnitData[]
        {
            // HP, AT, DF
            new UnitData(100,10,10),
            new UnitData(100,10,10),
            new UnitData(100,10,10),
            new UnitData(100,10,10),
            new UnitData(100,10,10),
            new UnitData(100,10,10),
            new UnitData(100,10,10),
            new UnitData(100,10,10),
        };

        weaponData = new WeaponData[]
        {
            // name, Type, skin, element, power
            new WeaponData("Sword",   WeaponType.Sword,   0, ElementType.None, 10,false),
            new WeaponData("Spear",   WeaponType.Spear,   0, ElementType.None, 10,false),
            new WeaponData("Bow",     WeaponType.Bow,     0, ElementType.None, 10,true),
            new WeaponData("FireRod", WeaponType.Rod,     0, ElementType.Fire, 10,true),
            new WeaponData("IceRod",  WeaponType.Rod,     1, ElementType.Ice,  10,true),
            new WeaponData("Axe",     WeaponType.Axe,     0, ElementType.None, 10,false),
            new WeaponData("Hammer",  WeaponType.Hammer,  0, ElementType.None, 10,false),
            new WeaponData("Sickle",  WeaponType.Sickle,  0, ElementType.None, 10,false),
        };
    }
}