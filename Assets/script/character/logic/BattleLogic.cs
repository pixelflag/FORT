using System.Collections.Generic;
using UnityEngine;

public class BattleLogic : DI
{
    public UnitType type { get; private set; }

    // 武器の影響を取り込む
    public LifeLogic lifeLogic { get; private set; }

    private int damageInvisibleFrame = 10;
    protected List<DamageEvent> damageEvents;

    public BattleLogic(UnitType type)
    {
        this.type = type;

        lifeLogic = new LifeLogic();
        lifeLogic.FullRecovery();

        damageEvents = new List<DamageEvent>();
    }

    public int GetAttackPower() => data.GetUnitData(type).attack;

    public void Execute()
    {
        for (int i = damageEvents.Count - 1; i < 0; i--)
        {
            damageEvents[i].CountDown();
            if (damageEvents[i].isZeroWait)
                damageEvents.RemoveAt(i);
        }
        damageInvisibleFrame--;
    }

    public void AddDamaeEvent(AttackData attack)
    {
        foreach (DamageEvent de in damageEvents)
        {
            if (de.id != attack.id)
                damageEvents.Add(new DamageEvent(attack.id, damageInvisibleFrame, attack));
        }
    }

    public DamageResult DamageExecute(Vector3 position)
    {
        var dr = new DamageResult();

        foreach (DamageEvent ev in damageEvents)
        {
            dr.damage += ev.attack.power;
            dr.vector += Calculate.PositionToNomaliseVector(ev.attack.position, position);
            ev.EndDamage();
        }

        lifeLogic.SetLife(lifeLogic.current - dr.damage);
        return dr;
    }
}

public struct DamageEvent
{
    public int id;
    public int wait;
    public AttackData attack;
    public bool isEndDamage;
    public bool isZeroWait => wait <= 0;

    public DamageEvent(int id, int wait, AttackData attack)
    {
        this.id = id;
        this.wait = wait;
        this.attack = attack;
        isEndDamage = false;
    }

    public void CountDown()
    {
        wait--;
    }

    public void EndDamage()
    {
        isEndDamage = true;
    }
}

public struct DamageResult
{
    public int damage;
    public Vector3 vector;
}