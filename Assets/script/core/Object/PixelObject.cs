using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelObject:DIMonoBehaviour
{
    private Transform __transform;
    protected Transform _transform
    {
        get
        {
            if (__transform == null)
            {
                __transform = transform;
            }
            return __transform;
        }
    }

    public Vector3 position
    {
        get{ return _transform.position; }
        set{ _transform.position = value; }
    }

    public float x
    {
        get{ return _transform.position.x; }
        set
        {
            Vector3 pos = _transform.position;
            pos.x = value;
            _transform.position = pos;
        }
    }

    public float y
    {
        get{ return _transform.position.y; }
        set
        {
            Vector3 pos = _transform.position;
            pos.y = value;
            _transform.position = pos;
        }
    }

    public float z
    {
        get{ return _transform.position.z; }
        set
        {
            Vector3 pos = _transform.position;
            pos.z = value;
            _transform.position = pos;
        }
    }

    public float h { get; set; }

    // ----------

    public Vector3 localPosition
    {
        get { return _transform.localPosition; }
        set { _transform.localPosition = value; }
    }

    public float localX
    {
        get { return _transform.localPosition.x; }
        set
        {
            Vector3 pos = _transform.position;
            pos.x = value;
            _transform.position = pos;
        }
    }

    public float localY
    {
        get { return _transform.localPosition.y; }
        set
        {
            Vector3 pos = _transform.localPosition;
            pos.y = value;
            _transform.localPosition = pos;
        }
    }

    public float localZ
    {
        get { return _transform.localPosition.z; }
        set
        {
            Vector3 pos = _transform.localPosition;
            pos.z = value;
            _transform.localPosition = pos;
        }
    }
}
