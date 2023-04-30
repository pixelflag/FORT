using System.Collections.Generic;
using UnityEngine;

public class Team : DI
{
    public TeamID teamID;
    public List<Platoon> platoons = new List<Platoon>();

    public Team(TeamID teamID)
    {
        this.teamID = teamID;
    }

    public void CreatePlatoon(PlatoonData data, PlatoonFormation formation)
    {
        var platoon = new Platoon(data.maxServant);

        Unit cUnit = creater.CreateUnit(data.captainType, teamID, new Vector3());
        cUnit.SetController(new NpcControl(cUnit, 123));
        platoon.AddUnit(cUnit);

        for (int j = 0; j < data.maxServant; j++)
        {
            Unit sUnit = creater.CreateUnit(data.servantType, teamID, new Vector3());
            sUnit.SetController(new ServantControl(cUnit, sUnit, 10, 123));
            platoon.AddUnit(sUnit);
        }

        platoon.SetFormation(formation);

        platoons.Add(platoon);
    }

    public void CreateNpc(UnitType uType, Vector3 position)
    {
        var platoon = new Platoon(0);

        uint seed = (uint)position.x + (uint)position.y + (uint)position.z;

        Unit nUnit = creater.CreateUnit(uType, teamID, position);
        nUnit.SetController(new NpcControl(nUnit, seed));
        platoon.AddUnit(nUnit);
        platoons.Add(platoon);
    }
}

public class Platoon
{
    // index 0がリーダー
    public List<Unit> units;

    public Platoon(int length)
    {
        units = new List<Unit>(length);
    }

    public void SetFormation(PlatoonFormation formation)
    {
        for(int i=1; i < units.Count; i++)
        {
            var controller = units[i].controller as ServantControl;
            controller.SetOffsetPositon(formation.GetPosition(i));
        }
    }

    public void AddUnit(Unit unit)
    {
        units.Add(unit);
    }
}

// チームデータ
public struct TeamData
{
    public PlatoonData[] platoon;
}

// 小隊データ
public struct PlatoonData
{
    public UnitType captainType;
    public UnitType servantType;
    public int maxServant;

    public PlatoonData(UnitType captainType, UnitType servantType)
    {
        this.captainType = captainType;
        this.servantType = servantType;
        maxServant = 20;
    }
}

public enum TeamID
{
    A,
    B,
    Neutral,
}