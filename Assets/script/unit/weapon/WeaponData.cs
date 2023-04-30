public struct WeaponData
{
    public string name;
    public WeaponType type;
    public int skinID;
    public ElementType element;
    public int power;
    public int attackRange;
    public bool isFire;

    public WeaponData(
        string name, WeaponType type, int skinID, ElementType element,
        int power, int attackRange, bool isFire)
    {
        this.name = name;
        this.type = type;
        this.skinID = skinID;
        this.element = element;
        this.power = power;
        this.attackRange = attackRange;
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