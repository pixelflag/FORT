using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : FireObject
{
    [SerializeField]
    private Sprite[] sprites = default;

    [SerializeField]
    private float arrowSpeed = 2.0f;

    private int maxLife = 120;
    private int life = 0;

    public override void Initialize(ObjectType type, WeaponParameters weapon, Direction4Type direction, int attackPower)
    {
        base.Initialize(type, weapon, direction, attackPower);

        int index = 0;
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        switch (direction)
        {
            case Direction4Type.Up:
                index = 1;
                render.flipY = true;
                break;
            case Direction4Type.Right:
                index = 0;
                break;
            case Direction4Type.Down:
                index = 1;
                break;
            case Direction4Type.Left:
                index = 0;
                render.flipX = true;
                break;
        }
        GetComponent<SpriteRenderer>().sprite = sprites[index];

        life = maxLife;
    }

    public override void Execute()
    {
        base.Execute();

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
