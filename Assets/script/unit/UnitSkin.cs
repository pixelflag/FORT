using UnityEngine;

public class UnitSkin : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    private string path = default;

    [ContextMenu("Load Sprite")]
    public void LoadSprite()
    {
        sprites = FileUtility.LoadSprite(path);
    }
#endif

    [SerializeField]
    private Sprite[] sprites = default;

    public Sprite GetSprite(int index)
    {
        return sprites[index];
    }

    public Sprite GetSimbolSprite()
    {
        return sprites[0];
    }
}