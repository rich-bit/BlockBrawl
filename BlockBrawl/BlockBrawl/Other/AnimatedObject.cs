using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockBrawl
{
    class AnimatedObject : GameObject
    {
        Rectangle srcRect;
        Point frame;
        Point nrOfFrames;
        double timeBetweenFrames, sinceLastFrame;
        public bool Done { get; set; }
        public AnimatedObject(Vector2 pos, Texture2D tex, Point nrOfFrames) : base(pos, tex)
        {
            this.nrOfFrames = nrOfFrames;
            Tex = tex;

            frame = new Point(0, 0);
            timeBetweenFrames = 0.1;
            srcRect = new Rectangle(frame.X * tex.Width / nrOfFrames.X, frame.Y * tex.Height / nrOfFrames.Y, tex.Width / nrOfFrames.X, tex.Height / nrOfFrames.Y);
            rect.Width = tex.Width / nrOfFrames.X;
            rect.Height = tex.Height / nrOfFrames.Y;
        }
        public void CycleSpriteSheetOnce(GameTime gameTime)
        {
            sinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;
            if (sinceLastFrame > timeBetweenFrames)
            {
                frame.X++;
                sinceLastFrame = 0;
                if (frame.X == nrOfFrames.X - 1 && frame.Y == nrOfFrames.Y - 1)
                {
                    //    frame.X = 0;
                    //    frame.Y = 0;
                    Done = true;
                }
                else if (frame.X == nrOfFrames.X - 1 && frame.Y != nrOfFrames.Y - 1)
                {
                    frame.X = 0;
                    frame.Y++;
                }
            }
            srcRect.X = frame.X * Tex.Width / nrOfFrames.X;
            srcRect.Y = frame.Y * Tex.Height / nrOfFrames.Y;
            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;
        }
        public void CycleSpriteSheet(GameTime gameTime)
        {
            sinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;
            if (sinceLastFrame > timeBetweenFrames)
            {
                frame.X++;
                sinceLastFrame = 0;
                if (frame.X == nrOfFrames.X - 1 && frame.Y == nrOfFrames.Y - 1)
                {
                    frame.X = 0;
                    frame.Y = 0;
                }
                else if (frame.X == nrOfFrames.X - 1 && frame.Y != nrOfFrames.Y - 1)
                {
                    frame.X = 0;
                    frame.Y++;
                }
            }
            srcRect.X = frame.X * Tex.Width / nrOfFrames.X;
            srcRect.Y = frame.Y * Tex.Height / nrOfFrames.Y;
            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Tex, rect, srcRect, Color.White);
        }
    }
}
