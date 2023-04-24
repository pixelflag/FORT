using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLibrary : MonoBehaviour
{
    public FireObject[] fires = default;

    // EffectSprite
    public string effectPath = "";
    public Sprite[] EffectSprites = default;
    [ContextMenu("Load Effect Sprite")]
    public void LoadEffectPass(){ EffectSprites = FileUtility.LoadSprite(effectPath); }

    // prefabs
    public GameObject player = default;
    public GameObject buddies = default;
    public Weapon weapon = default;
    public GameObject statue = default;
    public GameObject messageEffect = default;
    public GameObject battelEffect = default;
}