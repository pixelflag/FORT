using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLogic
{
    public int[][] spawnRetainerTable =
    {
        new[]{30, 20,  5, 0 },
        new[]{20, 20, 10, 0 },
        new[]{20, 30, 10, 0 },
        new[]{10, 30, 20, 0 },
        new[]{10, 20, 20, 5 },
        new[]{10, 20, 20, 5 },
        new[]{10, 20, 20, 5 },
        new[]{5 , 10, 30, 5 },
        new[]{5 , 10, 30, 10 },
        new[]{5 , 10, 30, 10 }
    };

    public int[][] spawnEnemyTable =
    {
        new[]{30, 20, 5 , 5 , 0 , 1 , 0 , 0 , 0 , 0 },
        new[]{20, 20, 10, 5 , 0 , 3 , 0 , 0 , 0 , 0 },
        new[]{20, 30, 10, 5 , 0 , 5 , 0 , 0 , 0 , 0 },
        new[]{10, 20, 20, 5 , 0 , 10, 0 , 0 , 0 , 0 },
        new[]{0 , 10, 20, 5 , 20, 15, 10, 0 , 0 , 0 },
        new[]{0 , 10, 20, 5 , 20, 15, 10, 0 , 0 , 0 },
        new[]{0 , 10, 10, 5 , 20, 20, 10, 5 , 0 , 0 },
        new[]{0 , 0 , 0 , 0 , 20, 20, 20, 10, 0 , 0 },
        new[]{0 , 0 , 0 , 0 , 0 , 20, 20, 20, 20, 0 },
        new[]{0 , 0 , 0 , 0 , 0 , 0 , 10, 20, 20, 10}
    };

    public SpawnLogic()
    {
        tempList = new List<int>();
        returnList = new List<int>();
    }

    public int[] GetRetainerSpawnList(int level, int spawnCount)
    {
        return GetSpawnList(spawnRetainerTable, level, spawnCount);
    }

    public int[] GetEnemySpawnList(int level, int spawnCount)
    {
        return GetSpawnList(spawnEnemyTable, level, spawnCount);
    }

    private List<int> tempList;
    private List<int> returnList;

    private int[] GetSpawnList(int[][] table, int level, int spawnCount)
    {
        if (table.Length <= level)
        {
            throw new System.Exception("out of level :" + level);
        }

        tempList.Clear();
        returnList.Clear();

        for (int i = 0; i < table[level].Length; i++)
        {
            for (int j = 0; j < table[level][i]; j++)
            {
                tempList.Add(i+1);
            }
        }

        // テーブルは事前にキャッシュしておいてもいいよね。今回はほっときます。

        for (int i = 0; i < spawnCount; i++)
        {
            int rand = Random.Range(0, tempList.Count);
            int num = tempList[rand];
            returnList.Add(num);
        }
        return returnList.ToArray();
    }
}
