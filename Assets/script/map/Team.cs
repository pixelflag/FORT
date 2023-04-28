using System.Collections.Generic;
using UnityEngine;

public class Team
{
    public int teamID;
    public List<Unit> units = new List<Unit>();

    public Team(int teamID)
    {
        this.teamID = teamID;
    }
}

// 大隊データ
public struct DattalionData
{
    public PlatoonData[] platoon;
}

// 小隊データ
public struct PlatoonData
{
    public UnitType[] units;
}