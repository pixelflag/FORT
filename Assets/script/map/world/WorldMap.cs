using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldMap : PixelObject, ICellMap
{
    private WorldMapData mapData;
    public WorldMapName mapName => mapData.mapName;
    public Vector2Int areaSize => mapData.areaSize;

    private Tilemap map;

    private WorldCell[,] cells;
    public ICell GetCell(Vector2Int location) => cells[location.x, location.y];

    public WorldMapCastle[] castle;

    public void Initialize(WorldMapData mapData)
    {
        this.mapData = mapData;

        Grid grid = gameObject.AddComponent<Grid>();
        grid.cellSize = new Vector3(16, 16, 0);

        cells = new WorldCell[areaSize.x, areaSize.y];

        for (int cy = 0; cy < areaSize.y; cy++)
        {
            for (int cx = 0; cx < areaSize.x; cx++)
            {
                WorldCellData cd = mapData.GetCellData(new Vector2Int(cx, cy));
                int moveCost = data.GetMoveCost(cd.type);
                cells[cx, cy] = new WorldCell(cd, moveCost);
            }
        }

        // Castle
        castle = new WorldMapCastle[mapData.castle.Length];
        for (int i = 0; i < castle.Length; i++)
        {
            castle[i] = creater.CreateWorldCastle(mapData.castle[i], grid.transform);
        }
        map = Instantiate(mapData.map, transform);
    }

    public bool ExistsCellData(Vector2Int location)
    {
        if (location.x < 0 || location.y < 0)
            return false;
        if (areaSize.x <= location.x || areaSize.y <= location.y)
            return false;
        return true;
    }

    public void Execute()
    {
        // Empty
        // アニメーションするかも
    }

}
