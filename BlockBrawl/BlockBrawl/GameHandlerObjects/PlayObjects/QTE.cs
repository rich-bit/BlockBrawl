using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BlockBrawl.Blocks;
using BlockBrawl.Objects;


namespace BlockBrawl
{
    class QTE
    {
        I i;
        J j;
        Z z;
        S s;
        L l;
        O o;
        T t;
        Vector2 pos;
        public QTE(Vector2 posP1, Vector2 posP2)
        {
            switch (SpawnRandomBlock())
            {
                case "J":
                    j = new J(TextureManager.qteColor, Vector2.Zero);
                    break;
                case "I":
                    i = new I(TextureManager.qteColor, Vector2.Zero);
                    break;
                case "T":
                    t = new T(TextureManager.qteColor, Vector2.Zero);
                    break;
                case "O":
                    o = new O(TextureManager.qteColor, Vector2.Zero);
                    break;
                case "L":
                    l = new L(TextureManager.qteColor, Vector2.Zero);
                    break;
                case "S":
                    s = new S(TextureManager.qteColor, Vector2.Zero);
                    break;
                case "Z":
                    z = new Z(TextureManager.qteColor, Vector2.Zero);
                    break;
            }

        }
        public void Update(GameTime gameTime, InputManager im)
        {
            if()
        }
        public string SpawnRandomBlock()
        {
            Random rnd = new Random();
            string[] feedRandomMachine = new string[] { "J", "I", "T", "O", "L", "S", "Z" };
            return feedRandomMachine[rnd.Next(0, feedRandomMachine.Length)];
        }
        public void Draw(SpriteBatch spritebatch)
        {
            if(i != null)
            {
                i.Draw(spritebatch);
            }
        }

    }
}
