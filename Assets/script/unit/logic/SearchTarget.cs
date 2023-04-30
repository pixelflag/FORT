using System.Collections.Generic;
using UnityEngine;

public class SearchTarget
{
    private static FixedArray<Selection> selection = new FixedArray<Selection>(256);

    private struct Selection
    {
        public Unit unit;
        public float distance;
    }

    public struct Result
    {
        public bool found;
        public Unit unit;
    }

    public static bool AddSearchTarget(Vector2 startPos, Unit targetUnit, float seachRadius)
    {
        var dap = new Selection();
        dap.unit = targetUnit;
        dap.distance = (startPos - (Vector2)targetUnit.position).sqrMagnitude;
        float area = new Vector2(seachRadius, 0).sqrMagnitude;

        if (dap.distance < area)
            selection.Add(dap);

        return true;
    }

    public static void ClearPotionList()
    {
        selection.Clear();
    }

    public static Result GetClosestPosition()
    {
        Result result = new Result();
        result.found = false;

        Selection current = new Selection();

        if (selection.length == 0)
            return result;
        else
        {
            current.distance = selection.Get(0).distance;
            current.unit = selection.Get(0).unit;
        }

        for(int i=0; i < selection.length; i++)
        {
            if (selection.Get(i).distance < current.distance)
            {
                current = selection.Get(i);
            }
        }
        result.found = true;
        result.unit = current.unit;

        return result;
    }
}

