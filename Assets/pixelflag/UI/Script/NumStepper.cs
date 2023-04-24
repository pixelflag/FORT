using UnityEngine;
using UnityEngine.EventSystems;

namespace pixelflag.UI
{
    public class NumStepper : NumStepperBase
    {
        [SerializeField]
        private Button plusButton = default;
        [SerializeField]
        private Button minusButton = default;
        [SerializeField]
        private TextObject numText = default;
        [SerializeField]
        private TextObject labelText = default;

        private int value = 0;

        public void Initialize(string label, int value)
        {
            this.value = value;
            isActive = true;

            numText.Initialize();
            numText.SetText(value.ToString());

            labelText.Initialize();
            labelText.SetText(label);

            plusButton.OnClick = OnPlusClick;
            minusButton.OnClick = OnMinusClick;
        }

        public void SetValue(int val)
        {
            value = CheckLimit(val);
            numText.SetText(value.ToString());
        }

        public void OnPlusClick(PointerEventData data)
        {
            if (!isActive) return;
            value += AmplificateValue(data.pointerId);
            UpdateValue();
        }

        public void OnMinusClick(PointerEventData data)
        {
            if (!isActive) return;
            value -= AmplificateValue(data.pointerId);
            UpdateValue();
        }

        private void UpdateValue()
        {
            value = CheckLimit(value);
            numText.SetText(value.ToString());
            if (OnUpdate != null) OnUpdate(value);
        }

        public delegate void UpdateDelegate(int value);
        public UpdateDelegate OnUpdate;
    }
}