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

        public enum GameState
        {
            play,
            settings,
            highscore,
            controlls,
            menu,
        }
        GameState currentGameState;
        //Management
        Play play;
        Menu menu;
        public GameHandler(GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            //Construct stuff;                        
            this.graphicsDevice = graphicsDevice;

            new FontManager(contentManager);
            new TextureManager(contentManager);
            new SettingsManager(graphicsDeviceManager);

            spriteBatch = new SpriteBatch(graphicsDevice);

            //Management
            play = new Play(SettingsManager.gamePadVersion,
                SettingsManager.tiles, SettingsManager.tileSize,
                graphicsDeviceManager.PreferredBackBufferWidth,
                SettingsManager.playerIndexOne, SettingsManager.playerIndexTwo,
                SettingsManager.playerOneColor, SettingsManager.playerTwoColor);
            menu = new Menu();

            currentGameState = GameState.menu;
        }
        public void Update(GameTime gameTime)
        {
            switch (currentGameState)
            {
                case GameState.play:
                    play.Update(gameTime);
                    break;
                case GameState.settings:
                    break;
                case GameState.highscore:
                    break;
                case GameState.controlls:
                    break;
                case GameState.menu:
                    menu.Update();
                    break;
            }
            this.gameTime = gameTime;
        }
        public void Draw()
        {
            graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            switch (currentGameState)
            {
                case GameState.play:
                    play.Draw(spriteBatch);
                    break;
                case GameState.settings:
                    break;
                case GameState.highscore:
                    break;
                case GameState.controlls:
                    break;
                case GameState.menu:
                    menu.Draw(spriteBatch);
                    break;
            }
            spriteBatch.End();
        }
    }
}
