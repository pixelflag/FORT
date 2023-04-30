using UnityEngine;

public class UnitView : VPixelObject
{
    private UnitSkin skin;
    private int walkProgress;
    private int walkStep = 8;

    public void SetSkin(UnitSkin skin)
    {
        this.skin = skin;
    }

    public void WalkAnimUpdate(Unit model)
    {
        walkProgress++;

        int indexHead = GetSpriteIndexHead(model.direction.direction4);
        int animFrame = ((int)(walkProgress / walkStep)) % 2;
        spriteRenderer.sprite = skin.GetSprite(indexHead + animFrame);
    }

    public void AttackAnimUpdate(Unit model, int progress, int total)
    {
        int indexHead = GetSpriteIndexHead(model.direction.direction4);

        // モーションはweaponモーション側からコントロールされるべき。
        if (progress < total / 2)
            spriteRenderer.sprite = skin.GetSprite(indexHead + 2);
        else
            spriteRenderer.sprite = skin.GetSprite(indexHead + 3);
    }

    public void Dead()
    {
        spriteRenderer.sortingOrder = -1;
        spriteRenderer.sprite = skin.GetSprite(12);
        spriteRenderer.flipX = false;
    }

    private int GetSpriteIndexHead(Direction4Type direction)
    {
        switch (direction)
        {
            case Direction4Type.Up:
                spriteRenderer.flipX = false;
                return 8;
            case Direction4Type.Down:
                spriteRenderer.flipX = false;
                return 0;
            case Direction4Type.Left:
                spriteRenderer.flipX = false;
                return 4;
            case Direction4Type.Right:
                spriteRenderer.flipX = true;
                return 4;
        }
        return 0;
    }

    // Life gauge ----------

    public LifeGauge lifeGauge { get; private set; }

    public void AddLifeGage()
    {
        if (lifeGauge != null) return;
        lifeGauge = creater.CreateLifeGauge(transform);
    }
}
