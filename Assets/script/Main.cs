using UnityEngine;
using pixelflag.controller;
using UnityEngine.Tilemaps;

public class Main : DIMonoBehaviour
{
    [SerializeField]
    private UIManager ui = default;
    [SerializeField]
    private CameraObject mainCamera = default;

    public Tilemap collisionMap;
    public Tilemap groundMap;

    private StageManager stageManager = default;

    private MapCollision mapCollision;
    private ObjectCollision objectCollision;

    private bool isPause = false;

    private State state;
    private enum State
    {
        Title,
        Start,
        Play,
        GameOver,
        GameClear,
    }

    private void Start()
    {
        Global.GlobalSetUp();

        objectCollision = new ObjectCollision();
        objectCollision.Initialize();

        mapCollision = new MapCollision();

        ui.Initialize();

        stageManager = GetComponent<StageManager>();
        stageManager.Initialize(mainCamera,ui);
        stageManager.OnStartGame = OnStartGame;

        ui.HideCenter();
        ui.GameStart();

        state = State.Title;
        RestartGame();
    }

    private void OnStartGame()
    {
        state = State.Play;
        ui.HideCenter();
    }

    private void FixedUpdate()
    {
        Global.input.Update();

        switch (state)
        {
            case State.Title:
            case State.Start:
                break;
            case State.Play:
                if (Global.input.pad1.GetKeyDown(ControllerButtonType.Option2))
                {
                    if(isPause)
                    {
                        isPause = false;
                        sound.PlayOneShot(SeType.Pause,0.5f);
                        sound.ResumeBgm();
                        ui.HideCenter();
                    }
                    else
                    {
                        isPause = true;
                        sound.PlayOneShot(SeType.Pause, 0.5f);
                        sound.PauseBgm();
                        ui.Pause();
                    }
                }
                break;
            case State.GameOver:
            case State.GameClear:
                if (Global.input.pad1.GetKeyDown(ControllerButtonType.Option2))
                {
                    state = State.Start;
                    ui.HideCenter();
                    RestartGame();
                }
                break;
        }

        if (isPause) return;

        objects.Execute();
        mapCollision.Execute(field.map, objects);
        objectCollision.Execute(objects);
        objects.CheckDestroy();
        mainCamera.Execute();
        stageManager.Excute();

        switch (state)
        {
            case State.Play:
                if (stageManager.player.isDead)
                {
                    sound.PlayJingle(BgmType.GameOver, 1);
                    ui.GameOver();
                    state = State.GameOver;
                }
                else if (stageManager.bossDead)
                {
                    sound.PlayJingle(BgmType.Clear, 0.6f);
                    ui.GameClear();
                    state = State.GameClear;
                }
                break;
        }
        ui.Excute();
    }

    private void RestartGame()
    {
        objects.Reset();
        stageManager.Reset();
        state = State.Start;

        isPause = false;

        ui.GameStart();

        sound.StopBgm();
    }
}
