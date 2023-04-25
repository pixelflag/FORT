using UnityEngine;

public class FireObject : MassObject
{

    public FireData fireData { get; private set; }

    public virtual void Initialize(FireData fireData)
    {
        base.Initialize();
        this.fireData = fireData;

        switch (fireData.direction)
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
        ad.power = fireData.attackPower;
        ad.weapon = fireData.weapon; // IDでマスターから引っ張るべきものでは？
        return ad;
    }

    public virtual void Hit()
    {
        ObjectDestroy();
    }
}

public struct FireData
{
    public FireType fireType;
    public int teamID;
    public WeaponData weapon;
    public int attackPower;
    public Direction4Type direction;
}

public enum FireType
{
    Fire
}