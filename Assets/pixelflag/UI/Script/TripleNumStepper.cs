using UnityEngine;
using UnityEngine.EventSystems;

namespace pixelflag.UI
{
    public class TripleNumStepper : NumStepperBase
    {
        [SerializeField]
        private TextObject labelText = default;

        // -----

        [SerializeField]
        private Button plusButton1 = default;
        [SerializeField]
        private Button minusButton1 = default;
        [SerializeField]
        private TextObject miniLabelText1 = default;
        [SerializeField]
        private TextObject numText1 = default;

        private int value1 = 0;

        // -----

        [SerializeField]
        private Button plusButton2 = default;
        [SerializeField]
        private Button minusButton2 = default;
        [SerializeField]
        private TextObject miniLabelText2 = default;
        [SerializeField]
        private TextObject numText2 = default;

        private int value2 = 0;

        // -----

        [SerializeField]
        private Button plusButton3 = default;
        [SerializeField]
        private Button minusButton3 = default;
        [SerializeField]
        private TextObject miniLabelText3 = default;
        [SerializeField]
        private TextObject numText3 = default;

        private int value3 = 0;
    
        public void Initialize(string label, int value1, int value2, int value3)
        {
            isActive = true;

            labelText.Initialize();
            labelText.SetText(label);

            // value1 -----
            this.value1 = value1;
            miniLabelText1.Initialize();

            numText1.Initialize();
            numText1.SetText(value1.ToString());

            plusButton1.OnClick = OnPlusClick1;
            minusButton1.OnClick = OnMinusClick1;

            // value2 -----
            this.value2 = value2;
            miniLabelText2.Initialize();

            numText2.Initialize();
            numText2.SetText(value2.ToString());

            plusButton2.OnClick = OnPlusClick2;
            minusButton2.OnClick = OnMinusClick2;

            // value3 -----
            this.value3 = value3;
            miniLabelText3.Initialize();

            numText3.Initialize();
            numText3.SetText(value3.ToString());

            plusButton3.OnClick = OnPlusClick3;
            minusButton3.OnClick = OnMinusClick3;
        }

        public void SetValue(int val1, int val2, int val3)
        {
            value1 = CheckLimit(val1);
            numText1.SetText(this.value1.ToString());

            value2 = CheckLimit(val2);
            numText2.SetText(this.value2.ToString());

            value3 = CheckLimit(val3);
            numText3.SetText(this.value3.ToString());
        }

        // value1 -----

        public void SetValue1(int value)
        {
            value1 = CheckLimit(value);
            numText1.SetText(value1.ToString());
        }

        public void OnPlusClick1(PointerEventData data)
        {
            if (!isActive) return;
            value1 += AmplificateValue(data.pointerId);
            UpdateValue();
        }

        public void OnMinusClick1(PointerEventData data)
        {
            if (!isActive) return;
            value1 -= AmplificateValue(data.pointerId);
            UpdateValue();
        }

        // value2 -----

        public void SetValue2(int value)
        {
            value2 = CheckLimit(value);
            numText2.SetText(value2.ToString());
        }

        public void OnPlusClick2(PointerEventData data)
        {
            if (!isActive) return;
            value2 += AmplificateValue(data.pointerId);
            UpdateValue();
        }

        public void OnMinusClick2(PointerEventData data)
        {
            if (!isActive) return;
            value2 -= AmplificateValue(data.pointerId);
            UpdateValue();
        }

        // value3 -----

        public void SetValue3(int value)
        {
            value3 = CheckLimit(value);
            numText3.SetText(value3.ToString());
        }

        public void OnPlusClick3(PointerEventData data)
        {
            if (!isActive) return;
            value3 += AmplificateValue(data.pointerId);
            UpdateValue();
        }

        public void OnMinusClick3(PointerEventData data)
        {
            if (!isActive) return;
            value3 -= AmplificateValue(data.pointerId);
            UpdateValue();
        }

        // ----------

        private void UpdateValue()
        {
            value1 = CheckLimit(value1);
            numText1.SetText(value1.ToString());

            value2 = CheckLimit(value2);
            numText2.SetText(value2.ToString());

            value3 = CheckLimit(value3);
            numText3.SetText(value3.ToString());

            if (OnUpdate != null) OnUpdate(value1, value2, value3);
        }

        public delegate void UpdateDelegate(int value1, int value2, int value3);
        public UpdateDelegate OnUpdate;
    }
}