using UnityEngine;
using UnityEngine.Tilemaps;

public static class FieldMapUtility
{
    public static int GetCellIndex(Vector3Int location, Vector2Int areaSize)
    {
        return location.y * areaSize.x + location.x;
    }

    public static bool AskFlipX(Tilemap map, Vector3Int location )
    {
        Matrix4x4 matrix = map.GetTransformMatrix(location);
        return matrix[0, 0] < 0;
    }

    public static bool AskFlipY(Tilemap map, Vector3Int location)
    {
        Matrix4x4 matrix = map.GetTransformMatrix(location);
        return matrix[1, 1] < 0;
    }
}
