using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace BlockBrawl
{
    class InputManager//We are using NES replica controllers with USB for this project.
    {//The slightly wierd monogame controlMapping is as follows: Buttons.back = NES select, Buttons.Y = NES B, Buttons.B = NES A, the rest is correct.
        GamePadState playerOneGstate, playerOneOldGstate, playerTwoGstate, playerTwoOldGstate;
        KeyboardState keyboardState, oldKeyboardState;

        int playerOneIndex, playerTwoIndex;
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
    }
}
