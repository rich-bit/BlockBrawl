﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Npgsql;
using Dapper;
using BlockBrawl.Objects;
using BlockBrawl.GameHandlerObjects.HighScoreObjects;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace BlockBrawl
{
    class HighScore
    {
        List<Dataread> dataReads;
        List<HighScoreEntry> currentRecords;
        NpgsqlConnection connection;
        bool unsuccesfullDataAccess = true;
        string unsuccesfullDataAccessMsg;
        int rowForDraw;
        public bool GoToMenu { get; set; }
        public bool UpdateHighScore { get; set; }

        Buttons p1Start, p1Select, p2Start, p2Select;
        public HighScore()
        {
            p1Start = SettingsManager.p1Start;
            p1Select = SettingsManager.p1PowerUp;
            p2Start = SettingsManager.p2Start;
            p2Select = SettingsManager.p2PowerUp;

            unsuccesfullDataAccessMsg = "Cannot access Database";
            new Thread(() =>
            {
                CreateHighScore();
            }).Start();
        }
        public void CreateHighScore()
        {
            try
            {
                PullFromDB();
                unsuccesfullDataAccess = false;
                currentRecords = new List<HighScoreEntry>();
                for (int i = 0; i < dataReads.Count; i++)
                {
                    if (dataReads[i].score1 > 0 || dataReads[i].score2 > 0)
                    {
                        currentRecords.Add(new HighScoreEntry(
                            dataReads[i].id,
                            dataReads[i].name1,
                            dataReads[i].name2,
                            dataReads[i].score1,
                            dataReads[i].score2,
                            dataReads[i].playedtime
                            ));
                    }
                }
                for (int j = 0; j < currentRecords.Count; j++)
                {
                    currentRecords[j].Pos = GetAlignment(FontManager.ScoreText,
                        Text(currentRecords[j].PlayerOneName,
                        currentRecords[j].PlayerTwoName,
                        currentRecords[j].PlayerOneScore,
                        currentRecords[j].PlayerTwoScore,
                        currentRecords[j].GameTime)
                        );
                }
                //UpdateHighScore = false;
            }
            catch
            {
                unsuccesfullDataAccess = true;
            }
        }
        public void Update(InputManager iM, int playerOneIndex, int playerTwoIndex)
        {
            if (iM.JustPressed(p1Select, playerOneIndex) || iM.JustPressed(p2Select, playerTwoIndex) || iM.JustPressed(Keys.Escape))
            {
                GoToMenu = true;
                UpdateHighScore = true;
                rowForDraw = 0;
            }
            if (iM.JustPressed(p1Start, playerOneIndex) || iM.JustPressed(p2Start, playerTwoIndex) || iM.JustPressed(Keys.F5))
            {
                UpdateHighScore = true;
            }
            if (UpdateHighScore)
            {
                new Thread(() =>
                {
                    CreateHighScore();
                }).Start();
                UpdateHighScore = false;
            }
        }
        public List<Dataread> PullFromDB()
        {
            using (connection = new NpgsqlConnection(SettingsManager.connectionString))
            {
                var output = connection.Query<Dataread>("select * from records order by id asc;").ToList();
                dataReads = output;
            }
            return dataReads;
        }
        private Vector2 GetAlignment(SpriteFont font, string text)
        {
            float marginTop = SettingsManager.tileSize.Y * 3;
            float nextPos = SettingsManager.tileSize.Y * rowForDraw;
            float x = SettingsManager.gameWidth / 2 - font.MeasureString(text).X / 2;
            float y = marginTop + nextPos;
            rowForDraw++;
            if (rowForDraw == 10) { rowForDraw = 0; }
            return new Vector2(x, y);
        }
        private Vector2 GetAlignment(SpriteFont font, string text, int row)
        {
            float marginTop = SettingsManager.tileSize.Y * row;
            float x = SettingsManager.gameWidth / 2 - font.MeasureString(text).X / 2;
            float y = marginTop;
            return new Vector2(x, y);
        }
        private string Text(string playerOne, string playerTwo, int score1, int score2, int gameTime)
        {
            if (playerOne == "")
            {
                playerOne = "Nameless";
            }
            if (playerTwo == "")
            {
                playerTwo = "Nameless";
            }

            if (score1 != score2)
            {
                return $"{playerOne} scored {score1} and beat {playerTwo}, score {score2}. Played time {gameTime} seconds.";
            }
            else return $"{playerOne} and {playerTwo} both scored {score1}. Played time {gameTime} seconds.";
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (unsuccesfullDataAccess)
            {
                spriteBatch.DrawString(FontManager.GeneralText, unsuccesfullDataAccessMsg, Vector2.Zero, Color.Red);
            }
            else
            {
                spriteBatch.DrawString(FontManager.GeneralText, "Best BlockBrawl scores:", GetAlignment(FontManager.GeneralText, "Best BlockBrawl scores:", 1), Color.Gold);
                if (currentRecords != null)
                {
                    for (int i = 0; i < currentRecords.Count; i++)
                    {
                        string drawString = Text(currentRecords[i].PlayerOneName, currentRecords[i].PlayerTwoName, currentRecords[i].PlayerOneScore, currentRecords[i].PlayerTwoScore, currentRecords[i].GameTime);
                        spriteBatch.DrawString(FontManager.ScoreText, drawString,
                            currentRecords[i].Pos,
                            Color.LightYellow);
                    }
                }
                if (currentRecords == null || currentRecords.Count == 0)
                {
                    spriteBatch.DrawString(FontManager.GeneralText, "No records to show!", GetAlignment(FontManager.GeneralText, "Highscores!", 3), Color.Yellow);
                }
            }
            spriteBatch.DrawString(FontManager.GeneralText, "To go menu? Use ESC / Select", new Vector2(0,
                SettingsManager.gameHeight - FontManager.GeneralText.MeasureString("To go menu? Use ESC / Select").Y), Color.Yellow);
            spriteBatch.DrawString(FontManager.GeneralText, "Refresh with F5/Start", new Vector2(SettingsManager.gameWidth - FontManager.GeneralText.MeasureString("Refresh with F5/Start").X,
                SettingsManager.gameHeight - FontManager.GeneralText.MeasureString("Refresh with F5/Start").Y), Color.Yellow);
        }
    }
}
