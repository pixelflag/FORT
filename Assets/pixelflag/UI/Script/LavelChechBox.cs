using UnityEngine;
using UnityEngine.EventSystems;

namespace pixelflag.UI
{
    public class LavelChechBox : MonoBehaviour
    {
        [SerializeField]
        private TextObject labelText = default;
        [SerializeField]
        private CheckBox checkBox = default;


        public void Initialize(string label, bool value)
        {
            labelText.Initialize();
            labelText.SetText(label);

            checkBox.SetValue(value);
            checkBox.OnChange = OnChange;
        }

        public void SetValue(bool value)
        {
            checkBox.SetValue(value);
        }

        private void OnChange(bool value)
        {
            if (OnUpdate != null) OnUpdate(value);
        }

        public void Click()
        {
            checkBox.OnPointerClick(null);
        }

        public delegate void UpdateDelegate(bool value);
        public UpdateDelegate OnUpdate;

        public void SetActive(bool b)
        {
            gameObject.SetActive(b);
            checkBox.SetActive(b);
        }
    }
}