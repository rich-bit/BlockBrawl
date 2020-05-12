using BlockBrawl.GameHandlerObjects;
using BlockBrawl.Objects;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BlockBrawl
{
    class GameHandler
    {
        SpriteBatch spriteBatch;
        GraphicsDevice graphicsDevice;
        //GameTime gameTime;
        InputManager iM;
        SettingsManager settings;
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
        HighScore highScore;
        public GameHandler(GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            //Construct stuff;                      
            this.graphicsDevice = graphicsDevice;

            new FontManager(contentManager);
            new SoundManager(contentManager);
            new TextureManager(contentManager);
            settings = new SettingsManager(graphicsDeviceManager);
            spriteBatch = new SpriteBatch(graphicsDevice);
            iM = new InputManager(SettingsManager.playerIndexOne, SettingsManager.playerIndexTwo);

            play = new Play(SettingsManager.gamePadVersion,
                SettingsManager.tiles, SettingsManager.tileSize,
                SettingsManager.gameWidth, SettingsManager.gameHeight,
                SettingsManager.playerIndexOne, SettingsManager.playerIndexTwo,
                SettingsManager.playerOneColor, SettingsManager.playerTwoColor);
            menu = new Menu();
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
                    play.Update(gameTime, iM, spriteBatch);
                    break;
                case GameState.settings:
                    break;
                case GameState.highscore:
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
                if(menu.menuChoiceSwitch == Menu.MenuChoice.play)
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
