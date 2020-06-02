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
            GeneralText = content.Load<SpriteFont>(@"menuText");
            NewRoundText = content.Load<SpriteFont>(@"newround");
            NeonText = content.Load<SpriteFont>(@"neonText");
        }

        public static SpriteFont GameText { get; set; }
        public static SpriteFont ScoreText { get; set; }
        public static SpriteFont GeneralText { get; set; }
        public static SpriteFont NewRoundText { get; set; }
        public static SpriteFont NeonText { get; set; }
    }
}
