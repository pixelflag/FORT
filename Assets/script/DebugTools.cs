using UnityEngine;

[ExecuteAlways]
public class DebugTools : MonoBehaviour
{
    [SerializeField]
    private Vector3 mousePosition;
    [SerializeField]
    private Vector3 worldPosition;
    [SerializeField]
    private Vector2Int mapLocation;
    [SerializeField]
    private Vector3 locationToPosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            mapLocation = MapUtility.PositionToLocation(worldPosition);
            locationToPosition = MapUtility.LocationToPosition(mapLocation);
        }
    }
}
