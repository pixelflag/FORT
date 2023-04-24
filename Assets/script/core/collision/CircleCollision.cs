using UnityEngine;

public static class CircleCollision
{
    public static Vector3 PositionCorrection(Vector3 position, float radius, Vector3 targetPosition, float targetRadius)
    {
        if(HitCheck(position, radius, targetPosition, targetRadius))
        {
            float radian = Calculate.PositionToRadian(targetPosition, position);
            return Calculate.RadianToVector(radian, radius + targetRadius) + targetPosition;
        }
        return position;
    }

    public static bool HitCheck(Vector3 position, float radius, Vector3 targetPosition, float targetRadius)
    {
        float dist = Calculate.Distance(position, targetPosition);
        float totalRadius = radius + targetRadius;

        return dist < totalRadius;
    }
}


