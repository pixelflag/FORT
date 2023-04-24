using UnityEngine;

namespace pixelflag.controller
{
    public class GamepadControl
    {
        public ControllerKey GetPad1()
        {
            ControllerKey input = new ControllerKey();

            input.stickX = Input.GetAxis("Gamepad1H");
            input.stickY = Input.GetAxis("Gamepad1V");

            if(input.stickX == 0)
                input.stickX = Input.GetAxis("Gamepad1POV_H");
            if (input.stickY == 0)
                input.stickY = Input.GetAxis("Gamepad1POV_V");

            input.button1 = Input.GetKey(KeyCode.Joystick1Button0);
            input.button2 = Input.GetKey(KeyCode.Joystick1Button1);
            input.button3 = Input.GetKey(KeyCode.Joystick1Button2);
            input.button4 = Input.GetKey(KeyCode.Joystick1Button3);

            input.L1 = Input.GetKey(KeyCode.Joystick1Button4);
            input.R1 = Input.GetKey(KeyCode.Joystick1Button5);

            input.option1 = Input.GetKey(KeyCode.Joystick1Button6);
            input.option2 = Input.GetKey(KeyCode.Joystick1Button7);

            return input;
        }

        public ControllerKey GetPad2()
        {
            // 検証はできないからどうしようか。

            ControllerKey input = new ControllerKey();

            input.stickX = Input.GetAxis("Gamepad2V");
            input.stickY = Input.GetAxis("Gamepad2H");

            if (input.stickX == 0)
                input.stickX = Input.GetAxis("Gamepad2POV_H");
            if (input.stickY == 0)
                input.stickY = Input.GetAxis("Gamepad2POV_V");

            input.button1 = Input.GetKey(KeyCode.Joystick2Button0);
            input.button2 = Input.GetKey(KeyCode.Joystick2Button1);
            input.button3 = Input.GetKey(KeyCode.Joystick2Button2);
            input.button4 = Input.GetKey(KeyCode.Joystick2Button3);

            input.L1 = Input.GetKey(KeyCode.Joystick2Button4);
            input.R1 = Input.GetKey(KeyCode.Joystick2Button5);

            input.option1 = Input.GetKey(KeyCode.Joystick2Button6);
            input.option2 = Input.GetKey(KeyCode.Joystick2Button7);

            return input;
        }
    }
}