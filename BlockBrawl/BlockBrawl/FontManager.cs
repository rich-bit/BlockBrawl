using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BlockBrawl
{
    class FontManager
    {
        public static SpriteFont gameText, scoreText, menuText, newRoundText;

        public FontManager(ContentManager content)
        {
            gameText = content.Load<SpriteFont>(@"gameText");
            scoreText = content.Load<SpriteFont>(@"scoreText");
            menuText = content.Load<SpriteFont>(@"menuText");
            newRoundText = content.Load<SpriteFont>(@"newround");
        }
    }
}
