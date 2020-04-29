using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace BlockBrawl
{
    class SettingsManager
    {
        //GraphicsDeviceManager graphicsDeviceManager;
        public static Vector2 tileSize;
        public static float speed, fallTime, newSpeedCounter;
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

            gamePadVersion = true;

            playerIndexOne = 0;
            playerIndexTwo = 1;
            tileSize = new Vector2(60, 60);//Note
            tiles = new Point(20, gameHeight / 60);

            playerOneName = "Player One";
            playerTwoName = "Player Two";
        }
    }
}
