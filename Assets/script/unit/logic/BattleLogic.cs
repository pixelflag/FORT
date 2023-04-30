using System.Collections.Generic;
using UnityEngine;

public class BattleLogic : DI
{
    public UnitType type { get; private set; }

    // 武器の影響を取り込む
    public LifeLogic lifeLogic { get; private set; }

    private int damageInvisibleFrame = 10; // マスターデータ参照へ
    protected List<DamageEvent> damageEvents;

    public BattleLogic(UnitType type)
    {
        this.type = type;

        lifeLogic = new LifeLogic();
        lifeLogic.SetMaxlife(data.GetUnitData(type).maxLife);
        lifeLogic.FullRecovery();

        damageEvents = new List<DamageEvent>();
    }

    public int GetAttackPower() => data.GetUnitData(type).attack;

    public void Execute()
    {
        for (int i = damageEvents.Count - 1; 0 <= i; i--)
        {
            damageEvents[i].CountDown();
            if (damageEvents[i].isZeroWait)
                damageEvents.RemoveAt(i);
        }
    }

    public void AddDamaeEvent(AttackData attack)
    {
        if(damageEvents.Count == 0)
        {
            damageEvents.Add(new DamageEvent(attack.id, damageInvisibleFrame, attack));
        }
        else
        {
            for(int i=0; i < damageEvents.Count; i++)
            {
                if (damageEvents[i].id != attack.id)
                    damageEvents.Add(new DamageEvent(attack.id, damageInvisibleFrame, attack));
            }
        }
    }

    public DamageResult DamageExecute(Vector3 position)
    {
        var dr = new DamageResult();

        for (int i = 0; i < damageEvents.Count; i++)
        {
            if (damageEvents[i].isEndDamage) continue;

            dr.damage += damageEvents[i].attack.power;
            dr.vector += Calculate.PositionToNomaliseVector(damageEvents[i].attack.position, position);
            damageEvents[i].EndDamage();
        }

        lifeLogic.SetLife(lifeLogic.current - dr.damage);
        return dr;
    }
}

public class DamageEvent
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
        wait = wait - 1;
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