using UnityEngine;

[RequireComponent(typeof(CharacterView))]
public class Character : MassObject
{
    public Weapon weapon { get; protected set; }
    public CharacterView view { get; protected set; }
    public BattleObject battle { get; protected set; }
    public DirectionControl direction { get; protected set; }
    public void SetDirection(Direction4Type d){ direction.SetDirection(d); }

    public float accele = 0.3f;
    [SerializeField]
    private float friction = 0.2f;
    [SerializeField]
    private int collisionSize = 16;

    public bool isDead { get; set; }
    private int deadWait = 180;
    private int deadCount = 0;

    public bool isAttack { get; private set; }
    public int attackProgress { get; private set; }
    public int attackTotalFrame { get; private set; }

    public bool isStealth { get; set; }

    public void Initialize(ObjectType type, BattleParameters[] levelTable)
    {
        base.Initialize();

        this.objectType = type;
        view = GetComponent<CharacterView>();
        view.Initialize();

        direction = new DirectionControl();

        battle = new BattleObject(type, levelTable);
        battle.lifeLogic.OnLifeUpdate += (int life, int max) => view.lifeGauge.SetLife(life, max);
        battle.OnLevelUp += LevelUp;

        collision.SetExtends(collisionSize / 2);
    }

    public void SetSkin(CharacterSkin skin)
    {
        view.SetSkin(skin);
    }

    public void SetWeapon(Weapon weapon)
    {
        if (this.weapon != null)
            Destroy(this.weapon.gameObject);

        this.weapon = weapon;
        weapon.transform.parent = view.GetParent();
        weapon.transform.localPosition = new Vector3();
    }

    public void SetMoveInput(float x, float y)
    {
        if (x == 0 && y == 0) return;

        _speed.x = x;
        _speed.y = y;
        direction.SetGoing(x, y);
    }

    public override void Execute()
    {
        if (isDestroy) return;

        base.Execute();

        x += _speed.x + _force.x;
        y += _speed.y + _force.y;

        _speed.x = 0;
        _speed.y = 0;

        z = y;

        direction.SetMoving(x, y);

        // Dead
        if (isDead == true)
        {
            deadCount++;
            if ( deadWait < deadCount)
            {
                if (OnCharacterDead != null) OnCharacterDead(this);
            }
        }

        // battle
        if (isAttack)
        {
            attackProgress++;
            if (attackTotalFrame <= attackProgress)
                isAttack = false;
        }

        // Heal
        if (isStop == false)
            battle.lifeLogic.ResetHealCount();
        if (prevStop == true && isStop == false)
            if(OnResetHeal != null ) OnResetHeal();

        // render
        view.AnimationUpdate(this, isAttack, attackProgress, attackTotalFrame);
        view.Draw();

        weapon.AnimationUpdate(isAttack, direction.direction4, attackProgress);
    }

    public void Attack()
    {
        if (isDead) return;

        attackTotalFrame = weapon.totalFrame;
        attackProgress = 0;
        isAttack = true;

        weapon.StartAnimation();
    }

    public void CancelAttack()
    {
        isAttack = false;
        weapon.StopAnimation();
    }

    public virtual void LifeZero()
    {
        if (isDead) return;
        isDead = true;

        collision.objectCollisionDisabled = true;
        CancelAttack();
    }

    private void LevelUp(int level, bool isMaxLevel)
    {
        if(isMaxLevel)
            creater.CreateMessageEffect(MessageEffectName.Up, position);
        else
            creater.CreateMessageEffect(MessageEffectName.Smail, position);
        sound.PlayOneShotOnChannel(2, SeType.Levelup, 1);
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

    public delegate void CharacterDeadDelegate(Character obj);
    public CharacterDeadDelegate OnCharacterDead;

    public delegate void CharacterResetHealDelegate();
    public CharacterResetHealDelegate OnResetHeal;

    // CollisionHitEvent -----
    public void HitAttack(AttackData attack)
    {
        // エヴェント処理のように、すべてのExcute実行後にEvent処理フェイズを行うようにする。

        int damage = battle.Damage(attack.power);

        if (battle.lifeLogic.isZeroLife)
            LifeZero();

        view.Flash(Color.red, 4);
        sound.PlayOneShotOnChannel(0, SeType.Attack, 0.5f);

        Vector3 mp = Calculate.MiddlePosition(attack.position, position);
        // creater.Create(ObjectName.EffectBattleEffect, mp);

        // knock back
        Vector3 vector = Calculate.PositionToNomaliseVector(attack.position, position);
        int dist = damage - 10; // ノックバックの仕組みを考え直す
        dist = dist < 0 ? 0 : dist;
        int refrectionPower = dist / 50 + 5;
        refrectionPower = refrectionPower > 8 ? 8 : refrectionPower;
        _force = vector * refrectionPower;

        // 同じ武器が連続して入らないようにするには。
        // 同じ武器で複数人には当たるべき。

        // さっきあたったリストを作る。
        // 武器ごとにそれもってたら、偉い数やで。

        // 対象を倒せたかどうかを渡す必要がある。
        // 死んだときにダメージ量によって経験値を分配するには、攻撃者を記録しておく必要がある。
    }
}