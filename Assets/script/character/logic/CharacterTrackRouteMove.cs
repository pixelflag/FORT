using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTrackRouteMove : ICharacterLogic
{
    private int idleWait = 30;
    private int count = 0;

    private Unit self;
    private Vector2 offsetPosition;

    private int seachWait = 20;
    private int seachCount = 0;
    private SearchTarget searchTarget;
    private TargetRouteMove targetRouteMove;

    private State state;
    private enum State
    {
        Idle,
        Walk,
    }
    public bool isIdle { get{ return state == State.Idle; } }

    public void Initialize(Unit self, int seachArea)
    {
        this.self = self;

        count = Random.Range(0, idleWait);
        seachCount = Random.Range(0, seachWait);
        searchTarget = new SearchTarget(seachArea);

        targetRouteMove = new TargetRouteMove();
        targetRouteMove.Initialize();

        offsetPosition = new Vector2(0,0);
    }

    public void SetPositionTarget(Vector2 position)
    {
        offsetPosition = position;
    }

    public void SetSeachRadius(float radius)
    {
        searchTarget.SetRadius(radius);
    }

    public void Execute(FieldMapObject map, Unit player, List<Unit> targets)
    {
        switch (state)
        {
            case State.Idle:
                count++;
                if (idleWait < count)
                {
                    count = 0;
                    Vector2 pPos = player.position;
                    Vector2 nextTarget = pPos + offsetPosition;
                    
                    // ここ、あとでfalseの時は動かないように変更
                    if( targetRouteMove.SetTarget(map, self.position, nextTarget))
                        state = State.Walk;
                }
                break;
            case State.Walk:
                count++;
                targetRouteMove.Execute(self.position);
                self.SetMoveInput(targetRouteMove.direction.x, targetRouteMove.direction.y);

                if (targetRouteMove.isGoal || count > 180)
                {
                    state = State.Idle;
                    count = 0;
                }
                break;
        }

        seachCount++;
        if (seachWait < seachCount)
        {
            seachCount = 0;
            SearchTargetResult result = searchTarget.Seach(self.position, targets);

            if (result.found == true)
            {
                targetRouteMove.SetTarget(map, self.position, result.position);

                // 今は変な動作をする。あとで修正する。
                self.Attack();
                state = State.Walk;
            }
        }
    }
}
