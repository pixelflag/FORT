namespace pixelflag.controller
{
    public class ControllerInput
    {
        public Gamepad pad1 { get; private set; }
        public Gamepad pad2 { get; private set; }

        public ControllerInput()
        {
            pad1 = new Gamepad(GamePadNum.Gamepad1);
            pad2 = new Gamepad(GamePadNum.Gamepad2);
        }

        public void Update()
        {
            pad1.Update();
            pad2.Update(); 
        }

        public void SetControllerType(ControllerType type)
        {
            pad1.SetControllerType(type);
            pad2.SetControllerType(type);
        }

        public void SetPcConfig(GamePadNum padNum, PcControl pcKeyInput)
        {
            switch (padNum)
            {
                case GamePadNum.Gamepad1:
                    pad1.SetPcKey(pcKeyInput);
                    break;
                case GamePadNum.Gamepad2:
                    pad2.SetPcKey(pcKeyInput);
                    break;
            }
        }
    }
}