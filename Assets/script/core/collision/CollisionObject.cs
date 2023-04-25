using UnityEngine;

public class CollisionObject
{
    // オブジェクト同士のコリジョンを無効にする
    public bool objectCollisionDisabled = false;
    // 地形のコリジョンを無効にする
    public bool mapCollisionDisabled = false;

    private Transform transform;
    public Vector3 position => transform.position;
    private int extends = 8;

    public Box box => new Box(transform.position, extends, extends);
    public int radius => extends;

    public CollisionObject(Transform transform, int extends)
    {
        this.transform = transform;
        this.extends = extends;
    }

    public void SetExtends(int extends)
    {
        this.extends = extends;
    }
}