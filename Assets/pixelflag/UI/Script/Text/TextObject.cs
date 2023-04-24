using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace pixelflag.UI
{
    [ExecuteAlways]
    public class TextObject: PixelObject
    {
        [SerializeField]
        private GameObject dummySprite = default;

        [SerializeField]
        private FontBase fontData = default;

        [SerializeField]
        private string text = "";
        [SerializeField]
        private Color color = Color.white;
        [SerializeField]
        private AlignType align = AlignType.Left;

        private SpritePool spritePool;
        private List<FontObject> fonts;
        public bool isHide { get; private set; }
        private int stringWidth;

        private void Update()
        {
            if (!Application.isPlaying)
            {
                SpriteRenderer sp = dummySprite.GetComponent<SpriteRenderer>();

                int width = 0;
                foreach (char c in text)
                {
                    CharData data = fontData.GetCharData(c);
                    width += data.width;
                }

                sp.size = new Vector2(width, 8);

                Vector3 pos = new Vector3();
                switch (align)
                {
                    case AlignType.Left:
                        pos.x = (int)width / 2;
                        break;
                    case AlignType.Right:
                        pos.x = -(int)width / 2;
                        break;
                    case AlignType.Center:
                        pos.x = 0;
                        break;
                }
                dummySprite.transform.localPosition = pos;
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            spritePool = SpritePool.instance;

            fonts = new List<FontObject>(32);

            Destroy(dummySprite);

            SetText(text);
        }

        public void SetText(string text)
        {
            Flush();

            this.text = text;

            stringWidth = 0;

            foreach (char c in text)
            {
                CharData data = fontData.GetCharData(c);
                stringWidth += data.width;
                
                PoolingSprite obj = spritePool.GetPoolSprite();
                obj.transform.SetParent(transform);
                obj.Initialize(data.sprite);

                FontObject font = new FontObject();
                font.data = data;
                font.obj = obj;

                fonts.Add(font);
            }

            SetTextAlign(align);
            SetColor(color);
        }

        public void SetColor(Color color)
        {
            this.color = color;
            if (fonts.Count == 0) return;

            foreach (FontObject font in fonts)
            {
                font.obj.color = color;
            }
        }

        public void SetTextAlign(AlignType align)
        {
            this.align = align;
            if (fonts.Count == 0) return;

            switch (align)
            {
                case AlignType.Left:
                    stringWidth = -(int)fonts[0].data.width / 2;
                    break;
                case AlignType.Right:
                    stringWidth = stringWidth - (int)fonts[fonts.Count - 1].data.width / 2;
                    break;
                case AlignType  .Center:
                    stringWidth = (int)(stringWidth * 0.5f - fonts[0].data.width / 2);
                    break;
            }

            int currentX = 0;

            foreach (FontObject font in fonts)
            {
                font.obj.x = currentX - stringWidth;
                currentX += font.data.width;
            }
        }

        public void Flush()
        {
            foreach (FontObject font in fonts)
            {
                font.obj.ReturnToPool();
            }
            fonts.Clear();
        }

        public void Show()
        {
            foreach (FontObject font in fonts)
                font.obj.Show();

            isHide = false;
        }

        public void Hide()
        {
            foreach (FontObject font in fonts)
                font.obj.Hide();

            isHide = true;
        }

        public void DestroyObject()
        {
            Flush();
            Destroy(gameObject);
        }
    }
}