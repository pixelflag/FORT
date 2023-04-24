using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : FireObject
{
    [SerializeField]
    private Sprite[] sprites = default;

    [SerializeField]
    private float arrowSpeed = 2.0f;

    private int maxLife = 120;
    private int life = 0;

    private int animWait = 4;
    private int animCount = 0;

    private SpriteRenderer render;

    public override void Initialize(ObjectType type, WeaponParameters weapon, Direction4Type direction, int attackPower)
    {
        base.Initialize(type, weapon, direction, attackPower);

        render = GetComponent<SpriteRenderer>();
        render.sprite = sprites[0];

        life = maxLife;
    }

    public override void Execute()
    {
        base.Execute();

        int index = (int)(animCount / animWait) % sprites.Length;
        render.sprite = sprites[index];

        // 将来的には誘導弾になる。
        life--;
        x += _force.x * arrowSpeed;
        y += _force.y * arrowSpeed;

        if (life < 0)
            ObjectDestroy();
    }

    public override void Hit()
    {
        // なんかヒットしたエフェクトとか。将来的に。
        base.Hit();
    }
}
