using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEffect : MassObject
{
    [SerializeField]
    private int life = 10;

    [SerializeField]
    private Sprite[] sprites = default;

    private VPixelObject view;
    private int count = 0;

    private Vector3 accel;
    private float power = 0.2f;

    public override void Initialize()
    {
        base.Initialize();

        this.position = position;
        view = GetComponent<VPixelObject>();
        view.Initialize();
        count = 0;

        accel = new Vector3();
    }

    public override void ResetObject()
    {
        count = 0;
        int rand = Random.Range(0, 6);
        switch (rand)
        {
            case 0:
                accel.x = -power;
                accel.y = power;
                view.ChangeSprite(sprites[0]);
                break;
            case 1:
                accel.x = -power;
                accel.y = -power;
                view.ChangeSprite(sprites[1]);
                break;
            case 2:
                accel.x = power;
                accel.y = power;
                view.ChangeSprite(sprites[2]);
                break;
            case 3:
                accel.x = power;
                accel.y = -power;
                view.ChangeSprite(sprites[3]);
                break;
            case 4:
            case 5:
                view.ChangeSprite(sprites[rand]);
                break;
            default:
                view.ChangeSprite(sprites[4]);
                break;
        }

        base.ResetObject();
    }

    public override void Execute()
    {
        base.Execute();
        view.Draw();

        _speed.x += accel.x;
        _speed.y += accel.y;

        x += _speed.x;
        y += _speed.y;

        count++;

        if (life < count)
        {
            ObjectDestroy();
        }
    }
}
