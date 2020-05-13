using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using BlockBrawl.Objects;
using Npgsql;
using System.Configuration;
using Dapper;

namespace BlockBrawl
{
    class CheckGameOver
    {
        string connectionString;
        List<Dataread> dataReads;
        NpgsqlConnection connection;
        bool unsuccesfullDataAccess = false;
        string unsuccesfullDataAccessMsg;
        int winnerScore, looserScore, playedTime;
        string winnerName, looserName, scoreProcessed;
        public bool RequestGoToMenu { get; set; }

        public CheckGameOver()
        {
            connectionString = ConfigurationManager.ConnectionStrings["visualstudio"].ConnectionString;
            unsuccesfullDataAccessMsg = "Cannot access Database!";
        }
        public void CheckHighScore(int winnerScore, string winnerName, int looserScore, string looserName, int playedTime, bool gamepad)
        {
            this.winnerName = winnerName;
            this.looserName = looserName;
            this.winnerScore = winnerScore;
            this.looserScore = looserScore;
            this.playedTime = playedTime;
            try
            {
                PullFromDB();
                unsuccesfullDataAccess = false;
                for (int i = 0; i < dataReads.Count; i++)
                {
                    if (winnerScore > dataReads[i].score1)
                    {
                        scoreProcessed = $"You made the Highscore {winnerScore} points! Place # {dataReads[i].id.ToString()}!";
                        UpdateDatabase(dataReads[i].id, winnerScore, looserScore, winnerName, looserName, playedTime);
                        break;
                    }
                    else
                    {
                        scoreProcessed = $"Play again!";
                    }
                }
            }
            catch (NpgsqlException e)
            {
                unsuccesfullDataAccess = true;
                string error = e.ToString();
                if (gamepad)
                {
                    scoreProcessed = "Please stay on this screen and try database again with A button!";
                }
                else
                {
                    scoreProcessed = "Please stay on this screen and try database again with F5!";
                }
            }
        }
        private void UpdateDatabase(int id, int winnerScore, int looserScore, string winnerName, string looserName, int playedTime)
        {

            using (connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                for (int i = id; i < dataReads.Count; i++)
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        if (i + 1 < dataReads.Count)
                        {
                            int row = i + 1;
                            string pushDownRows = $"update records set name1 = '{dataReads[i - 1].name1}', name2 = '{dataReads[i - 1].name2}', score1 = {dataReads[i - 1].score1}, score2 = {dataReads[i - 1].score2}, playedtime = {dataReads[i - 1].playedtime} where id = {row};";
                            connection.Execute(pushDownRows, transaction: transaction);
                            transaction.Commit();
                        }
                    }
                }
            }

            string sql = $"update records set name1 = '{winnerName}', name2 = '{looserName}', score1 = {winnerScore}, score2 = {looserScore}, playedtime = {playedTime} where id = {id};";
            using (connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    connection.Execute(sql, transaction: transaction);
                    transaction.Commit();
                }
            }
        }
        public List<Dataread> PullFromDB()
        {
            using (connection = new NpgsqlConnection(connectionString))
            {
                var output = connection.Query<Dataread>("select * from records order by id asc;").ToList();
                dataReads = output;
            }
            return dataReads;
        }
        public void Update(InputManager iM, bool gamepad, int playerOneIndex, int playerTwoIndex)
        {
            if (unsuccesfullDataAccess)
            {
                if (gamepad)
                {
                    if (iM.JustPressed(Buttons.B, playerOneIndex) || iM.JustPressed(Buttons.B, playerTwoIndex))
                    {
                        CheckHighScore(winnerScore, winnerName, looserScore, looserName, playedTime, gamepad);
                    }
                }
                else
                {
                    if (iM.JustPressed(Keys.F5))
                    {
                        CheckHighScore(winnerScore, winnerName, looserScore, looserName, playedTime, gamepad);
                    }
                }
            }
            if (gamepad)
            {
                if (iM.JustPressed(Buttons.Back, playerOneIndex) || iM.JustPressed(Buttons.Back, playerTwoIndex))
                {
                    RequestGoToMenu = true;
                }
            }
            if (!gamepad)
            {
                if (iM.JustPressed(Keys.F5))
                {
                    RequestGoToMenu = true;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (unsuccesfullDataAccess)
            {
                spriteBatch.DrawString(FontManager.MenuText, unsuccesfullDataAccessMsg, new Vector2(0, 0), Color.Red);
                spriteBatch.DrawString(FontManager.MenuText, scoreProcessed, new Vector2(0, 80), Color.Yellow);
                spriteBatch.DrawString(FontManager.MenuText, "Go to menu? Press Select/Esc", new Vector2(0, 160), Color.Yellow);
            }
            else
            {
                spriteBatch.DrawString(FontManager.MenuText, $"{winnerName} beat {looserName} with {winnerScore} against {looserScore}", new Vector2(0, 0), Color.Yellow);
                spriteBatch.DrawString(FontManager.MenuText, scoreProcessed, new Vector2(0, 80), Color.Yellow);
                spriteBatch.DrawString(FontManager.MenuText, "Go to menu? Press Select/Esc", new Vector2(0, 160), Color.Yellow);
            }
        }
    }
}
