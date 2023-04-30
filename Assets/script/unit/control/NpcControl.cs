using UnityEngine;

public class NpcControl : DI, IUnitcontroller
{
    private PRandom random;

    private Unit selfUnit;
    public void SetUnit(Unit unit) { this.selfUnit = unit; }

    private int idleWait => data.npcLogicUpdateWait;
    private int count = 0;

    private TargetRouteMove targetRouteMove;
    private Unit targetUnit;

    private State state;
    private enum State
    {
        Idle,
        Move,
        Attack,
    }
    public bool isIdle => state == State.Idle;
    public bool isControlLock { get; set; }

    public NpcControl(Unit selfUnit, uint randomSeed)
    {
        this.selfUnit = selfUnit;
        random = new PRandom(randomSeed);

        count = random.Range(0, idleWait);

        targetRouteMove = new TargetRouteMove();
        targetRouteMove.Initialize();
    }

    public void Execute()
    {
        if (selfUnit.isDead) return;

        switch (state)
        {
            case State.Idle:
                count++;
                if (idleWait < count)
                {
                    count = 0;
                    // 敵の探索
                    SearchTarget.ClearPotionList();
                    for (int t = 0; t < field.teams.Length; t++)
                    {
                        for (int p = 0; p < field.teams[t].platoons.Count; p++)
                        {
                            for (int u = 0; u < field.teams[t].platoons[p].units.Count; u++)
                            {
                                Unit tUnit = field.teams[t].platoons[p].units[u];

                                if (selfUnit.teamID == tUnit.teamID) continue;
                                if (tUnit.isStealth == true) continue;
                                if (tUnit.isDead == true) continue;

                                SearchTarget.AddSearchTarget(selfUnit.position, tUnit, data.npcSearchRadius);
                            }
                        }
                    }
                    SearchTarget.Result result = SearchTarget.GetClosestPosition();

                    if (result.found == true)
                    {
                        state = State.Move;
                        targetUnit = result.unit;
                        targetRouteMove.SetTarget(field.map, selfUnit.position, result.unit.position);
                    }
                    else
                    {
                        Vector3 randomOffset = new Vector3(random.Range(-64, 64), random.Range(-64, 64), 0);
                        count = 0;
                        if (targetRouteMove.SetTarget(field.map, selfUnit.position, selfUnit.position + randomOffset))
                            state = State.Move;
                    }
                }
                break;
            case State.Move:
                count++;

                targetRouteMove.Execute(selfUnit.position);
                selfUnit.SetVector(new Vector2(targetRouteMove.vector.x, targetRouteMove.vector.y));

                if ( targetRouteMove.isGoal || 180 < count)
                {
                    state = State.Idle;
                    count = 0;
                    break;
                }

                if(targetUnit != null)
                {
                    if (targetUnit.isDead == false)
                    {
                        float dist = Vector3.Distance(targetUnit.position, selfUnit.position);
                        if (dist < selfUnit.weapon.attackRange)
                        {
                            // ソード限定の挙動になっている。各武器によってアルゴリズムを変える必要がある。

                            state = State.Attack;
                            Vector2 vector = (targetUnit.position - selfUnit.position).normalized;
                            selfUnit.Attack(vector);
                        }
                    }
                }
                break;
            case State.Attack:
                count++;
                if (10 < count)
                {
                    state = State.Idle;
                    count = 0;
                }
                break;
        }

    }
}
