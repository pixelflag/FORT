using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSwitch
{
    public bool isEnter { get; private set; }
    public int count { get; private set; }

    public StateSwitch()
    {
        Reset();
    }

    public void Reset()
    {
        isEnter = false;
        count = 0;
    }

    public void setValue(bool value)
    {
        count++;

        if (value)
        {
            isEnter = true;
            count = 0;
        }
        else
        {
            isEnter = false;
        }
    }
}
