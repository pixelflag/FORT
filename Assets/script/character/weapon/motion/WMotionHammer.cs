using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WMotionHammer : WMotionBase
{
    [SerializeField]
    private int[] animFrame = { 4,2,8 };

    public override int GetTotalFrame()
    {
        int total = 0;
        foreach(int f in animFrame)
        {
            total += f;
        }
        return animIndexTable.GetLength(1) * total;
    }

    // inspecterで設定したり、ソースに書いたりばらばらなので、そのうちデータドリブンで統一していく方向で。
    private int[,] animIndexTable =
    {
        { 5,5,6 }, // U
        { 0,1,2 }, // R
        { 7,7,8 }, // D
        { 0,3,4 }, // L
    };

    public override MotionData GetMotion(Direction4Type direction, int progress)
    {
        int frame = AskFrame(progress);
        int index = animIndexTable[(int)direction, frame];
        return data[index];

        // この方式は重いので、やめないといけない。編集方法も含めてあとで考え直し。
        int AskFrame(int progress)
        {
            for (int i=0; i < animFrame.Length; i++ )
            {
                int total = 0;
                for (int f = 0; f <= i; f++)
                {
                    total += animFrame[f];
                }

                if(progress < total)
                {
                    return i;
                }
            }
            return animFrame.Length - 1;
        }
    }
}