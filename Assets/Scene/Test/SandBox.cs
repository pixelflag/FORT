using UnityEngine;

public class SandBox : DIMonoBehaviour
{
    [SerializeField]
    private FieldMapData mapData;
    private ObjectCollision objectCollision;
    private MapCollision mapCollision;

    private void Start()
    {
        Global.GlobalSetUp();
        Global.isDebugMode = true;
        Global.isShowCollision = false;

        TeamData dd1 = new TeamData();
        var p1 = new PlatoonData(UnitType.Knight, UnitType.Knight);
        p1.maxServant = 30;
        dd1.platoon = new PlatoonData[]
        {
            p1,
        };
        Team t1 = new Team(TeamID.A);
        for (int i = 0; i < dd1.platoon.Length; i++)
        {
            t1.CreatePlatoon(dd1.platoon[i], creater.GetFormation(0));
        }

        TeamData dd2 = new TeamData();
        var p2 = new PlatoonData(UnitType.Soldier, UnitType.Soldier);
        p2.maxServant = 30;
        dd2.platoon = new PlatoonData[]
        {
            p2,
        };
        Team t2 = new Team(TeamID.B);
        for (int i = 0; i < dd2.platoon.Length; i++)
        {
            t2.CreatePlatoon(dd2.platoon[i], creater.GetFormation(0));
        }

        field.CreateMap(mapData);
        field.SetTeam(t1);
        field.SetTeam(t2);

        CameraObject mainCamera = Camera.main.GetComponent<CameraObject>();
        mainCamera.Initialize();

        mapCollision = new MapCollision();
        objectCollision = new ObjectCollision();

        field.FieldStart();
    }

    private void Update()
    {
        Global.input.Update();

        objects.Execute();
        field.Execute();

        mapCollision.Execute();
        objectCollision.Execute();

        objects.ExecuteEvent();

        field.CheckEvent();
        objects.CheckDestroy();

        Global.count++;
    }
}
