using UnityEngine;

public class MapEnemyData : MonoBehaviour
{
    [SerializeField]
    private EnemyName _enemyName = default;
    public EnemyName enemyName => _enemyName;

    [SerializeField]
    private Direction4Type _direction = Direction4Type.Down;
    public Direction4Type direction => _direction;

    public Vector3 positon => transform.position;

#if UNITY_EDITOR
    [SerializeField]
    private EnemyLibrary enemyslib = default;

    private void Update()
    {
        MapEnemyData data = GetComponent<MapEnemyData>();

        switch (data.direction)
        {
            case Direction4Type.Up:
                ChangeSprite(8, false);
                break;
            case Direction4Type.Right:
                ChangeSprite(4, true);
                break;
            case Direction4Type.Down:
                ChangeSprite(0, false);
                break;
            case Direction4Type.Left:
                ChangeSprite(4, false);
                break;
        }

        void ChangeSprite(int spriteIndex, bool flip)
        {
            GetComponent<SpriteRenderer>().sprite = enemyslib.enemys[(int)enemyName].view.GetSimbolSprite();
            GetComponent<SpriteRenderer>().flipX = flip;
        }
    }
#endif
}