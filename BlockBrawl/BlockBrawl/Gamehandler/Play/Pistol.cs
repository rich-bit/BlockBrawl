using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BlockBrawl.Objects;

namespace BlockBrawl
{
    class Pistol
    {
        GameObject shot;

        public Vector2 ShotPos { get; set; }
        Vector2 posStartMiddle, speed;
        float tileSeize;
        bool fired;
        public bool EnemyDown { get; set; }
        public int PlayerIndexPistol { get; set; }
        int playerOneIndex;
        TetrisObject[,] sender, reciever;
        public Pistol(float timeToLive, int qteWinnerIndex, int playerOneIndex, int playerTwoIndex)
        {
            PlayerIndexPistol = qteWinnerIndex;
            this.playerOneIndex = playerOneIndex;

            speed = SettingsManager.pistolShotSpeed;

            tileSeize = SettingsManager.tileSize.X;
        }
        public void Action(TetrisObject[,] playerOneBlock, TetrisObject[,] playerTwoBlock, InputManager iM, bool gamePad, GameTime gameTime)
        {
            if (PlayerIndexPistol == playerOneIndex)
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
            if (!gamePad && PlayerIndexPistol == playerOneIndex)
            {
                fire = Keys.F;
            }

            if (gamePad && iM.JustPressed(Buttons.Back, PlayerIndexPistol) && !fired)
            {
                if (sender != null)
                {
                    SoundManager.laserShot.Play();
                    posStartMiddle = new Vector2(
                        (((sender[
                        sender.GetLength(0) - 1, 0
                        ].PosX + tileSeize)
                        - (sender[0, 0].PosX)) / 2),
                        (((sender[
                        sender.GetLength(1) - 1, 0
                        ].PosY + tileSeize)
                        - (sender[0, 0].PosY)) / 2));
                    shot = new GameObject(Vector2.Zero, TextureManager.bazookaShot);
                    shot.Pos = sender[0, 0].Pos + posStartMiddle;
                    fired = true;
                }
            }
            if (!gamePad && iM.JustPressed(fire) && !fired)
            {
                if (sender != null)
                {
                    SoundManager.laserShot.Play();
                    posStartMiddle = new Vector2(
                        (((sender[
                        sender.GetLength(0) - 1, 0
                        ].PosX + tileSeize)
                        - (sender[0, 0].PosX)) / 2),
                        (((sender[
                        sender.GetLength(1) - 1, 0
                        ].PosY + tileSeize)
                        - (sender[0, 0].PosY)) / 2));
                    shot = new GameObject(Vector2.Zero, TextureManager.bazookaShot);
                    shot.Pos = sender[0, 0].Pos + posStartMiddle;
                    fired = true;
                }
            }
            if (fired && !TargetHit)
            {
                if (reciever != null)
                {
                    SeekOtherPlayer(reciever);
                    CheckCollision(reciever);
                }
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
                    item.Shot = true;
                    item.ChangeState(true);
                    ShotPos = shot.Pos;
                    shot = null;
                    break;
                }
            }
            bool reset = false;
            for (int i = 0; i < target.GetLength(0); i++)
            {
                for (int j = 0; j < target.GetLength(1); j++)
                {
                    if (target[i, j].alive)
                    {
                        reset = true;
                    }
                    else if (i == target.GetLength(0) - 1 && j == target.GetLength(1) - 1) { EnemyDown = true; }
                }
                if (reset) { break; }
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
