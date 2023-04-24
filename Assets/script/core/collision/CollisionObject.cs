using UnityEngine;

public class CollisionObject
{
    // オブジェクト同士のコリジョンを無効にする
    public bool objectCollisionDisabled = false;
    // 地形のコリジョンを無効にする
    public bool mapCollisionDisabled = false;

    public Transform transform { get; private set; }
    private int extends = 8;

    public Vector3 position { get { return transform.position; } }

    public CollisionObject(Transform transform)
    {
        this.transform = transform;
    }

    public void ResetObject()
    {
        objectCollisionDisabled = false;
        mapCollisionDisabled = false;
    }

    public Box GetBox()
    {
        return new Box(transform.position,extends,extends);
    }

    public int GetRadius()
    {
        return extends;
    }

    public void SetExtends(int extends)
    {
        this.extends = extends;
    }
}