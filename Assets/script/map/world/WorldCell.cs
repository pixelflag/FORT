using UnityEngine;

public class WorldCell: ICell
{
    public WorldCellData data;
    public CellScore routeScore { get; set; }
    private Box _box;
    public Box box => _box;
    public Vector2Int location => data.location;
    public bool isCollision => false;

    public WorldCellType type => data.type;

    public WorldCell(WorldCellData cellData, int moveCost)
    {
        this.data = cellData;

        Vector2Int extends = (Global.gridSize / 2);
        int xx = data.location.x * Global.gridSize.x + extends.x;
        int yy = data.location.y * Global.gridSize.y + extends.y;
        Vector3 localPosition = new Vector3Int(xx, -yy, 0);

        _box = new Box(localPosition, extends.x, extends.y);
        routeScore = new CellScore(localPosition, data.location);
        routeScore.Cscore = moveCost;
    }
}