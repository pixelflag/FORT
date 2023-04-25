using playerLogic;
using UnityEngine;
using pixelflag.controller;

public class UnitController : DI, IUnitcontroller
{
    public bool isControlLock { get; set; }
    private Unit unit;

    public void SetUnit(Unit unit)
    {
        this.unit = unit;
    }

    public void Initialize()
    {
        // Empty
    }

    public void Execute()
    {
        if (isControlLock) return;
        if (unit == null) return;
        if (unit.state == Unit.State.Dead) return;

        if (unit.state == Unit.State.Move)
        {
            Vector2 vector = FixDigitalMove.Execute(Global.input.pad1.stickX, Global.input.pad1.stickY);
            unit.SetMoveInput(vector.x, vector.y);

            if (Global.input.pad1.GetKeyDown(ControllerButtonType.Button1))
                unit.Attack();
        }
    }
}

public interface IUnitcontroller
{
    bool isControlLock { get; }
    void SetUnit(Unit unit);
    void Initialize();
    void Execute();

}
