using UnityEngine;

public class FieldControl : DIMonoBehaviour
{
    public static FieldControl instance;
    public void Awake()
    {
        instance = this;
        cameraObject = Camera.main.GetComponent<CameraObject>();
        teams = new Team[3];
        teams[0] = new Team(TeamID.A);
        teams[1] = new Team(TeamID.B);
        teams[2] = new Team(TeamID.Neutral);
    }

    [SerializeField]
    private FieldDataLiblary fieldLibrary = default;
    public FieldMapObject map { get; private set; }

    private CameraObject cameraObject;
    public Team[] teams;    // A軍、B軍、中立軍

    private State state;
    private enum State
    {
        Play,
    }

    public void CreateMap(FieldMapName toMapName)
    {
        CreateMap(fieldLibrary.GetFieldMapData(toMapName));
    }

    public void CreateMap(FieldMapData mapData)
    {
        if (map != null)
            Destroy(map.gameObject);

        map = new GameObject(mapData.name).AddComponent<FieldMapObject>();
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

        forcusUnit.SetController(new UnitMouseController());
        // cUnit.SetController(new UnitController());

        // エントリーポイントに登場する。

        Spawn(field.teams[0]);
        Spawn(field.teams[1]);

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
    }

    public void CheckDestroy()
    {
        map.CheckDestroy();
    }

    // Event -----------
    public void CheckEvent()
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