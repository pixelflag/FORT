using UnityEngine;

public class BattleLogic
{
    public static int PlayerAttack(int attackPower)
    {
        int rand = Random.Range(0, 100);
        // 会心の一撃
        if (rand < 10)
        {
            attackPower *= 2;
        }
        // 超会心の一撃
        else if (rand < 1)
        {
            attackPower *= 4;
        }

        return attackPower;
    }

    public static int PlayerDefence(int attackPower)
    {
        int rand = Random.Range(0, 100);
        // 防御
        if (rand< 30)
        {
            attackPower /= 2;
        }
        // 回避
        else if (rand < 10)
        {
            attackPower = 1;
        }

        return attackPower;
    }

    public static int EnemyAttack(int attackPower)
    {
        // 複雑化するだけだから、今はそのまま返すのでいい。
        return attackPower;
    }

    public static int EnemyDefense(int attackPower)
    {
        // こっちも軽減処理せずにそのまま返せばいい。
        return attackPower;
    }

    // 一定確率で生き残る
    public static int SafeLife(int life, int attackPower)
    {
        life -= attackPower;

        if (life <= 0)
        {
            int rand = Random.Range(0, 100);
            if (rand < 10)
            {
                life = 1;
            }
        }

        return life;
    }
}