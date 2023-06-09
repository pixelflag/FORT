using playerLogic;
using UnityEngine;
using pixelflag.controller;

public class UnitController : DI, IUnitcontroller
{
    public bool isControlLock { get; set; }
    private Unit unit;

    private int weaponID;

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
        if (unit.isDead) return;

        if (unit.state == Unit.State.Move)
        {
            Vector2 vector = FixDigitalMove.Execute(Global.input.pad1.stickX, Global.input.pad1.stickY).normalized;
            unit.SetVector(vector);

            if (Global.input.pad1.GetKeyDown(ControllerButtonType.Button1))
                unit.Attack(unit.vector);
            if (Global.input.pad1.GetKeyDown(ControllerButtonType.Option1))
            {
                weaponID = (weaponID+1)% data.weaponData.Length;
                unit.SetWeapon(creater.CreateWeapon(unit, weaponID));
            }
        }
    }
}

public interface IUnitcontroller
{
    bool isControlLock { get; }
    void SetUnit(Unit unit);
    void Execute();

}

public enum UnitControlType
{
    PlayerControler,
    PlayerMouse,
    Servant,
}
