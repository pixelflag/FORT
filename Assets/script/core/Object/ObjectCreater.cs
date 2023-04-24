using UnityEngine;

public class ObjectCreater : DIMonoBehaviour
{
    private static ObjectCreater _instance;
    public static ObjectCreater instance{ get { return _instance; }}

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField]
    private ObjectLibrary ObjectLib = default;
    [SerializeField]
    private EnemyLibrary enemyLib = default;
    [SerializeField]
    private CharacterSkinLibrary characterSkinLib = default;
    [SerializeField]
    private MapSpriteLibrary mapSpriteLib = default;
    [SerializeField]
    private WeaponLibrary weaponLib = default;

    public Player CreatePlayer(Vector3 position)
    {
        GameObject obj = Instantiate(ObjectLib.player);
        Player pl = obj.GetComponent<Player>();
        pl.Initialize();
        pl.position = position;
        pl.SetSkin(characterSkinLib.Get(0));
        pl.SetWeapon(CreateWeapon(pl, 0));
        pl.OnCharacterDead += (Character character) =>
        {
            sound.PlayOneShotOnChannel(1, SeType.ReatinerDead, 1);
        };

        objects.player = pl;
        return pl;
    }

    public Buddies CreateBuddies(BuddiesType type, Vector3 position)
    {
        switch (type)
        {
            case BuddiesType.Knight:     return Create(0, 0);
            case BuddiesType.Soldier:    return Create(1, 1);
            case BuddiesType.Elf:        return Create(2, 2);
            case BuddiesType.RedWizard:  return Create(3, 3);
            case BuddiesType.BlueWizard: return Create(4, 4);
            case BuddiesType.Lumberjack: return Create(5, 5);
            case BuddiesType.Dwarves:    return Create(6, 6);
            case BuddiesType.Thief:      return Create(7, 7);
            default:
                throw new System.Exception("Failed to generate. : " + type);
        }

        Buddies Create(int skinID, int weapomID)
        {
            Buddies obj = Instantiate(ObjectLib.buddies).GetComponent<Buddies>();
            obj.Initialize(type, objects.player);
            obj.position = position;
            obj.OnCharacterDead += (Character character) =>
            {
                creater.CreateMessageEffect(MessageEffectName.Angel, position);
                sound.PlayOneShotOnChannel(1, SeType.ReatinerDead, 1);
                obj.ObjectDestroy();
            };

            CharacterTrackRouteMove logic = new CharacterTrackRouteMove();
            logic.Initialize(obj, 64);
            obj.moveLogic = logic;

            obj.SetSkin(characterSkinLib.Get(skinID));
            obj.SetWeapon(CreateWeapon(obj, weapomID));

            objects.buddies.Add(obj);

            return obj;
        }
    }

    public Weapon CreateWeapon(Character master, int weapomID)
    {
        Weapon wp = Instantiate(ObjectLib.weapon);
        WeaponParameters wpp = data.unit.weaponData[weapomID];
        wp.Initialize(master, wpp, weaponLib.GetMotion(wpp.type), weaponLib.GetSkin(wpp.type, wpp.skinID));
        return wp;
    }

    public Enemy CreateEnemy(EnemyName name, Vector3 position)
    {
        Enemy enemy = enemyLib.CreateEnemy(name);
        enemy.position = position;
        enemy.OnEnemyDead += (Enemy enemy) =>
        {
            creater.CreateMessageEffect(MessageEffectName.Devil, position);
            sound.PlayOneShotOnChannel(1, SeType.EnemyDead, 1);
        };
        return enemy;
    }

    public FireObject CreateFire(ObjectType cType, WeaponParameters wpp, Vector3 position, Direction4Type direction, int AttackPower)
    {
        switch (wpp.type)
        {
            case WeaponType.Bow: return Create(0);
            case WeaponType.Rod:
                switch (wpp.element)
                {
                    case ElementType.Fire: return Create(1);
                    case ElementType.Ice:  return Create(2);
                    default: throw new System.Exception("element not found. : " + wpp.element);
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
            default: throw new System.Exception("type not found. : " + wpp.type);
        }

        FireObject Create(int index)
        {
            FireObject fr = Instantiate(ObjectLib.fires[index]);
            fr.Initialize(cType, wpp, direction, AttackPower);
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

    public MassObject Create(ObjectName objectName, Vector3 position)
    {
        switch (objectName)
        {
            case ObjectName.EffectBattleEffect:
                // エフェクトは再設計が必要。
                MassObject ms = Instantiate(ObjectLib.battelEffect).GetComponent<MassObject>();
                ms.position = position;
                objects.effects.Add(ms);
                return ms;

            case ObjectName.ItemStatue:
            default:
                throw new System.Exception("Failed to generate. " + objectName);
        }
    }

    
    public Item CreateItem()
    {
        // 未実装。
        /*
        Statue stt = Instantiate(ObjectLib.statue).GetComponent<Statue>();
        stt.Initialize(objectName, position);
        return stt;
        */

        return null;
    }

    // Debug

    [SerializeField]
    private Sprite[] DebugSprites = default;
    [SerializeField]
    private Sprite[] DebugArrow = default;

    public Sprite GetDebugSprite(int index)
    {
        return DebugSprites[index];
    }
    public Sprite GetDebugArrow(Direction4Type direction)
    {
        return DebugArrow[(int)direction];
    }
}