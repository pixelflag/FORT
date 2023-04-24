using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace pixelflag.UI
{
    public abstract class FontBase: MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField]
        private string path = "";

        [ContextMenu("Load Sprite")]
        private void LoadTile()
        {
            sprites = AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().ToArray();
        }
#endif
        [SerializeField]
        protected Sprite[] sprites;

        public virtual CharData GetCharData(char c)
        {
            return new CharData();
        }
    }
}