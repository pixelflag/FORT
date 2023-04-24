using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace pixelflag.UI
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Button : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private Sprite onSprite = default;

        [SerializeField]
        private Sprite offSprite = default;

        private SpriteRenderer render;

        private void Awake()
        {
            render = GetComponent<SpriteRenderer>();
        }

        public void OnPointerClick(PointerEventData data)
        {
            if (OnClick != null)
                OnClick(data);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            render.sprite = onSprite;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            render.sprite = offSprite;
        }

        public delegate void Click(PointerEventData data);
        public Click OnClick;
    }
}