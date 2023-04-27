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

        _vector.x += accel.x;
        _vector.y += accel.y;

        x += _vector.x;
        y += _vector.y;

        count++;

        if (life < count)
        {
            ObjectDestroy();
        }
    }
}
