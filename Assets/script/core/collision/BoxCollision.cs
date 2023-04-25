using UnityEngine;

public static class BoxCollision
{
    public static bool PointHitCheck(Vector2 Position, Box box)
    {
        return box.bottom < Position.y && Position.y < box.top &&
               box.left   < Position.x && Position.x < box.right;
    }

    public static CollisionResult BoxPositionCorrection(MassObject obj, Box[] boxes)
    {
        CollisionResult result = new CollisionResult();
        Box box = obj.collision.box;
        box.position = obj.prevPosition;

        // X座標の補正
        box.x = obj.x;
        foreach (Box box2 in boxes)
        {
            if (BoxHitCheck(box, box2))
            {
                result.isHit = true;
                if (obj.prevPosition.x < box.x)
                    box.x = box2.left - box.extendsX;
                else
                    box.x = box2.right + box.extendsX;
            }
        }

        // Y座標の補正
        box.y = obj.y;
        foreach (Box box2 in boxes)
        {
            if (BoxHitCheck(box, box2))
            {
                result.isHit = true;
                if (obj.prevPosition.y < box.y)
                    box.y = box2.bottom - box.extendsY;
                else
                    box.y = box2.top + box.extendsY;
            }
        }
        result.position = box.position;
        return result;
    }

    public static bool BoxHitCheck(Box box1, Box box2)
    {
        return box1.top > box2.bottom && box1.bottom < box2.top &&
               box1.right > box2.left && box1.left < box2.right;
    }

    // --------
    public static Vector3 CirclePositionCorrection(Vector3 position, int radius, Box box)
    {
        if (CircleHitCheck(position, radius, box))
        {
            position = PositionCorrection(position, radius, box);
        }
        return position;

        Vector3 PositionCorrection(Vector3 pos, float radius, Box box)
        {
            bool R = box.right < pos.x;
            bool L = pos.x < box.left;

            bool T = box.top < pos.y;
            bool B = pos.y < box.bottom;

            if (L && T)
            {
                return CircleCollision.PositionCorrection(pos, radius, new Vector3(box.left, box.top, 0), 0);
            }
            else if (R && T)
            {
                return CircleCollision.PositionCorrection(pos, radius, new Vector3(box.right, box.top, 0), 0);
            }
            else if (L && B)
            {
                return CircleCollision.PositionCorrection(pos, radius, new Vector3(box.left, box.bottom, 0), 0);
            }
            else if (R && B)
            {
                return CircleCollision.PositionCorrection(pos, radius, new Vector3(box.right, box.bottom, 0), 0);
            }
            else if (!L && !R && T)
            {
                return new Vector3(pos.x, box.top + radius, pos.z);
            }
            else if (!L && !R && B)
            {
                return new Vector3(pos.x, box.bottom - radius, pos.z);
            }
            else if (!T && !B && L)
            {
                return new Vector3(box.left - radius, pos.y, pos.z);
            }
            else if (!T && !B && R)
            {
                return new Vector3(box.right + radius, pos.y, pos.z);
            }
            else
            {
                Debug.Log("object in box!!");
                return pos;
            }
        }
    }

    public static bool CircleHitCheck(Vector2 pos, float radius, Box box)
    {
        float sqrRadius = new Vector2(radius,0).sqrMagnitude;

        bool R = box.right < pos.x;
        bool L = pos.x < box.left;

        bool T = box.top < pos.y;
        bool B = pos.y < box.bottom;

        if (L && T)
        {
            float dist = (pos - box.topLeft).SqrMagnitude();
            return dist < sqrRadius;
        }
        else if (R && T)
        {
            float dist = (pos - box.topRight).SqrMagnitude();
            return dist < sqrRadius;
        }
        else if (L && B)
        {
            float dist = (pos - box.bottomLeft).SqrMagnitude();
            return dist < sqrRadius;
        }
        else if (R && B)
        {
            float dist = (pos - box.bottomRight).SqrMagnitude();
            return dist < sqrRadius;
        }
        else if (!L && !R && T)
        {
            return pos.y - radius < box.top;
        }
        else if (!L && !R && B)
        {
            return pos.y + radius > box.bottom;
        }
        else if (!T && !B && L)
        {
            return pos.x + radius > box.left;
        }
        else if (!T && !B && R)
        {
            return pos.x - radius < box.right;
        }
        else
        {
            Debug.Log("object in box!!");
            return false;
        }
    }
}