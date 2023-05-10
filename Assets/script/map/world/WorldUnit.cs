using UnityEngine;

public class WorldUnit : MassObject
{
    [SerializeField]
    private float speed = 0.5f;
    [SerializeField]
    private int collisionSize = 16;

    public float terrainGain = 1.0f;

    public UnitType unitType { get; protected set; }
    public TeamID teamID { get; protected set; }

    public UnitView view { get; protected set; }

    public DirectionControl direction { get; protected set; }
    public void SetDirection(Direction4Type d) { direction.SetDirection(d); }

    private int count = 0;

    public FixedArray<Vector2> routes;

    private State state;
    private enum State
    {
        Idle,
        Move,
    }

    public void Initialize(UnitType unitType, TeamID teamID)
    {
        base.Initialize();

        this.unitType = unitType;
        this.teamID = teamID;

        view = GetComponent<UnitView>();
        view.Initialize();

        direction = new DirectionControl();

        collision.SetExtends(collisionSize / 2);

        routes = new FixedArray<Vector2>(256);
    }

    public void SetVector(Vector2 vector)
    {
        _vector = vector;
        state = State.Move;
    }

    public override void Execute()
    {
        if (isDestroy) return;
        base.Execute();

        switch (state)
        {
            case State.Idle:
                break;
            case State.Move:
                count++;
                /*
                logic.Execute(position);

                if (logic.isGoal || 180 < count)
                {
                    count = 0;
                    state = State.Idle;
                    break;
                }

                if (vector.x == 0 && vector.y == 0)
                {
                    direction.SetLooking(logic.vector.x, logic.vector.y);
                }

                x += logic.vector.x * speed * terrainGain;
                y += logic.vector.y * speed * terrainGain;

                z = y * 0.01f;

                direction.SetMoving(x, y);
                */
                break;
        }

        // render
        view.WalkAnimUpdate(direction.direction4);
        view.ObjectUpdate();
        view.Draw();

        //UpdateRouteView();
    }

    // ‰½‚©‚Æ‘˜‹ö
    public void Hit()
    {
    }

    // debug -----
    private GameObject debugRouteRoot;

    public void ShowRouteView()
    {
        if (debugRouteRoot != null)
            Destroy(debugRouteRoot);

        debugRouteRoot = new GameObject("DebugRouteRoot");

        for (int i = 0; i < routes.length; i++)
        {
            GameObject box = DebugUtility.AddBoxView(new Box(routes.Get(i), 8, 8));
            box.transform.parent = debugRouteRoot.transform;
            box.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 0, 0.8f);
        }

        // Locator
        for (int i = 0; i < routes.length; i++)
        {
            int next = routes.length <= i + 1 ? i : i + 1;
            Vector2 pos = SeachNextHalfTarget(i, next);
            GameObject box = DebugUtility.AddLocator(pos);

            box.transform.parent = debugRouteRoot.transform;
            box.transform.Translate(0, 0, -1);
        }

        Vector3 SeachNextHalfTarget(int index, int nextIndex)
        {
            if (routes.length <= 1)
                nextIndex = index;

            if (routes.length <= nextIndex)
                nextIndex = index;

            Vector3 p1 = routes.Get(index);
            Vector3 p2 = routes.Get(nextIndex);
            Vector3 p3 = (p2 - p1) / 2 + p1;

            return p3;
        }
    }
}
