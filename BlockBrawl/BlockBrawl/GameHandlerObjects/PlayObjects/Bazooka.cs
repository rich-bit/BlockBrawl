using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BlockBrawl.Objects;

namespace BlockBrawl.GameHandlerObjects.PlayObjects
{
    class Bazooka
    {
        AnimatedObject shot;
        public Vector2 ShotPos { get; set; }
        Vector2 posStartMiddle, speed;
        float tileSeize;
        bool fired;
        public int PlayerIndexBazooka { get; set; }
        int playerOneIndex;
        TetrisObject[,] sender, reciever;
        public Bazooka(float timeToLive, int qteWinnerIndex, int playerOneIndex, int playerTwoIndex)
        {
            PlayerIndexBazooka = qteWinnerIndex;
            this.playerOneIndex = playerOneIndex;

            speed = SettingsManager.shotSpeed;

            tileSeize = SettingsManager.tileSize.X;
        }
        public void Action(TetrisObject[,] playerOneBlock, TetrisObject[,] playerTwoBlock, InputManager iM, bool gamePad, GameTime gameTime)
        {
            if (PlayerIndexBazooka == playerOneIndex)
            {
                sender = playerOneBlock;
                reciever = playerTwoBlock;
            }
            else
            {
                reciever = playerOneBlock;
                sender = playerTwoBlock;
            }

            Keys fire = Keys.RightControl;
            if (!gamePad && PlayerIndexBazooka == playerOneIndex)
            {
                fire = Keys.F;
            }

            if (gamePad && iM.JustPressed(Buttons.Back, PlayerIndexBazooka) && !fired)
            {
                posStartMiddle = new Vector2(
                    (((sender[
                    sender.GetLength(0) - 1, 0
                    ].PosX + tileSeize)
                    - (sender[0, 0].PosX)) / 2),
                    (((sender[
                    sender.GetLength(1) - 1, 0
                    ].PosY + tileSeize)
                    - (sender[0, 0].PosY)) / 2));
                shot = new AnimatedObject(Vector2.Zero, TextureManager.spriteSheetShot, new Point(7,1));
                shot.Pos = sender[0, 0].Pos + posStartMiddle;
                fired = true;
            }
            if (!gamePad && iM.JustPressed(fire) && !fired)
            {
                posStartMiddle = new Vector2(
                    (((sender[
                    sender.GetLength(0) - 1, 0
                    ].PosX + tileSeize)
                    - (sender[0, 0].PosX)) / 2),
                    (((sender[
                    sender.GetLength(1) - 1, 0
                    ].PosY + tileSeize)
                    - (sender[0, 0].PosY)) / 2));
                shot = new AnimatedObject(Vector2.Zero, TextureManager.spriteSheetShot, new Point(7, 1));
                shot.Pos = sender[0, 0].Pos + posStartMiddle;
                fired = true;
            }
            if (fired && !TargetHit)
            {
                SeekOtherPlayer(reciever);
                CheckCollision(reciever);
            }
            if(shot != null)
            {
                shot.CycleSpriteSheet(gameTime);
            }
        }
        public bool TargetHit { get; private set; }
        private void SeekOtherPlayer(TetrisObject[,] target)
        {
            Vector2 endPosition = Vector2.Zero;
            for (int i = 0; i < target.GetLength(0); i++)
            {
                for (int j = 0; j < target.GetLength(1); j++)
                {
                    if (target[i, j].alive)
                    {
                        endPosition = target[i, j].Pos;
                    }
                }
            }
            Vector2 direction = endPosition - shot.Pos;
            direction.Normalize();
            shot.Pos += speed * direction;
        }
        private void CheckCollision(TetrisObject[,] target)
        {
            foreach (TetrisObject item in target)
            {
                if (item.alive && item.Rect.Intersects(shot.Rect))
                {
                    TargetHit = true;
                    ShotPos = shot.Pos;
                    shot = null;
                    break;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (shot != null)
            {
                shot.Draw(spriteBatch);
            }
        }
    }
}
