using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    [SerializeField]
    private int type = 0;   // 0.Enemy, 1.Statue
    [SerializeField]
    private Sprite[] sprites = default;
    [SerializeField]
    private int level;

    private void OnValidate()
    {
        level = level < sprites.Length ? level : sprites.Length - 1;
        level = level < 0 ? 0 : level;

        GetComponent<SpriteRenderer>().sprite = sprites[level];
    }

    public MapObjectData GetData()
    {
        return new MapObjectData(type, level, transform.position);
    }
}

public struct MapObjectData
{
    public int type;
    public int level;
    public Vector3 position;

    public MapObjectData(int type, int level, Vector3 position)
    {
        this.type = type;
        this.level = level;
        this.position = position;
    }
}
