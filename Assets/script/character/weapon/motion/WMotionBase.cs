using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pixelflag.UI;
using System;

[ExecuteAlways]
public abstract class WMotionBase : MonoBehaviour
{
    [SerializeField]
    protected MotionData[] data = default;
    [SerializeField]
    private int _extends = 8;
    public int extends { get { return _extends; } }
    [SerializeField]
    private bool _collisionDisable = false;
    public bool collisionDisable { get { return _collisionDisable; } }

    // ここに手を出すのは保留しよう。
    // 属性処理を先にやる。

    // 攻撃判定が発生するタイミング
    [SerializeField]
    public int _fireFrame = 0;
    public int fireFrame { get { return _fireFrame; } }

    public virtual int GetTotalFrame()
    {
        throw new System.Exception("need override.");
    }

    public virtual MotionData GetMotion(Direction4Type direction, int progress)
    {
        throw new System.Exception("need override.");
    }

#if UNITY_EDITOR
    [SerializeField]
    protected WeaponSkin skin = default;
    [SerializeField]
    protected GameObject[] weapons = default;

    private void Update()
    {
        if (Application.isPlaying) return;

        List<MotionData> list = new List<MotionData>();
        for (int i= 0; i < weapons.Length; i++)
        {
            SpriteRenderer render = weapons[i].GetComponent<SpriteRenderer>();
            MotionData d = new MotionData();
            d.localPositon = weapons[i].transform.localPosition;
            d.spriteIndex = skin.AskIndexForSprite(render.sprite);
            d.flipX = render.flipX;
            d.flipY = render.flipY;
            list.Add(d);
        }
        data = list.ToArray();
    }

    [ContextMenu("AddGridFitObject")]
    private void AddGridFitObject()
    {
        for(int i=0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<GridFitObject>() == null)
            {
                GridFitObject obj = transform.GetChild(i).gameObject.AddComponent<GridFitObject>();
                obj.fitToGrid = false;
            }
        }
    }
#endif
}

[Serializable]
public struct MotionData
{
    public Vector3 localPositon;
    public int spriteIndex;
    public bool flipX;
    public bool flipY;
}