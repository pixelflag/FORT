using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapUtility
{
    private List<Vector3Int> pickupList;
    private List<Box> pickupBoxList;

    public TileMapUtility()
    {
        pickupList = new List<Vector3Int>();
        pickupBoxList = new List<Box>();
    }

    public List<Vector3Int> GetAroundCellPositions(Tilemap collisionMap, Tile.ColliderType type, Box box)
    {
        Vector3Int maxLocation = collisionMap.WorldToCell(box.topRight);
        Vector3Int minLocation = collisionMap.WorldToCell(box.bottomLeft);
        pickupList.Clear();

        for (int by = minLocation.y-1; by <= maxLocation.y+1; by++)
        {
            for (int bx = minLocation.x-1; bx <= maxLocation.x+1; bx++)
            {
                Vector3Int pos = new Vector3Int(bx, by, 0);
                if (collisionMap.GetColliderType(pos) == type)
                {
                    pickupList.Add(pos);
                }
            }
        }
        return pickupList;
    }

    public List<Box> GetAroundCellBounds(Tilemap collisionMap, Box box)
    {
        int halfCellSize = (int)collisionMap.layoutGrid.cellSize.x/2;

        List<Vector3Int> tilePositions = GetAroundCellPositions(collisionMap, Tile.ColliderType.Sprite, box);
        pickupBoxList.Clear();
        foreach (Vector3Int pos in tilePositions)
        {
            Box resultBox = new Box(collisionMap.GetCellCenterWorld(pos), halfCellSize, halfCellSize);
            pickupBoxList.Add(resultBox);
        }
        return pickupBoxList;
    }
}
