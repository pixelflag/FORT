using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System;

[ExecuteAlways]
public class FieldMapData : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    private MapSpriteLibrary mapSpriteLibrary = default;

    private void Update()
    {
        GameObject mapFrame = transform.Find("MapFrame").gameObject;
        Vector2 rect = new Vector2(areaSize.x * Global.gridSize.y, areaSize.y * Global.gridSize.y);
        mapFrame.GetComponent<SpriteRenderer>().size = rect;
        mapFrame.transform.localPosition = new Vector3(rect.x / 2, -rect.y / 2, 0);
    }
#endif

    [SerializeField]
    private FieldMapName _mapName = default;
    public FieldMapName mapName => _mapName;
    [SerializeField]
    private Vector2Int _areaSize = new Vector2Int(30, 20);
    public Vector2Int areaSize => _areaSize;
    [SerializeField]
    private Tilemap _collisionMap = default;
    public Tilemap collisionMap => _collisionMap;
    [SerializeField]
    private Tilemap _groundMap = default;
    public Tilemap groundMap => _groundMap;

    public MapEnemyData[] enemys;
    public MapEventData[] events;
    public MapEntranceData[] entrance;

    public FieldCellData[] cells;
    public FieldCellData GetCellData(Vector2Int location) => cells[LocationToIndex(location)];

    public int LocationToIndex(Vector2Int location)
    {
        return (location.y * areaSize.x) + location.x;
    }

    public void DataUpdate()
    {
        if (Application.isPlaying) return;

        _groundMap = transform.Find("GroundMap").GetComponent<Tilemap>();
        if (_groundMap.transform.position != new Vector3(0, 0, 0))
            Debug.Log("The groundmap is not centered.");

        _collisionMap = transform.Find("CollisionMap").GetComponent<Tilemap>();
        if (_collisionMap.transform.position != new Vector3(0, 0, 0))
            Debug.Log("The collisionmap is not centered.");

        // Enemy & Event ----------
        List<MapEnemyData> tempEnemyList = new List<MapEnemyData>();
        List<MapEventData> tempEventList = new List<MapEventData>();

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            MapEnemyData en = obj.GetComponent<MapEnemyData>();
            if (en != null)
            {
                en.name = en.enemyName.ToString();
                tempEnemyList.Add(en);
            }

            MapEventData eo = obj.GetComponent<MapEventData>();
            if (eo != null)
                tempEventList.Add(eo);
        }

        enemys = tempEnemyList.ToArray();
        events = tempEventList.ToArray();

        int length = areaSize.x * areaSize.y;
        cells = new FieldCellData[length];

        for (int cy = 0; cy < areaSize.y; cy++)
        {
            for (int cx = 0; cx < areaSize.x; cx++)
            {
                Vector2Int location = new Vector2Int(cx, cy);
                int index = LocationToIndex(location);

                string cellName = StringTools.Combine("map_", cx.ToString(), "_", cy.ToString());
                cells[index] = new FieldCellData(cellName, location);

                Vector3Int mapLocation = new Vector3Int(location.x, -location.y - 1, 0);
                Sprite cSprite = _collisionMap.GetSprite(mapLocation);
                Sprite gSprite = _groundMap.GetSprite(mapLocation);

                if (cSprite != null)
                {
                    BoolAndInt bai = mapSpriteLibrary.ExistsBrokenSprite(cSprite);
                    if (bai.b)
                        cells[index].type = mapSpriteLibrary.GetBrokenSpriteData(bai.i).type;
                    else
                        cells[index].type = CellType.Collision;
                    cells[index].broken = bai;
                }
                else if (gSprite != null)
                {
                    BoolAndInt bai = mapSpriteLibrary.ExistsBrokenSprite(gSprite);
                    if (bai.b)
                    {
                        cells[index].type = mapSpriteLibrary.GetBrokenSpriteData(bai.i).type;
                        cells[index].broken = bai;
                    }
                }
            }
        }

        EditorUtility.SetDirty(this);   
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(FieldMapData))]
public class FieldMapGUI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        FieldMapData fm = target as FieldMapData;

        if (GUILayout.Button("DataUpdate")) fm.DataUpdate();
    }
}
#endif

[Serializable]
public struct FieldCellData
{
    public string cellName;
    public CellType type;
    public Vector2Int location;
    public BoolAndInt broken;  // 破壊可能である。

    public FieldCellData(string cellName, Vector2Int location)
    {
        this.cellName = cellName;
        this.location = location;
        type = default;
        broken = default;
    }
}

[Serializable]
public struct BoolAndInt
{
    public bool b;
    public int i;

    public BoolAndInt(bool b, int i)
    {
        this.b = b;
        this.i = i;
    }
}