using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BlockBrawl
{
    class TextureManager
    {
        public static Texture2D blueBlock, lightblueBlock, lightgreenBlock, orangeBlock, purpleBlock, whiteBlock, yellowBlock, emptyBlock, transBlock;

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
        }
    }
}
