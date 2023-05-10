using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class WorldMapTileLibrary : MonoBehaviour
{
    [SerializeField]
    private WorldMapTileData[] tiles = default;

    public WorldCellType AskTileType(TileBase tile)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].tile == tile)
            {
                return tiles[i].type;
            }
        }
        throw new Exception("Not found.");
    }
}

[Serializable]
public struct WorldMapTileData
{
    public TileBase tile;
    public WorldCellType type;
}