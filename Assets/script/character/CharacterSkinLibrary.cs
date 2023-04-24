using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkinLibrary : MonoBehaviour
{
    [SerializeField]
    private CharacterSkin[] skins = default;

    public CharacterSkin Get(int index)
    {
        return skins[index];
    }

    public int Length { get { return skins.Length; } }
}
