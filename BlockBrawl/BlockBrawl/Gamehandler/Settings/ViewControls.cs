using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BlockBrawl.Gamehandler.Settings
{
    class ViewControls
    {
        GameObject keyboard, gamepad;
        public bool GoToSettings { get; set; }
        int playerOneIndex, playerTwoIndex;
        string controlMapPlayerOne;
        string controlMapPlayerTwo;
        Buttons p1Select, p2Select;
        public ViewControls()
        {
            p1Select = SettingsManager.p1PowerUp;
            p2Select = SettingsManager.p2PowerUp;

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
                    "W - Move Up\n" +
                    "LeftShift - Rotate Counter Clockwise\n" +
                    "F - Rotate Clockwise\n" +
                    "Esc - Pause\n";
                controlMapPlayerTwo = "" +
                    "Player Two:\n" +
                    "Up - Use Power-Up\n" +
                    "Left - Move Left\n" +
                    "Right - Move Right\n" +
                    "Down - Move Down\n" +
                    "Up - Move Up\n" +
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
                $"{SettingsManager.p1PowerUp.ToString()} - Use Power-Up\n" +
                $"{SettingsManager.p1MoveLeft.ToString()} - Move Left\n" +
                $"{SettingsManager.p1MoveRight.ToString()} - Move Right\n" +
                $"{SettingsManager.p1MoveDown.ToString()} - Move Down\n" +
                $"{SettingsManager.p1MoveUp.ToString()} - Move Up\n" +
                $"{SettingsManager.p1RotateCW.ToString()} - Rotate Clockwise\n" +
                $"{SettingsManager.p1RotateCC.ToString()} - Rotate Counter Clockwise\n" +
                $"{SettingsManager.p1Start.ToString()} - Pause\n";
                controlMapPlayerTwo = "" +
                    "Player two:\n" +
                $"{SettingsManager.p2PowerUp.ToString()} - Use Power-Up\n" +
                $"{SettingsManager.p2MoveLeft.ToString()} - Move Left\n" +
                $"{SettingsManager.p2MoveRight.ToString()} - Move Right\n" +
                $"{SettingsManager.p2MoveDown.ToString()} - Move Down\n" +
                $"{SettingsManager.p2MoveUp.ToString()} - Move Up\n" +
                $"{SettingsManager.p2RotateCW.ToString()} - Rotate Clockwise\n" +
                $"{SettingsManager.p2RotateCC.ToString()} - Rotate Counter Clockwise\n" +
                $"{SettingsManager.p2Start.ToString()} - Pause\n";
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

            if (iM.JustPressed(p1Select, playerOneIndex) || iM.JustPressed(p2Select, playerTwoIndex)
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
