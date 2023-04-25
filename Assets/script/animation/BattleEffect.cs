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
