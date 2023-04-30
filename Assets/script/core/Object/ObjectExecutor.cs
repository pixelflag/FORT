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

        for (int t = 0; t < field.teams.Length; t++)
        {
            for (int p = 0; p < field.teams[t].platoons.Count; p++)
            {
                for (int u = 0; u < field.teams[t].platoons[p].units.Count; u++)
                {
                    field.teams[t].platoons[p].units[u].Execute();
                }
            }
        }

        Vector2 areaSize = field.map.areaSize;
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

    public void ExecuteEvent()
    {
        for (int t = 0; t < field.teams.Length; t++)
        {
            for (int p = 0; p < field.teams[t].platoons.Count; p++)
            {
                for (int u = 0; u < field.teams[t].platoons[p].units.Count; u++)
                {
                    field.teams[t].platoons[p].units[u].ExecuteEvent();
                }
            }
        }
    }

    public void CheckDestroy()
    {
        for (int t = 0; t < field.teams.Length; t++)
        {
            for (int p = 0; p < field.teams[t].platoons.Count; p++)
            {
                for (int u = field.teams[t].platoons[p].units.Count - 1; 0 <= u; u--)
                {
                    if (field.teams[t].platoons[p].units[u].isDestroy == true)
                    {
                        Object.Destroy(field.teams[t].platoons[p].units[u].gameObject);
                        field.teams[t].platoons[p].units.RemoveAt(u);
                    }
                }
            }
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
