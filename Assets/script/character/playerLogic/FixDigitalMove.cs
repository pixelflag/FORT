using UnityEngine;

namespace playerLogic
{
    public class FixDigitalMove
    {
        public Vector2 Execute(float ix, float iy)
        {
            Vector2 vector;
            vector.x = ix < -0.5f ? -1 : (ix > 0.5f ? 1 : 0);
            vector.y = iy < -0.5f ? -1 : (iy > 0.5f ? 1 : 0);
            return vector;
        }
    }
}