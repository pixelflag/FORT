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
            taegetPosition.z = 0;
            logic.SetTarget(field.map, unit.position, taegetPosition);
        }

        logic.Execute(unit.position);
        unit.SetVector(logic.direction);
    }
}
