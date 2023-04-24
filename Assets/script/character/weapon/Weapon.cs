using UnityEngine;

public class Weapon : VPixelObject
{
    private Character master;
    private WeaponSkin skin = default;
    private WMotionBase motion = default;
    public WeaponParameters weaponData { get; private set; }

    public WeaponType type { get; private set; }
    public CollisionObject collision { get; private set; }
    public int totalFrame { get { return motion.GetTotalFrame(); } }

    private State state;
    private enum State
    {
        Hide,
        Attack,
    }

    public void Initialize(Character master, WeaponParameters weaponData, WMotionBase motion, WeaponSkin skin)
    {
        if (motion == null) throw new System.Exception("motion is null!!");
        if (skin == null) throw new System.Exception("skin is null!!");

        this.weaponData = weaponData;
        this.master = master;
        this.motion = motion;
        this.skin = skin;

        base.Initialize();
        collision = new CollisionObject(transform);
        collision.objectCollisionDisabled = true;
        collision.SetExtends(motion.extends);

        spriteRenderer.sprite = null;
    }

    public void StartAnimation()
    {
        collision.objectCollisionDisabled = motion.collisionDisable;
        state = State.Attack;
    }

    public void StopAnimation()
    {
        spriteRenderer.sprite = null;
        transform.localPosition = new Vector3();

        collision.objectCollisionDisabled = true;
        state = State.Hide;
    }

    public void AnimationUpdate(bool isAttack, Direction4Type direction4, int progress)
    {
        switch (state)
        {
            case State.Hide:
                break;
            case State.Attack:
                if (isAttack)
                {
                    MotionData d = motion.GetMotion(direction4, progress);
                    spriteRenderer.sprite = skin.GetSprite(d.spriteIndex);
                    spriteRenderer.flipX = d.flipX;
                    spriteRenderer.flipY = d.flipY;
                    _transform.localPosition = d.localPositon;

                    if (weaponData.isFire && progress == motion.fireFrame)
                        SpawnFire();
                }
                else
                {
                    StopAnimation();
                }
                break;
        }
    }

    private void SpawnFire()
    {
        Vector3 position = transform.position;
        int attack = master.GetAttackPower();
        Direction4Type direction = master.direction.direction4;
        creater.CreateFire(master.objectType, weaponData, position, direction, attack);
    }
}