using UnityEngine;
using pixelflag.controller;

public class FieldMapTest : DIMonoBehaviour
{
    [SerializeField]
    private CameraObject mainCamera;
    [SerializeField]
    private FieldMapData mapData;

    private ObjectCollision objectCollision;
    private MapCollision mapCollision;

    private void Start()
    {
        Global.GlobalSetUp();
        Global.isDebugMode = true;
        Global.isShowCollision = true;

        field.OnEventHit = OnEventHit;

        objectCollision = new ObjectCollision();
        objectCollision.Initialize();

        mapCollision = new MapCollision();

        mainCamera.Initialize();
        field.CreateMap(mapData);
        field.SpawnPlayer();

        mainCamera.SetTarget(objects.player.view.baseSprite);
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

        Global.count++;
    }

    private void OnEventHit(string eventName)
    {
        Debug.Log("Event Hit : " + eventName);
    }
}
