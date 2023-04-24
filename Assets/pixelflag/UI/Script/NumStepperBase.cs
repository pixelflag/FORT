using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumStepperBase : MonoBehaviour
{
    private int min = -1024;
    private int max = 1024;

    public bool isActive { get; protected set; }

    protected int AmplificateValue(int pointerId)
    {
        if (pointerId == -2)
            return 10;
        else
            return 1;
    }

    protected int CheckLimit(int value)
    {
        value = min > value ? min : value;
        value = max < value ? max : value;
        return value;
    }

    public virtual void SetLimit(int min, int max)
    {
        this.min = min;
        this.max = max;
    }

    public void SetActive(bool b)
    {
        gameObject.SetActive(b);
        isActive = b;
    }
}
