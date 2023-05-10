using UnityEngine;

public class MiniNum : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer Num1;
    [SerializeField]
    private SpriteRenderer Num2;
    [SerializeField]
    private SpriteRenderer Num3;
    [SerializeField]
    private Sprite[] sprites;

    private int num; 

    public void SetNum(int num)
    {
        num = Mathf.Clamp(num, 0, 999);

        int n1 = (int)(num / 100);
        int n2 = (int)(num / 10) % 10;
        int n3 = num%10;

        Num1.sprite = sprites[n1];
        Num2.sprite = sprites[n2];
        Num3.sprite = sprites[n3];

        this.num = num;
    }
}
