using UnityEngine;

public class MessageEffect : MassObject
{
    [SerializeField]
    private float upPower = 1.0f;

    [SerializeField]
    private int life = 10;

    private VPixelObject view;
    private int count = 0;

    public override void Initialize()
    {
        base.Initialize();
        view = GetComponent<VPixelObject>();
        view.Initialize();
    }

    public override void Execute()
    {
        _speed.y += upPower;

        y += _speed.y;

        base.Execute();
        view.Draw();

        count++;

        if (life < count)
        {
            ObjectDestroy();
        }
    }

    public void SetSprite(Sprite sprite)
    {
        view.ChangeSprite(sprite);
    }
}

public enum MessageEffectName
{
    Angel,
    Devil,
    Smail,
    Up,
    Thankyou,
    Help,
    Heal,
}