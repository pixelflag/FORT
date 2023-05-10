using UnityEngine;

public class Cell: ICell
{
    public FieldCellData data;
    public CellScore routeScore { get; set; }
    private Box _box;
    public Box box => _box;
    public Vector2Int location => data.location;

    public bool canBreak => data.broken.b;

    public bool isBroken { get; private set; }
    public void Broken()
    {
        isBroken = true;
        routeScore.Cscore = 0;
        if (OnBroken != null) OnBroken();
    }

    public bool isGround
    {
        get
        {
            switch (type)
            {
                case CellType.None:
                case CellType.Grass:
                    return true;
            }
            return false;
        }
    }

    public bool isCollision
    {
        get
        {
            switch (type)
            {
                case CellType.None:
                case CellType.Grass:
                    return false;
                case CellType.Collision:
                    return true;
                case CellType.Wood:
                case CellType.Rock:
                case CellType.Fire:
                case CellType.Ice:
                    if (isBroken)
                        return false;
                    else
                        return true;
            }
            return false;
        }
    }

    public CellType type
    {
        get { return data.type; }
        set
        {
            data.type = value;
            switch (value)
            {
                case CellType.Collision:
                case CellType.None:
                    routeScore.Cscore = 10000;
                    break;
                default:
                    routeScore.Cscore = 0;
                    break;
            }
        }
    }

    public Cell(FieldCellData cellData)
    {
        this.data = cellData;

        Vector2Int extends = (Global.gridSize / 2);
        int xx = data.location.x * Global.gridSize.x + extends.x;
        int yy = data.location.y * Global.gridSize.y + extends.y;
        Vector3 localPosition = new Vector3Int(xx, -yy, 0);

        _box = new Box(localPosition, extends.x, extends.y);
        routeScore = new CellScore(localPosition, data.location);
    }

    public delegate void CellBrokenDelegate();
    public CellBrokenDelegate OnBroken;
}

public interface ICell
{
    bool isCollision { get; }
    CellScore routeScore { get; }
    Vector2Int location { get; }
}
