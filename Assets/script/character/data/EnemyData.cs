public struct EnemyData
{
    public int maxLife;
    public int attack;
    public int weight;
    public int attackWait;
    public int exp;
    public float accele;

    public EnemyData(int maxLife, int attack, int weight, int attackWait, int exp, float accele)
    {
        this.maxLife = maxLife;
        this.attack = attack;
        this.weight = weight;
        this.attackWait = attackWait;
        this.exp = exp;
        this.accele = accele;
    }
}
