using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AnimationMapSpriteData : MonoBehaviour
{
    public Sprite symbolSprite = default;
    public Sprite[] sprites = default;
    public int animWait;
}

#if UNITY_EDITOR
[CustomEditor(typeof(AnimationMapSpriteData), true)]
public class AnimationMapSpriteDataGUI : Editor
{
    public override void OnInspectorGUI()
    {
        AnimationMapSpriteData _class = target as AnimationMapSpriteData;
        Texture2D texture = _class.symbolSprite.texture;
        Rect tr = _class.symbolSprite.textureRect;

        Rect rect = new Rect();
        rect.x = tr.x / texture.width;
        rect.y = tr.y / texture.height;
        rect.width  = tr.width  / texture.width;
        rect.height = tr.height / texture.height;

		EditorGUILayout.BeginVertical();
		{
			GUI.DrawTextureWithTexCoords(new Rect(16, 0, 32, 32), texture, rect);
            EditorGUILayout.Space(24);
        }
		EditorGUILayout.EndVertical();

		DrawDefaultInspector();
	}
}
#endif

