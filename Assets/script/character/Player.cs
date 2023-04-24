using UnityEngine;
using pixelflag.controller;
using playerLogic;

public class Player : Character
{
    public bool isControlLock { get; private set; }
    public void ControlLock(bool value) { isControlLock = value; }

    private int weaponTypeTableIndex = 0;
    private FixDigitalMove moveLogic;

    public override void Initialize()
    {
        base.Initialize(ObjectType.Player, data.unit.playerLevelData);
        moveLogic = new FixDigitalMove();
        // view.AddLifeGage();

        // 初期レベルデータの挿入が必要
    }

    public override void Execute()
    {
        base.Execute();

        if (isDead) return;
        if (isControlLock) return;

        if (!isAttack)
        {
            Vector2 vector = moveLogic.Execute(Global.input.pad1.stickX, Global.input.pad1.stickY);
            SetMoveInput(vector.x, vector.y);
        }
        WeaponParameters[] wpp = data.unit.weaponData;

        if (Global.input.pad1.GetKeyDown(ControllerButtonType.Button1))
            Attack();
        if (Global.input.pad1.GetKeyDown(ControllerButtonType.Button2))
            if (OnCommand != null) OnCommand();
        if (Global.input.pad1.GetKeyDown(ControllerButtonType.L1))
        {
            weaponTypeTableIndex++;
            weaponTypeTableIndex = wpp.Length <= weaponTypeTableIndex ? 0 : weaponTypeTableIndex;
            SetWeapon(creater.CreateWeapon(this, weaponTypeTableIndex));
        }
        if (Global.input.pad1.GetKeyDown(ControllerButtonType.R1))
        {
            weaponTypeTableIndex--;
            weaponTypeTableIndex = weaponTypeTableIndex < 0 ? wpp.Length - 1 : weaponTypeTableIndex;
            SetWeapon(creater.CreateWeapon(this, weaponTypeTableIndex));
        }
    }

    public void HealProgress()
    {
        if (!battle.lifeLogic.HealProgress())
            creater.CreateMessageEffect(MessageEffectName.Heal, position);
    }

    // CollisionHitEvent -----
    public void HitItem(MassObject obj)
    {
        if (isDead) return;

        // knock back
        Vector3 vector = Calculate.PositionToNomaliseVector(obj.position, position);
        int refrectionPower = 3;
        _force = vector * refrectionPower;
    }

    public delegate void CommandDelegate();
    public CommandDelegate OnCommand;
}