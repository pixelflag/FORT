using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLibrary : DIMonoBehaviour
{
    public Enemy[] enemys = default;

    public Enemy CreateEnemy(EnemyName name)
    {
        uint seed = 10;
        int index = (int)name;

        Enemy enemy = Instantiate(enemys[index]).GetComponent<Enemy>();
        enemy.Initialize(data.unit.enemyLevelData[index], new SlimeLogic(), seed);
        return enemy;
    }
}