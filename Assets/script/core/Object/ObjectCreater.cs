﻿using UnityEngine;

public class ObjectCreater : DIMonoBehaviour
{
    private static ObjectCreater _instance;
    public static ObjectCreater instance => _instance;

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField]
    private WorldMapTileLibrary _worldMapTiles = default;
    public WorldMapTileLibrary worldMapTiles => _worldMapTiles;
    [SerializeField]
    private WorldMapCastle _worldCastlePrefab = default;
    [SerializeField]
    private WorldUnit _worldUnitPrefab = default;

    [SerializeField]
    private FieldDataLiblary _fieldLibrary = default;
    public FieldDataLiblary fieldLibrary => _fieldLibrary;

    [SerializeField]
    private Unit unitPrefab = default;
    [SerializeField]
    private LifeGauge lifeGaugePrefav = default;
    [SerializeField]
    private PlatoonFormation[] formations = default;
    public PlatoonFormation GetFormation(int index) => formations[index];

    [SerializeField]
    private ObjectLibrary ObjectLib = default;
    [SerializeField]
    private UnitSkinLibrary unitSkinLib = default;

    [SerializeField]
    private MapSpriteLibrary mapSpriteLib = default;
    [SerializeField]
    private WeaponLibrary weaponLib = default;

    public WorldMapCastle CreateWorldCastle(WorldMapCastleData data, Transform parent)
    {
        var castle = Instantiate(_worldCastlePrefab, parent).GetComponent<WorldMapCastle>();
        castle.Initialize(data);
        return castle;
    }

    public WorldUnit CreateWorldUnit(UnitType type, TeamID teamID, Vector3 position)
    {
        switch (type)
        {
            case UnitType.Knight:     return Create(0);
            case UnitType.Soldier:    return Create(1);
            case UnitType.Lancer:     return Create(12);
            case UnitType.Elf:        return Create(9);
            case UnitType.RedWizard:  return Create(6);
            case UnitType.BlueWizard: return Create(5);
            case UnitType.Lumberjack: return Create(13);
            case UnitType.Dwarves:    return Create(16);
            case UnitType.Thief:      return Create(8);
            default:
                throw new System.Exception("Failed to generate. : " + type);
        }

        WorldUnit Create(int skinID)
        {
            WorldUnit unit = Instantiate(_worldUnitPrefab).GetComponent<WorldUnit>();
            unit.Initialize(type, teamID);
            unit.position = position;
            unit.view.SetSkin(unitSkinLib.Get(skinID));

            return unit;
        }
    }

    public Unit CreateUnit(UnitType type, TeamID teamID, Vector3 position)
    {
        switch (type)
        {
            case UnitType.Knight:     return Create(0, 0);
            case UnitType.Soldier:    return Create(1, 0);
            case UnitType.Lancer:     return Create(12, 1);
            case UnitType.Elf:        return Create(9, 2);
            case UnitType.RedWizard:  return Create(6, 3);
            case UnitType.BlueWizard: return Create(5, 4);
            case UnitType.Lumberjack: return Create(13, 5);
            case UnitType.Dwarves:    return Create(16, 6);
            case UnitType.Thief:      return Create(8, 7);
            default:
                throw new System.Exception("Failed to generate. : " + type);
        }

        Unit Create(int skinID, int weapomID)
        {
            Unit unit = Instantiate(unitPrefab).GetComponent<Unit>();
            unit.Initialize(type, teamID);
            unit.position = position;
            unit.OnDead += (Unit u) =>
            {
                // とりあえずこのエフェクトを採用しておく。
                // creater.CreateMessageEffect(MessageEffectName.Angel, position);
                // sound.PlayOneShotOnChannel(1, SeType.ReatinerDead, 1);
            };

            unit.view.SetSkin(unitSkinLib.Get(skinID));
            unit.SetWeapon(CreateWeapon(unit, weapomID));

            return unit;
        }
    }

    public Weapon CreateWeapon(Unit master, int weapomID)
    {
        Weapon wp = Instantiate(ObjectLib.weapon);
        WeaponData wpp = data.weaponData[weapomID];
        wp.Initialize(master, wpp, weaponLib.GetMotion(wpp.type), weaponLib.GetSkin(wpp.type, wpp.skinID));
        return wp;
    }

    public FireObject CreateFire(FireData fireData, Vector3 position)
    {
        switch (fireData.weapon.type)
        {
            case WeaponType.Bow: return Create(0);
            case WeaponType.Rod:
                switch (fireData.weapon.element)
                {
                    case ElementType.Fire: return Create(1);
                    case ElementType.Ice:  return Create(2);
                    default: throw new System.Exception("element not found. : " + fireData.weapon.element);
                }
            case WeaponType.Sword:
            case WeaponType.Axe:
                // ここのFireをどう設計するかだなあ。
            case WeaponType.Spear:
                // 突きエフェクト
            case WeaponType.Sickle:
                // かまいたちが発生
            case WeaponType.Hammer:
                // 衝撃波が発生
            default: throw new System.Exception("type not found. : " + fireData.weapon.type);
        }

        FireObject Create(int index)
        {
            FireObject fr = Instantiate(ObjectLib.fires[index]);
            fr.Initialize(fireData);
            fr.position = position;
            objects.fires.Add(fr);
            return fr;
        }
    }

    public MapSpriteLibrary GetMapSpriteLibrary()
    {
        return mapSpriteLib;
    }

    public MassObject CreateMessageEffect(MessageEffectName EffectName, Vector3 position)
    {
        MessageEffect effect = Instantiate(ObjectLib.messageEffect).GetComponent<MessageEffect>();
        effect.Initialize();
        effect.position = position;

        // パラメーターのインジェクトも必要。
        switch (EffectName)
        {
            case MessageEffectName.Angel:
                effect.SetSprite(ObjectLib.EffectSprites[13]);
                break;
            case MessageEffectName.Devil:
                effect.SetSprite(ObjectLib.EffectSprites[12]);
                break;
            case MessageEffectName.Smail:
                effect.SetSprite(ObjectLib.EffectSprites[8]);
                break;
            case MessageEffectName.Up:
                effect.SetSprite(ObjectLib.EffectSprites[10]);
                break;
            case MessageEffectName.Thankyou:
                effect.SetSprite(ObjectLib.EffectSprites[9]);
                break;
            case MessageEffectName.Help:
                effect.SetSprite(ObjectLib.EffectSprites[11]);
                break;
            case MessageEffectName.Heal:
                effect.SetSprite(ObjectLib.EffectSprites[14]);
                break;
            default:
                throw new System.Exception("There is no such name. " + EffectName);
        }

        objects.effects.Add(effect);

        return effect;
    }

    public MassObject CreateEffect(EffectName name, Vector3 position)
    {
        switch (name)
        {
            case EffectName.EffectBattleEffect:
                // エフェクトは再設計が必要。
                MassObject ms = Instantiate(ObjectLib.battelEffect).GetComponent<MassObject>();
                ms.position = position;
                objects.effects.Add(ms);
                return ms;

            case EffectName.ItemStatue:
            default:
                throw new System.Exception("Failed to generate. " + name);
        }
    }

    public LifeGauge CreateLifeGauge(Transform parent)
    {
        var lifeGauge = Instantiate(lifeGaugePrefav).GetComponent<LifeGauge>();
        lifeGauge.Initialize(parent, new Vector3(0, 12, 0));
        return lifeGauge;
    }

    // Debug -----
    [SerializeField]
    private MiniNum miniNum = default;
    [SerializeField]
    private Sprite[] DebugSprites = default;
    [SerializeField]
    private Sprite[] DebugArrow = default;

    public MiniNum CreateNumiNum(int num, Transform parent, Vector3 position)
    {
        MiniNum mNum = Instantiate(miniNum, parent);
        mNum.transform.position = position;
        mNum.SetNum(num);
        return mNum;
    }

    public Sprite GetDebugSprite(int index)
    {
        return DebugSprites[index];
    }
    public Sprite GetDebugArrow(Direction4Type direction)
    {
        return DebugArrow[(int)direction];
    }
}