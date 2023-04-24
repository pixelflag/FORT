using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleObject
{
    private ObjectType type;
    private BattleParameters[] levelTable;

    public int level { get; private set; }
    public int exp { get; private set; }

    public bool isMaxLevel { get { return level == levelTable.Length; } }
    public int nextExp { get { return levelTable[level - 1].exp; } }

    public LifeLogic lifeLogic { get; private set; }

    public BattleObject(ObjectType type, BattleParameters[] levelTable)
    {
        this.type = type;
        this.levelTable = levelTable;

        lifeLogic = new LifeLogic();
        SetLevel(1);
        lifeLogic.FullRecovery();
    }

    public void SetLevel(int level)
    {
        this.level = level;
        lifeLogic.SetData(levelTable[level - 1].maxLife, 1);
    }

    public int GetAttackPower()
    {
        int attack = levelTable[level - 1].attack;

        switch (type)
        {
            case ObjectType.Player:
                return BattleLogic.PlayerAttack(attack);
            case ObjectType.Buddies:
                return BattleLogic.PlayerAttack(attack);
        }
        throw new System.Exception("Unknown type : " + type);
    }

    public int Damage(int power)
    {
        switch (type)
        {
            case ObjectType.Player:
                power = BattleLogic.PlayerDefence(power);
                int resultLife1 = BattleLogic.SafeLife(lifeLogic.current, power);
                lifeLogic.SetLife(resultLife1);
                break;
            case ObjectType.Buddies:
                power = BattleLogic.PlayerDefence(power);
                int resultLife2 = BattleLogic.SafeLife(lifeLogic.current, power);
                lifeLogic.SetLife(resultLife2);
                break;
        }

        return power;
    }

    public void AddExp(int dropExp)
    {
        exp += dropExp;
        bool levelup = false;

        while (nextExp < exp)
        {
            exp = exp - nextExp;
            if (!isMaxLevel)
            {
                level++;
                levelup = true;
            }
            else
            {
                break;
            }
        }

        if (levelup)
        {
            if (OnLevelUp != null) OnLevelUp(level, isMaxLevel);
            SetLevel(level);
            lifeLogic.FullRecovery();   // 回復判断はここですべきではないのでは。
        }

        if (OnExpUpdate != null) OnExpUpdate(exp, nextExp);
    }

    public delegate void LevelupDelegate(int level, bool isMaxLevel);
    public LevelupDelegate OnLevelUp;

    public delegate void ExpUpdateDelegate(int exp, int max);
    public ExpUpdateDelegate OnExpUpdate;
}