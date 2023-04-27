using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionControl
{
    // 向いている方向(4方向)
    public Direction4Type direction4 { get; private set; }
    // 向いている方向(8方向)
    public Direction8Type direction8 { get; private set; }
    // 動いている方向(8方向)
    public DirectionType moveDirection8 { get; private set; }

    public DirectionControl()
    {
        direction4 = Direction4Type.Down;
        direction8 = Direction8Type.Down;
        moveDirection8 = DirectionType.Stop;
    }

    public void SetMoving(float x, float y)
    {
        float t = 0.1f;
        if (x == 0 && t == y) moveDirection8 = DirectionType.Stop;
        if (x == 0 && t <  y) moveDirection8 = DirectionType.Up;
        if (t <  x && t <  y) moveDirection8 = DirectionType.UpRight;
        if (t <  x && y == 0) moveDirection8 = DirectionType.Right;
        if (t <  x && y < -t) moveDirection8 = DirectionType.DownRight;
        if (x == 0 && y < -t) moveDirection8 = DirectionType.Down;
        if (x < -t && y < -t) moveDirection8 = DirectionType.DownLeft;
        if (x < -t && y == 0) moveDirection8 = DirectionType.Left;
        if (x < -t && t <  y) moveDirection8 = DirectionType.UpLeft;
    }

    public void SetGoing(float x, float y)
    {
        if (x == 0 && y == 0) return;

        float t = 0.1f;
        // 8方向
        if (x == 0 && t <  y) direction8 = Direction8Type.Up;
        if (t <  x && t <  y) direction8 = Direction8Type.UpRight;
        if (t <  x && y == 0) direction8 = Direction8Type.Right;
        if (t <  x && y < -t) direction8 = Direction8Type.DownRight;
        if (x == 0 && y < -t) direction8 = Direction8Type.Down;
        if (x < -t && y < -t) direction8 = Direction8Type.DownLeft;
        if (x < -t && y == 0) direction8 = Direction8Type.Left;
        if (x < -t && t <  y) direction8 = Direction8Type.UpLeft;

        // 4方向
        if (x == 0 && t <  y) direction4 = Direction4Type.Up;
        if (t <  x && y == 0) direction4 = Direction4Type.Right;
        if (x == 0 && y < -t) direction4 = Direction4Type.Down;
        if (x < -t && y == 0) direction4 = Direction4Type.Left;
    }

    public void SetDirection(Direction4Type direction)
    {
        direction4 = direction;
    }
}
