using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField]
    private bool showFrame = false;

    [SerializeField]
    private GameObject eventContainers = default;

    private Player player;
    private List<EventObject> events;

    public void Initialize()
    {
        events = new List<EventObject>();
        for (int i = 0; i < eventContainers.transform.childCount; i++)
        {
            GameObject obj = eventContainers.transform.GetChild(i).gameObject;
            EventObject eo = obj.GetComponent<EventObject>();
            if (eo != null)
            {
                eo.Initialize();
                if (!showFrame)
                    eo.HideFrame();
                events.Add(eo);
            }
        }
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void ResetAll()
    {
        foreach (EventObject ev in events)
        {
            ev.ObjectReset();
        }
    }

    public void Excute()
    {
        if (player == null) return;

		Box pBox = player.collision.GetBox();
        foreach( EventObject ev in events)
        {
            ev.SetValue(BoxCollision.BoxHitCheck(pBox, ev.box));
            if (ev.isEnter)
            {
                if (OnEventHit != null) OnEventHit(ev.eventName);
                break;
            }
        }
    }

    public delegate void EventHitDelegate(string eventName);
    public EventHitDelegate OnEventHit;
}
