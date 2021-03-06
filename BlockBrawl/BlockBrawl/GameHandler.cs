﻿using BlockBrawl.Gamehandler;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BlockBrawl
{
    class GameHandler
    {
        SpriteBatch spriteBatch;
        GraphicsDevice graphicsDevice;
        InputManager iM;
        SettingsManager settingsManager;
        public enum GameState
        {
            play,
            settings,
            highscore,
            credits,
            controlls,
            menu,
        }
        GameState currentGameState;
        //Management
        PrePlayScreen prePlayScreen;
        Play play;
        Settings settings;
        Menu menu;
        Credits credits;
        HighScore highScore;
        public GameHandler(GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            //Construct stuff;                      
            this.graphicsDevice = graphicsDevice;

            iM = new InputManager(0, 1);

            new FontManager(contentManager);
            new SoundManager(contentManager);
            new TextureManager(contentManager);
            settingsManager = new SettingsManager(graphicsDeviceManager, iM);
            spriteBatch = new SpriteBatch(graphicsDevice);

            prePlayScreen = new PrePlayScreen(SettingsManager.playerIndexOne, SettingsManager.playerIndexTwo);
            menu = new Menu();
            settings = new Settings(iM, SettingsManager.playerIndexOne, SettingsManager.playerIndexTwo);
            highScore = new HighScore();
            credits = new Credits();
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
                        if (prePlayScreen.GoToMenu) { currentGameState = GameState.menu; prePlayScreen.GoToMenu = false; }
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
                    if (play != null && play.Pause != null && play.Pause.GoToMenu)
                    {
                        play = null;
                        currentGameState = GameState.menu;
                    }
                    break;
                case GameState.settings:
                    settings.Update(iM, SettingsManager.playerIndexOne, SettingsManager.playerIndexTwo, gameTime, settingsManager);
                    if (settings.GoToMenu) { currentGameState = GameState.menu; settings.GoToMenu = false; }
                    break;
                case GameState.highscore:
                    highScore.Update(iM, SettingsManager.playerIndexOne, SettingsManager.playerIndexTwo);
                    if (highScore.GoToMenu) { currentGameState = GameState.menu; highScore.GoToMenu = false; }
                    break;
                case GameState.credits:
                    credits.Update(iM);
                    if (credits.GoToMenu) { currentGameState = GameState.menu; credits.GoToMenu = false; }
                    break;
                case GameState.controlls:
                    break;
                case GameState.menu:
                    menu.Update(iM, SettingsManager.playerIndexOne, SettingsManager.playerIndexTwo, gameTime);
                    break;
            }
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
                else if (menu.menuChoiceSwitch == Menu.MenuChoice.credits)
                {
                    currentGameState = GameState.credits;
                    menu.EnterChoice = false;
                }
                else if (menu.menuChoiceSwitch == Menu.MenuChoice.quit)
                {
                    Program.Game.Exit();
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
                case GameState.credits:
                    credits.Draw(spriteBatch);
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
