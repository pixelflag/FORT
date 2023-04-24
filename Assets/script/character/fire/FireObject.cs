using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObject : MassObject
{
    public ObjectType type { get; private set; }
    public WeaponParameters weapon { get; private set; }
    public int attackPower { get; private set; }
    public Direction4Type direction { get; private set; }

    public virtual void Initialize(ObjectType type, WeaponParameters weapon, Direction4Type direction, int attackPower)
    {
        base.Initialize();
        this.type = type;
        this.weapon = weapon;
        this.attackPower = attackPower;
        this.direction = direction;

        switch (direction)
        {
            case Direction4Type.Up:
                _force = new Vector2( 0, 1);
                break;
            case Direction4Type.Right:
                _force = new Vector2( 1, 0);
                break;
            case Direction4Type.Down:
                _force = new Vector2( 0,-1);
                break;
            case Direction4Type.Left:
                _force = new Vector2(-1, 0);
                break;
        }
    }

    public AttackData GetAttackData()
    {
        AttackData ad = new AttackData();
        ad.position = position;
        ad.power = attackPower;
        ad.weapon = weapon;
        return ad;
    }

    public virtual void Hit()
    {
        ObjectDestroy();
    }
}