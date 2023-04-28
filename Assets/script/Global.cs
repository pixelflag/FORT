using UnityEngine;
using pixelflag.controller;

public static class Global
{
    public static int screenWidth = 320;
    public static int screenHeight = 240;

    public static Vector2Int gridSize = new Vector2Int(16,16);

    public static bool isShowCollision = false;
    public static bool isDebugMode = false;
    public static PlayerInputType playerInputType = PlayerInputType.Mouse;

    public static ControllerInput input;

    public static int count;

    // random seed -----
    public static uint mapSeed { get; private set; }
    public static uint enemySeed { get; private set; }

    // ----------

    private static bool isSetupCompleted = false;

    public static void GlobalSetUp()
    {
        Application.targetFrameRate = 60;

        mapSeed = 123;

        if (isSetupCompleted)
            throw new System.Exception("Setup is only once.");

        new ObjectExecutor();
        new GameData();

        DIMonoBehaviour.Injection();
        DI.Injection();

        RouteSearch.Initialize();

        // key config;
        input = new ControllerInput();
        PcControl pckey = new PcControl();
        pckey.P1Button1 = KeyCode.K;
        pckey.P1Option2 = KeyCode.P;
        input.SetPcConfig(GamePadNum.Gamepad1, pckey);

        isSetupCompleted = true;
    }
}

public class DIMonoBehaviour : MonoBehaviour
{
    protected static ObjectExecutor objects;
    protected static ObjectCreater creater;
    protected static FieldControl field;
    protected static SoundManager sound;
    protected static GameData data;

    public static void Injection()
    {
        objects = ObjectExecutor.instance;
        creater = ObjectCreater.instance;
        sound = SoundManager.instance;
        data = GameData.instance;
        field = FieldControl.instance;
    }
}

public class DI
{
    protected static ObjectExecutor objects;
    protected static ObjectCreater creater;
    protected static FieldControl field;
    protected static SoundManager sound;
    protected static GameData data;

    public static void Injection()
    {
        objects = ObjectExecutor.instance;
        creater = ObjectCreater.instance;
        sound = SoundManager.instance;
        data = GameData.instance;
        field = FieldControl.instance;
    }
}

public enum PlayerInputType
{
    Controler,
    Mouse,
}
