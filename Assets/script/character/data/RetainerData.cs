public struct RetainerData
{
    public int maxLife;
    public int attack;
    public int weight;
    public float healGain;
    public int nextExp;

    public RetainerData(int maxLife, int attack, int weight, float healGain, int nextExp)
    {
        this.maxLife = maxLife;
        this.attack = attack;
        this.weight = weight;
        this.healGain = healGain;
        this.nextExp = nextExp;
    }
}
