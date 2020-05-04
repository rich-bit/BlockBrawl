using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockBrawl
{
    class GameObject
    {
        protected Vector2 pos;
        public Texture2D tex;
        protected Rectangle rect;
        public float transparency;
        public float Time { get; set; }
        public GameObject(Vector2 pos, Texture2D tex)
        {
            this.pos = pos;
            this.tex = tex;
            transparency = 1f;
            rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
        }
        public float PosX
        {
            get { return pos.X; }
            set { pos.X = value; rect.X = (int)pos.X; rect.Y = (int)pos.Y; }
        }
        public float PosY
        {
            get { return pos.Y; }
            set { pos.Y = value; rect.X = (int)pos.X; rect.Y = (int)pos.Y; }
        }
        public Rectangle Rect
        {
            get { return rect; }
        }
        public Vector2 Pos
        {
            get
            {
                return pos;
            }
            set
            {
                pos = value;
                rect.X = (int)Pos.X;
                rect.Y = (int)Pos.Y;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, rect, Color.White);
        }
        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(tex, rect, color * transparency);
        }
    }
}
