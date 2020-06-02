using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BlockBrawl.Gamehandler.Settings
{
    class ViewControls
    {
        GameObject keyboard, gamepad;
        public bool GoToSettings { get; set; }
        int playerOneIndex, playerTwoIndex;
        string controlMapPlayerOne;
        string controlMapPlayerTwo;
        public ViewControls()
        {
            if (!SettingsManager.gamePadVersion)
            {
                keyboard = new GameObject(Vector2.Zero, TextureManager.keyboard);
                keyboard.Pos = PicPos(keyboard.Tex);
                controlMapPlayerOne = "" +
                    "Player one:\n" +
                    "W - Use Power-Up\n" +
                    "A - Move Left\n" +
                    "D - Move Right\n" +
                    "S - Move Down\n" +
                    "LeftShift - Rotate Counter Clockwise\n" +
                    "F - Rotate Clockwise\n" +
                    "Esc - Pause\n";
                controlMapPlayerTwo = "" +
                    "Player Two\n" +
                    "Up - Use Power-Up\n" +
                    "Left - Move Left\n" +
                    "Right - Move Right\n" +
                    "Down - Move Down\n" +
                    "LeftCTRL - Rotate Counter Clockwise\n" +
                    "NUM0 - Rotate Clockwise\n" +
                    "Esc - Pause\n";                
            }
            else
            {
                gamepad = new GameObject(Vector2.Zero, TextureManager.gamepad);
                gamepad.Pos = PicPos(gamepad.Tex);
                controlMapPlayerOne = "" +
                "Player one:\n" +
                "Select - Use Power-Up\n" +
                "Dpad Left - Move Left\n" +
                "Dpad Right - Move Right\n" +
                "Dpad Down - Move Down\n" +
                "A - Rotate Clockwise\n" +
                "B - Rotate Counter Clockwise\n" +
                "Start - Pause\n";
                controlMapPlayerTwo = "" +
                    "Player two:\n" +
                "Select - Use Power-Up\n" +
                "Dpad Left - Move Left\n" +
                "Dpad Right - Move Right\n" +
                "Dpad Down - Move Down\n" +
                "A - Rotate Clockwise\n" +
                "B - Rotate Counter Clockwise\n" +
                "Start - Pause\n";
            }
        }
        private Vector2 PicPos(Texture2D tex)
        {
            float x = SettingsManager.gameWidth / 2 - tex.Width / 2;
            float y = 0;
            return new Vector2(x, y);
        }
        public void Update(InputManager iM, int playerOneIndex, int playerTwoIndex)
        {
            this.playerOneIndex = playerOneIndex;
            this.playerTwoIndex = playerTwoIndex;

            if (iM.JustPressed(Buttons.Back, playerOneIndex) || iM.JustPressed(Buttons.Back, playerTwoIndex)
            || iM.JustPressed(Keys.Escape))
            {
                GoToSettings = true;
                SoundManager.menuChoice.Play();
            }
        }
        private Vector2 TextPosition(float startY, string text, SpriteFont font, int playerIndex)
        {
            float x = SettingsManager.gameWidth / 2 - font.MeasureString(text).X / 2;
            if(playerIndex == playerOneIndex)
            {
                x -= SettingsManager.gameWidth / 4;
            }
            else
            {
                x += SettingsManager.gameWidth / 4;
            }
            float y = startY;
            return new Vector2(x, y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!SettingsManager.gamePadVersion)
            {
                keyboard.Draw(spriteBatch);
                spriteBatch.DrawString(FontManager.GeneralText, controlMapPlayerOne,
                TextPosition(keyboard.Tex.Height, controlMapPlayerOne, FontManager.GeneralText, playerOneIndex), Color.LightPink);
                spriteBatch.DrawString(FontManager.GeneralText, controlMapPlayerTwo,
                    TextPosition(keyboard.Tex.Height, controlMapPlayerTwo, FontManager.GeneralText, playerTwoIndex), Color.LightPink);
            }
            else
            {
                gamepad.Draw(spriteBatch);
                spriteBatch.DrawString(FontManager.GeneralText, controlMapPlayerOne,
                TextPosition(gamepad.Tex.Height, controlMapPlayerOne, FontManager.GeneralText, playerOneIndex), Color.LightPink);
                spriteBatch.DrawString(FontManager.GeneralText, controlMapPlayerTwo,
                    TextPosition(gamepad.Tex.Height, controlMapPlayerTwo, FontManager.GeneralText, playerTwoIndex), Color.LightPink);
            }            
        }
    }
}
