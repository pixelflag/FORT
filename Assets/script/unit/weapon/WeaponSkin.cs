using UnityEngine;

public class WeaponSkin : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites = default;
    public Sprite GetSprite(int index) => sprites[index];

#if UNITY_EDITOR
    public int AskIndexForSprite(Sprite sprite)
    {
        for (int i=0; i < sprites.Length; i++ )
        {
            if (sprites[i] == sprite)
                return i;
        }
        throw new System.Exception("no matching sprites.");
    }
#endif
}
