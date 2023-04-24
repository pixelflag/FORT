using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CharacterSkin : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    private string path = default;

    public void LoadSprite()
    {
        sprites = FileUtility.LoadSprite(path);
    }
#endif

    [SerializeField]
    private Sprite[] sprites = default;

    // スプライト番号の意味を知ってる必要がある。もっと具体性があったほうが良いのではないか。
    public Sprite GetSprite(int index)
    {
        return sprites[index];
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CharacterSkin))]
public class CharacterSkinEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("LoadSprite"))
        {
            CharacterSkin t = target as CharacterSkin;
            t.LoadSprite();
        }
    }
}
#endif