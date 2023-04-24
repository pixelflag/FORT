using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGauge : VPixelObject
{
    [SerializeField]
    private Sprite[] sprites = default;

    public void Initialize(Transform parent, Vector3 localPosition)
    {
        base.Initialize();
        transform.parent = parent;
        transform.localPosition = localPosition;
    }

    public void SetLife(int life, int max)
    {
        float lifeRate = (float)life / (float)max;

        int value = (int)(10 * lifeRate);

        if(value <= 0)
        {
            spriteRenderer.sprite = sprites[0];
        }
        else if(0 < value && value < 11)
        {
            spriteRenderer.sprite = sprites[(int)value];
        }
        else
        {
            spriteRenderer.sprite = sprites[10];
        }
    }

    public void ShowLifeGauge()
    {
        gameObject.SetActive(true);
    }

    public void HideLifeGauge()
    {
        gameObject.SetActive(false);
    }
}
