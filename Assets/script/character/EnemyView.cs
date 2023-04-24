using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : VPixelObject
{
#if UNITY_EDITOR
    [SerializeField]
    private string path = default;

    [ContextMenu("Load Sprite")]
    public void LoadSprite()
    {
        sprites = FileUtility.LoadSprite(path);
    }
#endif

    [SerializeField]
    private Sprite[] sprites = default;

    public Sprite GetSimbolSprite() { return sprites[0]; }

    public void SetSprite(int index)
    {
        try
        {
            spriteRenderer.sprite = sprites[index];
        }
        catch
        {
            throw new System.Exception("[catch] path:" + path + ", index:" + index);
        }
    }
}