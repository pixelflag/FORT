using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTest : DIMonoBehaviour
{
    [SerializeField]
    private FieldMapData map;

    private ObjectCollision objectCollision;
    private MapCollision mapCollision;

    public CameraObject mainCamera;

    private void Start()
    {
        Global.GlobalSetUp();
        Global.isDebugMode = true;

        objectCollision = new ObjectCollision();
        objectCollision.Initialize();

        mapCollision = new MapCollision();

        // ----------
        // creater.CreateEnemy(EnemyName.Slime, new Vector3(100, 100, 0));

        // mainCamera.Initialize();
        // mainCamera.SetTarget(objects.player.gameObject);

        //Player player = objects.player;
        //player.position = mapEntrance.position;
        //player.direction.SetDirection(mapEntrance.direction);
        //objects.player.ControlLock(false);

        // field.TestEnterMap(map.GetComponent<FieldMapData>());
    }

    void Update()
    {
        Global.input.Update();

        objects.Execute();
        field.Execute();

        mapCollision.Execute();
        objectCollision.Execute();

        // ダメージイベント処理

        field.CheckEvent();

        objects.CheckDestroy();
        field.CheckDestroy();
    }
}