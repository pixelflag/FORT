using UnityEngine;

public class TargetRouteMove
{
    private int routeIndex;
    private RouteArray routes;

    private float goalDistance = 16;

    private Vector2 targetPos;
    public Vector3 direction { get; private set; }
    public bool isGoal { get; private set; }

    public void Initialize()
    {
        routes = new RouteArray(RouteSearch.searchLimit);
        routeIndex = 0;
        isGoal = true;
        targetPos = new Vector2();
    }

    public bool SetTarget(FieldMapObject map, Vector3 position, Vector3 targetPosition)
    {
        RouteSearchResult result = RouteSearch.Search(map, routes, position, targetPosition);

        switch (result)
        {
            case RouteSearchResult.Unknown:
            case RouteSearchResult.OutOfField:
            case RouteSearchResult.StartingPosition:
                return false;
            case RouteSearchResult.TargetCollision:
            case RouteSearchResult.Limit:
            case RouteSearchResult.Goal:
                isGoal = false;
                routeIndex = 0;
                targetPos = SeachNextHalfTarget(routeIndex);
                routeIndex++;
                break;
        }
        return true;
    }

    public void Execute(Vector2 currentPosition)
    {
        if(routes.Length == 0) isGoal = true;

        if (!isGoal)
        {
            direction = Calculate.PositionToNomaliseVector(currentPosition, targetPos);
            if ((targetPos - currentPosition).sqrMagnitude < goalDistance)
            {
                if (routes.Length <= routeIndex)
                {
                    isGoal = true;
                }
                else
                {
                    targetPos = SeachNextHalfTarget(routeIndex);
                    routeIndex++;
                }
            }
        }
    }

    // 配列内の今と次のターゲットの間を追う
    private Vector3 SeachNextHalfTarget(int index)
    {
        Vector3 p1 = routes.GetPosition(index);
        Vector3 p2 = routes.GetPosition((routes.Length > routeIndex + 1)? index + 1: index);
        Vector3 p3 = (p2 - p1) / 2 + p1;
        p3.x += Global.gridSize.x / 2;
        p3.y += Global.gridSize.y / 2;

        return p3;
    }
}
