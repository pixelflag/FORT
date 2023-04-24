using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation
{
    public int length{ get; private set; }
    public int animSpeed { get; set; }
    public bool isLoop { get; private set; }

    public bool isWake { get; private set; }
    public bool isEnd { get; private set; }

    public int animFrame { get; private set; }
    private int animCount = 0;

    public int totalFrame { get { return length * animSpeed; } }

    public SpriteAnimation(int length, int animSpeed, bool isLoop)
    {
        this.length = length;
        this.animSpeed = animSpeed;
        this.isLoop = isLoop;
        ResetAnimation();
    }

    public void ResetAnimation()
    {
        animFrame = 0;
        animCount = 0;
        isEnd = false;
        isWake = true;
    }

    public void NextFrame()
    {
        if (isEnd) return;
        isWake = false;
        animCount++;

        if (animSpeed <= animCount)
        {
            if (length - 1 <= animFrame)
            {
                if (!isLoop)
                {
                    isEnd = true;
                    return;
                }
                animFrame = 0;
            }
            else
            {
                animFrame += 1;
            }
            animCount = 0;
        }
    }

    public void SetAnimFrame(int frame)
    {
        animFrame = frame % length;
    }
}