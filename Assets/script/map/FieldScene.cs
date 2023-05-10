using UnityEngine;

public class FieldScene : DI
{
    public FieldScene()
    {
        cameraObject = Camera.main.GetComponent<CameraObject>();
        teams = new Team[3];
        teams[0] = new Team(TeamID.A);
        teams[1] = new Team(TeamID.B);
        teams[2] = new Team(TeamID.Neutral);
    }

    public FieldMap map { get; private set; }

    private ObjectCollision objectCollision;
    private MapCollision mapCollision;

    private CameraObject cameraObject;
    public Team[] teams;    // A軍、B軍、中立軍

    private State state;
    private enum State
    {
        Play,
    }

    public void CreateMap(FieldMapName toMapName)
    {
        CreateMap(creater.fieldLibrary.GetFieldMapData(toMapName));
    }

    public void CreateMap(FieldMapData mapData)
    {
        if (map != null)
            GameObject.Destroy(map.gameObject);

        map = new GameObject(mapData.name).AddComponent<FieldMap>();
        map.Initialize(mapData);

        cameraObject.topRight = new Vector2(map.areaSize.x * Global.gridSize.x, 0);
        cameraObject.bottomLeft = new Vector2(0, -map.areaSize.y * Global.gridSize.y);

        // 中立を作る
        if(0 < mapData.units.Length)
        {
            Team nTeam = new Team(TeamID.Neutral);
            foreach (MapUnitData ud in mapData.units)
            {
                nTeam.CreateNpc(ud.unitType, ud.positon);
            }
            teams[(int)TeamID.Neutral] = nTeam;
        }

        mapCollision = new MapCollision(teams, map);
        objectCollision = new ObjectCollision();

        objects.areaSize = map.areaSize;

        ServantControl.field = this;
        NpcControl.field = this;
    }

    public void SetTeam(Team team)
    {
        teams[(int)team.teamID] = team;
    }

    public void FieldStart()
    {
        // ひとまず先頭にフォーカスする。
        Unit forcusUnit = teams[0].platoons[0].units[0];
        cameraObject.SetTarget(forcusUnit.gameObject);

        forcusUnit.SetController(new UnitMouseController(map));
        // cUnit.SetController(new UnitController());

        // エントリーポイントに登場する。

        Spawn(teams[0]);
        Spawn(teams[1]);

        void Spawn(Team team)
        {
            var ent = GetEnterPosition(team.teamID);

            for (int p = 0; p < team.platoons.Count; p++)
            {
                for (int u = 0; u < team.platoons[p].units.Count; u++)
                {
                    Unit unit = team.platoons[p].units[u];
                    unit.position = ent.position;
                    unit.SetDirection(ent.direction);
                }
            }
        }

        MapEntranceObject GetEnterPosition(TeamID teamID)
        {
            switch (teamID)
            {
                case TeamID.A:
                    return map.GetEntranceA(0);
                case TeamID.B:
                    return map.GetEntranceB(0);
                default:
                    throw new System.Exception("enter point not not found. : " + teamID);
            }
        }
    }

    public void Execute()
    {
        if (map == null) return;

        switch (state)
        {
            case State.Play:
                map.Execute();
                cameraObject.Execute();
                break;
        }

        for (int t = 0; t < teams.Length; t++)
        {
            for (int p = 0; p < teams[t].platoons.Count; p++)
            {
                for (int u = 0; u < teams[t].platoons[p].units.Count; u++)
                {
                    teams[t].platoons[p].units[u].Execute();
                }
            }
        }

        mapCollision.Execute();

        for (int i = 0; i < teams.Length; i++)
        {
            for (int j = 0; j < teams.Length; j++)
            {
                if (i != j)
                    HitCkeck(teams[i], teams[j]);
            }
        }

        void HitCkeck(Team t1, Team t2)
        {
            for (int p1 = 0; p1 < t1.platoons.Count; p1++)
            {
                for (int u1 = 0; u1 < t1.platoons[p1].units.Count; u1++)
                {
                    Unit unit1 = t1.platoons[p1].units[u1];
                    if (unit1.weapon != null)
                    {
                        CollisionObject co1 = unit1.weapon.collision;
                        for (int p2 = 0; p2 < t2.platoons.Count; p2++)
                        {
                            for (int u2 = 0; u2 < t2.platoons[p2].units.Count; u2++)
                            {
                                Unit unit2 = t2.platoons[p2].units[u2];
                                if (CheckCollision(co1, unit2.collision))
                                    unit2.HitAttack(unit1.GetAttackData());
                            }
                        }
                    }
                }
            }
        }

        bool CheckCollision(CollisionObject objA, CollisionObject objB)
        {
            if (!objA.objectCollisionDisabled && !objB.objectCollisionDisabled)
            {
                if (BoxCollision.BoxHitCheck(objA.box, objB.box))
                {
                    return true;
                }
            }
            return false;
        }

        CheckEvent();

        ExecuteEvent();
    }

    public void CheckDestroy()
    {
        map.CheckDestroy();

        for (int t = 0; t < teams.Length; t++)
        {
            for (int p = 0; p < teams[t].platoons.Count; p++)
            {
                for (int u = teams[t].platoons[p].units.Count - 1; 0 <= u; u--)
                {
                    if (teams[t].platoons[p].units[u].isDestroy == true)
                    {
                        GameObject.Destroy(teams[t].platoons[p].units[u].gameObject);
                        teams[t].platoons[p].units.RemoveAt(u);
                    }
                }
            }
        }
    }

    public void ExecuteEvent()
    {
        for (int t = 0; t < teams.Length; t++)
        {
            for (int p = 0; p < teams[t].platoons.Count; p++)
            {
                for (int u = 0; u < teams[t].platoons[p].units.Count; u++)
                {
                    teams[t].platoons[p].units[u].ExecuteEvent();
                }
            }
        }
    }

    // Event -----------
    private void CheckEvent()
    {
        /*
        // イベントに誰が対象になるのかが不明確なので保留
        //Vector2 pPos = objects.player.position;

        // Event
        foreach (MapEventObject ev in map.events)
        {
            // イベントはあとで再設計になる。いろんなタイプのイベントが登場してどうかというところ。
            ev.UpdateState(BoxCollision.PointHitCheck(pPos, ev.box));
            if (ev.isEnd == false && ev.isEnter == true)
            {
                ev.CheckOneShot();
                if (OnEventHit != null) OnEventHit(ev.eventName);
                break;
            }
        }
        */
    }

    public delegate void EventHitDelegate(string eventName);
    public EventHitDelegate OnEventHit;
}