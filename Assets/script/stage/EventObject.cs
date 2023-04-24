using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObject : MonoBehaviour
{
    public string eventName = "events";

    public Box box { get; private set; }

    public bool OneShot = false;
    public bool isEnd { get; private set; }

    public void Initialize()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        box = new Box(transform.position, (int)render.size.x/2, (int)render.size.y / 2);
    }

    public void ObjectReset()
    {
        isEnd = false;
    }

    public void HideFrame()
    {
        GetComponent<SpriteRenderer>().sprite = null;
    }

    public bool isEnter { get; private set; }
    private bool prev;

    public void SetValue(bool value)
    {
        if (prev != value)
        {
            if (OneShot && isEnd)
            {
                isEnter = false;
            }
            else
            {
                isEnter = value;
                isEnd = true;
            }
        }
        else
        {
            isEnter = false;
        }

        prev = value;
    }
}
