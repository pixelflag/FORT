using UnityEngine;

namespace pixelflag.controller
{
    public class JoyconControl
    {
        public ControllerKey GetPad1()
        {
            ControllerKey input = new ControllerKey();

            input.stickX = Input.GetAxis("Joycon1H");
            input.stickY = Input.GetAxis("Joycon1V");

            input.button1 = Input.GetKey(KeyCode.Joystick1Button0);
            input.button2 = Input.GetKey(KeyCode.Joystick1Button1);
            input.button3 = Input.GetKey(KeyCode.Joystick1Button2);
            input.button4 = Input.GetKey(KeyCode.Joystick1Button3);

            input.L1 = Input.GetKey(KeyCode.Joystick1Button4);
            input.R1 = Input.GetKey(KeyCode.Joystick1Button5);

            input.option1 = Input.GetKey(KeyCode.Joystick1Button13);
            input.option2 = Input.GetKey(KeyCode.Joystick1Button8);

            return input;
        }

        public ControllerKey GetPad2()
        {
            ControllerKey input = new ControllerKey();

            input.stickX = Input.GetAxis("Joycon2H");
            input.stickY = Input.GetAxis("Joycon2V");

            input.button1 = Input.GetKey(KeyCode.Joystick2Button0);
            input.button2 = Input.GetKey(KeyCode.Joystick2Button1);
            input.button3 = Input.GetKey(KeyCode.Joystick2Button2);
            input.button4 = Input.GetKey(KeyCode.Joystick2Button3);

            input.L1 = Input.GetKey(KeyCode.Joystick2Button4);
            input.R1 = Input.GetKey(KeyCode.Joystick2Button5);

            input.option1 = Input.GetKey(KeyCode.Joystick2Button12);
            input.option2 = Input.GetKey(KeyCode.Joystick2Button9);

            return input;
        }
    }
}