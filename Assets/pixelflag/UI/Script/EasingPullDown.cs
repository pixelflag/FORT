using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace pixelflag.UI
{
    public class EasingPullDown : MonoBehaviour
    {

#if UNITY_EDITOR
        private string path1 = "Assets/pixelflag/UI/Graphics/easing_off.png";
        private string path2 = "Assets/pixelflag/UI/Graphics/easing_on.png";

        [ContextMenu("Load Sprite")]
        void LoadTile()
        {
            sprites_off = AssetDatabase.LoadAllAssetsAtPath(path1).OfType<Sprite>().ToArray();
            sprites_on = AssetDatabase.LoadAllAssetsAtPath(path2).OfType<Sprite>().ToArray();
        }
#endif
        [SerializeField]
        private Sprite[] sprites_off = default;
        [SerializeField]
        private Sprite[] sprites_on = default;

        [SerializeField]
        private EasingButton buttonPrefav = default;

        [SerializeField]
        private TextObject labelText = default;
        [SerializeField]
        private TextObject selectedText = default;
        [SerializeField]
        private Button pullDownButton = default;

        private EasingType selectedType = EasingType.Constant;
        private List<EasingButton> buttonList;

        private List<EasingType> typeList;

        private bool isOpen = false;

        public bool isActive { get; private set; }

        private void Awake()
        {
            buttonList = new List<EasingButton>();
            typeList = new List<EasingType>();

            foreach (EasingType value in Enum.GetValues(typeof(EasingType)))
            {
                typeList.Add(value);
            }
        }

        public void Initialize(string label, EasingType type)
        {
            this.selectedType = type;
            isActive = true;

            labelText.Initialize();
            labelText.SetText(label);

            selectedText.Initialize();
            selectedText.SetText(selectedType.ToString());

            pullDownButton.OnClick = PulldownClick;

            CreateWindow();
        }

        public void SetValue(int index)
        {
            selectedType = typeList[index];
            selectedText.SetText(selectedType.ToString());
        }

        private void CreateWindow()
        {
            int initX = -8;
            int initY = -24;
            int stepX = 32;
            int stepY = 32;
            int rawMax = 3;
            int currentRaw = 0;
            int currentY = initY;

            // 表示順
            EasingType[] list = new EasingType[]
            {
                EasingType.SineIn,
                EasingType.SineOut,
                EasingType.SineInOut,
                EasingType.QuadIn,
                EasingType.QuadOut,
                EasingType.QuadInOut,
                EasingType.CubicIn,
                EasingType.CubicOut,
                EasingType.CubicInOut,
                EasingType.QuartIn,
                EasingType.QuartOut,
                EasingType.QuartInOut,
                EasingType.QuintIn,
                EasingType.QuintOut,
                EasingType.QuintInOut,
                EasingType.ExpIn,
                EasingType.ExpOut,
                EasingType.ExpInOut,
                EasingType.CircIn,
                EasingType.CircOut,
                EasingType.CircInOut,
                EasingType.BackIn,
                EasingType.BackOut,
                EasingType.BackInOut,
                EasingType.ElasticIn,
                EasingType.ElasticOut,
                EasingType.ElasticInOut,
                EasingType.BounceIn,
                EasingType.BounceOut,
                EasingType.BounceInOut,
                EasingType.Constant,
                EasingType.Linear
            };

            for (int i=0; i < list.Length; i++)
            {
                int xx = initX + stepX* currentRaw;
                int yy = currentY;

                CreateButton(list[i], xx,yy);

                currentRaw++;
                if(rawMax <= currentRaw)
                {
                    currentRaw = 0;
                    currentY -= stepY;
                }
            }

            CloseWindow();
        }

        private void CreateButton(EasingType type, int xx, int yy)
        {
            EasingButton button = Instantiate(buttonPrefav, transform);

            int index = GetEasingSpriteIndex(type);
            button.Initialize(type, sprites_on[index], sprites_off[index]);
            button.x = xx;
            button.y = yy;
            button.OnClick = ButtonClick;

            buttonList.Add(button);
        }

        private void PulldownClick(PointerEventData eventData)
        {
            if (!isActive) return;

            if (isOpen)
                CloseWindow();
            else
                OpenWindow();
        }

        private void OpenWindow()
        {
            foreach (EasingButton button in buttonList)
            {
                button.gameObject.SetActive(true);
            }
            isOpen = true;
        }

        private void CloseWindow()
        {
            foreach (EasingButton button in buttonList)
            {
                button.gameObject.SetActive(false);
            }
            isOpen = false;
        }

        public void ButtonClick(EasingType type)
        {
            if (!isActive) return;

            this.selectedType = type;
            selectedText.SetText(selectedType.ToString());
            CloseWindow();

            if (OnUpdate != null) OnUpdate((int)selectedType);
        }

        public delegate void UpdateDelegate(int index);
        public UpdateDelegate OnUpdate;

        public void SetActive(bool b)
        {
            gameObject.SetActive(b);
            isActive = b;
        }

        private int GetEasingSpriteIndex(EasingType type)
        {
            switch (type)
            {
                case EasingType.Constant: return 3;
                case EasingType.Linear: return 7;
                case EasingType.SineIn: return 0;
                case EasingType.SineOut: return 1;
                case EasingType.SineInOut: return 2;
                case EasingType.QuadIn: return 4;
                case EasingType.QuadOut: return 5;
                case EasingType.QuadInOut: return 6;
                case EasingType.CubicIn: return 8;
                case EasingType.CubicOut: return 9;
                case EasingType.CubicInOut: return 10;
                case EasingType.QuartIn: return 12;
                case EasingType.QuartOut: return 13;
                case EasingType.QuartInOut: return 14;
                case EasingType.QuintIn: return 16;
                case EasingType.QuintOut: return 17;
                case EasingType.QuintInOut: return 18;
                case EasingType.ExpIn: return 20;
                case EasingType.ExpOut: return 21;
                case EasingType.ExpInOut: return 22;
                case EasingType.CircIn: return 24;
                case EasingType.CircOut: return 25;
                case EasingType.CircInOut: return 26;
                case EasingType.BackIn: return 28;
                case EasingType.BackOut: return 29;
                case EasingType.BackInOut: return 30;
                case EasingType.ElasticIn: return 11;
                case EasingType.ElasticOut: return 15;
                case EasingType.ElasticInOut: return 19;
                case EasingType.BounceIn: return 23;
                case EasingType.BounceOut: return 27;
                case EasingType.BounceInOut: return 31;
            }
            return 0;
        }
    }
}