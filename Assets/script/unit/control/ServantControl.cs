using UnityEngine;

public class ServantControl : DI, IUnitcontroller
{
    private PRandom random;

    private Unit masterUnit;
    private Unit selfUnit;
    public void SetUnit(Unit unit) { this.selfUnit = unit; }

    private int idleWait => data.npcLogicUpdateWait;
    private int count = 0;

    private Vector2 offsetPosition;

    private TargetRouteMove targetRouteMove;
    private Unit targetUnit;

    private State state;
    private enum State
    {
        Idle,
        Move,
        Chase,
        Attack,
    }
    public bool isIdle => state == State.Idle;
    public bool isControlLock { get; set; }

    public ServantControl(Unit masterUnit, Unit selfUnit, int seachArea, uint randomSeed)
    {
        this.masterUnit = masterUnit;
        this.selfUnit = selfUnit;
        random = new PRandom(randomSeed);

        count = random.Range(0, idleWait);

        targetRouteMove = new TargetRouteMove();
        targetRouteMove.Initialize();

        offsetPosition = new Vector2(0,0);
    }

    public void Execute()
    {
        // masterUnitが居なくなった時の処理
        if (selfUnit.isDead) return;

        switch (state)
        {
            case State.Idle:
                count++;
                if (idleWait < count)
                {
                    count = 0;
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

                                SearchTarget.AddSearchTarget(selfUnit.position, tUnit, data.servantSearchRadius);
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
                        Vector2 nextTarget = (Vector2)masterUnit.position + offsetPosition;
                        if (targetRouteMove.SetTarget(field.map, selfUnit.position, nextTarget))
                            state = State.Move;
                    }
                }
                break;
            case State.Move:
                count++;

                targetRouteMove.Execute(selfUnit.position);
                selfUnit.SetVector(new Vector2(targetRouteMove.vector.x, targetRouteMove.vector.y));

                if (targetRouteMove.isGoal || 180 < count)
                {
                    count = 0;
                    state = State.Idle;
                    break;
                }

                if(targetUnit != null)
                {
                    if(targetUnit.isDead == false)
                    {
                        float dist = Vector3.Distance(targetUnit.position, selfUnit.position);
                        if (dist < selfUnit.weapon.attackRange)
                        {
                            state = State.Attack;
                            Vector2 vector = (targetUnit.position - selfUnit.position).normalized;
                            selfUnit.Attack(vector);
                        }
                    }
                }
                break;
            case State.Chase:
                count++;


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

    public void SetOffsetPositon(Vector2 position)
    {
        offsetPosition = position;
    }
}
