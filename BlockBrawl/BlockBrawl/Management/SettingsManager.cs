using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BlockBrawl
{
    class SettingsManager
    {
        GraphicsDeviceManager graphicsDeviceManager;
        InputManager iM;
        public static Point windowSize;
        public static Vector2 tileSize;
        public static float speed, fallTime, newSpeedCounter;
        public static Texture2D playerOneColor, playerTwoColor;
        public static string playerOneName, playerTwoName;
        public static int playerIndexOne, playerIndexTwo, gameWidth, gameHeight;
        public static bool gamePadVersion;
        public static Point tiles;
        public SettingsManager(GraphicsDeviceManager graphicsDeviceManager)
        {
            this.graphicsDeviceManager = graphicsDeviceManager;
            graphicsDeviceManager.PreferredBackBufferWidth = 1920;
            graphicsDeviceManager.PreferredBackBufferHeight = 1080;
            graphicsDeviceManager.IsFullScreen = true;
            graphicsDeviceManager.ApplyChanges();

            gameWidth = graphicsDeviceManager.PreferredBackBufferWidth;
            gameHeight = graphicsDeviceManager.PreferredBackBufferHeight;

            windowSize.X = graphicsDeviceManager.PreferredBackBufferWidth;
            windowSize.Y = graphicsDeviceManager.PreferredBackBufferHeight;

            speed = tileSize.X;
            fallTime = 1.3f;
            newSpeedCounter = 15f;

            playerOneColor = TextureManager.whiteBlock;
            playerTwoColor = TextureManager.purpleBlock;

            gamePadVersion = true;

            playerIndexOne = 0;
            playerIndexTwo = 1;
            iM = new InputManager(playerIndexOne, playerIndexTwo);
            tileSize = new Vector2(TextureManager.transBlock.Width, TextureManager.transBlock.Height);
            tiles = new Point(20, graphicsDeviceManager.PreferredBackBufferHeight / TextureManager.transBlock.Height);

            playerOneName = "Player One";
            playerTwoName = "Player Two";
        }
        public void Update()
        {
            iM.Update();
            ToggleFullscreen();
        }
        public void ToggleFullscreen()
        {
            if (iM.JustPressed(Buttons.Back, playerIndexOne) || iM.JustPressed(Buttons.Back, playerIndexTwo))
            {
                graphicsDeviceManager.ToggleFullScreen();
            }
            if (iM.JustPressed(Keys.F4))
            {
                graphicsDeviceManager.ToggleFullScreen();
            }
        }
    }
}
