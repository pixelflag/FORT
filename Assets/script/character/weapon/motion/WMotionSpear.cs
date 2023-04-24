using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WMotionSpear : WMotionBase
{
    [SerializeField]
    private int[] animFrame = { 2, 4 };

    public override int GetTotalFrame()
    {
        int total = 0;
        foreach (int f in animFrame)
        {
            total += f;
        }
        return animIndexTable.GetLength(1) * total;
    }

    private int[,] animIndexTable =
    {
        { 0,1 }, // U
        { 2,3 }, // R
        { 4,5 }, // D
        { 6,7 }, // L
    };

    public Vector3 GetWeaponPosition(int directionIndex)
    {
        return weapons[directionIndex].transform.localPosition;
    }

    public override MotionData GetMotion(Direction4Type direction, int progress)
    {
        int frame = AskFrame(progress);
        int index = animIndexTable[(int)direction, frame];
        return data[index];

        // この方式は重いので、やめないといけない。編集方法も含めてあとで考え直し。
        int AskFrame(int progress)
        {
            for (int i = 0; i < animFrame.Length; i++)
            {
                int total = 0;
                for (int f = 0; f <= i; f++)
                {
                    total += animFrame[f];
                }

                if (progress < total)
                {
                    return i;
                }
            }
            return animFrame.Length - 1;
        }
    }
}