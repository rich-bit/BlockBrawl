using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BlockBrawl.Gamehandler.Play
{
    class Pause
    {
        public bool GoToMenu { get; set; }
        GameObject pause;
        public Pause()
        {
            pause = new GameObject(Vector2.Zero, TextureManager.pause);
            pause.Pos = AssignPos(pause);
        }
        private Vector2 AssignPos(GameObject gameObject)
        {
            float x = SettingsManager.gameWidth / 2 - gameObject.Tex.Width / 2;
            float y = SettingsManager.gameHeight / 2 - gameObject.Tex.Height / 2;
            return new Vector2(x, y);
        }
        public void Update(InputManager iM, int playerOneIndex, int playerTwoIndex)
        {
            if (iM.JustPressed(Buttons.Back, playerOneIndex) || iM.JustPressed(Buttons.Back, playerTwoIndex)
            || iM.JustPressed(Keys.Escape))
            {
                GoToMenu = true;
                SoundManager.menuChoice.Play();
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            pause.Draw(spriteBatch);
            spriteBatch.DrawString(FontManager.GeneralText, "To go menu? Use ESC / Select (Game will abort)", new Vector2(0,
        SettingsManager.gameHeight - FontManager.GeneralText.MeasureString("To go menu? Use ESC / Select (Game will abort)").Y), Color.White);
            spriteBatch.DrawString(FontManager.GeneralText, "Hit start / enter to continue", new Vector2(pause.PosX + pause.Tex.Width / 2 -
                FontManager.GeneralText.MeasureString("Hit start / enter to continue").X / 2
                , pause.PosY + pause.Tex.Height), Color.White);
        }
    }
}
