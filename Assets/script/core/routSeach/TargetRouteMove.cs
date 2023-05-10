using UnityEngine;

public class TargetRouteMove
{
    private int routeIndex;
    public FixedArray<Vector2> routes { get; private set; }

    private float goalDistance = 16;

    private Vector2 targetPos;
    public Vector2 vector { get; private set; }
    public bool isGoal { get; private set; }

    public void Initialize()
    {
        routes = new FixedArray<Vector2>(RouteSearch.searchLimit);
        routeIndex = 0;
        isGoal = true;
        targetPos = new Vector2();
    }

    public bool SetTarget(ICellMap map, Vector3 position, Vector3 targetPosition)
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
                targetPos = SeachNextHalfTarget(0,1);
                routeIndex++;
                break;
        }
        return true;
    }

    public void Execute(Vector2 currentPosition)
    {
        if (routes.length <= 1) isGoal = true;

        if (!isGoal)
        {
            vector = Calculate.PositionToNomaliseVector(currentPosition, targetPos);
            if ((targetPos - currentPosition).sqrMagnitude < goalDistance)
            {
                if (routes.length <= routeIndex)
                {
                    isGoal = true;
                    vector = new Vector2();
                }
                else
                {
                    targetPos = SeachNextHalfTarget(routeIndex, routeIndex + 1);
                    routeIndex++;
                }
            }
        }
    }

    // 配列内の今と次のターゲットの間を追う
    public Vector3 SeachNextHalfTarget(int index, int nextIndex)
    {
        if (routes.length <= 1)
            nextIndex = index;

        if(routes.length <= nextIndex)
            nextIndex = index;

        Vector3 p1 = routes.Get(index);
        Vector3 p2 = routes.Get(nextIndex);
        Vector3 p3 = (p2 - p1) / 2 + p1;

        return p3;
    }
}
