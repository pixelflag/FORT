using UnityEngine;

public class CameraObject : PixelObject
{
    public GameObject targetObject { get; private set; }
    public void SetTarget(GameObject target)
    {
        this.targetObject = target;
    }

    public Vector2 topRight;
    public Vector2 bottomLeft;

    public Vector2 buffer = new Vector2(64, 64);

    private GameObject fadeEffect;

    public void Initialize()
    {
        fadeEffect = transform.Find("FadeEffect").gameObject;
        fadeEffect.GetComponent<SpriteRenderer>().size = new Vector2(Global.screenWidth, Global.screenHeight);
        fadeEffect.SetActive(false);
    }

    public void Execute()
    {
        if (targetObject != null)
            position = CalculatePosition(targetObject.transform.position, topRight, bottomLeft);
    }

    public Vector3 CalculatePosition(Vector3 target, Vector2 topRight, Vector2 bottomLeft)
    {
        Vector3 cPos = transform.position;

        // x
        cPos.x = cPos.x < target.x - buffer.x ? target.x - buffer.x : cPos.x;
        cPos.x = target.x + buffer.x < cPos.x ? target.x + buffer.x : cPos.x;

        float left = bottomLeft.x + Global.screenWidth / 2;
        float right = topRight.x - Global.screenWidth / 2;

        cPos.x = cPos.x < left ? left : cPos.x;
        cPos.x = right < cPos.x ? right : cPos.x;
        cPos.x = Mathf.Round(cPos.x);

        // y
        cPos.y = cPos.y < target.y - buffer.y ? target.y - buffer.y : cPos.y;
        cPos.y = target.y + buffer.y < cPos.y ? target.y + buffer.y : cPos.y;

        float top = topRight.y - Global.screenHeight / 2;
        float bottom = bottomLeft.y + Global.screenHeight / 2;

        cPos.y = cPos.y < bottom ? bottom : cPos.y;
        cPos.y = top < cPos.y ? top : cPos.y;
        cPos.y = Mathf.Round(cPos.y);

        cPos.z = transform.position.z;

        return cPos;
    }

    public void SetPosition2D(Vector2 position)
    {
        x = position.x;
        y = position.y;
    }

    public void SetAlpha(float value)
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = Mathf.Floor(value * 2) / 2;
        GetComponent<SpriteRenderer>().color = color;

        if (value <= 0)
            fadeEffect.SetActive(false);
        else
            fadeEffect.SetActive(true);
    }
}
