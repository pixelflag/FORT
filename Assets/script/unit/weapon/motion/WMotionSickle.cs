using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WMotionSickle : WMotionBase
{
    [SerializeField]
    private int animFrame = 2;

    public override int GetTotalFrame()
    {
        return animIndexTable.GetLength(1) * animFrame;
    }

    private int[,] animIndexTable =
    {
        { 0,1,2,3,4,5,6,7,0 }, // U
        { 2,3,4,5,6,7,0,1,2 }, // R
        { 4,5,6,7,0,1,2,3,4 }, // D
        { 6,7,0,1,2,3,4,5,6 }, // L
    };

    public Vector3 GetWeaponPosition(int directionIndex)
    {
        return weapons[directionIndex].transform.localPosition;
    }

    public override MotionData GetMotion(Direction4Type direction, int progress)
    {
        int frame = (int)(progress / animFrame);
        int index = animIndexTable[(int)direction, frame];
        return data[index];
    }
}