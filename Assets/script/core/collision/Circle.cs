using UnityEngine;

public struct Circle
{
    public Vector3 position;
    public float radius;

    public float x
    {
        get { return position.x; }
        set { position.x = value; }
    }
    public float y
    {
        get { return position.y; }
        set { position.y = value; }
    }

    public void Inisialize(Vector3 position, float radius)
    {
        this.position = position;
        this.radius = radius;
    }

    public float top { get { return position.y + radius; } }
    public float right { get { return position.x + radius; } }
    public float bottom { get { return position.y - radius; } }
    public float left { get { return position.x - radius; } }
}
