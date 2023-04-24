public class LifeLogic
{
    // Œ»Ý’l
    public int current { get; private set; }
    // Å‘å’l
    public int max { get; private set; }
    // ‰ñ•œ—¦
    public float healGain { get; private set; }

    private int autoHealPower = 30;
    private int healWait = 180;
    private int healCount = 0;

    public bool isFullLife { get { return current >= max; } }
    public bool isZeroLife { get { return current <= 0; } }

    public void SetData(int max, float healGain)
    {
        this.max = max;
        this.healGain = healGain;
    }

    public void SetLife(int life)
    {
        life = life < 0 ? 0 : life;
        current = max < life ? max : life;
    }

    public bool HealProgress()
    {
        healCount--;
        if (healCount < 0)
        {
            ResetHealCount();
            Heal(autoHealPower);
        }
        return isFullLife;
    }

    public void Heal(int power)
    {
        int result = (int)(current + power * healGain);
        current = max < result ? max : result;
        if (OnLifeUpdate != null) OnLifeUpdate(current, max);
    }

    public void FullRecovery()
    {
        current = max;
        if (OnLifeUpdate != null) OnLifeUpdate(current, max);
    }

    public void ResetHealCount()
    {
        healCount = healWait;
    }

    public delegate void LifeUpdateDelegate(int life, int maxLife);
    public LifeUpdateDelegate OnLifeUpdate;
}