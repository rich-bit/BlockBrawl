using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BlockBrawl
{
    class GameHandler
    {
        SpriteBatch spriteBatch;
        GraphicsDevice graphicsDevice;
        GameTime gameTime;

        //Management
        Play play;

        public GameHandler(GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            //Construct stuff;                        
            this.graphicsDevice = graphicsDevice;

            new FontManager(contentManager);
            new TextureManager(contentManager);
            new SettingsManager(graphicsDeviceManager);
                        
            spriteBatch = new SpriteBatch(graphicsDevice);

            //Management
            play = new Play(SettingsManager.tiles, SettingsManager.tileSize, graphicsDeviceManager.PreferredBackBufferWidth, 
                SettingsManager.playerIndexOne, SettingsManager.playerIndexTwo, 
                SettingsManager.playerOneColor, SettingsManager.playerTwoColor);
        }
        public void Draw()
        {
            graphicsDevice.Clear(Color.Silver);

            spriteBatch.Begin();
            play.Draw(spriteBatch);
            spriteBatch.End();
        }
        public void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
            play.Update(gameTime);
        }
    }
}
