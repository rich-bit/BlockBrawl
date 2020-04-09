using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BlockBrawl
{
    class FontManager
    {
        public FontManager(ContentManager content)
        {
            GameText = content.Load<SpriteFont>(@"gameText");
            ScoreText = content.Load<SpriteFont>(@"scoreText");
            MenuText = content.Load<SpriteFont>(@"menuText");
            NewRoundText = content.Load<SpriteFont>(@"newround");
        }

        public static SpriteFont GameText { get; set; }
        public static SpriteFont ScoreText { get; set; }
        public static SpriteFont MenuText { get; set; }
        public static SpriteFont NewRoundText { get; set; }
    }
}
