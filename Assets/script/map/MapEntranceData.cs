using UnityEngine;

[ExecuteAlways]
public class MapEntranceData : PixelObject
{
    [SerializeField]
    private Direction4Type _direction;
    public Direction4Type direction => _direction;

#if UNITY_EDITOR
    [SerializeField]
    private Sprite[] sprites = default;

    private void Update()
    {
        if (Application.isPlaying) return;

        SpriteRenderer render = GetComponent<SpriteRenderer>();
        render.sprite = sprites[(int)direction];
    }
#endif
}

public class MapEntranceObject : PixelObject
{
    public Direction4Type direction;

    public void Initialize(MapEntranceData data, Transform parent)
    {
        transform.parent = parent;

        this.position = data.position;
        this.direction = data.direction;
    }
}