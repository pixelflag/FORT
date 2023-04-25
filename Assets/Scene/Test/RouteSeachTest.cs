using UnityEngine;

public class RouteSeachTest : DIMonoBehaviour
{
    [SerializeField]
    private FieldMapData mapData;
    private MapCollision mapCollision;

    private void Start()
    {
        Global.GlobalSetUp();
        Global.isDebugMode = true;
        Global.isShowCollision = false;

        mapCollision = new MapCollision();

        Team[] teams = new Team[]
        {
            new Team(0),
            new Team(1),
        };

        field.CreateMap(teams, mapData);
        field.AddUnit(UnitType.Knight, 0);
    }

    void FixedUpdate()
    {
        Global.input.Update();

        objects.Execute();
        field.Execute();

        mapCollision.Execute();

        objects.ExecuteEvent();

        field.CheckEvent();
        objects.CheckDestroy();

        Global.count++;
    }
}
