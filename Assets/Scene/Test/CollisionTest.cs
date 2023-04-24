using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pixelflag.controller;

public class CollisionTest : DIMonoBehaviour
{
    private ObjectCollision objectCollision;
    private MapCollision mapCollision;
    private ControllerInput input;

    public FieldMapData map;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        Global.GlobalSetUp();

        objectCollision = new ObjectCollision();
        objectCollision.Initialize();

        mapCollision = new MapCollision();

        /*
         * 使えなくなったので、必要な時に直して使用する。
        RouteSearch routeSearch = new RouteSearch();
        routeSearch.SetMap(map.topRight, map.bottomLeft, map.collisionMap);
        */

        creater.CreatePlayer(new Vector3());
        creater.CreateEnemy(EnemyName.Slime, new Vector3( 50,  50, 0));
    }

    void FixedUpdate()
    {
        Global.input.Update();

        objects.Execute();
        
        objectCollision.Execute(objects);
        mapCollision.Execute(field.map, objects);

        objects.CheckDestroy();
    }
}
