using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassObject : PixelObject
{
    public ObjectType objectType { get; protected set; }

    public CollisionObject collision { get; set; }

    // 自身のベクトル
    protected Vector2 _speed = new Vector2();
    public Vector2 speed { get { return _speed; } }

    // 外的なベクトル
    protected Vector2 _force = new Vector2();
    public Vector2 force { get { return _force; } }

    public Vector3 prevPosition { get; private set; }
    public bool isStop{ get { return (speed.x == 0 && speed.y == 0); } }
    protected bool prevStop;

    public bool isDestroy { get; private set; }
    
    public virtual void Initialize()
    {
        collision = new CollisionObject(transform);
    }

    public virtual void ResetObject()
    {
        _speed.x = 0;
        _speed.y = 0;

        _force.x = 0;
        _force.y = 0;

        destroyDelegate = null;
        collision.ResetObject();
        isDestroy = false;
    }

    public virtual void Execute()
    {
        prevPosition = position;
        prevStop = isStop;
    }

    public virtual void Hold()
    {
        _speed.x = 0;
        _speed.y = 0;
    }

    public virtual void ObjectDestroy()
    {
        isDestroy = true;
        if (destroyDelegate != null) destroyDelegate(this);
    }

    // delegate -----
    public delegate void DestroyDelegate(MassObject obj);
    public DestroyDelegate destroyDelegate;
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

