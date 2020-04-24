﻿using Microsoft.Xna.Framework;
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
        string[] nextBlock;
        Texture2D[] playerColors;
        int playerOneIndex, playerTwoIndex;
        public SideBars(Texture2D[] playerColors)
        {
            this.playerColors = playerColors;

            playerOneIndex = SettingsManager.playerIndexOne;
            playerTwoIndex = SettingsManager.playerIndexTwo;

            playerOnePos = Vector2.Zero;
            playerTwoPos = new Vector2(SettingsManager.gameWidth, 0);
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
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(FontManager.MenuText,
            $"{SettingsManager.playerOneName.ToString()}\nscore: " + score[playerOneIndex].ToString(),
            playerOnePos,
            Color.Red);

            spriteBatch.DrawString(FontManager.MenuText,
            $"{SettingsManager.playerTwoName.ToString()}\nscore: " + score[playerTwoIndex].ToString(),
            GetPlayerTwoAllignment(
                FontManager.MenuText.MeasureString(
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
                            spriteBatch.DrawString(FontManager.MenuText,
                            "Bonus: " + bonusRecieved[playerIndex, bonusRow].ToString(),
                            new Vector2(playerOnePos.X, playerTwoPos.Y + SettingsManager.tileSize.Y * bonusRow),
                            Color.Blue);
                        }
                        else if (playerIndex == playerTwoIndex)
                        {
                            spriteBatch.DrawString(FontManager.MenuText,
                            "Bonus: " + bonusRecieved[playerIndex, bonusRow].ToString(),
                            GetPlayerTwoAllignment(
                                FontManager.MenuText.MeasureString(
                            "Bonus: " + bonusRecieved[playerIndex, bonusRow].ToString()).X
                            , bonusRow),
                            Color.Blue);
                        }
                    }
                }
            }
            spriteBatch.DrawString(FontManager.MenuText, "NextBlock", GetPlayerOneAllignment(3), Color.Red);
            if (NextBlock(playerOneIndex) != null)
            {
                foreach (TetrisObject item in NextBlock(playerOneIndex))
                {
                    item.Draw(spriteBatch);
                }
            }
            spriteBatch.DrawString(FontManager.MenuText, "NextBlock",
                GetPlayerTwoAllignment(
                    FontManager.MenuText.MeasureString(
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
        }
    }
}