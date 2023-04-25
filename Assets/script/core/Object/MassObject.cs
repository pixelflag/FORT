using UnityEngine;

public class MassObject : PixelObject
{
    public CollisionObject collision { get; set; }

    // 自身のベクトル
    protected Vector2 _speed = new Vector2();
    public Vector2 speed => _speed;

    // 外的なベクトル
    protected Vector2 _force = new Vector2();
    public Vector2 force => _force;

    public Vector3 prevPosition { get; private set; }
    public bool isStop => (speed.x == 0 && speed.y == 0);

    public bool isDestroy { get; private set; }
    
    public virtual void Initialize()
    {
        collision = new CollisionObject(transform, 8);
    }

    public virtual void Execute()
    {
        prevPosition = position;
    }

    public virtual void ObjectDestroy()
    {
        isDestroy = true;
        if (OnDestroy != null) OnDestroy(this);
    }

    // delegate -----
    public delegate void DestroyDelegate(MassObject obj);
    public DestroyDelegate OnDestroy;
}

public enum ObjectType
{
    Player,
    Buddies,
    Enemy,
    Item,
    Effect,
    Fire,
}

