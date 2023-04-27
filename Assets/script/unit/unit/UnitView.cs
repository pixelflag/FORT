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

    public void AnimationUpdate(Unit model, bool isAttack, int progress, int total)
    {
        base.ObjectUpdate();

        if (model.isDead)
        {
            spriteRenderer.sprite = skin.GetSprite(12);
            spriteRenderer.flipX = false;
            return;
        }

        int indexHead = 0;

        switch (model.direction.direction4)
        {
            case Direction4Type.Up:
                indexHead = 8;
                spriteRenderer.flipX = false;
                break;
            case Direction4Type.Down:
                indexHead = 0;
                spriteRenderer.flipX = false;
                break;
            case Direction4Type.Left:
                indexHead = 4;
                spriteRenderer.flipX = false;
                break;
            case Direction4Type.Right:
                indexHead = 4;
                spriteRenderer.flipX = true;
                break;
        }

        if (isAttack)
        {
            // ���[�V������weapon���[�V����������R���g���[�������ׂ��B
            if (progress < total / 2)
                spriteRenderer.sprite = skin.GetSprite(indexHead + 2);
            else
                spriteRenderer.sprite = skin.GetSprite(indexHead + 3);
        }
        else
        {
            if (!model.isStop)
                walkProgress++;

            int animFrame = ((int)(walkProgress / walkStep)) % 2;
            spriteRenderer.sprite = skin.GetSprite(indexHead + animFrame);
        }
    }

    // Life gauge ----------

    public LifeGauge lifeGauge { get; private set; }

    public void AddLifeGage()
    {
        if (lifeGauge != null) return;
        lifeGauge = creater.CreateLifeGauge(transform);
    }
}