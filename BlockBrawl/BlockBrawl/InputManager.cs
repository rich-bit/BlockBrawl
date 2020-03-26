using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace BlockBrawl
{
    class InputManager//We are using NES replica controllers with USB for this project.
    {//The slightly wierd monogame controlMapping is as follows: Buttons.back = NES select, Buttons.Y = NES B, Buttons.B = NES A, the rest is correct.
        GamePadState playerOneGstate, playerOneOldGstate, playerTwoGstate, playerTwoOldGstate;
        public InputManager() { }
        public void Update()
        {
            playerOneOldGstate = playerOneGstate;
            playerOneGstate = GamePad.GetState(PlayerIndex.One);
            playerTwoOldGstate = playerTwoGstate;
            playerTwoGstate = GamePad.GetState(PlayerIndex.Two);
        }
        public bool JustPressed(Buttons button, int player)
        {
            if (player == 1) { return playerOneGstate.IsButtonDown(button) && !playerOneOldGstate.IsButtonDown(button); }
            if (player == 2) { return playerTwoGstate.IsButtonDown(button) && !playerTwoOldGstate.IsButtonDown(button); }
            return false;
        }
        public bool IsHeld(Buttons button, int player)
        {
            if (player == 1) { return playerOneGstate.IsButtonDown(button) && playerOneOldGstate.IsButtonDown(button); }
            if (player == 2) { return playerTwoGstate.IsButtonDown(button) && playerTwoOldGstate.IsButtonDown(button); }
            return false;
        }
    }
}
