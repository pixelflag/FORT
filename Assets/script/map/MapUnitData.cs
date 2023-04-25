using UnityEngine;

public class MapUnitData : MonoBehaviour
{
    [SerializeField]
    private UnitType _unitType = default;
    public UnitType unitType => _unitType;

    [SerializeField]
    private int _teamID = default;
    public int teamID => _teamID;

    [SerializeField]
    private Direction4Type _direction = Direction4Type.Down;
    public Direction4Type direction => _direction;

    public Vector3 positon => transform.position;

#if UNITY_EDITOR
    [SerializeField]
    private UnitSkinLibrary unitLib = default;

    private void Update()
    {
        MapUnitData data = GetComponent<MapUnitData>();

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
            GetComponent<SpriteRenderer>().sprite = unitLib.Get((int)_unitType).GetSimbolSprite();
            GetComponent<SpriteRenderer>().flipX = flip;
        }
    }
#endif
}