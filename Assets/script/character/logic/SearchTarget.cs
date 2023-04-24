using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTarget
{
    private float eyeRadius = 32;
    private float seachArea;

    public SearchTarget(float eyeRadius)
    {
        SetRadius(eyeRadius);
    }

    public void SetRadius(float eyeRadius)
    {
        this.eyeRadius = eyeRadius;
        seachArea = new Vector2(eyeRadius, 0).sqrMagnitude;
    }

    public SearchTargetResult Seach(Vector2 startPos, Enemy target)
    {
        SearchTargetResult result = new SearchTargetResult();
        result.found = false;
        result.position = startPos;

        Vector2 targetPos = target.position;
        float dist = (startPos - targetPos).sqrMagnitude;

        if (dist < seachArea && target.isStealth == false)
        {
            result.found = true;
            result.position = target.position;
        }
        return result;
    }

    public SearchTargetResult Seach(Vector2 startPos, List<Enemy> targets)
    {
        SearchTargetResult result = new SearchTargetResult();
        result.found = false;
        result.position = startPos;
        float nearDist = new Vector2(eyeRadius * 2, 0).sqrMagnitude;

        foreach (Enemy target in targets)
        {
            Vector2 targetPos = target.position;
            float dist = (startPos - targetPos).sqrMagnitude;
            if (seachArea < dist) continue;
            if (nearDist > dist && target.isStealth == false)
            {
                nearDist = dist;
                result.position = targetPos;
                result.found = true;
            }
        }
        return result;
    }
}

public struct SearchTargetResult
{
    public bool found;
    public Vector2 position;
}
