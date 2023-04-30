using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FieldMapObject : PixelObject
{
    private FieldMapData mapData;
    public FieldMapName mapName => mapData.mapName;
    public Vector2Int areaSize => mapData.areaSize;

    private Tilemap collisionMap;
    private Tilemap groundMap;

    private Cell[,] cells;
    public Cell GetCell(Vector2Int location) => cells[location.x, location.y];

    public List<AnimationMapSprite> animationMapSprite { get; private set; }
    public void AddAnimationSprite(AnimationMapSprite aSprite) { animationMapSprite.Add(aSprite); }
    public void RemoveAnimationSprite(AnimationMapSprite aSprite) { animationMapSprite.Remove(aSprite); }

    public MapEventObject[] events;
    public MapEntranceObject[] entranceA;
    public MapEntranceObject[] entranceB;

    public void Initialize(FieldMapData mapData)
    {
        this.mapData = mapData;

        Grid grid = gameObject.AddComponent<Grid>();
        grid.cellSize = new Vector3(16, 16, 0);

        cells = new Cell[areaSize.x, areaSize.y];

        for (int cy = 0; cy < areaSize.y; cy++)
        {
            for (int cx = 0; cx < areaSize.x; cx++)
            {
                cells[cx, cy] = new Cell(mapData.GetCellData(new Vector2Int(cx, cy)));

                if (Global.isShowCollision)
                {
                    GameObject obj = DebugUtility.AddBoxView(cells[cx, cy].box);

                    switch (cells[cx, cy].type)
                    {
                        case CellType.None:
                        case CellType.Grass:
                            obj.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.1f, 0.1f, 0.5f);
                            break;
                        case CellType.Collision:
                        case CellType.Wood:
                        case CellType.Rock:
                        case CellType.Fire:
                        case CellType.Ice:
                            obj.GetComponent<SpriteRenderer>().color = new Color(0, 0.5f, 0.5f, 0.5f);
                            break;
                    }
                }
            }
        }

        // EntranceA
        entranceA = new MapEntranceObject[mapData.entranceA.Length];
        for (int i = 0; i < entranceA.Length; i++)
        {
            var ed = mapData.entranceA[i];
            entranceA[i] = new GameObject(ed.name).AddComponent<MapEntranceObject>();
            entranceA[i].Initialize(ed, transform);

            if (Global.isDebugMode)
            {
                GameObject obj = DebugUtility.AddArrowView(ed.direction);
                obj.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.1f, 0);
                obj.transform.parent = entranceA[i].transform;
                obj.transform.localPosition = Vector3.zero;
            }
        }

        // EntranceB
        entranceB = new MapEntranceObject[mapData.entranceB.Length];
        for (int i = 0; i < entranceB.Length; i++)
        {
            var ed = mapData.entranceB[i];
            entranceB[i] = new GameObject(ed.name).AddComponent<MapEntranceObject>();
            entranceB[i].Initialize(ed, transform);

            if (Global.isDebugMode)
            {
                GameObject obj = DebugUtility.AddArrowView(ed.direction);
                obj.GetComponent<SpriteRenderer>().color = new Color(0, 0.1f, 0.8f);
                obj.transform.parent = entranceB[i].transform;
                obj.transform.localPosition = Vector3.zero;
            }
        }


        // Events
        events = new MapEventObject[mapData.events.Length];
        for (int i = 0; i < events.Length; i++)
        {
            events[i] = new GameObject(mapData.events[i].eventName).AddComponent<MapEventObject>();
            events[i].Initialize(mapData.events[i]);

            if (Global.isDebugMode)
            {
                GameObject obj = DebugUtility.AddBoxView(events[i].size);
                obj.transform.parent = events[i].transform;
                obj.transform.localPosition = Vector3.zero;
                obj.GetComponent<SpriteRenderer>().color = new Color(0, 1, 1);
                obj.GetComponent<SpriteRenderer>().size = mapData.events[i].size;
            }
        }

        animationMapSprite = new List<AnimationMapSprite>();

        PRandom random = new PRandom(Global.mapSeed);
        collisionMap = Instantiate(mapData.collisionMap, transform);
        collisionMap.transform.localPosition = new Vector3();

        groundMap = Instantiate(mapData.groundMap, transform);
        groundMap.transform.localPosition = new Vector3();

        for (int cy = 0; cy < cells.GetLength(1); cy++)
        {
            for (int cx = 0; cx < cells.GetLength(0); cx++)
            {
                Cell cell = cells[cx, cy];
                if (cell.data.broken.b)
                {
                    cell.OnBroken += () =>
                    {
                        // êVÇΩÇ…âÛÇÍÇÈèàóùÇçÏÇËíºÇ≥ÇÀÇŒÇ»ÇÁÇ»Ç¢ÅB
                        /*
                        collisionSprites[cx, cy].render.sprite = null;
                        var bData = creater.GetMapSpriteLibrary().GetBrokenSpriteData(cell.data.broken.i);
                        groundSprites[cx, cy].render.sprite = bData.brokenSprite;
                        */
                    };
                }
            }
        }
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
        for (int i = 0; i < animationMapSprite.Count; i++)
            animationMapSprite[i].Execute();
    }

    public void CheckDestroy()
    {
        // Empty
    }

    // ----------

    public MapEntranceObject GetEntranceA(int index)
    {
        return entranceA[index];
    }

    public MapEntranceObject GetEntranceB(int index)
    {
        return entranceB[index];
    }
}