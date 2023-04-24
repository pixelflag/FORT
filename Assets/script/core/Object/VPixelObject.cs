using UnityEngine;

[RequireComponent(typeof(PixelObject))]
public class VPixelObject:DIMonoBehaviour
{
    public GameObject baseSprite { get; private set; }
    private Vector3 baseSpriteOffset;

    protected Transform _transform;
    protected Transform spriteTransform;
    protected SpriteRenderer spriteRenderer;
    private PixelObject pixelObj;

    private int count = 0;

    public virtual void Initialize()
    {
        baseSprite = transform.Find("BaseSprite").gameObject;
        baseSpriteOffset = baseSprite.transform.localPosition;
        spriteRenderer = baseSprite.GetComponent<SpriteRenderer>();
        pixelObj = GetComponent<PixelObject>();

        _transform = transform;
        spriteTransform = baseSprite.transform;
    }

    public virtual void ResetObject()
    {
        count = 0;
        spriteRenderer.material.SetFloat("_MaskOn", 0);
    }

    public void ObjectUpdate()
    {
        count--;
        if( count == 0)
        {
            spriteRenderer.material.SetFloat("_MaskOn", 0);
        }
    }

    public void Draw()  
    {
        Vector3 pos = new Vector3();
        pos.x = Mathf.Round(_transform.position.x + baseSpriteOffset.x);
        pos.y = Mathf.Round(_transform.position.y + baseSpriteOffset.y);
        pos.z = pos.y + pixelObj.h;
        spriteTransform.position = pos;
    }

    public void ChangeColor(Color color)
    {
        spriteRenderer.material.SetColor("_MaskColor", color);
        spriteRenderer.material.SetFloat("_MaskOn", 1);
    }

    public void ChangeSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void Flash(Color color, int flashCount)
    {
        count = flashCount;
        ChangeColor(color);
    }

    public Transform GetParent()
    {
        return spriteTransform;
    }
}