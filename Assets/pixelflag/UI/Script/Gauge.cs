using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pixelflag.UI
{
    [ExecuteAlways]
    public class Gauge : PixelObject
    {
        private GameObject frameObj;
        private SpriteRenderer frameSprite;

        private GameObject gaugeObj;
        private SpriteRenderer gaugeSprite;

        private GameObject baseObj;
        private SpriteRenderer baseSprite;

        [SerializeField]
        private int value = 64;

        [SerializeField]
        public int maxLength = 64;

        [SerializeField]
        private int thickness = 8;

        [SerializeField]
        private Color color = Color.white;

        [SerializeField]
        private AlignType align = AlignType.Left;

        public int normalValue { get { return value / maxLength; } }

        private void OnUpdate()
        {
            if (!Application.isPlaying)
            {
                Initialize();
            }
        }

        public override void Initialize()
        {
            frameObj = transform.Find("frame").gameObject;
            frameSprite = frameObj.GetComponent<SpriteRenderer>();

            gaugeObj = transform.Find("body").gameObject;
            gaugeSprite = gaugeObj.GetComponent<SpriteRenderer>();

            baseObj = transform.Find("base").gameObject;
            baseSprite = baseObj.GetComponent<SpriteRenderer>();

            SetMaxLength(maxLength);
            SetValue(value);
            SetColor(color);
            SetAlign(align);
        }

        // 縦ゲージ対応は必要になってからでいい。

        public void SetMaxLength(int maxLength)
        {
            this.maxLength = maxLength;
            frameSprite.size = new Vector2(maxLength, thickness);
            baseSprite.size = new Vector2(maxLength, thickness);
            SetValue(value);
            SetAlign(align);
        }

        public void SetValue(int value)
        {
            this.value = value < 0 ? 0 : value;
            this.value = maxLength < value ? maxLength : value;
            gaugeSprite.size = new Vector2(this.value, thickness);
            SetAlign(align);
        }

        public void SetNormal(float value)
        {
            value = value < 0 ? 0 : value;
            value = 1 < value ? 1 : value;
            this.value = (int)(maxLength * value);
            gaugeSprite.size = new Vector2(this.value, thickness);
            SetAlign(align);
        }

        public void SetColor(Color color)
        {
            this.color = color;
            gaugeSprite.color = color;
        }

        public void SetFrameColor(Color color)
        {
            frameSprite.color = color;
        }

        public void SetBaseColor(Color color)
        {
            frameSprite.color = color;
        }

        public void SetAlign(AlignType align)
        {
            this.align = align;

            Vector3 framePos = new Vector3();
            Vector3 gaugePos = new Vector3();
            int frameWidth = (int)frameSprite.size.x;
            int gaugeWidth = (int)gaugeSprite.size.x;

            switch (align)
            {
                case AlignType.Left:
                    framePos.x = (int)Mathf.Floor(frameWidth / 2);
                    gaugePos.x = (int)Mathf.Floor(gaugeWidth / 2);
                    break;
                case AlignType.Right:
                    framePos.x = -(int)Mathf.Ceil(frameWidth / 2);
                    gaugePos.x = -(int)Mathf.Ceil(gaugeWidth / 2);
                    break;
                case AlignType.Center:
                    framePos.x = 0;
                    gaugePos.x = -frameWidth/2+ gaugeWidth/2;
                    break;
            }

            frameObj.transform.localPosition = framePos;
            gaugeObj.transform.localPosition = gaugePos;
            baseObj.transform.localPosition = framePos;
        }
    }
}