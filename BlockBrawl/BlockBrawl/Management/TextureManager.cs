using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BlockBrawl
{
    class TextureManager
    {
        public static Texture2D blueBlock, lightblueBlock, lightgreenBlock, orangeBlock, purpleBlock, whiteBlock, yellowBlock, emptyBlock, transBlock,
            menuPlay, menuSettings, menuHighScore, menuBlockBrawl;

        public TextureManager(ContentManager content)
        {
            blueBlock = content.Load<Texture2D>(@"blueblock");
            lightblueBlock = content.Load<Texture2D>(@"lightblue");
            lightgreenBlock = content.Load<Texture2D>(@"lightgreenblock");
            orangeBlock = content.Load<Texture2D>(@"orangeblock");
            purpleBlock = content.Load<Texture2D>(@"purpleblock");
            whiteBlock = content.Load<Texture2D>(@"whiteblock");
            yellowBlock = content.Load<Texture2D>(@"yellowblock");
            emptyBlock = content.Load<Texture2D>(@"emptyblock");
            transBlock = content.Load<Texture2D>(@"transparentblock");
            menuPlay = content.Load<Texture2D>(@"Menu.Images/play1");
            menuSettings = content.Load<Texture2D>(@"Menu.Images/settings1");
            menuHighScore = content.Load<Texture2D>(@"Menu.Images/highscore1");
            menuBlockBrawl = content.Load<Texture2D>(@"Menu.Images/blockbrawl1");
        }
    }
}
