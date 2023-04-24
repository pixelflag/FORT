using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WMotionRod : WMotionBase
{
    [SerializeField]
    private int animFrame = 8;

    public override int GetTotalFrame()
    {
        return animIndexTable.GetLength(1) * animFrame;
    }

    private int[,] animIndexTable =
    {
        { 0 }, // U
        { 1 }, // R
        { 2 }, // D
        { 3 }, // L
    };

    public Vector3 GetWeaponPosition(int directionIndex)
    {
        return weapons[directionIndex].transform.localPosition;
    }

    public override MotionData GetMotion(Direction4Type direction, int progress)
    {
        int index = animIndexTable[(int)direction, 0];
        return data[index];
    }
}