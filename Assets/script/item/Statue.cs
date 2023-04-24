using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MassObject
{
    [SerializeField]
    private Sprite[] sprites =default;

    private VPixelObject view;

    public void Initialize(ObjectName objectName, Vector3 position)
    {
        base.Initialize();

        this.objectType = ObjectType.Item;
        this.position = position;
        collision.SetExtends(8);

        view = GetComponent<VPixelObject>();
        view.Initialize();

        switch (objectName)
        {
            case ObjectName.ItemStatue:
                view.ChangeSprite(sprites[Random.Range(0, 3)]);
                break;
        }
    }

    public override void Execute()
    {
        view.Draw();
    }

    public void Open()
    {
        creater.CreateMessageEffect(MessageEffectName.Thankyou, position);
        sound.PlayOneShotOnChannel(2, SeType.Rescue, 2);
        if (onOpen != null) onOpen(position);

        ObjectDestroy();
    }

    public delegate void OpenDelegate(Vector3 position);
    public OpenDelegate onOpen;
}