using UnityEngine;

namespace pixelflag.controller
{
    public class Gamepad
    {
        public ControllerType controllerType { get; private set; }
        public GamePadNum gamePadNum { get; private set; }

        private bool usePC = true;
        private bool useJoycon = false;
        private bool useGamepad = true;

        private PcControl pc;
        private JoyconControl jc;
        private GamepadControl gp;

        public ControllerKey pcInput { get; private set; }
        public ControllerKey joyconInput { get; private set; }
        public ControllerKey gamePadInput { get; private set; }

        // ----------

        public float stickX;
        public float stickY;

        //   3 
        // 4   1
        //   2

        public bool button1;
        public bool button2;
        public bool button3;
        public bool button4;

        public bool L1;
        public bool R1;

        public bool option1;
        public bool option2;

        // ----------

        public bool prevButton1;
        public bool prevButton2;
        public bool prevButton3;
        public bool prevButton4;

        public bool prevL1;
        public bool prevR1;

        public bool prevOption1;
        public bool prevOption2;

        public Gamepad(GamePadNum gamePadNum)
        {
            this.gamePadNum = gamePadNum;

            pc = new PcControl();
            jc = new JoyconControl();
            gp = new GamepadControl();
        }

        public void Update()
        {
            prevButton1 = button1;
            prevButton2 = button2;
            prevButton3 = button3;
            prevButton4 = button4;

            prevL1 = L1;
            prevR1 = R1;

            prevOption1 = option1;
            prevOption2 = option2;

            stickX = 0;
            stickY = 0;

            button1 = false;
            button2 = false;
            button3 = false;
            button4 = false;

            L1 = false;
            R1 = false;

            option1 = false;
            option2 = false;

            switch (gamePadNum)
            {
                case GamePadNum.Gamepad1:
                    if (usePC)
                    {
                        pcInput = pc.GetPad1();
                        OverrideKey(pcInput);
                    }

                    if (useJoycon)
                    {
                        joyconInput = jc.GetPad1();
                        OverrideKey(joyconInput);
                    }

                    if (useGamepad)
                    {
                        gamePadInput = gp.GetPad1();
                        OverrideKey(gamePadInput);
                    }

                    break;
                case GamePadNum.Gamepad2:
                    if (usePC)
                    {
                        pcInput = pc.GetPad2();
                        OverrideKey(pcInput);
                    }
                    if (useJoycon)
                    {
                        joyconInput = jc.GetPad2();
                        OverrideKey(joyconInput);
                    }
                    if (useGamepad)
                    {
                        gamePadInput = gp.GetPad2();
                        OverrideKey(gamePadInput);
                    }
                    break;
            }

        }

        private void OverrideKey(ControllerKey newKey)
        {
            float straight = 1;

            stickX = stickX + newKey.stickX;
            stickY = stickY + newKey.stickY;

            stickX = stickX > straight ? straight : stickX;
            stickX = stickX < -straight ? -straight : stickX;
            stickY = stickY > straight ? straight : stickY;
            stickY = stickY < -straight ? -straight : stickY;

            button1 = newKey.button1 || button1;
            button2 = newKey.button2 || button2;
            button3 = newKey.button3 || button3;
            button4 = newKey.button4 || button4;

            R1 = newKey.R1 || R1;
            L1 = newKey.L1 || L1;

            option1 = newKey.option1 || option1;
            option2 = newKey.option2 || option2;
        }

        public Vector3 GetStick()
        {
            return new Vector3(stickX, stickY,0);
        }

        public bool GetKeyDown(ControllerButtonType type)
        {
            switch (type)
            {
                case ControllerButtonType.Button1: return button1 && !prevButton1;
                case ControllerButtonType.Button2: return button2 && !prevButton2;
                case ControllerButtonType.Button3: return button3 && !prevButton3;
                case ControllerButtonType.Button4: return button4 && !prevButton4;
                case ControllerButtonType.R1: return R1 && !prevR1;
                case ControllerButtonType.L1: return L1 && !prevL1;
                case ControllerButtonType.Option1: return option1 && !prevOption1;
                case ControllerButtonType.Option2: return option2 && !prevOption2;
                default: return false;
            }
        }

        public bool Getp(ControllerButtonType type)
        {
            switch (type)
            {
                case ControllerButtonType.Button1: return !button1 && prevButton1;
                case ControllerButtonType.Button2: return !button2 && prevButton2;
                case ControllerButtonType.Button3: return !button3 && prevButton3;
                case ControllerButtonType.Button4: return !button4 && prevButton4;
                case ControllerButtonType.R1: return !R1 && prevR1;
                case ControllerButtonType.L1: return !L1 && prevL1;
                case ControllerButtonType.Option1: return !option1 && prevOption1;
                case ControllerButtonType.Option2: return !option2 && prevOption2;
                default: return false;
            }
        }

        public bool GetKey(ControllerButtonType type)
        {
            switch (type)
            {
                case ControllerButtonType.Button1: return button1;
                case ControllerButtonType.Button2: return button2;
                case ControllerButtonType.Button3: return button3;
                case ControllerButtonType.Button4: return button4;
                case ControllerButtonType.R1: return R1;
                case ControllerButtonType.L1: return L1;
                case ControllerButtonType.Option1: return option1;
                case ControllerButtonType.Option2: return option2;
                default: return false;
            }
        }

        public void SetControllerType(ControllerType type)
        {
            controllerType = type;

            usePC = false;
            useJoycon = false;
            useGamepad = false;

            switch (type)
            {
                case ControllerType.All:
                    usePC = true;
                    useJoycon = true;
                    useGamepad = true;
                    break;
                case ControllerType.PC:
                    usePC = true;
                    break;
                case ControllerType.Joycon:
                    useJoycon = true;
                    break;
                case ControllerType.Gamepad:
                    useGamepad = true;
                    break;
            }
        }

        public void SetPcKey(PcControl pcKeyInput)
        {
            pc = pcKeyInput;
        }
    }
}