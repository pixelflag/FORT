using UnityEngine;

#if UNITY_EDITOR
namespace pixelflag.Editor
{
    [ExecuteAlways]
    [RequireComponent(typeof(SpriteRenderer))]
    public class LocationTest : MonoBehaviour
    {
        public int index;
        public Vector2Int location;
        public FieldCellData activeCell;

        private void Update()
        {
            Vector3 pos = transform.localPosition;
            int gx = Global.gridSize.x;
            int gy = Global.gridSize.y;

            pos.x = Mathf.Round((pos.x) / gx ) * gx;
            pos.y = Mathf.Round((pos.y) / gy ) * gy;

            transform.localPosition = pos;
            location = new Vector2Int((int)pos.x / gx, (int)pos.y/ gy);

            var map = transform.parent.GetComponent<FieldMapData>();
            if (map != null)
            {
                activeCell = map.GetCellData(location);
            }
        }
    }
}
#endif