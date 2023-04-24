using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Easing2DObject
{
    public Vector2 fromValue;
    public Vector2 toValue;

    public int totalFrame;
    public int frame;
    public int delay;

    public EasingType type;

    public bool isGoal { get { return totalFrame <= frame; } }

    public Easing2DObject(EasingType type, Vector2 fromValue, Vector2 toValue, int frame, int delay)
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

    public Vector2 GetVelue()
    {
        return Easing2D.ForType(type, frame, totalFrame, fromValue, toValue);
    }

    public delegate void Delegate();
    public Delegate OnComplete;
}