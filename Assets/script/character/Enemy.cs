using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ロジックベースのほうと役割が重複しているところがある。
public class Enemy : MassObject
{
    [SerializeField]
    private EnemyName enemyName;

    public float accele = 0.3f;
    [SerializeField]
    private float friction = 0.2f;
    [SerializeField]
    private int collisionSize = 16;

    public EnemyView view { get; private set; }
    public DirectionControl direction { get; private set; }

    private EnemyLogic logic;
    public LifeLogic lifeLogic { get; private set; }
    protected List<DamageEvent> damageEvents;
    public bool isDead { get; set; }

    private int damageInvisibleFrame = 10;

    public bool isStealth { get; set; }

    public void Initialize(BattleParameters battleData, EnemyLogic logic, uint seed)
    {
        base.Initialize();
        objectType = ObjectType.Enemy;

        this.logic = logic;
        logic.Initialize(this, battleData, seed);
        collision.SetExtends(collisionSize / 2);

        lifeLogic = new LifeLogic();
        lifeLogic.FullRecovery();

        damageEvents = new List<DamageEvent>();
    }

    public override void Execute()
    {
        if (logic != null)
            logic.Execute();
        base.Execute();
        view.ObjectUpdate();

        for (int i = damageEvents.Count - 1; i < 0; i--)
        {
            damageEvents[i].CountDown();
            if (damageEvents[i].isZeroWait)
                damageEvents.RemoveAt(i);
        }
    }

    public AttackData GetAttackData()
    {
        AttackData ad = new AttackData();
        ad.position = position;
        ad.power = logic.GetAttack();
        return ad;
    }

    public void HitAttack(AttackData attack)
    {
        foreach (DamageEvent de in damageEvents)
        {
            if (de.id != attack.id)
                damageEvents.Add(new DamageEvent(attack.id, damageInvisibleFrame, attack ));
        }
    }

    public void EventExecute()
    {
        int totalDamage = 0;
        Vector3 totalVector = new Vector3();

        foreach (DamageEvent ev in damageEvents)
        {
            totalDamage += logic.Damage(ev.attack.power);
            totalVector += Calculate.PositionToNomaliseVector(ev.attack.position, position);
            ev.EndDamage();
            logic.CancelMove();
        }
        lifeLogic.SetLife(lifeLogic.current - totalDamage);
        if (lifeLogic.isZeroLife)
            LifeZero();

        // ダメージの表現方法もあとで考える。
        // 表示関係はこのクラスの役割ではない。
        view.Flash(Color.red, 4);
        sound.PlayOneShotOnChannel(0, SeType.Attack, 0.5f);

        // Vector3 mp = Calculate.MiddlePosition(attack.position, position);
        // creater.Create(ObjectName.EffectBattleEffect, mp);

        // knock back
        int dist = totalDamage - 10; // ノックバックの仕組みを考え直す
        dist = dist < 0 ? 0 : dist;
        int refrectionPower = dist / 50 + 5;
        refrectionPower = refrectionPower > 8 ? 8 : refrectionPower;
        _force = totalVector * refrectionPower;
    }

    public virtual void LifeZero()
    {
        if (isDead) return;
        isDead = true;

        collision.objectCollisionDisabled = true;
        logic.CancelMove();
    }

    public delegate void EnemyDeadDelegate(Enemy enemy);
    public EnemyDeadDelegate OnEnemyDead;
}

public struct DamageEvent
{
    public int id;
    public int wait;
    public AttackData attack;
    public bool isEndDamage;
    public bool isZeroWait { get { return wait <= 0; } }

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