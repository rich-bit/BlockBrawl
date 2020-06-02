using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BlockBrawl.Blocks;
using BlockBrawl.Objects;

namespace BlockBrawl
{
    class SideBars
    {
        Vector2 playerOnePos, playerTwoPos;
        int[] score;
        int[,] bonusRecieved;
        float[] spawnWaitTime;
        string[] nextBlock;
        Texture2D[] playerColors;
        int playerOneIndex, playerTwoIndex;
        public int QTEWinner { get; set; }
        public bool Music { get; set; }
        GameObject playMusicP1, playMusicP2;
        bool gamepadVersion;
        float blinkTime;
        double betweenBlinks = 0.2;
        public SideBars(Texture2D[] playerColors, float[] spawnBlock, bool gamepadVersion)
        {
            this.playerColors = playerColors;
            this.spawnWaitTime = spawnBlock;
            this.gamepadVersion = gamepadVersion;
            QTEWinner = int.MinValue;
            
            playerOneIndex = SettingsManager.playerIndexOne;
            playerTwoIndex = SettingsManager.playerIndexTwo;

            playerOnePos = Vector2.Zero;
            playerTwoPos = new Vector2(SettingsManager.gameWidth, 0);

            playMusicP1 = new GameObject(GetPlayerOneAllignment(9), TextureManager.playMusic);
            playMusicP2 = new GameObject(GetPlayerTwoAllignment(TextureManager.playMusic.Width, 9), TextureManager.playMusic);
        }
        private Vector2 GetPlayerTwoAllignment(float width, int row)
        {
            return new Vector2(playerTwoPos.X - width, playerTwoPos.Y + SettingsManager.tileSize.Y * row);
        }
        private Vector2 GetPlayerOneAllignment(int row)
        {
            return Vector2.Zero + new Vector2(0, row * SettingsManager.tileSize.Y);
        }
        public void Update(int[] playerScores, int[,] bonusRecieved, string[] nextBlock)
        {
            score = playerScores;
            this.bonusRecieved = bonusRecieved;
            this.nextBlock = nextBlock;
        }
        private TetrisObject[,] NextBlock(int playerIndex)
        {
            TetrisObject[,] previewBlock = null;

            int row = 4;
            float marginRightBlock = SettingsManager.tileSize.X * 4;

            Vector2 pos = Vector2.Zero;

            if (playerIndex == playerOneIndex)
            {
                pos = GetPlayerOneAllignment(row);
            }
            else if (playerIndex == playerTwoIndex)
            {
                pos = GetPlayerTwoAllignment(marginRightBlock, row);
            }

            switch (nextBlock[playerIndex])
            {
                case "J":
                    previewBlock = new J(playerColors[playerIndex], pos).jMatrix;
                    break;
                case "I":
                    previewBlock = new I(playerColors[playerIndex], pos).iMatrix;
                    break;
                case "T":
                    previewBlock = new T(playerColors[playerIndex], pos).tMatrix;
                    break;
                case "O":
                    previewBlock = new O(playerColors[playerIndex], pos).oMatrix;
                    break;
                case "L":
                    previewBlock = new L(playerColors[playerIndex], pos).lMatrix;
                    break;
                case "S":
                    previewBlock = new S(playerColors[playerIndex], pos).sMatrix;
                    break;
                case "Z":
                    previewBlock = new Z(playerColors[playerIndex], pos).zMatrix;
                    break;
            }
            return previewBlock;
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(FontManager.GeneralText,
            $"{SettingsManager.playerOneName.ToString()}\nscore: " + score[playerOneIndex].ToString(),
            playerOnePos,
            Color.Red);

            spriteBatch.DrawString(FontManager.GeneralText,
            $"{SettingsManager.playerTwoName.ToString()}\nscore: " + score[playerTwoIndex].ToString(),
            GetPlayerTwoAllignment(
                FontManager.GeneralText.MeasureString(
                    $"{SettingsManager.playerTwoName.ToString()}\nscore: " + score[playerTwoIndex].ToString()
                    ).X
                , 0
                ),
            Color.Red);

            for (int playerIndex = 0; playerIndex < bonusRecieved.GetLength(0); playerIndex++)
            {
                for (int bonusRow = 0; bonusRow < bonusRecieved.GetLength(1); bonusRow++)
                {
                    if (bonusRecieved[playerIndex, bonusRow] != 0)
                    {
                        if (playerIndex == playerOneIndex)
                        {
                            spriteBatch.DrawString(FontManager.GeneralText,
                            "Bonus: " + bonusRecieved[playerIndex, bonusRow].ToString(),
                            new Vector2(playerOnePos.X, playerTwoPos.Y + SettingsManager.tileSize.Y * bonusRow),
                            Color.Blue);
                        }
                        else if (playerIndex == playerTwoIndex)
                        {
                            spriteBatch.DrawString(FontManager.GeneralText,
                            "Bonus: " + bonusRecieved[playerIndex, bonusRow].ToString(),
                            GetPlayerTwoAllignment(
                                FontManager.GeneralText.MeasureString(
                            "Bonus: " + bonusRecieved[playerIndex, bonusRow].ToString()).X
                            , bonusRow),
                            Color.Blue);
                        }
                    }
                }
            }
            spriteBatch.DrawString(FontManager.GeneralText, "NextBlock", GetPlayerOneAllignment(3), Color.Red);
            if (NextBlock(playerOneIndex) != null)
            {
                foreach (TetrisObject item in NextBlock(playerOneIndex))
                {
                    item.Draw(spriteBatch);
                }
            }
            spriteBatch.DrawString(FontManager.GeneralText, "NextBlock",
                GetPlayerTwoAllignment(
                    FontManager.GeneralText.MeasureString(
                        "NextBlock"
                        ).X
                    , 3),
                Color.Red);
            if (NextBlock(playerTwoIndex) != null)
            {
                foreach (TetrisObject item in NextBlock(playerTwoIndex))
                {
                    item.Draw(spriteBatch);
                }
            }
            if (spawnWaitTime[playerOneIndex] > 0f)
            {
                spriteBatch.DrawString(FontManager.GeneralText, "Wait\nfor spawn!" + Convert.ToInt32(spawnWaitTime[playerOneIndex]).ToString(), GetPlayerOneAllignment(7), Color.Yellow);
            }
            if (QTEWinner == playerOneIndex && gamepadVersion)
            {
                blinkTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (blinkTime > betweenBlinks)
                {
                    if (blinkTime > betweenBlinks * 2) { blinkTime = 0f; }
                }
                else
                {
                    spriteBatch.DrawString(FontManager.ScoreText, "Press Select!", GetPlayerOneAllignment(9), Color.Gold);
                }
            }
            if (QTEWinner == playerOneIndex && !gamepadVersion)
            {
                blinkTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (blinkTime > betweenBlinks)
                {
                    if (blinkTime > betweenBlinks * 2) { blinkTime = 0f; }
                }
                else
                {
                    spriteBatch.DrawString(FontManager.ScoreText, "Press W!", GetPlayerOneAllignment(9), Color.Gold);
                }
            }
            if (spawnWaitTime[playerTwoIndex] > 0f)
            {
                spriteBatch.DrawString(FontManager.GeneralText, "Wait\nfor spawn!\n" + Convert.ToInt32(spawnWaitTime[playerTwoIndex]).ToString(),
                    GetPlayerTwoAllignment(
                        FontManager.GeneralText.MeasureString("Wait\nfor spawn!\n" + Convert.ToInt32(spawnWaitTime[playerTwoIndex]).ToString()
                            ).X
                        , 7),
                    Color.Yellow);
            }
            if (QTEWinner == playerTwoIndex && gamepadVersion)
            {
                blinkTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (blinkTime > betweenBlinks)
                {
                    if (blinkTime > betweenBlinks * 2) { blinkTime = 0f; }
                }
                else
                {
                    spriteBatch.DrawString(FontManager.ScoreText, "Press Select!", GetPlayerTwoAllignment(FontManager.ScoreText.MeasureString("Press Select!").X, 9), Color.Gold);
                }
            }
            if (QTEWinner == playerTwoIndex && !gamepadVersion)
            {
                blinkTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (blinkTime > betweenBlinks)
                {
                    if (blinkTime > betweenBlinks * 2) { blinkTime = 0f; }
                }
                else
                {
                    spriteBatch.DrawString(FontManager.ScoreText, "Press UP!", GetPlayerTwoAllignment(FontManager.ScoreText.MeasureString("Press Select!").X, 9), Color.Gold);
                }
            }
            if (Music)
            {
                playMusicP1.Draw(spriteBatch);
                playMusicP2.Draw(spriteBatch);
                playMusicP1.Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(playMusicP1.Time > 6f) { playMusicP1.Time = 0f; Music = false; }
            }
        }
    }
}
