using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoveLogic
{
    public static void InertiaMove(ref Vector2 vector, float friction)
    {
        vector.x = vector.x * (1 - friction);
        vector.y = vector.y * (1 - friction);
        vector.x = (int)(vector.x * 100) / 100;
        vector.y = (int)(vector.y * 100) / 100;
    }
}