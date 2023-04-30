using UnityEngine;

// マップ上で動くユニットの基礎構造
public class Unit : MassObject
{
    [SerializeField]
    private float accele = 1.0f;
    [SerializeField]
    private float friction = 0.9f;
    [SerializeField]
    private int collisionSize = 16;

    private float speed = 0;

    public UnitType unitType { get; protected set; }
    public TeamID teamID { get; protected set; }

    public BattleLogic battle { get; protected set; }
    public Weapon weapon { get; private set; }
    public UnitView view { get; protected set; }

    public IUnitcontroller controller;

    public DirectionControl direction { get; protected set; }
    public void SetDirection(Direction4Type d) { direction.SetDirection(d); }

    private int deadWait = 30;
    private int deadCount = 0;

    public int attackProgress { get; private set; }
    public int attackTotalFrame { get; private set; }

    public bool isStealth { get; set; }
    public bool isDead => state == State.Dead;

    public State state { get; private set; }
    public enum State
    {
        Move,
        Attack,
        Dead,
    }

    public void Initialize(UnitType unitType, TeamID teamID)
    {
        base.Initialize();

        this.unitType = unitType;
        this.teamID = teamID;

        view = GetComponent<UnitView>();
        view.Initialize();

        direction = new DirectionControl();

        battle = new BattleLogic(unitType);
        battle.lifeLogic.OnLifeUpdate += (int life, int max) => view.lifeGauge.SetLife(life, max);

        collision.SetExtends(collisionSize / 2);

        state = State.Move;
    }

    public void SetController(IUnitcontroller controller)
    {
        this.controller = controller;
        controller.SetUnit(this);
    }

    public void SetWeapon(Weapon weapon)
    {
        if (this.weapon != null)
            Destroy(this.weapon.gameObject);

        this.weapon = weapon;
        weapon.transform.parent = transform;
        weapon.transform.localPosition = new Vector3();
    }

    public void SetVector(Vector2 vector)
    {
        if (isDead) return;
        if (vector.x == 0 && vector.y == 0) return;
        direction.SetLooking(vector.x, vector.y);
        _vector = vector;
        speed += accele;
    }

    public override void Execute()
    {
        if (isDestroy) return;
        base.Execute();

        if (controller != null) controller.Execute();

        x += (_vector.x * speed) + _force.x;
        y += (_vector.y * speed) + _force.y;
        speed *= friction;
        _force *= friction;

        z = y;

        direction.SetMoving(x, y);

        switch (state)
        {
            case State.Move:
                view.WalkAnimUpdate(this);
                break;
            case State.Attack:
                attackProgress++;
                if (attackTotalFrame <= attackProgress)
                    state = State.Move;
                view.AttackAnimUpdate(this, attackProgress, attackTotalFrame);
                weapon.AnimationUpdate(state == State.Attack, direction.direction4, attackProgress);
                break;
            case State.Dead:
                deadCount++;
                if (deadWait < deadCount)
                {
                    // ObjectDestroy();
                    if (OnDead != null) OnDead(this);
                }
                break;
        }

        battle.Execute();

        // render
        view.ObjectUpdate();
        view.Draw();

    }

    public void Attack(Vector2 vector)
    {
        if (isDead) return;
        direction.SetLooking(vector.x, vector.y);

        attackTotalFrame = weapon.totalframe;
        attackProgress = 0;
        state = State.Attack;

        weapon.StartAnimation();
    }

    public void CancelAttack()
    {
        weapon.StopAnimation();
    }

    public virtual void LifeZero()
    {
        if (isDead) return;
        state = State.Dead;

        // 死んだ表示。
        view.Dead();

        collision.objectCollisionDisabled = true;
        CancelAttack();
    }

    public AttackData GetAttackData()
    {
        AttackData ad = new AttackData();
        ad.position = position;
        ad.power = GetAttackPower(); // ここ武器が考慮にないな。
        ad.weapon = weapon.weaponData;
        return ad;
    }

    public int GetAttackPower()
    {
        // 武器攻撃力と、所有者のステータスから算出する。あとで。
        return battle.GetAttackPower();
    }

    public delegate void DeadDelegate(Unit unit);
    public DeadDelegate OnDead;

    // CollisionHitEvent -----
    public void HitAttack(AttackData attack)
    {
        battle.AddDamaeEvent(attack);
    }

    public void ExecuteEvent()
    {
        DamageResult result = battle.DamageExecute(position);

        if (result.damage <= 0) return;

        if (battle.lifeLogic.isZeroLife)
            LifeZero();

        view.Flash(Color.red, 4);
        sound.PlayOneShotOnChannel(0, SeType.Attack, 0.5f);

        // Vector3 mp = Calculate.MiddlePosition(attack.position, position);
        // creater.Create(ObjectName.EffectBattleEffect, mp);

        // knock back
        float refrectionPower = Mathf.Clamp((result.damage / data.knockBackDamageGain), 0.1f, data.knockBackMaxPower) + data.knockBackPowerOffset;
        _force = result.vector * refrectionPower;
    }
}
