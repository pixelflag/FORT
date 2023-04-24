using UnityEngine;

public class FieldControl : DIMonoBehaviour
{
    public static FieldControl instance;
    public void Awake()
    {
        instance = this;
        cameraObject = Camera.main.GetComponent<CameraObject>();
    }

    public FieldMapObject map { get; private set; }

    [SerializeField]
    private FieldDataLiblary fieldLibrary = default;

    private CameraObject cameraObject;

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

    public void CheckDestroy()
    {
        map.CheckDestroy();
    }

    public void CreateMap(FieldMapName toMapName)
    {
        CreateMap(fieldLibrary.GetFieldMapData(toMapName));
    }

    public void SpawnPlayer()
    {
        // とりあえず

        if (map == null) return;
        Player player = creater.CreatePlayer(new Vector3());
        var ent = map.GetEntrance(0);
        player.position = ent.position;
        player.SetDirection(ent.direction);
    }

    public void CreateMap(FieldMapData mapData)
    {
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
        Vector2 pPos = objects.player.position;
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
    }

    public delegate void EventHitDelegate(string eventName);
    public EventHitDelegate OnEventHit;
}