using UnityEngine;

public class MapSprite : PixelObject
{
    protected Sprite[] sprites = default;

    private int animWait = 4;
    private int animCount = 0;

    public SpriteRenderer render { get; private set; }
    private int extends = 8;

    public void Initialize()
    {
        render = gameObject.AddComponent<SpriteRenderer>();
    }

    public void SetAnimation(int animWait)
    {
        this.animWait = animWait;
    }

    public Box GetBox()
    {
        return new Box(transform.position, extends, extends);
    }

    public int GetRadius()
    {
        return extends;
    }

    public void ObjectDestroy()
    {
        Destroy(gameObject);
    }
}

public struct AnimationMapSprite
{
    private Sprite[] sprites;
    private MapSprite mapSprite;
    private int animWait;
    private int animCount;

    public AnimationMapSprite(MapSprite mapSprite, Sprite[] sprites, int animWait)
    {
        this.mapSprite = mapSprite;
        this.sprites = sprites;
        this.animWait = animWait;
        animCount = 0;
    }

    public void Execute()
    {
        int index = (int)(animCount / animWait) % sprites.Length;
        mapSprite.render.sprite = sprites[index];
        animCount++;
    }
}