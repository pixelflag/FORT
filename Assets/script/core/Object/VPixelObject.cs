using UnityEngine;

[RequireComponent(typeof(PixelObject))]
public class VPixelObject:DIMonoBehaviour
{
    protected Transform _transform;
    protected SpriteRenderer spriteRenderer;
    private PixelObject pixelObj;

    private int count = 0;

    public virtual void Initialize()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pixelObj = GetComponent<PixelObject>();
        _transform = transform;
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
        Vector3 pos = _transform.position;
        pos.z = pos.y + pixelObj.h;
        _transform.position = pos;
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
}