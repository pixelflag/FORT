using UnityEngine;

public class WorldRouteSearch
{
    public int[,] score;
    private Vector2Int mapSize;


    public WorldRouteSearch(Vector2Int mapSize)
    {
        this.mapSize = mapSize;
        score = new int[mapSize.x, mapSize.y];
    }

    public void ExcuteScoring(ICellMap map, Vector2Int startLocation)
    {
        // 初期位置がマップ外にはみ出したら終了
        if (startLocation.x < 0) return;
        if (startLocation.y < 0) return;
        if (mapSize.x <= startLocation.x) return;
        if (mapSize.y <= startLocation.y) return;

        for(int y=0;y < mapSize.y; y++)
            for (int x = 0; x < mapSize.x; x++)
                score[x, y] = 10000;

        Scoring(startLocation.x, startLocation.y, 0);

        void Scoring(int x, int y, int Score)
        {
            if (x < 0) return;
            if (y < 0) return;
            if (mapSize.x <= x) return;
            if (mapSize.y <= y) return;

            int moveCost = (int)map.GetCell(new Vector2Int(x, y)).routeScore.Cscore;
            moveCost = 10 <= moveCost ? 1000 : moveCost;
            int newScore = moveCost + Score + 1;
            if (score[x, y] <= newScore) return;

            score[x, y] = newScore;

            Scoring(x+1, y, newScore);
            Scoring(x-1, y, newScore);
            Scoring(x, y+1, newScore);
            Scoring(x, y-1, newScore);
        }
    }

    public void GetRoute(FixedArray<Vector2> routes, Vector2Int startLocation, Vector2Int goalLocation)
    {
        // 初期位置がマップ外であれば終了
        if (startLocation.x < 0) return;
        if (startLocation.y < 0) return;
        if (mapSize.x <= startLocation.x) return;
        if (mapSize.y <= startLocation.y) return;

        // 目的地がマップ外であれば終了
        if (goalLocation.x < 0) return;
        if (goalLocation.y < 0) return;
        if (mapSize.x <= goalLocation.x) return;
        if (mapSize.y <= goalLocation.y) return;

        routes.Clear();
        routes.Add(MapUtility.LocationToPosition(goalLocation));

        // 現在位置と目標位置が一緒であれば終了。
        if (startLocation == goalLocation) return;

        Pick(goalLocation.x, goalLocation.y);

        void Pick(int x, int y)
        {
            if (x < 0) return;
            if (y < 0) return;
            if (mapSize.x <= x) return;
            if (mapSize.y <= y) return;

            var n1 = GetScore(x + 1, y);
            var n2 = GetScore(x - 1, y);
            var n3 = GetScore(x, y + 1);
            var n4 = GetScore(x, y - 1);
            var n5 = n1.score < n2.score? n1:n2;
            var n6 = n3.score < n4.score? n3:n4;
            var n7 = n5.score < n6.score ? n5 : n6;

            var location = new Vector2Int(n7.x, n7.y);
            routes.Add(MapUtility.LocationToPosition(location));

            if(location == startLocation || routes.length == routes.fixedLength)
                return;
            else
                Pick(n7.x, n7.y);
        }

        ScoreAndLocation GetScore(int x, int y)
        {
            if (x < 0) return new ScoreAndLocation(1000, x, y);
            if (y < 0) return new ScoreAndLocation(1000, x, y);
            if (mapSize.x <= x) return new ScoreAndLocation(1000, x, y);
            if (mapSize.y <= y) return new ScoreAndLocation(1000, x, y);

            return new ScoreAndLocation(score[x, y], x, y);
        }
    }

    private struct ScoreAndLocation
    {
        public int score;
        public int x;
        public int y;

        public ScoreAndLocation(int score, int x, int y)
        {
            this.score = score;
            this.x = x;
            this.y = y;
        }
    }

}
