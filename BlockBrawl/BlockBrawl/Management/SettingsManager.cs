using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
        public static Point tiles;
        public SettingsManager(GraphicsDeviceManager graphicsDeviceManager, int setGameWidth, int setGameHeight, bool fulllscreen)
        {
            gameWidth = setGameWidth;
            gameHeight = setGameHeight;
            graphicsDeviceManager.PreferredBackBufferWidth = gameWidth;
            graphicsDeviceManager.PreferredBackBufferHeight = gameHeight;

            graphicsDeviceManager.HardwareModeSwitch = false;
            graphicsDeviceManager.IsFullScreen = fulllscreen;
            graphicsDeviceManager.ApplyChanges();

            speed = tileSize.X;
            fallTime = 1.3f;
            newSpeedCounter = 15f;

            playerOneColor = TextureManager.whiteBlock;
            playerTwoColor = TextureManager.purpleBlock;

            qteWaitTime = 10f;

            shotSpeed = new Vector2(10, 10);
            spawnBlockBazooka = 5f;
            gamePadVersion = false;

            playerIndexOne = 0;
            playerIndexTwo = 1;
            tileSize = new Vector2(60, 60);//Note
            tiles = new Point(20, gameHeight / 60);

            playerOneName = "Player One";
            playerTwoName = "Player Two";
        }
    }
}
