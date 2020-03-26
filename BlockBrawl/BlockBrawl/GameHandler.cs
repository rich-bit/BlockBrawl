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
            //Settings, where to put?
            graphicsDeviceManager.PreferredBackBufferWidth = 1920;
            graphicsDeviceManager.PreferredBackBufferHeight = 1080;
            graphicsDeviceManager.ApplyChanges();

            //Construct stuff;                        
            this.graphicsDevice = graphicsDevice;

            new FontManager(contentManager);
            new TextureManager(contentManager);
            new SettingsManager(graphicsDeviceManager);
                        
            spriteBatch = new SpriteBatch(graphicsDevice);

            //Management
            play = new Play(SettingsManager.tiles, new Vector2(TextureManager.transBlock.Width, TextureManager.transBlock.Height), graphicsDeviceManager.PreferredBackBufferWidth, SettingsManager.playerIndexOne, SettingsManager.playerIndexTwo);
            //Removing the hardcoding at some point soon with a settings class.
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
