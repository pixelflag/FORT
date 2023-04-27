using UnityEngine;

public class UnitMouseController : DI, IUnitcontroller
{
    public bool isControlLock { get; set; }
    private Unit unit;
    private TargetRouteMove logic;

    public void SetUnit(Unit unit)
    {
        this.unit = unit;
        logic = new TargetRouteMove();
        logic.Initialize();
    }

    public void Initialize()
    {
        // Empty
    }

    public void Execute()
    {
        if (isControlLock) return;
        if (unit == null) return;
        if (unit.isDead) return;

        if(Input.GetMouseButtonDown(0))
        {
            Vector3 taegetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Debug.Log("mouse position : " + taegetPosition);

            taegetPosition.z = 0;
            logic.SetTarget(field.map, unit.position, taegetPosition);

            UpdateRouteView();
        }

        logic.Execute(unit.position);
        unit.SetVector(logic.vector);
    }

    // debug
    private GameObject[] routeBox = new GameObject[0];
    private GameObject[] routeLocator = new GameObject[0];

    private void UpdateRouteView()
    {
        // Box
        for (int i=0; i < routeBox.Length; i++)
        {
            GameObject.Destroy(routeBox[i]);
        }

        routeBox = new GameObject[logic.routes.length];
        for (int i = 0; i < routeBox.Length; i++)
        {
            routeBox[i] = DebugUtility.AddBoxView(new Box(logic.routes.Get(i), 8, 8));
            routeBox[i].GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 0, 0.8f);
        }

        // Locator
        for (int i = 0; i < routeLocator.Length; i++)
        {
            GameObject.Destroy(routeLocator[i]);
        }
        routeLocator = new GameObject[logic.routes.length];
        for (int i = 0; i < routeLocator.Length; i++)
        {
            int next = routeLocator.Length <= i + 1 ? i : i + 1;
            Vector2 pos = logic.SeachNextHalfTarget(i, next);
            routeLocator[i] = DebugUtility.AddLocator(pos);
            routeLocator[i].transform.Translate(0,0,-1);
        }
    }
}
