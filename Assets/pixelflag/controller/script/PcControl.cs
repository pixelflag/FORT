using UnityEngine;

namespace pixelflag.controller
{
    public class PcControl
    {
        // Pad1 key;
        public KeyCode P1Left = KeyCode.A;
        public KeyCode P1Right = KeyCode.D;
        public KeyCode P1Up = KeyCode.W;
        public KeyCode P1Down = KeyCode.S;

        public KeyCode P1Button1 = KeyCode.V;
        public KeyCode P1Button2 = KeyCode.C;
        public KeyCode P1Button3 = KeyCode.X;
        public KeyCode P1Button4 = KeyCode.F;

        public KeyCode P1L1 = KeyCode.Q;
        public KeyCode P1R1 = KeyCode.E;

        public KeyCode P1Option1 = KeyCode.R;
        public KeyCode P1Option2 = KeyCode.T;


        public ControllerKey GetPad1()
        {
            ControllerKey input = new ControllerKey();

            if (Input.GetKey(P1Left)  || Input.GetKey(KeyCode.LeftArrow))  { input.stickX -= 1; }
            if (Input.GetKey(P1Right) || Input.GetKey(KeyCode.RightArrow)) { input.stickX += 1; }
            if (Input.GetKey(P1Up)    || Input.GetKey(KeyCode.UpArrow))    { input.stickY += 1; }
            if (Input.GetKey(P1Down)  || Input.GetKey(KeyCode.DownArrow))  { input.stickY -= 1; }

            input.button1 = Input.GetKey(P1Button1);
            input.button2 = Input.GetKey(P1Button2);
            input.button3 = Input.GetKey(P1Button3);
            input.button4 = Input.GetKey(P1Button4);

            input.L1 = Input.GetKey(P1L1);
            input.R1 = Input.GetKey(P1R1);

            input.option1 = Input.GetKey(P1Option1);
            input.option2 = Input.GetKey(P1Option2);

            return input;
        }


        // Pad2 key;
        public KeyCode P2Left = KeyCode.J;
        public KeyCode P2Right = KeyCode.L;
        public KeyCode P2Up = KeyCode.I;
        public KeyCode P2Down = KeyCode.K;

        public KeyCode P2Button1 = KeyCode.M;
        public KeyCode P2Button2 = KeyCode.N;
        public KeyCode P2Button3 = KeyCode.B;
        public KeyCode P2Button4 = KeyCode.H;

        public KeyCode P2L1 = KeyCode.U;
        public KeyCode P2R1 = KeyCode.O;

        public KeyCode P2Option1 = KeyCode.P;
        public KeyCode P2Option2 = KeyCode.LeftBracket;

        public ControllerKey GetPad2()
        {
            ControllerKey input = new ControllerKey();

            if (Input.GetKey(P2Left))  { input.stickX -= 1; }
            if (Input.GetKey(P2Right)) { input.stickX += 1; }
            if (Input.GetKey(P2Up))    { input.stickY += 1; }
            if (Input.GetKey(P2Down))  { input.stickY -= 1; }

            input.button1 = Input.GetKey(P2Button1);
            input.button2 = Input.GetKey(P2Button2);
            input.button3 = Input.GetKey(P2Button3);
            input.button4 = Input.GetKey(P2Button4);

            input.L1 = Input.GetKey(P2L1);
            input.R1 = Input.GetKey(P2R1);

            input.option1 = Input.GetKey(P2Option1);
            input.option2 = Input.GetKey(P2Option2);

            return input;
        }
    }
}