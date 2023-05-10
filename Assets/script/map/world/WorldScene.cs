using UnityEngine;

public class WorldScene : DI
{
    private WorldMapData mapData;
    private WorldMap worldMap;
    private WorldRouteSearch routeSearch;

    private WorldUnit[] units;
    private int head;

    private Camera camera;
    private int scroolSpeed = 4;

    public WorldScene(WorldMapData mapData)
    {
        this.mapData = mapData;
        camera = Camera.main;
        worldMap = new GameObject("WorldMap").AddComponent<WorldMap>();
        worldMap.Initialize(mapData);

        routeSearch = new WorldRouteSearch(worldMap.areaSize);

        units = new WorldUnit[64];
        // とりあえず、ユニットを配置。
        units[0] = creater.CreateWorldUnit(UnitType.Knight, 0, new Vector3(256+8, -256+8, 0));
        Sort();
    }

    public void Execute()
    {
        for(int i = 0; i < head; i++)
        {
            Vector2Int location = MapUtility.PositionToLocation(units[i].position);
            int moveCost = data.GetMoveCost(mapData.GetCellData(location).type);
            units[i].Execute();
            units[i].terrainGain = 1 - (float)moveCost / 10;
        }

        int threshold = Global.gridSize.x * 2;

        Vector3 cPos = camera.transform.position;
        Vector2 areaSize = worldMap.areaSize * Global.gridSize;

        // Left
        if (Input.GetKey(KeyCode.A))
//      if (Input.mousePosition.x < threshold)
        {
            cPos.x -= scroolSpeed;
            if (cPos.x < Global.screenWidth / 2)
                cPos.x = Global.screenWidth / 2;
        }
        // Right
        if (Input.GetKey(KeyCode.D))
//      if (Screen.width - threshold < Input.mousePosition.x)
        {
            cPos.x += scroolSpeed;
            if (areaSize.x - Global.screenWidth / 2 < cPos.x)
                cPos.x = areaSize.x - Global.screenWidth / 2;
        }
        // Down
        if (Input.GetKey(KeyCode.S))
//      if (Input.mousePosition.y < threshold)
        {
            cPos.y -= scroolSpeed;
            if (cPos.y < -areaSize.y + Global.screenHeight / 2)
                cPos.y = -areaSize.y + Global.screenHeight / 2;
        }
        // Up
        if (Input.GetKey(KeyCode.W))
//      if (Screen.height - threshold < Input.mousePosition.y)
        {
            cPos.y += scroolSpeed;
            if (-Global.screenHeight / 2 < cPos.y)
                cPos.y = -Global.screenHeight / 2;
        }
        camera.transform.position = cPos;

        if (Input.GetMouseButtonDown(0))
        {
            WorldUnit SelectedUnit = units[0];
            Vector2Int currentLocation = MapUtility.PositionToLocation(SelectedUnit.position);

            Debug.Log("Route Scoreing");
            routeSearch.ExcuteScoring(worldMap, currentLocation);
            // ShowRouteScore();
        }

        if (Input.GetMouseButtonDown(1))
        {
            WorldUnit SelectedUnit = units[0];

            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int currentLocation = MapUtility.PositionToLocation(SelectedUnit.position);
            Vector2Int targetLocation  = MapUtility.PositionToLocation(targetPosition);

            routeSearch.GetRoute(SelectedUnit.routes, currentLocation, targetLocation);
            SelectedUnit.ShowRouteView();
        }
    }

    public void Sort()
    {
        head = 0;
        for (int i = 0; i < units.Length; i++)
        {
            if (units[i] != null)
            {
                WorldUnit temp = units[i];
                units[i] = null;
                units[head] = temp;
                head++;
            }
        }
    }

    // Debug -----
    private GameObject debugScoreRoot;

    public void ShowRouteScore()
    {
        int w = routeSearch.score.GetLength(0);
        int h = routeSearch.score.GetLength(1);

        if(debugScoreRoot != null)
            GameObject.Destroy(debugScoreRoot);

        debugScoreRoot = new GameObject("DebugRoot");

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                Vector3 pos = MapUtility.LocationToPosition(new Vector2Int(x, y)) + new Vector3(-7,0,0);
                MiniNum mNum = creater.CreateNumiNum(routeSearch.score[x, y], debugScoreRoot.transform, pos);
                mNum.name = "S_" + x.ToString() + "_" + y.ToString();
            }
        }
    }
}