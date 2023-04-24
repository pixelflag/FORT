using UnityEngine;

public class MapSpriteLibrary : MonoBehaviour
{
    [SerializeField]
    private BrokenMapSpriteData[] brokenSprites = default;

    public BoolAndInt ExistsBrokenSprite(Sprite sprite)
    {
        for (int i = 0; i < brokenSprites.Length; i++)
        {
            if (brokenSprites[i].symbolSprite == sprite)
            {
                return new BoolAndInt(true, i);
            }
        }
        return new BoolAndInt(false, -1);
    }

    public BrokenMapSpriteData GetBrokenSpriteData(int index)
    {
        return brokenSprites[index];
    }

    // -----

    [SerializeField]
    private RandomMapSpriteData[] ramdomSprites = default;

    public BoolAndInt ExistsRandomSprite(Sprite sprite)
    {
        for (int i = 0; i < ramdomSprites.Length; i++)
        {
            if (ramdomSprites[i].symbolSprite == sprite)
            {
                return new BoolAndInt(true, i);
            }
        }
        return new BoolAndInt(false, -1);
    }

    public RandomMapSpriteData GetRandomSpriteData(int index)
    {
        return ramdomSprites[index];
    }

    // -----

    [SerializeField]
    private AnimationMapSpriteData[] animationSprites = default;

    public BoolAndInt ExistsAnimationSprite(Sprite sprite)
    {
        for (int i = 0; i < animationSprites.Length; i++)
        {
            if (animationSprites[i].symbolSprite == sprite)
            {
                return new BoolAndInt(true, i);
            }
        }
        return new BoolAndInt(false, -1);
    }

    public AnimationMapSpriteData GetAnimationSpriteData(int index)
    {
        return animationSprites[index];
    }
}
