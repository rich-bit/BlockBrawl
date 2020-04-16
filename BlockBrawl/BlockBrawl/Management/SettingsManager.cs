using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockBrawl
{
    class SettingsManager
    {
        //public static bool fullScreen;
        public static Point windowSize;
        public static Vector2 tileSize;
        public static float speed;
        public static Texture2D playerOneColor, playerTwoColor;
        public static string playerOneName, playerTwoName;
        public static int playerIndexOne, playerIndexTwo;
        public static bool gamePadVersion;
        public static Point tiles;
        public SettingsManager(GraphicsDeviceManager graphicsDeviceManager)
        {
            graphicsDeviceManager.PreferredBackBufferWidth = 1920;
            graphicsDeviceManager.PreferredBackBufferHeight = 1080;
            graphicsDeviceManager.ApplyChanges();

            windowSize.X = graphicsDeviceManager.PreferredBackBufferWidth;
            windowSize.Y = graphicsDeviceManager.PreferredBackBufferHeight;

            speed = tileSize.X;

            playerOneColor = TextureManager.whiteBlock;
            playerTwoColor = TextureManager.purpleBlock;

            gamePadVersion = false;

            playerIndexOne = 0;
            playerIndexTwo = 1;
            tileSize = new Vector2(TextureManager.transBlock.Width, TextureManager.transBlock.Height);
            tiles = new Point(20, graphicsDeviceManager.PreferredBackBufferHeight / TextureManager.transBlock.Height - 1);

            playerOneName = "Player One";
            playerTwoName = "Player Two";
        }
    }
}
