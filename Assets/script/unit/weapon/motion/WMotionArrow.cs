using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WMotionArrow : WMotionBase
{
    [SerializeField]
    private int ReadyFrame = 4;
    [SerializeField]
    private int ReleaseFrame = 4;

    public override int GetTotalFrame()
    {
        return ReadyFrame + ReleaseFrame;
    }

    private int[,] animIndexTable =
    {
        { 4,0 }, // U
        { 5,1 }, // R
        { 6,2 }, // D
        { 7,3 }, // L
    };

    public override MotionData GetMotion(Direction4Type direction, int progress)
    {
        int frame = 0;
        if (ReadyFrame <= progress)
            frame = 1;

        int index = animIndexTable[(int)direction, frame];
        return data[index];
    }
}