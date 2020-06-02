using Microsoft.Xna.Framework.Input;


namespace BlockBrawl
{
    class InputManager//We are using NES replica controllers with USB for this project.
    {//The slightly wierd monogame controlMapping is as follows: Buttons.back = NES select, Buttons.Y = NES B, Buttons.B = NES A, the rest is correct.
        GamePadState playerOneGstate, playerOneOldGstate, playerTwoGstate, playerTwoOldGstate;
        KeyboardState keyboardState, oldKeyboardState;

        int playerOneIndex, playerTwoIndex;

        public Buttons PressedButton { get; set; }

        public InputManager(int playerOneIndex, int playerTwoIndex)
        {
            this.playerOneIndex = playerOneIndex;
            this.playerTwoIndex = playerTwoIndex;
        }
        public void Update()
        {
            playerOneOldGstate = playerOneGstate;
            playerOneGstate = GamePad.GetState(playerOneIndex);
            playerTwoOldGstate = playerTwoGstate;
            playerTwoGstate = GamePad.GetState(playerTwoIndex);
            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
        }
        public bool JustPressed(Buttons button, int player)
        {
            if (player == playerOneIndex) { return playerOneGstate.IsButtonDown(button) && !playerOneOldGstate.IsButtonDown(button); }
            if (player == playerTwoIndex) { return playerTwoGstate.IsButtonDown(button) && !playerTwoOldGstate.IsButtonDown(button); }
            return false;
        }
        public bool JustPressed(Keys keys)
        {
            return keyboardState.IsKeyDown(keys) && !oldKeyboardState.IsKeyDown(keys);
        }
        public bool IsHeld(Buttons button, int player)
        {
            if (player == playerOneIndex) { return playerOneGstate.IsButtonDown(button) && playerOneOldGstate.IsButtonDown(button); }
            if (player == playerTwoIndex) { return playerTwoGstate.IsButtonDown(button) && playerTwoOldGstate.IsButtonDown(button); }
            return false;
        }
        public bool IsHeld(Keys keys)
        {
            return keyboardState.IsKeyDown(keys) && oldKeyboardState.IsKeyDown(keys);
        }
        public string CapitalLetterTyped()
        {
            if (JustPressed(Keys.A)) { return "A"; }
            if (JustPressed(Keys.B)) { return "B"; }
            if (JustPressed(Keys.C)) { return "C"; }
            if (JustPressed(Keys.D)) { return "D"; }
            if (JustPressed(Keys.E)) { return "E"; }
            if (JustPressed(Keys.F)) { return "F"; }
            if (JustPressed(Keys.G)) { return "G"; }
            if (JustPressed(Keys.H)) { return "H"; }
            if (JustPressed(Keys.I)) { return "I"; }
            if (JustPressed(Keys.J)) { return "J"; }
            if (JustPressed(Keys.K)) { return "K"; }
            if (JustPressed(Keys.L)) { return "L"; }
            if (JustPressed(Keys.M)) { return "M"; }
            if (JustPressed(Keys.N)) { return "N"; }
            if (JustPressed(Keys.O)) { return "O"; }
            if (JustPressed(Keys.P)) { return "P"; }
            if (JustPressed(Keys.Q)) { return "Q"; }
            if (JustPressed(Keys.R)) { return "R"; }
            if (JustPressed(Keys.S)) { return "S"; }
            if (JustPressed(Keys.T)) { return "T"; }
            if (JustPressed(Keys.U)) { return "U"; }
            if (JustPressed(Keys.V)) { return "V"; }
            if (JustPressed(Keys.W)) { return "W"; }
            if (JustPressed(Keys.X)) { return "X"; }
            if (JustPressed(Keys.Y)) { return "Y"; }
            if (JustPressed(Keys.Z)) { return "Z"; }
            return null;
        }
        public bool SavedButtonPress(int playerIndex)
        {
            if (JustPressed(Buttons.Back, playerIndex)) { PressedButton = Buttons.Back; return true; }
            if (JustPressed(Buttons.A, playerIndex)) { PressedButton = Buttons.A; return true; }
            if (JustPressed(Buttons.B, playerIndex)) { PressedButton = Buttons.B; return true; }
            if (JustPressed(Buttons.BigButton, playerIndex)) { PressedButton = Buttons.BigButton; return true; }
            if (JustPressed(Buttons.DPadDown, playerIndex)) { PressedButton = Buttons.DPadDown; return true; }
            if (JustPressed(Buttons.DPadLeft, playerIndex)) { PressedButton = Buttons.DPadLeft; return true; }
            if (JustPressed(Buttons.DPadRight, playerIndex)) { PressedButton = Buttons.DPadRight; return true; }
            if (JustPressed(Buttons.DPadUp, playerIndex)) { PressedButton = Buttons.DPadUp; return true; }
            if (JustPressed(Buttons.LeftShoulder, playerIndex)) { PressedButton = Buttons.LeftShoulder; return true; }
            if (JustPressed(Buttons.LeftStick, playerIndex)) { PressedButton = Buttons.LeftStick; return true; }
            if (JustPressed(Buttons.LeftThumbstickDown, playerIndex)) { PressedButton = Buttons.LeftThumbstickDown; return true; }
            if (JustPressed(Buttons.LeftThumbstickLeft, playerIndex)) { PressedButton = Buttons.LeftThumbstickLeft; return true; }
            if (JustPressed(Buttons.LeftThumbstickRight, playerIndex)) { PressedButton = Buttons.LeftThumbstickRight; return true; }
            if (JustPressed(Buttons.LeftThumbstickUp, playerIndex)) { PressedButton = Buttons.LeftThumbstickUp; return true; }
            if (JustPressed(Buttons.LeftTrigger, playerIndex)) { PressedButton = Buttons.LeftTrigger; return true; }
            if (JustPressed(Buttons.RightShoulder, playerIndex)) { PressedButton = Buttons.RightShoulder; return true; }
            if (JustPressed(Buttons.RightStick, playerIndex)) { PressedButton = Buttons.RightStick; return true; }
            if (JustPressed(Buttons.RightThumbstickDown, playerIndex)) { PressedButton = Buttons.RightThumbstickDown; return true; }
            if (JustPressed(Buttons.RightThumbstickLeft, playerIndex)) { PressedButton = Buttons.RightThumbstickLeft; return true; }
            if (JustPressed(Buttons.RightThumbstickRight, playerIndex)) { PressedButton = Buttons.RightThumbstickRight; return true; }
            if (JustPressed(Buttons.RightThumbstickUp, playerIndex)) { PressedButton = Buttons.RightThumbstickUp; return true; }
            if (JustPressed(Buttons.RightTrigger, playerIndex)) { PressedButton = Buttons.RightTrigger; return true; }
            if (JustPressed(Buttons.Start, playerIndex)) { PressedButton = Buttons.Start; return true; }
            if (JustPressed(Buttons.X, playerIndex)) { PressedButton = Buttons.X; return true; }
            if (JustPressed(Buttons.Y, playerIndex)) { PressedButton = Buttons.Y; return true; }
            return false;
        }
    }
}
