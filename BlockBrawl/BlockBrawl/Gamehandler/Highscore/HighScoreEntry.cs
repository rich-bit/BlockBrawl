using Microsoft.Xna.Framework;

namespace BlockBrawl.GameHandlerObjects.HighScoreObjects
{
    class HighScoreEntry
    {
        public int Id { get; set; }
        public string PlayerOneName { get; set; }
        public string PlayerTwoName { get; set; }
        public int PlayerOneScore { get; set; }
        public int PlayerTwoScore { get; set; }
        public int GameTime { get; set; }
        public Vector2 Pos { get; set; }
        public HighScoreEntry(int id, string playerOneName, string playerTwoName, int playerOneScore, int playerTwoScore, int gameTime)
        {
            Id = id;
            PlayerOneName = playerOneName;
            PlayerTwoName = playerTwoName;
            PlayerOneScore = playerOneScore;
            PlayerTwoScore = playerTwoScore;
            GameTime = gameTime;
        }
    }
}
