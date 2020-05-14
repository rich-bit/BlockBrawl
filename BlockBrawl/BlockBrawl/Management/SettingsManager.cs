using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockBrawl
{
    class SettingsManager
    {
        public static Vector2 tileSize, shotSpeed;
        public static float speed, fallTime, newSpeedCounter, qteWaitTime, spawnBlockBazooka;
        public static Texture2D playerOneColor, playerTwoColor;
        public static string playerOneName, playerTwoName;
        public static int playerIndexOne, playerIndexTwo;
        public static int gameWidth, gameHeight;
        public static bool gamePadVersion;
        public static Point tiles, arrowsInMenuMaxX;
        public SettingsManager(GraphicsDeviceManager graphicsDeviceManager)
        {
            gameWidth = PreConfigurations.gameWidth;
            gameHeight = PreConfigurations.gameHeight;
            graphicsDeviceManager.PreferredBackBufferWidth = gameWidth;
            graphicsDeviceManager.PreferredBackBufferHeight = gameHeight;

            //graphicsDeviceManager.HardwareModeSwitch = false;
            graphicsDeviceManager.IsFullScreen = PreConfigurations.fullScreen;
            graphicsDeviceManager.ApplyChanges();

            gamePadVersion = PreConfigurations.gamePadVersion;

            speed = tileSize.X;
            fallTime = 1.3f;
            newSpeedCounter = 15f;

            playerOneColor = TextureManager.blueBlock1920;
            playerTwoColor = TextureManager.greenBlock1920;

            qteWaitTime = 40f;

            shotSpeed = new Vector2(10, 10);
            spawnBlockBazooka = 5f;

            playerIndexOne = 0;
            playerIndexTwo = 1;
            tileSize = new Vector2(60, 60);//Note
            tiles = new Point(20, gameHeight / 60);

            arrowsInMenuMaxX = new Point(10, 0); // Set y to 0 please.

            playerOneName = "Player One";
            playerTwoName = "Player Two";
        }
    }
}
