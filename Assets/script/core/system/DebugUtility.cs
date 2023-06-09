#if UNITY_EDITOR

using UnityEngine;

public static class DebugUtility
{
    private static int sortingOrder = 10;

    public static GameObject AddLocator(Vector2 position)
    {
        GameObject newObj = new GameObject("Locator");

        SpriteRenderer render = newObj.AddComponent<SpriteRenderer>();
        render.drawMode = SpriteDrawMode.Sliced;
        render.sprite = ObjectCreater.instance.GetDebugSprite(1);
        render.sortingOrder = sortingOrder;

        newObj.transform.position = position;

        return newObj;
    }

    public static GameObject AddBoxView(Box box)
    {
        GameObject newObj = new GameObject("DebugBox");

        SpriteRenderer render = newObj.AddComponent<SpriteRenderer>();
        render.drawMode = SpriteDrawMode.Sliced;
        render.sprite = ObjectCreater.instance.GetDebugSprite(0);
        render.size = new Vector2(box.extendsX*2, box.extendsY * 2);
        render.color = Color.red; // とりあえず赤。
        render.sortingOrder = sortingOrder;

        newObj.transform.position = box.position;

        return newObj;
    }

    public static GameObject AddBoxView(Vector2 size)
    {
        GameObject newObj = new GameObject("DebugBox");

        SpriteRenderer render = newObj.AddComponent<SpriteRenderer>();
        render.drawMode = SpriteDrawMode.Sliced;
        render.sprite = ObjectCreater.instance.GetDebugSprite(0);
        render.size = size;
        render.color = Color.red; // とりあえず赤。
        render.sortingOrder = sortingOrder;

        return newObj;
    }

    public static GameObject AddArrowView(Direction4Type direction)
    {
        GameObject newObj = new GameObject("DirectionArrow");

        SpriteRenderer render = newObj.AddComponent<SpriteRenderer>();
        render.sprite = ObjectCreater.instance.GetDebugArrow(direction);
        render.color = Color.red; // とりあえず赤。
        render.sortingOrder = sortingOrder;

        return newObj;
    }
}
#endif