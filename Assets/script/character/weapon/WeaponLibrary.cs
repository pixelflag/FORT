using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLibrary : MonoBehaviour
{
    [SerializeField]
    private WMotionBase[] motions = default;

    [SerializeField]
    private WeaponSkin[] swordSkins = default;
    [SerializeField]
    private WeaponSkin[] spearSkins = default;
    [SerializeField]
    private WeaponSkin[] bowSkins = default;
    [SerializeField]
    private WeaponSkin[] rodSkin = default;
    [SerializeField]
    private WeaponSkin[] axeSkin = default;
    [SerializeField]
    private WeaponSkin[] hammerSkin = default;
    [SerializeField]
    private WeaponSkin[] sickleSkin = default;

    public WMotionBase GetMotion(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.Sword: return motions[0];
            case WeaponType.Spear: return motions[1];
            case WeaponType.Bow: return motions[2];
            case WeaponType.Rod: return motions[3];
            case WeaponType.Axe: return motions[0];
            case WeaponType.Hammer: return motions[4];
            case WeaponType.Sickle: return motions[5];
            default:
                throw new System.Exception("This type not Found. : " + type);
        }
    }

    public WeaponSkin GetSkin(WeaponType type, int index)
    {
        switch (type)
        {
            case WeaponType.Sword:  return swordSkins[index];
            case WeaponType.Spear:  return spearSkins[index];
            case WeaponType.Bow:    return bowSkins[index];
            case WeaponType.Rod:    return rodSkin[index];
            case WeaponType.Axe:    return axeSkin[index];
            case WeaponType.Hammer: return hammerSkin[index];
            case WeaponType.Sickle: return sickleSkin[index];
            default:
                throw new System.Exception("This type not Found. : " + type);
        }
    }
}