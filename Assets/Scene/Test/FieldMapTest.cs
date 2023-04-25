using UnityEngine;

public class FieldMapTest : DIMonoBehaviour
{
    [SerializeField]
    private FieldMapData mapData;
    private MapCollision mapCollision;

    private void Start()
    {
        Global.GlobalSetUp();
        Global.isDebugMode = true;
        Global.isShowCollision = true;

        mapCollision = new MapCollision();

        Team[] teams = new Team[]
        {
            new Team(0),
            new Team(1),
        };

        field.CreateMap(teams, mapData);
        field.OnEventHit = OnEventHit;

        Unit unit = field.AddUnit(UnitType.Knight, 0);

        CameraObject mainCamera = Camera.main.GetComponent<CameraObject>();
        mainCamera.Initialize();
        mainCamera.SetTarget(unit.view.baseSprite);
    }

    void FixedUpdate()
    {
        Global.input.Update();

        objects.Execute();
        field.Execute();

        mapCollision.Execute();

        field.CheckEvent();
        objects.CheckDestroy();

        Global.count++;
    }

    private void OnEventHit(string eventName)
    {
        Debug.Log("Event Hit : " + eventName);
    }
}
