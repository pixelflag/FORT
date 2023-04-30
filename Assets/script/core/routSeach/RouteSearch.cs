using System.Collections.Generic;
using UnityEngine;

public static class RouteSearch
{
    private static int _searchLimit = 128;
    public static int searchLimit => _searchLimit;
    private static FixedArray<Vector2Int> tempRoutes;
    private static List<CellScore> scoreList;

    private static Vector2Int[] aroundOffset =
    {
        new Vector2Int( 0, 0),
        new Vector2Int( 0, 1),
        new Vector2Int( 1, 0),
        new Vector2Int( 0,-1),
        new Vector2Int(-1, 0),
        new Vector2Int( 1, 1),
        new Vector2Int( 1,-1),
        new Vector2Int(-1,-1),
        new Vector2Int(-1, 1),
        new Vector2Int( 0, 2),
        new Vector2Int( 2, 0),
        new Vector2Int( 0,-2),
        new Vector2Int(-2, 0),
    };

    public static void Initialize()
    {
        tempRoutes = new FixedArray<Vector2Int>(_searchLimit);
        scoreList = new List<CellScore>();
    }

    public static RouteSearchResult Search(
        FieldMapObject map,
        FixedArray<Vector2> routes,
        Vector3 startPosition,
        Vector3 targetPosition)
    {
        routes.Clear();

        // map座標に変換
        Vector2Int currentLocation = MapUtility.PositionToLocation(startPosition);
        Vector2Int targetLocation  = MapUtility.PositionToLocation(targetPosition);
        routes.Add(MapUtility.LocationToPosition(currentLocation));

        // 初期位置がマップ外にはみ出したら終了
        if (!map.ExistsCellData(currentLocation)) 
        {
            return RouteSearchResult.OutOfField;
        }

        // 目的地がマップ外にはみ出したら修正
        if (!map.ExistsCellData(targetLocation))
        {
            targetLocation.x = targetLocation.x < 0 ? 0 : targetLocation.x;
            targetLocation.y = targetLocation.y < 0 ? 0 : targetLocation.y;
            targetLocation.x = targetLocation.x > map.areaSize.x ? map.areaSize.x : targetLocation.x;
            targetLocation.y = targetLocation.y > map.areaSize.y ? map.areaSize.y : targetLocation.y;
        }

        // 目的地を安全な場所に修正
        targetLocation = SearchSafeCell(targetLocation);
        Vector2Int SearchSafeCell(Vector2Int location)
        {
            for (int i = 0; i < aroundOffset.Length; i++)
            {
                Vector2Int newloc = location + aroundOffset[i];
                if (map.ExistsCellData(newloc))
                {
                    Cell cell = map.GetCell(newloc);
                    if (!cell.isCollision)
                        return newloc;
                }
            }
            return location;
        }

        // 現在位置と目標位置が一緒の場合は終了。
        if (targetLocation == currentLocation)
        {
            routes.Add(MapUtility.LocationToPosition(targetLocation));
            return RouteSearchResult.StartingPosition;
        }

        scoreList.Clear();
        int count = 0;
        bool open = true;
        CellScore activeCell; // 現在のセル
        RouteSearchResult result = RouteSearchResult.Unknown; // 探索結果

        // スタート地点登録 -----
        activeCell = map.GetCell(currentLocation).routeScore;
        activeCell.Ascore = (targetLocation - activeCell.localLocation).sqrMagnitude;
        activeCell.Bscore = (currentLocation - activeCell.localLocation).sqrMagnitude;
        activeCell.isChecked = true;
        activeCell.isClose = true;
        activeCell.isRootCell = true;
        scoreList.Add(activeCell);

        // セルにスコアをつける
        while (open)
        {
            open = false;
            activeCell.isClose = true;

            // 現在のセル周辺のセルにスコアをつける
            ScoringCell(activeCell.localLocation + new Vector2Int(-1, 0));
            ScoringCell(activeCell.localLocation + new Vector2Int( 1, 0));
            ScoringCell(activeCell.localLocation + new Vector2Int( 0, 1));
            ScoringCell(activeCell.localLocation + new Vector2Int( 0,-1));

            // 最もスコアが小さく探索が終了していないセルを次の目標とする。
            open = false;
            scoreList.Sort((a, b) => (int)(a.TotalScore * 1000 - b.TotalScore * 1000));
            foreach (CellScore cellScore in scoreList)
            {
                if (cellScore.isClose == false)
                {
                    activeCell = cellScore;
                    open = true;
                    break;
                }
            }

            // 目的地に到着したら終了
            if (activeCell.localLocation == targetLocation)
            {
                result = RouteSearchResult.Goal;
                break;
            }

            // 最大探索数に達したら終了
            count++;
            if (count >= searchLimit)
            {
                result = RouteSearchResult.Limit;
                break;
            }
        }

        ExtractRoute(activeCell, routes);

        // 状態をクリア
        foreach (CellScore cellScore in scoreList)
        {
            cellScore.RouteReset();
        }

        return result;

        void ScoringCell(Vector2Int location)
        {
            if (map.ExistsCellData(location))
            {
                Cell cell = map.GetCell(location);
                CellScore score = cell.routeScore;
                if (!cell.isCollision && score.isClose == false && score.isChecked == false)
                {
                    score.Ascore = (targetLocation  - cell.data.location).sqrMagnitude;
                    score.Bscore = (currentLocation - cell.data.location).sqrMagnitude;
                    score.isChecked = true;
                    score.parent = activeCell;
                    scoreList.Add(score);
                }
            }
        }

        // スコアを元にルート座標を抽出していく
        void ExtractRoute(CellScore startCell, FixedArray<Vector2> routes)
        {
            int count = 0;
            tempRoutes.Clear();

            CellScore activeCell = startCell;

            while (!activeCell.isRootCell)
            {
                tempRoutes.Add(activeCell.localLocation);
                activeCell = activeCell.parent;
                count++;

                if (searchLimit < count) break;
            }

            // 配列を反転

            for (int i = tempRoutes.length - 1; i >= 0; i--)
            {
                Vector2Int r = tempRoutes.Get(i);
                Vector3 pos = MapUtility.LocationToPosition(r);
                routes.Add(pos);
            }
        }
    }
}

public enum RouteSearchResult
{
    Unknown,
    OutOfField,
    StartingPosition,
    Limit,
    TargetCollision,
    Goal,
}