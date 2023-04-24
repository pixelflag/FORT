using System.Collections.Generic;
using UnityEngine;
using pixelflag.controller;

public class ObjectExecutor
{
    public static ObjectExecutor instance;
    public ObjectExecutor()
    {
        instance = this;

        buddies = new List<Character>();
        fires = new List<FireObject>();
        effects = new List<MassObject>();
    }

    public Player player;
    public List<Character> buddies { get; private set; }
    public List<FireObject> fires { get; private set; }
    public List<MassObject> effects { get; private set; }

    public void Reset()
    {
        // Destroyはフラグをたてて、確実にdestroyの処理タイミングで処分する。

        if (player != null)
            player.ObjectDestroy();

        for (int i = buddies.Count - 1; 0 <= i; i--)
            buddies[i].ObjectDestroy();

        for (int i = fires.Count - 1; 0 <= i; i--)
            fires[i].ObjectDestroy();

        for (int i = effects.Count - 1; 0 <= i; i--)
            effects[i].ObjectDestroy();
    }


    public void FlushFire()
    {
        for (int i = fires.Count - 1; 0 <= i; i--)
            Object.Destroy(fires[i].gameObject);
        fires.Clear();
    }

    public void Execute()
    {
        player.Execute();
        foreach (MassObject obj in buddies) ObjectExecute(obj);
        foreach (MassObject obj in effects) ObjectExecute(obj);

        Vector2 areaSize = FieldControl.instance.map.areaSize;
        foreach (MassObject obj in fires)
        {
            if (obj.x < 0 || areaSize.x < obj.x || obj.y < areaSize.y || 0 < obj.y)
                obj.ObjectDestroy();
            else
                ObjectExecute(obj);
        }

        void ObjectExecute(MassObject obj)
        {
            if (obj.isDestroy == false )
                obj.Execute();
        }
    }

    public void CheckDestroy()
    {
        for (int i = buddies.Count - 1; 0 <= i; i--)
            if (buddies[i].isDestroy == true)
            {
                Object.Destroy(buddies[i].gameObject);
                buddies.RemoveAt(i);
            }

        for (int i = fires.Count - 1; 0 <= i; i--)
            if (fires[i].isDestroy == true)
            {
                Object.Destroy(fires[i].gameObject);
                fires.RemoveAt(i);
            }

        for (int i = effects.Count - 1; 0 <= i; i--)
            if (effects[i].isDestroy == true)
            {
                Object.Destroy(effects  [i].gameObject);
                effects.RemoveAt(i);
            }
    }
}
