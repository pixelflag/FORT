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

    public List<Enemy> enemys { get; private set; }
    public void AddEnemy(Enemy enemy) { enemys.Add(enemy); }

    public List<Item> items { get; private set; }
    public void AddItem(Item item) { items.Add(item); }

    public MapEventObject[] events;
    public MapEntranceObject[] entrance;

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

        // Entrance
        entrance = new MapEntranceObject[mapData.entrance.Length];
        for (int i = 0; i < entrance.Length; i++)
        {
            var ed = mapData.entrance[i];
            entrance[i] = new GameObject(ed.name).AddComponent<MapEntranceObject>();
            entrance[i].Initialize(ed, transform);

            if (Global.isDebugMode)
            {
                GameObject obj = DebugUtility.AddArrowView(ed.direction);
                obj.transform.parent = entrance[i].transform;
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

        enemys = new List<Enemy>();
        /*
        foreach (MapEnemyData en in mapData.enemys)
        {
            Enemy enemy = creater.CreateEnemy(en.enemyName, en.positon);
            enemy.transform.parent = transform;
            AddEnemy(enemy);
        }
        */

        // itemÇ‡çÏÇÈÅB
        items = new List<Item>();

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
        if (areaSize.x < location.x || areaSize.y < location.y)
            return false;
        return true;
    }

    public void Execute()
    {
        for (int i = 0; i < animationMapSprite.Count; i++)
            animationMapSprite[i].Execute();
        for (int i = 0; i < enemys.Count; i++) 
            ObjectExecute(enemys[i]);
        for (int i = 0; i < items.Count; i++)
            ObjectExecute(items[i]);

        void ObjectExecute(MassObject obj)
        {
            if (obj.isDestroy == false)
                obj.Execute();
        }
    }

    public void CheckDestroy()
    {
        for (int i = enemys.Count - 1; 0 <= i; i--)
            if (enemys[i].isDestroy == true)
            {
                Destroy(enemys[i].gameObject);
                enemys.RemoveAt(i);
            }

        for (int i = items.Count - 1; 0 <= i; i--)
            if (items[i].isDestroy == true)
            {
                Destroy(items[i].gameObject);
                items.RemoveAt(i);
            }
    }

    // ----------

    public MapEntranceObject GetEntrance(int index)
    {
        return entrance[index];
    }
}