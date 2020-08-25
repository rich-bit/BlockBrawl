using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BlockBrawl
{
    class TextureManager
    {
        public static Texture2D emptyBlock, transBlock, menuArrowLeft, menuArrowRight,
            menuPlay, menuSettings, menuCredits, menuHighScore, menuBlockBrawl, menuQuit, qteColor, qteDotted,
            settingsConfigureGamePad, settingsShowPreConfig, settingsViewControls, pause, keyboard, gamepad, playMusic,
            blueBlock1920, greenBlock1920, yellowBlock1920, purpleBlock1920, lightBlueBlock1920, orangeBlock1920, redBlock1920,
            bazookaShot, spriteSheetExplosion, spritesheetBlodSpatter1920x1080, spriteSheetExplosion1920x1080, spriteSheetShot;

        public TextureManager(ContentManager content)
        {
            menuArrowLeft = content.Load<Texture2D>(@"Menu.Images/menuArrowLeft");
            menuArrowRight = content.Load<Texture2D>(@"Menu.Images/menuArrowRight");

            settingsConfigureGamePad = content.Load<Texture2D>(@"Menu.Images/settings/configureGamepads");
            settingsShowPreConfig = content.Load<Texture2D>(@"Menu.Images/settings/resetPreConfig");
            settingsViewControls = content.Load<Texture2D>(@"Menu.Images/settings/viewControls");
            keyboard = content.Load<Texture2D>(@"Menu.Images/settings/keyboard");
            gamepad = content.Load<Texture2D>(@"Menu.Images/settings/gamepad");
            playMusic = content.Load<Texture2D>(@"playmusic");

            emptyBlock = content.Load<Texture2D>(@"emptyblock");
            transBlock = content.Load<Texture2D>(@"transparentblock");
            menuPlay = content.Load<Texture2D>(@"Menu.Images/play");
            menuQuit = content.Load<Texture2D>(@"Menu.Images/quit");
            menuCredits = content.Load<Texture2D>(@"Menu.Images/credits");
            menuSettings = content.Load<Texture2D>(@"Menu.Images/settings");
            menuHighScore = content.Load<Texture2D>(@"Menu.Images/highscore");
            menuBlockBrawl = content.Load<Texture2D>(@"Menu.Images/blockbrawl1");

            pause = content.Load<Texture2D>(@"Menu.Images/pause");

            qteColor = content.Load<Texture2D>(@"qteblock");
            qteDotted = content.Load<Texture2D>(@"qtedottedblock");
            bazookaShot = content.Load<Texture2D>(@"Bazooka/bazooka-shot");

            blueBlock1920 = content.Load<Texture2D>(@"1920x1080/blockblå");
            greenBlock1920 = content.Load<Texture2D>(@"1920x1080/blockgrön");
            yellowBlock1920 = content.Load<Texture2D>(@"1920x1080/blockgul");
            purpleBlock1920 = content.Load<Texture2D>(@"1920x1080/blocklila");
            lightBlueBlock1920 = content.Load<Texture2D>(@"1920x1080/blockljusblå");
            orangeBlock1920 = content.Load<Texture2D>(@"1920x1080/blockorange");
            redBlock1920 = content.Load<Texture2D>(@"1920x1080/blockröd");

            spriteSheetExplosion = content.Load<Texture2D>(@"spritesheetExp");
            spriteSheetExplosion1920x1080 = content.Load<Texture2D>(@"1920x1080/spritesheetExp-1920x1080");

            spritesheetBlodSpatter1920x1080 = content.Load<Texture2D>(@"1920x1080/spritesheetBloods1920x1080");

            spriteSheetShot = content.Load<Texture2D>(@"spritesheetShot");
        }
    }
}
