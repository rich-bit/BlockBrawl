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

            //Settings, where to put?
            graphicsDeviceManager.PreferredBackBufferWidth = 1920;
            graphicsDeviceManager.PreferredBackBufferHeight = 1080;
            graphicsDeviceManager.ApplyChanges();

            spriteBatch = new SpriteBatch(graphicsDevice);

            //Management
            play = new Play(20,graphicsDeviceManager.PreferredBackBufferHeight / TextureManager.transBlock.Height - 1, new Vector2(TextureManager.transBlock.Width, TextureManager.transBlock.Height), graphicsDeviceManager.PreferredBackBufferWidth);
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
