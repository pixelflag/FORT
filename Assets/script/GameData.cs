using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public static GameData instance;

    public GameData()
    {
        instance = this;
        unit = new UnitData();
    }

    public UnitData unit { get; private set; }
}