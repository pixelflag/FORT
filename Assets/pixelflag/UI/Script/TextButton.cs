using UnityEngine;
using UnityEngine.EventSystems;

namespace pixelflag.UI
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class TextButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private TextObject labelText = default;

        [SerializeField]
        private Sprite onSprite = default;

        [SerializeField]
        private Sprite offSprite = default;

        [SerializeField]
        private Color onTextColor = Color.white;

        [SerializeField]
        private Color offTextColor = Color.black;

        private SpriteRenderer render;
        public bool isActive { get; private set; }

        private void Awake()
        {
            render = GetComponent<SpriteRenderer>();
        }

        public void Initialize()
        {
            isActive = true;

            labelText.Initialize();

            GetComponent<BoxCollider2D>().size = render.size;
            labelText.SetColor(offTextColor);
        }

        public void OnPointerClick(PointerEventData data)
        {
            if (OnClick != null)
                OnClick(data);
            if (OnClickForNoEvent != null)
                OnClickForNoEvent();
        }

        public void SetText(string text)
        {
            labelText.SetText(text);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            render.sprite = onSprite;
            labelText.SetColor(onTextColor);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            render.sprite = offSprite;
            labelText.SetColor(offTextColor);
        }

        public delegate void Click(PointerEventData data);
        public Click OnClick;

        public delegate void ClickForNoEvent();
        public ClickForNoEvent OnClickForNoEvent;

    }
}