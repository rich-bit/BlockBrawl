using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BlockBrawl
{
    class TextureManager
    {
        public static Texture2D emptyBlock, transBlock, menuArrowLeft, menuArrowRight,
            menuPlay, menuSettings, menuHighScore, menuBlockBrawl, qteColor, qteDotted,
            blueBlock1920, greenBlock1920, yellowBlock1920, purpleBlock1920, lightBlueBlock1920, orangeBlock1920, redBlock1920,
            bazookaShot, spriteSheetExplosion, spritesheetBlodSpatter1920x1080, spriteSheetExplosion1920x1080, spriteSheetShot;

        public TextureManager(ContentManager content)
        {
            menuArrowLeft = content.Load<Texture2D>(@"Menu.Images/menuArrowLeft");
            menuArrowRight = content.Load<Texture2D>(@"Menu.Images/menuArrowRight");

            emptyBlock = content.Load<Texture2D>(@"emptyblock");
            transBlock = content.Load<Texture2D>(@"transparentblock");
            menuPlay = content.Load<Texture2D>(@"Menu.Images/play1");
            menuSettings = content.Load<Texture2D>(@"Menu.Images/settings1");
            menuHighScore = content.Load<Texture2D>(@"Menu.Images/highscore1");
            menuBlockBrawl = content.Load<Texture2D>(@"Menu.Images/blockbrawl1");
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
