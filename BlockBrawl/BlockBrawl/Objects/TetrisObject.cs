using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockBrawl.Objects
{
    class TetrisObject : GameObject
    {
        private Texture2D deadTex;
        private Texture2D aliveTex;
        public bool alive;
        public bool Shot { get; set; }
        public TetrisObject(Vector2 pos, Texture2D tex) : base(pos, tex)
        {
            aliveTex = tex;
            deadTex = TextureManager.emptyBlock;
        }
        public void ChangeState(bool kill)
        {
            if (kill)
            {
                Tex = deadTex; alive = false;
            }
            else { if (!Shot) { Tex = aliveTex; alive = true; } }
        }
    }
}
