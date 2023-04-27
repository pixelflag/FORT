using UnityEngine;

public class FieldControl : DIMonoBehaviour
{
    public static FieldControl instance;
    public void Awake()
    {
        instance = this;
        cameraObject = Camera.main.GetComponent<CameraObject>();
    }

    [SerializeField]
    private FieldDataLiblary fieldLibrary = default;
    public FieldMapObject map { get; private set; }

    private CameraObject cameraObject;
    public Team[] teams;

    private State state;
    private enum State
    {
        Play,
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

    public Unit AddUnit(UnitType type, int teamID)
    {
        // とりあえず。エントリーポイントの仕様はあとで考える。

        var ent = map.GetEntrance(0);
        Unit unit = creater.CreateUnit(type, teamID, ent.position);
        unit.SetDirection(ent.direction);
        unit.OnDestroy += (MassObject mo) =>
        {
            teams[unit.teamID].units.Remove(unit);
        };
        teams[unit.teamID].units.Add(unit);

        // 動作ロジックの選択
        // もうすこし詳細が決まて来てから考える。
        if(true)
        {
            unit.SetController(new UnitMouseController());
        }
        else
        {
            unit.SetController(new UnitController());
        }

        return unit;
    }

    public void CheckDestroy()
    {
        map.CheckDestroy();
    }

    public void CreateMap(Team[] teams, FieldMapName toMapName)
    {
        CreateMap(teams, fieldLibrary.GetFieldMapData(toMapName));
    }

    public void CreateMap(Team[] teams, FieldMapData mapData)
    {
        this.teams = teams;

        if (map != null)
            Destroy(map.gameObject);

        map = new GameObject(mapData.name).AddComponent<FieldMapObject>();
        map.Initialize(mapData);

        cameraObject.topRight   = new Vector2(map.areaSize.x * Global.gridSize.x, 0);
        cameraObject.bottomLeft = new Vector2(0, -map.areaSize.y * Global.gridSize.y);
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