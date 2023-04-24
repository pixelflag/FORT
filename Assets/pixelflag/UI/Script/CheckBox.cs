using UnityEngine;
using UnityEngine.EventSystems;

namespace pixelflag.UI
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class CheckBox : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private Sprite onSprite = default;

        [SerializeField]
        private Sprite offSprite = default;

        private SpriteRenderer render;

        public bool isActive { get; private set; }

        private void Awake()
        {
            render = GetComponent<SpriteRenderer>();
            isActive = true;
        }

        public bool value { get; private set; }
        public void SetValue(bool value)
        {
            this.value = value;
            UpdateView();
        }

        private void UpdateView()
        {
            if (value)
                render.sprite = onSprite;
            else
                render.sprite = offSprite;
        }

        public void OnPointerClick(PointerEventData data)
        {
            if (!isActive) return;

            SetValue(!value);
            UpdateView();

            if (OnChange != null) OnChange(value);
        }

        public delegate void ValueChange(bool value);
        public ValueChange OnChange;

        public void SetActive(bool b)
        {
            gameObject.SetActive(b);
            isActive = b;
        }
    }
}