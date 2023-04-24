using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pixelflag.controller;

public class PlayerTest : DIMonoBehaviour
{
    private ControllerInput input;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        Global.GlobalSetUp();
        creater.CreatePlayer(new Vector3());
    }

    void FixedUpdate()
    {
        Global.input.Update();

        objects.Execute();
        objects.CheckDestroy();
    }
}
