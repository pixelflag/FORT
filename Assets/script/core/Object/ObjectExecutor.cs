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

        for (int i = 0; i < field.teams.Length; i++)
        {
            for (int j = 0; j < field.teams[i].units.Count; j++)
            {
                field.teams[i].units[j].Execute();
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
        for (int i = 0; i < field.teams.Length; i++)
        {
            for (int j = 0; j < field.teams[i].units.Count; j++)
            {
                field.teams[i].units[j].ExecuteEvent();
            }
        }
    }

    public void CheckDestroy()
    {
        for (int i = 0; i < field.teams.Length; i++)
        {
            for (int j = field.teams[i].units.Count - 1; 0 <= j; j--)
                if (field.teams[i].units[i].isDestroy == true)
                {
                    Object.Destroy(field.teams[i].units[i].gameObject);
                    field.teams[i].units.RemoveAt(i);
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
