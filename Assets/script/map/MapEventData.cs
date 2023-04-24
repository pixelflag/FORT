using UnityEngine;


[ExecuteAlways]
public class MapEventData : MonoBehaviour
{
    public Vector2Int size;
    public bool OneShot = false;
    public string eventName = "events";

#if UNITY_EDITOR
    private void Update()
    {
        EditBox b = GetComponent<EditBox>();
        b.size = size;
    }
#endif
}

public class MapEventObject : PixelObject
{
    public string eventName = "events";

    public Vector2Int size;
    public bool OneShot = false;
    public bool isEnd { get; protected set; }

    private Switch eventSwitch;
    public bool isEnter { get { return eventSwitch.isEnter; } }
    public bool isExit { get { return eventSwitch.isExit; } }
    public bool isStay { get { return eventSwitch.isStay; } }

    public Box box { get { return new Box(position, size.x / 2, size.y / 2); } }

    public void Initialize(MapEventData data)
    {
        this.eventName = data.eventName;
        this.size = data.size;
        this.OneShot = data.OneShot;
        this.position = data.transform.position;

        isEnd = false;
        this.eventSwitch = new Switch();
    }

    public void UpdateState(bool value)
    {
        eventSwitch.SetValue(value);
    }

    public void CheckOneShot()
    {
        if (OneShot) isEnd = true;
    }
}