using UnityEngine;

public class Weapon : VPixelObject
{
    public WeaponType type { get; private set; }
    private Unit parent;

    private WeaponSkin skin = default;
    private WMotionBase motion = default;
    public WeaponData weaponData { get; private set; }

    public CollisionObject collision { get; private set; }
    public int totalframe => motion.GetTotalFrame();

    private State state;
    private enum State
    {
        Hide,
        Attack,
    }

    public void Initialize(Unit parent, WeaponData weaponData, WMotionBase motion, WeaponSkin skin)
    {
        if (motion == null) throw new System.Exception("motion is null!!");
        if (skin   == null) throw new System.Exception("skin is null!!");

        this.weaponData = weaponData;
        this.parent = parent;
        this.motion = motion;
        this.skin = skin;

        base.Initialize();
        collision = new CollisionObject(transform, motion.extends);
        collision.objectCollisionDisabled = true;

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
        FireData fireData = new FireData();
        fireData.fireType = FireType.Fire;
        fireData.teamID = parent.teamID;
        fireData.weapon = weaponData;
        fireData.attackPower = parent.GetAttackPower();
        fireData.direction = parent.direction.direction4;

        creater.CreateFire(fireData, position);
    }
}