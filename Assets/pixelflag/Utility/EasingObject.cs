using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EasingObject
{
    public float fromValue;
    public float toValue;

    public int totalFrame;
    public int frame;
    public int delay;

    public EasingType type;

    public bool isGoal { get { return totalFrame <= frame; } }

    public EasingObject(EasingType type, float fromValue, float toValue, int frame, int delay)
    {
        this.type = type;
        this.fromValue = fromValue;
        this.toValue = toValue;

        this.delay = delay;
        this.frame = 0;
        totalFrame = frame;

        OnComplete = default;
    }

    public void Next()
    {
        delay--;
        if(delay <= 0)
        {
            frame++;
            if(isGoal)
            {
                if (OnComplete != null) OnComplete();
            }
        }
    }

    public float GetVelue()
    {
        return Easing.ForType(type, frame, totalFrame, fromValue, toValue);
    }

    public delegate void Delegate();
    public Delegate OnComplete;
}