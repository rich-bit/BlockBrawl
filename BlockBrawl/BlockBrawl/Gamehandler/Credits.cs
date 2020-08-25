using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BlockBrawl
{
    class Credits
    {
        List<string> credits;
        GameObject creditsTex;
        Buttons p1Select, p2Select;
        float speedUp = 1.5f;
        public bool GoToMenu { get; set; }
        public Credits()
        {
            creditsTex = new GameObject(
                new Vector2(
                    SettingsManager.gameWidth / 2 - TextureManager.menuCredits.Width / 2,
                SettingsManager.gameHeight),
                TextureManager.menuCredits);
            credits = new List<string>();
            //creditsTex.PosY = SettingsManager.gameHeight - 500;

            p1Select = SettingsManager.p1PowerUp;

            p2Select = SettingsManager.p2PowerUp;

            AddCredit("'Bloodsplat Animations' art by PWN, at opengameart.org/\nLicensed CC BY-SA 3.0\nopengameart.org/content/blood-splat-animations\n");
            AddCredit("'Explosion' art by Cuzco, at opengameart.org/\nLicensed CC0 1.0 Universal\nopengameart.org/content/explosion\n");
            AddCredit("'Big Explosion' sound by PWN, at opengameart.org/\nLicensed CC BY 3.0\nopengameart.org/content/big-explosion\n");
            AddCredit("'Collaboration / Sound Effects Shooting sounds 002' sound effects by jalastram, at opengameart.org/\nLicensed CC BY 3.0\nopengameart.org/content/collaboration-sound-effects-shooting-sounds-002\n");
            AddCredit("'Level up, power up, Coin get (13 Sounds)' sound effects by wobbleboxx, at opengameart.org/\nLicensed CC0 1.0\nopengameart.org/content/level-up-power-up-coin-get-13-sounds\n");
            AddCredit("'RetroKiller' font by Woodcutter\nPersonal permission to use his art in this game.\nThank you!");
        }
        public void Reset()
        {
            creditsTex = new GameObject(
        new Vector2(
        SettingsManager.gameWidth / 2 - TextureManager.menuCredits.Width / 2,
        SettingsManager.gameHeight),
        TextureManager.menuCredits);
        }
        public void Update(InputManager iM)
        {
            if (!iM.IsHeld(Keys.Space))
            {
                creditsTex.PosY -= speedUp;
            }
            if (iM.JustPressed(p1Select, SettingsManager.playerIndexOne) || iM.JustPressed(p2Select, SettingsManager.playerIndexTwo)
            || iM.JustPressed(Keys.Escape))
            {
                GoToMenu = true;
                Reset();
                SoundManager.menuChoice.Play();
            }
        }
        public void AddCredit(string text)
        {
            credits.Add(text + "\n");
        }
        public Vector2 RollUp(SpriteFont font, string text, int row)
        {
            int startMargin = TextureManager.menuCredits.Height + 50;
            int rowMargin = 150;
            return new Vector2(
                SettingsManager.gameWidth / 2 - font.MeasureString(text).X / 2,
                creditsTex.PosY + startMargin + row * rowMargin);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < credits.Count; i++)
            {
                spriteBatch.DrawString(FontManager.ScoreText, credits[i],
                    RollUp(FontManager.ScoreText, credits[i], i),
                    Color.White);
            }
            spriteBatch.DrawString(FontManager.GeneralText, "To go menu? Use ESC / Select", new Vector2(0,
            SettingsManager.gameHeight - FontManager.GeneralText.MeasureString("To go menu? Use ESC / Select").Y), Color.Yellow);
            spriteBatch.DrawString(FontManager.GeneralText, "Pause? Hold SPACE", new Vector2(0,
SettingsManager.gameHeight - FontManager.GeneralText.MeasureString("Pause? Hold SPACE").Y - FontManager.GeneralText.MeasureString("To go menu? Use ESC / Select").Y), 
Color.IndianRed);
            creditsTex.Draw(spriteBatch);
        }
    }
}
