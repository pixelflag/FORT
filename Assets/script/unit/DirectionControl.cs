using System;
using UnityEngine;

[Serializable]
public class DirectionControl
{
    // 向いている方向(4方向)
    public Direction4Type direction4 { get; private set; }
    // 向いている方向(8方向)
    public Direction8Type direction8 { get; private set; }
    // 動いている方向(8方向)
    public DirectionType moveDirection8 { get; private set; }

    private float q1;
    private float q2;
    private float q3;
    private float q5;
    private float q6;
    private float q7;
    private float q9;
    private float q10;
    private float q11;
    private float q13;
    private float q14;
    private float q15;

    public DirectionControl()
    {
        direction4 = Direction4Type.Down;
        direction8 = Direction8Type.Down;
        moveDirection8 = DirectionType.Stop;

        q1 = Mathf.PI / 8;
        q2 = q1 * 2;
        q3 = q1 * 3;
        q5 = q1 * 5;
        q6 = q1 * 6;
        q7 = q1 * 7;
        q9 = q1 * 9;
        q10 = q1 * 10;
        q11 = q1 * 11;
        q13 = q1 * 13;
        q14 = q1 * 14;
        q15 = q1 * 15;
    }

    public void SetMoving(float x, float y)
    {
        float rad = Calculate.PositionToRadian(Vector3.zero, new Vector3(x,y,0));

        // 8方向
        if (x == 0 && y == 0) moveDirection8 = DirectionType.Stop;
        else if (0 < rad && rad < q1) moveDirection8 = DirectionType.Right;
        else if (q1 < rad && rad < q3) moveDirection8 = DirectionType.UpRight;
        else if (q3 < rad && rad < q5) moveDirection8 = DirectionType.Up;
        else if (q5 < rad && rad < q7) moveDirection8 = DirectionType.UpLeft;
        else if (q7 < rad && rad < q9) moveDirection8 = DirectionType.Left;
        else if (q9 < rad && rad < q11) moveDirection8 = DirectionType.DownLeft;
        else if (q11 < rad && rad < q13) moveDirection8 = DirectionType.Down;
        else if (q13 < rad && rad < q15) moveDirection8 = DirectionType.DownRight;
        else moveDirection8 = DirectionType.Right;
    }

    public void SetLooking(float x, float y)
    {
        if (x == 0 && y == 0) return;

        float rad = Calculate.PositionToRadian(Vector3.zero, new Vector3(x, y, 0));

        // 8方向
        if (rad < q1) direction8 = Direction8Type.Right;
        else if (rad < q3) direction8 = Direction8Type.UpRight;
        else if (rad < q5) direction8 = Direction8Type.Up;
        else if (rad < q7) direction8 = Direction8Type.UpLeft;
        else if (rad < q9) direction8 = Direction8Type.Left;
        else if (rad < q11) direction8 = Direction8Type.DownLeft;
        else if (rad < q13) direction8 = Direction8Type.Down;
        else if (rad < q15) direction8 = Direction8Type.DownRight;
        else direction8 = Direction8Type.Right;

        // 4方向
        if (rad < q2) direction4 = Direction4Type.Right;
        else if(rad < q6) direction4 = Direction4Type.Up;
        else if(rad < q10) direction4 = Direction4Type.Left;
        else if(rad < q14) direction4 = Direction4Type.Down;
        else direction4 = Direction4Type.Right;
    }

    public void SetDirection(Direction4Type direction)
    {
        direction4 = direction;
    }
}
