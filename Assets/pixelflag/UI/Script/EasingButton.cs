using UnityEngine;
using UnityEngine.EventSystems;

namespace pixelflag.UI
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class EasingButton : PixelObject, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        private Sprite onSprite;
        private Sprite offSprite;

        public EasingType type;

        private SpriteRenderer render;

        private void Awake()
        {
            render = GetComponent<SpriteRenderer>();
        }

        public void Initialize(EasingType type, Sprite onSprite, Sprite offSprite)
        {
            base.Initialize();

            this.type = type;

            this.onSprite = onSprite;
            this.offSprite = offSprite;

            render.sprite = offSprite;
        }

        public void OnPointerClick(PointerEventData data)
        {
            if (OnClick != null)
                OnClick(type);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            render.sprite = onSprite;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            render.sprite = offSprite;
        }

        public delegate void Click(EasingType type);
        public Click OnClick;

    }
}