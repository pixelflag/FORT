using UnityEngine;

[ExecuteAlways]
public class MapEntranceData : PixelObject
{
    [SerializeField]
    private TeamID _teamID;
    public TeamID teamID => _teamID;

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
        switch (_teamID)
        {
            case TeamID.A:
                render.color = new Color(0.8f ,0.1f, 0);
                break;
            case TeamID.B:
                render.color = new Color(0, 0.1f, 0.8f);
                break;
            case TeamID.Neutral:
                render.color = new Color(0.5f, 0.5f, 0.5f);
                break;
        }
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