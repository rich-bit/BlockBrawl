using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using BlockBrawl.Gamehandler;

namespace BlockBrawl
{
    class GameHandler
    {
        SpriteBatch spriteBatch;
        GraphicsDevice graphicsDevice;
        //GameTime gameTime;
        InputManager iM;
        SettingsManager settingsManager;
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
        PrePlayScreen prePlayScreen;
        Play play;
        Settings settings;
        Menu menu;
        HighScore highScore;
        public GameHandler(GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            //Construct stuff;                      
            this.graphicsDevice = graphicsDevice;

            new FontManager(contentManager);
            new SoundManager(contentManager);
            new TextureManager(contentManager);
            settingsManager = new SettingsManager(graphicsDeviceManager);
            spriteBatch = new SpriteBatch(graphicsDevice);
            iM = new InputManager(SettingsManager.playerIndexOne, SettingsManager.playerIndexTwo);

            prePlayScreen = new PrePlayScreen(SettingsManager.playerIndexOne, SettingsManager.playerIndexTwo);
            menu = new Menu();
            settings = new Settings();
            highScore = new HighScore();
            currentGameState = GameState.menu;
        }
        public void Update(GameTime gameTime)
        {
            iM.Update();
            MenuSwitcher();
            switch (currentGameState)
            {
                case GameState.play:
                    if (play == null)
                    {
                        prePlayScreen.Update(iM, SettingsManager.gamePadVersion);
                    }
                    if (prePlayScreen.ReadyEnterPlay)
                    {
                        play = prePlayScreen.Play;
                        prePlayScreen.ReadyEnterPlay = false;
                        SoundManager.menuChoice.Play();
                    }
                    if (play != null)
                    {
                        play.Update(gameTime, iM);
                        if (play.GoToMenu) { play = null; currentGameState = GameState.menu; }
                    }
                    break;
                case GameState.settings:
                    settings.Update(iM, SettingsManager.playerIndexOne, SettingsManager.playerIndexTwo, gameTime);
                    break;
                case GameState.highscore:
                    highScore.Update(iM, SettingsManager.playerIndexOne, SettingsManager.playerIndexTwo);
                    if (highScore.GoToMenu) { currentGameState = GameState.menu; highScore.GoToMenu = false; }
                    break;
                case GameState.controlls:
                    break;
                case GameState.menu:
                    menu.Update(iM, SettingsManager.playerIndexOne, SettingsManager.playerIndexTwo, gameTime);
                    break;
            }
            // this.gameTime = gameTime;
        }
        public void MenuSwitcher()
        {
            if (menu.EnterChoice)
            {
                if (menu.menuChoiceSwitch == Menu.MenuChoice.play)
                {
                    currentGameState = GameState.play;
                    menu.EnterChoice = false;
                }
                else if (menu.menuChoiceSwitch == Menu.MenuChoice.settings)
                {
                    currentGameState = GameState.settings;
                    menu.EnterChoice = false;
                }
                else if (menu.menuChoiceSwitch == Menu.MenuChoice.highscore)
                {
                    currentGameState = GameState.highscore;
                    menu.EnterChoice = false;
                }
            }
        }
        public void Draw()
        {
            graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            switch (currentGameState)
            {
                case GameState.play:
                    if (play == null)
                    {
                        prePlayScreen.Draw(spriteBatch);
                    }
                    if (play != null)
                    {
                        play.Draw(spriteBatch);
                    }
                    break;
                case GameState.settings:
                    settings.Draw(spriteBatch);
                    break;
                case GameState.highscore:
                    highScore.Draw(spriteBatch);
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
