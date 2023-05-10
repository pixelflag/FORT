using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System;

[ExecuteAlways]
public class WorldMapData : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    private WorldMapTileLibrary mapTileLibrary = default;

    private void Update()
    {
        GameObject mapFrame = transform.Find("MapFrame").gameObject;
        Vector2 rect = new Vector2(_areaSize.x * Global.gridSize.y, _areaSize.y * Global.gridSize.y);
        mapFrame.GetComponent<SpriteRenderer>().size = rect;
        mapFrame.transform.localPosition = new Vector3(rect.x / 2, -rect.y / 2, 0);
    }
#endif

    [SerializeField]
    private WorldMapName _mapName = default;
    public WorldMapName mapName => _mapName;
    [SerializeField]
    private Vector2Int _areaSize = new Vector2Int(30, 20);
    public Vector2Int areaSize => _areaSize;
    [SerializeField]
    private Tilemap _map = default;
    public Tilemap map => _map;

    public WorldMapCastleData[] castle;

    public WorldCellData[] cells;
    public WorldCellData GetCellData(Vector2Int location) => cells[LocationToIndex(location)];

    public int LocationToIndex(Vector2Int location)
    {
        return (location.y * _areaSize.x) + location.x;
    }

    public void DataUpdate()
    {
        if (Application.isPlaying) return;

        _map = transform.Find("map").GetComponent<Tilemap>();
        if (_map.transform.position != new Vector3(0, 0, 0))
            Debug.Log("The map is not centered.");

        // Enemy & Event ----------
        List<WorldMapCastleData> tempCastelList = new List<WorldMapCastleData>();

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            WorldMapCastleData castel = obj.GetComponent<WorldMapCastleData>();
            if (castel != null)
            {
                obj.name = "Castel_" + tempCastelList.Count.ToString();
                tempCastelList.Add(castel);
            }
        }

        castle = tempCastelList.ToArray();

        int length = _areaSize.x * _areaSize.y;
        cells = new WorldCellData[length];

        for (int cy = 0; cy < _areaSize.y; cy++)
        {
            for (int cx = 0; cx < _areaSize.x; cx++)
            {
                Vector2Int location = new Vector2Int(cx, cy);
                int index = LocationToIndex(location);

                string cellName = StringTools.Combine("cell_", cx.ToString(), "_", cy.ToString());

                Vector3Int mapLocation = new Vector3Int(location.x, -location.y - 1, 0);
                TileBase mTile = _map.GetTile(mapLocation);
                WorldCellType type = mapTileLibrary.AskTileType(mTile);

                cells[index] = new WorldCellData(cellName, type, location);
            }
        }
        EditorUtility.SetDirty(this);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(WorldMapData))]
public class WorldMapDataGUI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        WorldMapData fm = target as WorldMapData;

        if (GUILayout.Button("DataUpdate")) fm.DataUpdate();
    }
}
#endif

[Serializable]
public struct WorldCellData
{
    public string name;
    public WorldCellType type;
    public Vector2Int location;

    public WorldCellData(string name, WorldCellType type, Vector2Int location)
    {
        this.name = name;
        this.location = location;
        this.type = type;
    }
}

public enum WorldCellType
{
    Sea,
    Land,
    Rord,
    Forest,
    Mountain,
    Abyss,
    Bridge,
}

public enum WorldMapName
{
    Test,
}