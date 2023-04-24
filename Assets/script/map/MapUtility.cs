using UnityEngine;

public class MapUtility : MonoBehaviour
{
    public static Vector2Int PositionToLocation(Vector3 position)
    {
        int x = (int)Mathf.Ceil(position.x / Global.gridSize.x);
        int y = (int)Mathf.Ceil(position.y / Global.gridSize.y)-1;

        return new Vector2Int(x, -y);
    }

    public static Vector3 LocationToPosition(Vector2Int location)
    {
        int x = location.x * Global.gridSize.x;
        int y = location.y * Global.gridSize.y;

        return new Vector3(x, -y, 0);
    }

    public Vector3Int PositionToMapLocation(Vector3 position)
    {
        return new Vector3Int((int)(position.x / Global.gridSize.x), -(int)(position.y / Global.gridSize.y) - 1, 0);
    }
}
