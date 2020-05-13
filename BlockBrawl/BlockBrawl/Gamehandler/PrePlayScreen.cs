using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BlockBrawl.Gamehandler
{
    class PrePlayScreen
    {
        public Play Play { get; set; }
        public bool ReadyEnterPlay { get; set; }
        bool playerOneReady, playerTwoReady;
        int playerOneIndex, playerTwoIndex, playerOneChoice, playerTwoChoice;
        string instructionsPlayer1, instructionsPlayer2;
        List<GameObject> colorsP1, colorsP2;

        public PrePlayScreen(int playerOneIndex, int playerTwoIndex)
        {
            this.playerOneIndex = playerOneIndex;
            this.playerTwoIndex = playerTwoIndex;

            colorsP1 = new List<GameObject>();
            SetColors(colorsP1, playerOneIndex);
            colorsP2 = new List<GameObject>();
            SetColors(colorsP2, playerTwoIndex);

            instructionsPlayer1 = "Player one please select color:";
            instructionsPlayer2 = "Player two please select color:";
        }
        private void SetColors(List<GameObject> playerColors, int playerIndex)
        {
            playerColors.Add(new GameObject(GetScreenAlignment(TextureManager.blueBlock1920.Width, SettingsManager.tileSize.X * 5, playerIndex), TextureManager.blueBlock1920));
            playerColors.Add(new GameObject(GetScreenAlignment(TextureManager.blueBlock1920.Width, SettingsManager.tileSize.X * 5, playerIndex), TextureManager.greenBlock1920));
            playerColors.Add(new GameObject(GetScreenAlignment(TextureManager.blueBlock1920.Width, SettingsManager.tileSize.X * 5, playerIndex), TextureManager.lightBlueBlock1920));
            playerColors.Add(new GameObject(GetScreenAlignment(TextureManager.blueBlock1920.Width, SettingsManager.tileSize.X * 5, playerIndex), TextureManager.orangeBlock1920));
            playerColors.Add(new GameObject(GetScreenAlignment(TextureManager.blueBlock1920.Width, SettingsManager.tileSize.X * 5, playerIndex), TextureManager.purpleBlock1920));
            playerColors.Add(new GameObject(GetScreenAlignment(TextureManager.blueBlock1920.Width, SettingsManager.tileSize.X * 5, playerIndex), TextureManager.redBlock1920));
            playerColors.Add(new GameObject(GetScreenAlignment(TextureManager.blueBlock1920.Width, SettingsManager.tileSize.X * 5, playerIndex), TextureManager.yellowBlock1920));
        }
        public void Update(InputManager iM, bool gamePad)
        {
            CreatePlay();
            if (gamePad)
            {
                if (iM.JustPressed(Buttons.DPadLeft, playerOneIndex) && !playerOneReady)
                {
                    if (playerOneChoice == 0) { playerOneChoice = colorsP1.Count - 1; }
                    else
                    {
                        playerOneChoice--;
                    }
                }
                if (iM.JustPressed(Buttons.DPadRight, playerOneIndex) && !playerOneReady)
                {
                    if (playerOneChoice == colorsP1.Count - 1)
                    {
                        playerOneChoice = 0;
                    }
                    else { playerOneChoice++; }
                }
                if (iM.JustPressed(Buttons.Start, playerOneIndex) && !playerOneReady) { playerOneReady = true; }
                if (iM.JustPressed(Buttons.DPadLeft, playerTwoIndex) && !playerTwoReady)
                {
                    if (playerTwoChoice == 0) { playerTwoChoice = colorsP2.Count - 1; }
                    else
                    {
                        playerTwoChoice--;
                    }
                }
                if (iM.JustPressed(Buttons.DPadRight, playerTwoIndex) && !playerTwoReady)
                {
                    if (playerTwoChoice == colorsP2.Count - 1)
                    {
                        playerTwoChoice = 0;
                    }
                    else { playerTwoChoice++; }
                }
                if (iM.JustPressed(Buttons.Start, playerTwoIndex) && !playerTwoReady) { playerTwoReady = true; }
            }
            else
            {
                if (iM.JustPressed(Keys.A) && !playerOneReady)
                {
                    if (playerOneChoice == 0) { playerOneChoice = colorsP1.Count - 1; }
                    else
                    {
                        playerOneChoice--;
                    }
                }
                if (iM.JustPressed(Keys.D) && !playerOneReady)
                {
                    if (playerOneChoice == colorsP1.Count - 1)
                    {
                        playerOneChoice = 0;
                    }
                    else { playerOneChoice++; }
                }
                if (iM.JustPressed(Keys.Space) && !playerOneReady) { playerOneReady = true; }
                if (iM.JustPressed(Keys.Left) && !playerTwoReady)
                {
                    if (playerTwoChoice == 0) { playerTwoChoice = colorsP2.Count - 1; }
                    else
                    {
                        playerTwoChoice--;
                    }
                }
                if (iM.JustPressed(Keys.Right) && !playerTwoReady)
                {
                    if (playerTwoChoice == colorsP2.Count - 1)
                    {
                        playerTwoChoice = 0;
                    }
                    else { playerTwoChoice++; }
                }
                if (iM.JustPressed(Keys.Enter) && !playerTwoReady) { playerTwoReady = true; }
            }
        }
        private void CreatePlay()
        {
            if (playerOneReady && playerTwoReady)
            {
                Play = new Play(SettingsManager.gamePadVersion,
                    SettingsManager.tiles, SettingsManager.tileSize,
                    SettingsManager.gameWidth, SettingsManager.gameHeight,
                    SettingsManager.playerIndexOne, SettingsManager.playerIndexTwo,
                    colorsP1[playerOneChoice].Tex, colorsP2[playerTwoChoice].Tex);
                ReadyEnterPlay = true;
                playerOneReady = false;
                playerTwoReady = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(FontManager.MenuText, instructionsPlayer1,
                GetScreenAlignment(FontManager.MenuText.MeasureString(instructionsPlayer1).X, SettingsManager.tileSize.X * 3, playerOneIndex), Color.Red);
            colorsP1[playerOneChoice].Draw(spriteBatch);
            if (playerOneReady)
            {
                spriteBatch.DrawString(FontManager.MenuText, "Player One Ready!",
                GetScreenAlignment(FontManager.MenuText.MeasureString("Player One Ready!").X, SettingsManager.tileSize.X * 7, playerOneIndex), Color.Yellow);
            }
            else
            {
                spriteBatch.DrawString(FontManager.MenuText, "Press start / Space when ready!",
                GetScreenAlignment(FontManager.MenuText.MeasureString("Press start / Space when ready!").X, SettingsManager.tileSize.X * 7, playerOneIndex), Color.White);
            }
            spriteBatch.DrawString(FontManager.MenuText, instructionsPlayer2,
                GetScreenAlignment(FontManager.MenuText.MeasureString(instructionsPlayer2).X, SettingsManager.tileSize.X * 3, playerTwoIndex), Color.Red);
            colorsP2[playerTwoChoice].Draw(spriteBatch);
            if (playerTwoReady)
            {
                spriteBatch.DrawString(FontManager.MenuText, "Player Two Ready!",
                GetScreenAlignment(FontManager.MenuText.MeasureString("Player Two Ready!").X, SettingsManager.tileSize.X * 7, playerTwoIndex), Color.Yellow);
            }
            else
            {
                spriteBatch.DrawString(FontManager.MenuText, "Press start / Space when ready!",
                GetScreenAlignment(FontManager.MenuText.MeasureString("Press start / Space when ready!").X, SettingsManager.tileSize.X * 7, playerTwoIndex), Color.White);
            }
        }
        private Vector2 GetScreenAlignment(float objectWidth, float marginTop, int playerIndex)
        {
            float playerPlaceMentCorrection = SettingsManager.gameWidth / 4;
            float x;
            float y;
            if (playerIndex == playerOneIndex)
            {
                x = SettingsManager.gameWidth / 2 - playerPlaceMentCorrection - objectWidth / 2;
                y = marginTop;
            }
            else
            {
                x = SettingsManager.gameWidth / 2 + playerPlaceMentCorrection - objectWidth / 2;
                y = marginTop;
            }
            return new Vector2(x, y);
        }
    }
}
