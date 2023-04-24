using UnityEngine;
using pixelflag.controller;

public class RouteSeachTest : DIMonoBehaviour
{
    [SerializeField]
    private GameObject map;

    private ControllerInput input;

    private ObjectCollision objectCollision;
    private MapCollision mapCollision;

    public CameraObject mainCamera;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        Global.GlobalSetUp();
        Global.isDebugMode = true;

        // key config;
        input = new ControllerInput();
        PcControl pckey = new PcControl();
        pckey.P1Button1 = KeyCode.K;
        pckey.P1Option2 = KeyCode.P;
        input.SetPcConfig(GamePadNum.Gamepad1, pckey);

        creater.CreatePlayer(new Vector3());
        creater.CreateBuddies(BuddiesType.Knight, new Vector3());

        objectCollision = new ObjectCollision();
        objectCollision.Initialize();

        mapCollision = new MapCollision();

        mainCamera.Initialize();
        mainCamera.SetTarget(objects.player.gameObject);

        // field.EnterMap(map.GetComponent<FieldMapData>());
    }

    void FixedUpdate()
    {
        Global.input.Update();

        objects.Execute();
        field.Execute();

        mapCollision.Execute(field.map, objects);
        objectCollision.Execute(objects);

        field.CheckEvent();

        objects.CheckDestroy();
    }
}
