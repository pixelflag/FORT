using UnityEngine;

public class GameData
{
    public static GameData instance;

    public UnitData[] unitData;
    public UnitData GetUnitData (UnitType type) => unitData[(int)type];

    public WeaponData[] weaponData;

    [SerializeField]
    private int _npcLogicUpdateWait = 10;
    public int npcLogicUpdateWait => _npcLogicUpdateWait;

    [SerializeField]
    private float _knockBackDamageGain = 20.0f;
    public float knockBackDamageGain => _knockBackDamageGain;

    [SerializeField]
    private float _knockBackMaxPower = 3;
    public float knockBackMaxPower => _knockBackMaxPower;

    [SerializeField]
    private float _knockBackPowerOffset = 5;
    public float knockBackPowerOffset => _knockBackPowerOffset;

    [SerializeField]
    private float _npcSearchRadius = 64;
    public float npcSearchRadius => _npcSearchRadius;

    [SerializeField]
    private float _servantSearchRadius = 32;
    public float servantSearchRadius => _servantSearchRadius;

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
            new UnitData(100,10,10),
        };

        weaponData = new WeaponData[]
        {
            // name, Type, skin, element, power
            new WeaponData("Sword",   WeaponType.Sword,   0, ElementType.None, 10, 16, false),
            new WeaponData("Spear",   WeaponType.Spear,   0, ElementType.None, 10, 32, false),
            new WeaponData("Bow",     WeaponType.Bow,     0, ElementType.None, 10, 64, true),
            new WeaponData("FireRod", WeaponType.Rod,     0, ElementType.Fire, 10, 48, true),
            new WeaponData("IceRod",  WeaponType.Rod,     1, ElementType.Ice,  10, 48, true),
            new WeaponData("Axe",     WeaponType.Axe,     0, ElementType.None, 10, 16, false),
            new WeaponData("Hammer",  WeaponType.Hammer,  0, ElementType.None, 10, 16, false),
            new WeaponData("Sickle",  WeaponType.Sickle,  0, ElementType.None, 10, 16, false),
        };
    }
}