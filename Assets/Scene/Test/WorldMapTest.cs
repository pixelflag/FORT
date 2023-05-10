using UnityEngine;

public class WorldMapTest : DIMonoBehaviour
{
    [SerializeField]
    private WorldMapData worldData;

    private ObjectCollision objectCollision;

    private WorldScene scene;

    private void Start()
    {
        Global.GlobalSetUp();
        Global.isDebugMode = true;
        Global.isShowCollision = false;

        RouteSearch.searchLimit = 512;

        CameraObject mainCamera = Camera.main.GetComponent<CameraObject>();
        mainCamera.Initialize();

        objectCollision = new ObjectCollision();

        scene = new WorldScene(worldData);
    }

    private void Update()
    {
        Global.input.Update();

        scene.Execute();

        objects.Execute();
        objectCollision.Execute();
        objects.CheckDestroy();

        Global.count++;
    }

    [ContextMenu("Show Route Score")]
    private void ShowScore()
    {
        scene.ShowRouteScore();
    }
}
