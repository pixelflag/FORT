using System.Collections.Generic;
using UnityEngine;

public class ObjectExecutor :DI
{
    public static ObjectExecutor instance;
    public ObjectExecutor()
    {
        instance = this;

        fires = new List<FireObject>();
        effects = new List<MassObject>();
    }

    public List<FireObject> fires { get; private set; }
    public List<MassObject> effects { get; private set; }

    public Vector2 areaSize;

    public void Reset()
    {
        // Destroyはフラグをたてて、確実にdestroyの処理タイミングで処分する。

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
        foreach (MassObject obj in effects)
            if (obj.isDestroy == false)
                obj.Execute();

        foreach (MassObject obj in fires)
        {
            if (obj.x < 0 || areaSize.x < obj.x || obj.y < areaSize.y || 0 < obj.y)
            {
                obj.ObjectDestroy();
            }
            else
            {
                if (obj.isDestroy == false)
                    obj.Execute();
            }
        }
    }

    public void CheckDestroy()
    {
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
