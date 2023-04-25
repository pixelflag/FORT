// UNIT共通パラメータ
public struct UnitData
{
    public int maxLife;
    public int attack;
    public int defense;

    public UnitData(int maxLife, int attack, int defense)
    {
        this.maxLife = maxLife;
        this.attack = attack;
        this.defense = defense;
    }
}

public enum UnitType
{
    Knight,
    Soldier,
    Elf,
    RedWizard,
    BlueWizard,
    Lumberjack,
    Dwarves,
    Thief,
}
