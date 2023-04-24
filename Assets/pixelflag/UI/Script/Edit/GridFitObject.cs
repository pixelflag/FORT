using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pixelflag.UI
{
    [ExecuteAlways]
    public class GridFitObject : MonoBehaviour
    {
        public bool fitToGrid = true;
        public bool disableZ = true;
        public int grid = 8;
        public int offset = 0;

#if UNITY_EDITOR
        private void Update()
        {
            if (fitToGrid)
                GridFitting(grid, offset);
            else
                GridFitting(1, 0);
        }

        private void GridFitting(float grid, float offset)
        {
            Vector3 pos = transform.localPosition;
            pos.x = Mathf.Round((pos.x - offset) / grid) * grid + offset;
            pos.y = Mathf.Round((pos.y - offset) / grid) * grid + offset;

            if (!disableZ)
                pos.z = Mathf.Round((pos.z - offset) / grid) * grid + offset;

            transform.localPosition = pos;
        }
    }
#endif
}