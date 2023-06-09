using UnityEngine;

public class LifeLogic
{
    // 現在値
    public int current { get; private set; }
    // 最大値
    public int max { get; private set; }

    public bool isFullLife => current >= max;
    public bool isZeroLife => current <= 0;

    public void SetMaxlife(int max)
    {
        this.max = max;
    }

    public void SetLife(int life)
    {
        current = Mathf.Clamp(life, 0, max);
    }

    public void FullRecovery()
    {
        current = max;
        if (OnLifeUpdate != null) OnLifeUpdate(current, max);
    }

    public delegate void LifeUpdateDelegate(int life, int maxLife);
    public LifeUpdateDelegate OnLifeUpdate;
}