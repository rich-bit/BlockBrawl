using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBrawl
{
    class SettingsManager
    {
        //public static bool fullScreen;
        public static Vector2 tileSize;
        //public static Point resolution;
        public static int playerIndexOne, playerIndexTwo;
        public static Point tiles;
        public SettingsManager(GraphicsDeviceManager graphicsDeviceManager)
        {
            playerIndexOne = 1;
            playerIndexTwo = 2;
            tileSize = new Vector2(TextureManager.transBlock.Width, TextureManager.transBlock.Height);
            tiles = new Point(20, graphicsDeviceManager.PreferredBackBufferHeight / TextureManager.transBlock.Height - 1);
        }
    }
}
