using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pixelflag.UI;
using UnityEngine.EventSystems;

public class UiTest : MonoBehaviour
{
    public TextObject gameFont;
    public TextButton textButton;

    public Button button;
    public NumStepper numStepper;
    public DoubleNumStepper doubleNumStepper;
    public TripleNumStepper tripleNumStepper;
    public LavelChechBox CheckBox;
    public EasingPullDown easingPullDown;

    public Gauge gauge;

    void Start()
    {
        gameFont.Initialize();

        button.OnClick = ButtonClick;

        textButton.Initialize();
        textButton.OnClick = TextButtonClick;

        numStepper.Initialize("Float", 128000);
        numStepper.OnUpdate = NumStepperUpdate;

        doubleNumStepper.Initialize("Vector2", 16, 32);
        doubleNumStepper.OnUpdate = DoubleNumStepperUpdate;

        tripleNumStepper.Initialize("Vector3", 1024, 512, 65535);
        tripleNumStepper.OnUpdate = TripleNumStepperUpdate;

        CheckBox.Initialize("CheckBox", true);
        CheckBox.OnUpdate = CheckBoxUpdate;

        easingPullDown.Initialize("Easing", EasingType.Constant);
        easingPullDown.OnUpdate = EasingTypeUpdate;

        gauge.Initialize();
    }

    private void TextButtonClick(PointerEventData data)
    {
        Debug.Log("TextButtonClick");
    }

    private void ButtonClick(PointerEventData data)
    {
        Debug.Log("ButtonClick");
    }

    private void NumStepperUpdate(int value)
    {
        Debug.Log("NumStepper Update : " + value);
    }

    private void DoubleNumStepperUpdate(int value1, int value2)
    {
        Debug.Log("DoubleNumStepper Update : " + value1 + " : " + value2);
    }

    private void TripleNumStepperUpdate(int value1, int value2, int value3)
    {
        Debug.Log("TripleNumStepper Update : " + value1 + " : " + value2 + " : " + value3);
    }

    private void CheckBoxUpdate(bool value)
    {
        Debug.Log("CheckBox Update : " + value);
    }

    private void EasingTypeUpdate(int index)
    {
        Debug.Log("EasingType Update : " + index);
    }

}
