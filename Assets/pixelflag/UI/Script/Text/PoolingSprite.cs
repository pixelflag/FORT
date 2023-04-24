using UnityEngine;

namespace pixelflag.UI
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PoolingSprite : PixelObject
    {
        public bool isActive { get; private set; }

        private SpriteRenderer render;
        private Transform defaultParent;

        public void SetDefaultParent(Transform defaultParent)
        {
            this.defaultParent = defaultParent;
            transform.SetParent(defaultParent);
        }

        public void Initialize(Sprite sprite)
        {
            base.Initialize();

            isActive = true;
            render = GetComponent<SpriteRenderer>();
            render.sprite = sprite;
            color = Color.white;
            position = Vector3.zero;
            Show();
        }

        public void ResetObject()
        {

        }

        public Color color
        {
            get
            {
                return render.color;
            }
            set
            {
                render.color = value;
            }
        }

        public void ReturnToPool()
        {
            render.sprite = null;
            isActive = false;
            Hide();
            transform.SetParent(defaultParent);
        }

        public void Show()
        {
            z = 0;
        }

        public void Hide()
        {
            // カメラの裏側へ。もっと良い方法があれば書き換え。
            z = -100000;
        }
    }
}