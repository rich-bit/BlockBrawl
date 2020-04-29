using Microsoft.Xna.Framework;

namespace BlockBrawl
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        GameHandler gameHandler;

        bool fullscreen;
        int gameWidth, gameHeight;
        public Game1(bool fullscreen, int gameWidth, int gameHeight)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.fullscreen = fullscreen;
            this.gameHeight = gameHeight;
            this.gameWidth = gameWidth;
        }
        protected override void LoadContent()
        {
            gameHandler = new GameHandler(graphics, GraphicsDevice, Content, fullscreen, gameWidth, gameHeight);
        }
        protected override void Update(GameTime gameTime)
        {
            gameHandler.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            gameHandler.Draw();
            base.Draw(gameTime);
        }
    }
}
