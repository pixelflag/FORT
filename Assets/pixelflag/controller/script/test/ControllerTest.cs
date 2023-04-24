using UnityEngine;

namespace pixelflag.controller
{
    public class ControllerTest : MonoBehaviour
    {
        private ControllerInput input;

        private void Start()
        {
            input = new ControllerInput();
        }

        private void OnGUI()
        {
            currentX = 20;
            currentY = 0;

            // keybord
            GUI.Label(CreateRect(), "Pc Keybord1");

            GUI.Label(CreateRect(), "[A,D]Stick.x : " + input.pad1.pcInput.stickX);
            GUI.Label(CreateRect(), "[W,S]Stick.y : " + input.pad1.pcInput.stickY);
            GUI.Label(CreateRect(), "[V]button1 : " + input.pad1.pcInput.button1);
            GUI.Label(CreateRect(), "[C]button2 : " + input.pad1.pcInput.button2);
            GUI.Label(CreateRect(), "[X]button3 : " + input.pad1.pcInput.button3);
            GUI.Label(CreateRect(), "[F]button4 : " + input.pad1.pcInput.button4);
            GUI.Label(CreateRect(), "[Q]L1 : " + input.pad1.pcInput.L1);
            GUI.Label(CreateRect(), "[E]L2 : " + input.pad1.pcInput.R1);
            GUI.Label(CreateRect(), "[R]option1 : " + input.pad1.pcInput.option1);
            GUI.Label(CreateRect(), "[T]option2 : " + input.pad1.pcInput.option2);

            currentY += 16;

            GUI.Label(CreateRect(), "Pc Keybord2");
            GUI.Label(CreateRect(), "[I,K]Stick.x : " + input.pad2.pcInput.stickX);
            GUI.Label(CreateRect(), "[J,L]Stick.y : " + input.pad2.pcInput.stickY);
            GUI.Label(CreateRect(), "[M]button1 : " + input.pad2.pcInput.button1);
            GUI.Label(CreateRect(), "[N]button2 : " + input.pad2.pcInput.button2);
            GUI.Label(CreateRect(), "[B]button3 : " + input.pad2.pcInput.button3);
            GUI.Label(CreateRect(), "[H]button4 : " + input.pad2.pcInput.button4);
            GUI.Label(CreateRect(), "[U]L1 : " + input.pad2.pcInput.L1);
            GUI.Label(CreateRect(), "[O]R1 : " + input.pad2.pcInput.R1);
            GUI.Label(CreateRect(), "[Y]option1 : " + input.pad2.pcInput.option1);
            GUI.Label(CreateRect(), "[U]option2 : " + input.pad2.pcInput.option2);

            currentX = 200;
            currentY = 0;

            // Gamepad
            GUI.Label(CreateRect(), "Gamepad1");

            GUI.Label(CreateRect(), "Stick.x : " + input.pad1.gamePadInput.stickX);
            GUI.Label(CreateRect(), "Stick.y : " + input.pad1.gamePadInput.stickY);
            GUI.Label(CreateRect(), "button1 : " + input.pad1.gamePadInput.button1);
            GUI.Label(CreateRect(), "button2 : " + input.pad1.gamePadInput.button2);
            GUI.Label(CreateRect(), "button3 : " + input.pad1.gamePadInput.button3);
            GUI.Label(CreateRect(), "button4 : " + input.pad1.gamePadInput.button4);
            GUI.Label(CreateRect(), "L1 : " + input.pad1.gamePadInput.L1);
            GUI.Label(CreateRect(), "L2 : " + input.pad1.gamePadInput.R1);
            GUI.Label(CreateRect(), "option1 : " + input.pad1.gamePadInput.option1);
            GUI.Label(CreateRect(), "option2 : " + input.pad1.gamePadInput.option2);

            currentY += 16;

            GUI.Label(CreateRect(), "Gamepad2");
            GUI.Label(CreateRect(), "Stick.x : " + input.pad2.gamePadInput.stickX);
            GUI.Label(CreateRect(), "Stick.y : " + input.pad2.gamePadInput.stickY);
            GUI.Label(CreateRect(), "button1 : " + input.pad2.gamePadInput.button1);
            GUI.Label(CreateRect(), "button2 : " + input.pad2.gamePadInput.button2);
            GUI.Label(CreateRect(), "button3 : " + input.pad2.gamePadInput.button3);
            GUI.Label(CreateRect(), "button4 : " + input.pad2.gamePadInput.button4);
            GUI.Label(CreateRect(), "L1 : " + input.pad2.gamePadInput.L1);
            GUI.Label(CreateRect(), "R1 : " + input.pad2.gamePadInput.R1);
            GUI.Label(CreateRect(), "option1 : " + input.pad2.gamePadInput.option1);
            GUI.Label(CreateRect(), "option2 : " + input.pad2.gamePadInput.option2);

            currentX = 400;
            currentY = 0;

            // Joycon
            GUI.Label(CreateRect(), "Joycon1");

            GUI.Label(CreateRect(), "Stick.x : " + input.pad1.joyconInput.stickX);
            GUI.Label(CreateRect(), "Stick.y : " + input.pad1.joyconInput.stickY);
            GUI.Label(CreateRect(), "button1 : " + input.pad1.joyconInput.button1);
            GUI.Label(CreateRect(), "button2 : " + input.pad1.joyconInput.button2);
            GUI.Label(CreateRect(), "button3 : " + input.pad1.joyconInput.button3);
            GUI.Label(CreateRect(), "button4 : " + input.pad1.joyconInput.button4);
            GUI.Label(CreateRect(), "L1 : " + input.pad1.joyconInput.L1);
            GUI.Label(CreateRect(), "L2 : " + input.pad1.joyconInput.R1);
            GUI.Label(CreateRect(), "option1 : " + input.pad1.joyconInput.option1);
            GUI.Label(CreateRect(), "option2 : " + input.pad1.joyconInput.option2);

            currentY += 16;

            GUI.Label(CreateRect(), "Joycon2");
            GUI.Label(CreateRect(), "Stick.x : " + input.pad2.joyconInput.stickX);
            GUI.Label(CreateRect(), "Stick.y : " + input.pad2.joyconInput.stickY);
            GUI.Label(CreateRect(), "button1 : " + input.pad2.joyconInput.button1);
            GUI.Label(CreateRect(), "button2 : " + input.pad2.joyconInput.button2);
            GUI.Label(CreateRect(), "button3 : " + input.pad2.joyconInput.button3);
            GUI.Label(CreateRect(), "button4 : " + input.pad2.joyconInput.button4);
            GUI.Label(CreateRect(), "L1 : " + input.pad2.joyconInput.L1);
            GUI.Label(CreateRect(), "R1 : " + input.pad2.joyconInput.R1);
            GUI.Label(CreateRect(), "option1 : " + input.pad2.joyconInput.option1);
            GUI.Label(CreateRect(), "option2 : " + input.pad2.joyconInput.option2);

            currentX = 600;
            currentY = 0;

            // All
            GUI.Label(CreateRect(), "all pad1");

            GUI.Label(CreateRect(), "Stick.x : " + input.pad1.stickX);
            GUI.Label(CreateRect(), "Stick.y : " + input.pad1.stickY);
            GUI.Label(CreateRect(), "button1 : " + input.pad1.GetKey(ControllerButtonType.Button1));
            GUI.Label(CreateRect(), "button2 : " + input.pad1.GetKey(ControllerButtonType.Button2));
            GUI.Label(CreateRect(), "button3 : " + input.pad1.GetKey(ControllerButtonType.Button3));
            GUI.Label(CreateRect(), "button4 : " + input.pad1.GetKey(ControllerButtonType.Button4));
            GUI.Label(CreateRect(), "L1 : " + input.pad1.GetKey(ControllerButtonType.L1));
            GUI.Label(CreateRect(), "L2 : " + input.pad1.GetKey(ControllerButtonType.R1));
            GUI.Label(CreateRect(), "option1 : " + input.pad1.GetKey(ControllerButtonType.Option1));
            GUI.Label(CreateRect(), "option2 : " + input.pad1.GetKey(ControllerButtonType.Option2));

            currentY += 16;

            GUI.Label(CreateRect(), "all pad2");
            GUI.Label(CreateRect(), "Stick.x : " + input.pad2.stickX);
            GUI.Label(CreateRect(), "Stick.y : " + input.pad2.stickY);
            GUI.Label(CreateRect(), "button1 : " + input.pad2.GetKey(ControllerButtonType.Button1));
            GUI.Label(CreateRect(), "button2 : " + input.pad2.GetKey(ControllerButtonType.Button2));
            GUI.Label(CreateRect(), "button3 : " + input.pad2.GetKey(ControllerButtonType.Button3));
            GUI.Label(CreateRect(), "button4 : " + input.pad2.GetKey(ControllerButtonType.Button4));
            GUI.Label(CreateRect(), "L1 : " + input.pad2.GetKey(ControllerButtonType.L1));
            GUI.Label(CreateRect(), "L2 : " + input.pad2.GetKey(ControllerButtonType.R1));
            GUI.Label(CreateRect(), "option1 : " + input.pad2.GetKey(ControllerButtonType.Option1));
            GUI.Label(CreateRect(), "option2 : " + input.pad2.GetKey(ControllerButtonType.Option2));

            currentX = 800;
            currentY = 0;

            // KeyCode Test
            GUI.Label(CreateRect(), "keycode");
            GUI.Label(CreateRect(), "Joystick1Button0 : " + Input.GetKey(KeyCode.Joystick1Button0));
            GUI.Label(CreateRect(), "Joystick1Button1 : " + Input.GetKey(KeyCode.Joystick1Button1));
            GUI.Label(CreateRect(), "Joystick1Button2 : " + Input.GetKey(KeyCode.Joystick1Button2));
            GUI.Label(CreateRect(), "Joystick1Button3 : " + Input.GetKey(KeyCode.Joystick1Button3));
            GUI.Label(CreateRect(), "Joystick1Button4 : " + Input.GetKey(KeyCode.Joystick1Button4));
            GUI.Label(CreateRect(), "Joystick1Button5 : " + Input.GetKey(KeyCode.Joystick1Button5));
            GUI.Label(CreateRect(), "Joystick1Button6 : " + Input.GetKey(KeyCode.Joystick1Button6));
            GUI.Label(CreateRect(), "Joystick1Button7 : " + Input.GetKey(KeyCode.Joystick1Button7));
            GUI.Label(CreateRect(), "Joystick1Button8 : " + Input.GetKey(KeyCode.Joystick1Button8));
            GUI.Label(CreateRect(), "Joystick1Button9 : " + Input.GetKey(KeyCode.Joystick1Button9));
            GUI.Label(CreateRect(), "Joystick1Button10 : " + Input.GetKey(KeyCode.Joystick1Button10));
            GUI.Label(CreateRect(), "Joystick1Button11 : " + Input.GetKey(KeyCode.Joystick1Button11));
            GUI.Label(CreateRect(), "Joystick1Button12 : " + Input.GetKey(KeyCode.Joystick1Button12));
            GUI.Label(CreateRect(), "Joystick1Button13 : " + Input.GetKey(KeyCode.Joystick1Button13));
            GUI.Label(CreateRect(), "Joystick1Button14 : " + Input.GetKey(KeyCode.Joystick1Button14));
            GUI.Label(CreateRect(), "Joystick1Button15 : " + Input.GetKey(KeyCode.Joystick1Button15));
            GUI.Label(CreateRect(), "Joystick1Button16 : " + Input.GetKey(KeyCode.Joystick1Button16));
            GUI.Label(CreateRect(), "Joystick1Button17 : " + Input.GetKey(KeyCode.Joystick1Button17));
            GUI.Label(CreateRect(), "Joystick1Button18 : " + Input.GetKey(KeyCode.Joystick1Button18));
            GUI.Label(CreateRect(), "Joystick1Button19 : " + Input.GetKey(KeyCode.Joystick1Button19));

            GUI.Label(CreateRect(), "Joystick2Button0 : " + Input.GetKey(KeyCode.Joystick2Button0));
            GUI.Label(CreateRect(), "Joystick2Button1 : " + Input.GetKey(KeyCode.Joystick2Button1));
            GUI.Label(CreateRect(), "Joystick2Button2 : " + Input.GetKey(KeyCode.Joystick2Button2));
            GUI.Label(CreateRect(), "Joystick2Button3 : " + Input.GetKey(KeyCode.Joystick2Button3));
            GUI.Label(CreateRect(), "Joystick2Button4 : " + Input.GetKey(KeyCode.Joystick2Button4));
            GUI.Label(CreateRect(), "Joystick2Button5 : " + Input.GetKey(KeyCode.Joystick2Button5));
            GUI.Label(CreateRect(), "Joystick2Button6 : " + Input.GetKey(KeyCode.Joystick2Button6));
            GUI.Label(CreateRect(), "Joystick2Button7 : " + Input.GetKey(KeyCode.Joystick2Button7));
            GUI.Label(CreateRect(), "Joystick2Button8 : " + Input.GetKey(KeyCode.Joystick2Button8));
            GUI.Label(CreateRect(), "Joystick2Button9 : " + Input.GetKey(KeyCode.Joystick2Button9));
            GUI.Label(CreateRect(), "Joystick2Button10 : " + Input.GetKey(KeyCode.Joystick2Button10));
            GUI.Label(CreateRect(), "Joystick2Button11 : " + Input.GetKey(KeyCode.Joystick2Button11));
            GUI.Label(CreateRect(), "Joystick2Button12 : " + Input.GetKey(KeyCode.Joystick2Button12));
            GUI.Label(CreateRect(), "Joystick2Button13 : " + Input.GetKey(KeyCode.Joystick2Button13));
            GUI.Label(CreateRect(), "Joystick2Button14 : " + Input.GetKey(KeyCode.Joystick2Button14));
            GUI.Label(CreateRect(), "Joystick2Button15 : " + Input.GetKey(KeyCode.Joystick2Button15));
            GUI.Label(CreateRect(), "Joystick2Button16 : " + Input.GetKey(KeyCode.Joystick2Button16));
            GUI.Label(CreateRect(), "Joystick2Button17 : " + Input.GetKey(KeyCode.Joystick2Button17));
            GUI.Label(CreateRect(), "Joystick2Button18 : " + Input.GetKey(KeyCode.Joystick2Button18));
            GUI.Label(CreateRect(), "Joystick2Button19 : " + Input.GetKey(KeyCode.Joystick2Button19));

            currentX = 1000;
            currentY = 0;

            GUI.Label(CreateRect(), "Joystick3Button0 : " + Input.GetKey(KeyCode.Joystick3Button0));
            GUI.Label(CreateRect(), "Joystick3Button1 : " + Input.GetKey(KeyCode.Joystick3Button1));
            GUI.Label(CreateRect(), "Joystick3Button2 : " + Input.GetKey(KeyCode.Joystick3Button2));
            GUI.Label(CreateRect(), "Joystick3Button3 : " + Input.GetKey(KeyCode.Joystick3Button3));
            GUI.Label(CreateRect(), "Joystick3Button4 : " + Input.GetKey(KeyCode.Joystick3Button4));
            GUI.Label(CreateRect(), "Joystick3Button5 : " + Input.GetKey(KeyCode.Joystick3Button5));
            GUI.Label(CreateRect(), "Joystick3Button6 : " + Input.GetKey(KeyCode.Joystick3Button6));
            GUI.Label(CreateRect(), "Joystick3Button7 : " + Input.GetKey(KeyCode.Joystick3Button7));
            GUI.Label(CreateRect(), "Joystick3Button8 : " + Input.GetKey(KeyCode.Joystick3Button8));
            GUI.Label(CreateRect(), "Joystick3Button9 : " + Input.GetKey(KeyCode.Joystick3Button9));
            GUI.Label(CreateRect(), "Joystick3Button10 : " + Input.GetKey(KeyCode.Joystick3Button10));
            GUI.Label(CreateRect(), "Joystick3Button11 : " + Input.GetKey(KeyCode.Joystick3Button11));
            GUI.Label(CreateRect(), "Joystick3Button12 : " + Input.GetKey(KeyCode.Joystick3Button12));
            GUI.Label(CreateRect(), "Joystick3Button13 : " + Input.GetKey(KeyCode.Joystick3Button13));
            GUI.Label(CreateRect(), "Joystick3Button14 : " + Input.GetKey(KeyCode.Joystick3Button14));
            GUI.Label(CreateRect(), "Joystick3Button15 : " + Input.GetKey(KeyCode.Joystick3Button15));
            GUI.Label(CreateRect(), "Joystick3Button16 : " + Input.GetKey(KeyCode.Joystick3Button16));
            GUI.Label(CreateRect(), "Joystick3Button17 : " + Input.GetKey(KeyCode.Joystick3Button17));
            GUI.Label(CreateRect(), "Joystick3Button18 : " + Input.GetKey(KeyCode.Joystick3Button18));
            GUI.Label(CreateRect(), "Joystick3Button19 : " + Input.GetKey(KeyCode.Joystick3Button19));

        }

        private int currentX;
        private int currentY;

        private Rect CreateRect()
        {
            currentY += 16;
            return new Rect(currentX, currentY, 200, 20);
        }

        private void Update()
        {
            input.Update();
        }
    }

}