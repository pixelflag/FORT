using UnityEngine;

[ExecuteAlways]
public class WorldMapCastleData : MonoBehaviour
{
#if UNITY_EDITOR
    private void Update()
    {
        Vector3 pos = transform.position;
        _location = new Vector2Int((int)(pos.x / Global.gridSize.x), -(int)(pos.y / Global.gridSize.y));
    }
#endif

    [SerializeField]
    private Vector2Int _location;
    public Vector2Int location => _location;   
}
