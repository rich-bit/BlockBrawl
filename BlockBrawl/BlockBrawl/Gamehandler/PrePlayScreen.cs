using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BlockBrawl
{
    class PrePlayScreen
    {
        public Play Play { get; set; }
        public bool ReadyEnterPlay { get; set; }
        bool playerOneReady, playerTwoReady;
        int playerOneIndex, playerTwoIndex, playerOneChoice, playerTwoChoice;
        string instructionsPlayer1, instructionsPlayer2;
        string playerOneName, playerTwoName;
        Vector2 changeWriterPos;
        enum Typer
        {
            playerOne,
            playerTwo,
        }
        Typer playerTyping;
        List<GameObject> colorsP1, colorsP2;

        Buttons p1Start, p1MoveLeft, p1MoveRight, p2Start, p2MoveLeft, p2MoveRight;

        public PrePlayScreen(int playerOneIndex, int playerTwoIndex)
        {
            this.playerOneIndex = playerOneIndex;
            this.playerTwoIndex = playerTwoIndex;

            p1Start = SettingsManager.p1Start;
            p1MoveLeft = SettingsManager.p1MoveLeft;
            p1MoveRight = SettingsManager.p1MoveRight;

            p2Start = SettingsManager.p2Start;
            p2MoveLeft = SettingsManager.p2MoveLeft;
            p2MoveRight = SettingsManager.p2MoveRight;

            playerTyping = Typer.playerOne;

            colorsP1 = new List<GameObject>();
            SetColors(colorsP1, playerOneIndex);
            colorsP2 = new List<GameObject>();
            SetColors(colorsP2, playerTwoIndex);

            instructionsPlayer1 = "Player one please select color:";
            instructionsPlayer2 = "Player two please select color:";
        }
        private void SetColors(List<GameObject> playerColors, int playerIndex)
        {
            if (playerIndex == playerOneChoice)
            {
                playerColors.Add(new GameObject(GetScreenAlignment(TextureManager.blueBlock1920.Width, SettingsManager.tileSize.X * 5, playerIndex), TextureManager.blueBlock1920));
                playerColors.Add(new GameObject(GetScreenAlignment(TextureManager.blueBlock1920.Width, SettingsManager.tileSize.X * 5, playerIndex), TextureManager.greenBlock1920));
                playerColors.Add(new GameObject(GetScreenAlignment(TextureManager.blueBlock1920.Width, SettingsManager.tileSize.X * 5, playerIndex), TextureManager.lightBlueBlock1920));
            }
            else
            {
                playerColors.Add(new GameObject(GetScreenAlignment(TextureManager.blueBlock1920.Width, SettingsManager.tileSize.X * 5, playerIndex), TextureManager.orangeBlock1920));
                playerColors.Add(new GameObject(GetScreenAlignment(TextureManager.blueBlock1920.Width, SettingsManager.tileSize.X * 5, playerIndex), TextureManager.purpleBlock1920));
                playerColors.Add(new GameObject(GetScreenAlignment(TextureManager.blueBlock1920.Width, SettingsManager.tileSize.X * 5, playerIndex), TextureManager.redBlock1920));
                playerColors.Add(new GameObject(GetScreenAlignment(TextureManager.blueBlock1920.Width, SettingsManager.tileSize.X * 5, playerIndex), TextureManager.yellowBlock1920));
            }
        }
        private void SetNames(InputManager iM)
        {
            switch (playerTyping)
            {
                case Typer.playerOne:
                    if (iM.CapitalLetterTyped() != null) { playerOneName += iM.CapitalLetterTyped(); }
                    if (iM.JustPressed(Keys.Back) && playerOneName.Length > 0) { playerOneName = playerOneName.Remove(playerOneName.Length - 1, 1); }
                    if (playerOneName != null && playerOneName.Length > 9) { playerOneName = null; }
                    changeWriterPos = GetScreenAlignment(FontManager.GeneralText.MeasureString("Change with TAB").X, AlignmentTop(8), playerOneIndex);
                    break;
                case Typer.playerTwo:
                    if (iM.CapitalLetterTyped() != null) { playerTwoName += iM.CapitalLetterTyped(); }
                    if (iM.JustPressed(Keys.Back) && playerTwoName.Length > 0) { playerTwoName = playerTwoName.Remove(playerTwoName.Length - 1, 1); }
                    if (playerTwoName != null && playerTwoName.Length > 9) { playerTwoName = null; }
                    changeWriterPos = GetScreenAlignment(FontManager.GeneralText.MeasureString("Change with TAB").X, AlignmentTop(8), playerTwoIndex);
                    break;
            }
        }
        private void UpdateNames()
        {
            if(playerOneName == null) { instructionsPlayer1 = "Player one please select color:"; }
            else { instructionsPlayer1 = $"{playerOneName} please select color:"; }
            if (playerTwoName == null) { instructionsPlayer2 = "Player one please select color:"; }
            else { instructionsPlayer2 = $"{playerTwoName} please select color:"; }
        }
        public void Update(InputManager iM, bool gamePad)
        {
            SetNames(iM);
            UpdateNames();
            if (iM.JustPressed(Keys.Tab))
            {
                if (playerTyping == Typer.playerOne) { playerTyping = Typer.playerTwo; }
                else if (playerTyping == Typer.playerTwo) { playerTyping = Typer.playerOne; }
            }
            CreatePlay();
            if (gamePad)
            {
                if (iM.JustPressed(p1MoveLeft, playerOneIndex) && !playerOneReady)
                {
                    if (playerOneChoice == 0) { playerOneChoice = colorsP1.Count - 1; }
                    else
                    {
                        playerOneChoice--;
                    }
                }
                if (iM.JustPressed(p1MoveRight, playerOneIndex) && !playerOneReady)
                {
                    if (playerOneChoice == colorsP1.Count - 1)
                    {
                        playerOneChoice = 0;
                    }
                    else { playerOneChoice++; }
                }
                if (iM.JustPressed(p1Start, playerOneIndex) && !playerOneReady) { playerOneReady = true; }
                if (iM.JustPressed(p2MoveLeft, playerTwoIndex) && !playerTwoReady)
                {
                    if (playerTwoChoice == 0) { playerTwoChoice = colorsP2.Count - 1; }
                    else
                    {
                        playerTwoChoice--;
                    }
                }
                if (iM.JustPressed(p2MoveRight, playerTwoIndex) && !playerTwoReady)
                {
                    if (playerTwoChoice == colorsP2.Count - 1)
                    {
                        playerTwoChoice = 0;
                    }
                    else { playerTwoChoice++; }
                }
                if (iM.JustPressed(p2Start, playerTwoIndex) && !playerTwoReady) { playerTwoReady = true; }
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
                if(playerOneName != null) { SettingsManager.playerOneName = playerOneName; }
                if(playerTwoName != null) { SettingsManager.playerTwoName = playerTwoName; }
                ReadyEnterPlay = true;
                playerOneReady = false;
                playerTwoReady = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(FontManager.GeneralText, "Change with TAB", changeWriterPos, Color.Aquamarine);

            spriteBatch.DrawString(FontManager.GeneralText, instructionsPlayer1,
                GetScreenAlignment(FontManager.GeneralText.MeasureString(instructionsPlayer1).X, AlignmentTop(3), playerOneIndex), Color.Red);
            colorsP1[playerOneChoice].Draw(spriteBatch);
            spriteBatch.DrawString(FontManager.GeneralText, "Type your name:",
                GetScreenAlignment(FontManager.GeneralText.MeasureString("Type your name:").X, AlignmentTop(7), playerOneIndex), Color.Red);
            if (playerOneName != null)
            {
                spriteBatch.DrawString(FontManager.GeneralText, playerOneName,
                    GetScreenAlignment(FontManager.GeneralText.MeasureString(playerOneName).X, AlignmentTop(9), playerOneIndex), Color.Blue);
            }
            if (playerOneReady)
            {
                spriteBatch.DrawString(FontManager.GeneralText, "Player One Ready!",
                GetScreenAlignment(FontManager.GeneralText.MeasureString("Player One Ready!").X, AlignmentTop(11), playerOneIndex), Color.Yellow);
            }
            else
            {
                spriteBatch.DrawString(FontManager.GeneralText, "Press start / Space when ready!",
                GetScreenAlignment(FontManager.GeneralText.MeasureString("Press start / Space when ready!").X, AlignmentTop(11), playerOneIndex), Color.White);
            }
            spriteBatch.DrawString(FontManager.GeneralText, instructionsPlayer2,
                GetScreenAlignment(FontManager.GeneralText.MeasureString(instructionsPlayer2).X, AlignmentTop(3), playerTwoIndex), Color.Red);
            colorsP2[playerTwoChoice].Draw(spriteBatch);
            spriteBatch.DrawString(FontManager.GeneralText, "Type your name:",
                GetScreenAlignment(FontManager.GeneralText.MeasureString("Type your name:").X, AlignmentTop(7), playerTwoIndex), Color.Red);
            if (playerTwoName != null)
            {
                spriteBatch.DrawString(FontManager.GeneralText, playerTwoName,
        GetScreenAlignment(FontManager.GeneralText.MeasureString(playerTwoName).X, AlignmentTop(9), playerTwoIndex), Color.Blue);
            }
            if (playerTwoReady)
            {
                spriteBatch.DrawString(FontManager.GeneralText, "Player Two Ready!",
                GetScreenAlignment(FontManager.GeneralText.MeasureString("Player Two Ready!").X, AlignmentTop(11), playerTwoIndex), Color.Yellow);
            }
            else
            {
                spriteBatch.DrawString(FontManager.GeneralText, "Press start / Enter when ready!",
                GetScreenAlignment(FontManager.GeneralText.MeasureString("Press start / Space when ready!").X, AlignmentTop(11), playerTwoIndex), Color.White);
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
        private float AlignmentTop(int row)
        {
            return SettingsManager.tileSize.X * row;
        }
    }
}
