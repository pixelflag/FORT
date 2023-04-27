using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WMotionSwing : WMotionBase
{
    [SerializeField]
    private int animWait = 2;

    public override int GetTotalFrame()
    {
        return animIndexTable.GetLength(1) * animWait;
    }

    private int[,] animIndexTable =
    {
        { 2,1,0,7 }, // U
        { 0,1,2,3 }, // R
        { 6,5,4,3 }, // D
        { 0,7,6,5 }, // L
    };

    public override MotionData GetMotion(Direction4Type direction, int progress)
    {
        int frame = (int)(progress / animWait);
        int index = animIndexTable[(int)direction, frame];
        return data[index];
    }
}

