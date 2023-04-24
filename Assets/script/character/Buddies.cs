using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buddies : Character
{
    public BuddiesType buddiesType{ get; private set; }

    private Character master;
    public ICharacterLogic moveLogic { get; set; }

    public void Initialize(BuddiesType buddiesType, Character master)
    {
        base.Initialize(ObjectType.Buddies, data.unit.playerLevelData);
        this.buddiesType = buddiesType;
        this.master = master;
    }

    public override void Execute()
    {
        if (moveLogic != null)
            moveLogic.Execute(field.map, master, field.map.enemys);
        base.Execute();
    }
}

public enum BuddiesType
{
    None,
    Knight,
    Soldier,
    Elf,
    RedWizard,
    BlueWizard,
    Lumberjack,
    Dwarves,
    Thief,
}
