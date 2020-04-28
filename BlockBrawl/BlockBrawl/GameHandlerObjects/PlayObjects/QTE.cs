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
        I i, iDotted;
        J j, jDotted;
        Z z, zDotted;
        S s, sDotted;
        L l, lDotted;
        O o, oDotted;
        T t, tDotted;
        Random rnd = new Random();
        public QTE()
        {
            switch (SpawnRandomBlock())
            {
                case "J":
                    j = new J(TextureManager.qteColor, Vector2.Zero);
                    jDotted = new J(TextureManager.qteDotted, j.jMatrix[rnd.Next(j.jMatrix.GetLength(0)), rnd.Next(j.jMatrix.GetLength(1))].Pos);
                    break;
                case "I":
                    i = new I(TextureManager.qteColor, Vector2.Zero);
                    iDotted = new I(TextureManager.qteDotted, i.iMatrix[rnd.Next(i.iMatrix.GetLength(0)), rnd.Next(i.iMatrix.GetLength(1))].Pos);
                    break;
                case "T":
                    t = new T(TextureManager.qteColor, Vector2.Zero);
                    tDotted = new T(TextureManager.qteDotted, t.tMatrix[rnd.Next(t.tMatrix.GetLength(0)), rnd.Next(t.tMatrix.GetLength(1))].Pos);
                    break;
                case "O":
                    o = new O(TextureManager.qteColor, Vector2.Zero);
                    oDotted = new O(TextureManager.qteDotted, o.oMatrix[rnd.Next(o.oMatrix.GetLength(0)), rnd.Next(o.oMatrix.GetLength(1))].Pos);
                    break;
                case "L":
                    l = new L(TextureManager.qteColor, Vector2.Zero);
                    lDotted = new L(TextureManager.qteDotted, l.lMatrix[rnd.Next(l.lMatrix.GetLength(0)), rnd.Next(l.lMatrix.GetLength(1))].Pos);
                    break;
                case "S":
                    s = new S(TextureManager.qteColor, Vector2.Zero);
                    sDotted = new S(TextureManager.qteDotted, s.sMatrix[rnd.Next(s.sMatrix.GetLength(0)), rnd.Next(s.sMatrix.GetLength(1))].Pos);
                    break;
                case "Z":
                    z = new Z(TextureManager.qteColor, Vector2.Zero);
                    zDotted = new Z(TextureManager.qteDotted, z.zMatrix[rnd.Next(z.zMatrix.GetLength(0)), rnd.Next(z.zMatrix.GetLength(1))].Pos);
                    break;
            }
        }
        public void Update(GameTime gameTime, InputManager im)
        {

        }
        public string SpawnRandomBlock()
        {
            Random rnd = new Random();
            string[] feedRandomMachine = new string[] { "J", "I", "T", "O", "L", "S", "Z" };
            return feedRandomMachine[rnd.Next(0, feedRandomMachine.Length)];
        }
        public void Draw(SpriteBatch spritebatch)
        {
            
            if (i != null)
            {
                iDotted.Draw(spritebatch);
                i.Draw(spritebatch);
            }
            if (j != null)
            {
                jDotted.Draw(spritebatch);
                j.Draw(spritebatch);
            }
            if (l != null)
            {
                lDotted.Draw(spritebatch);
                l.Draw(spritebatch);
            }
            if (s != null)
            {
                sDotted.Draw(spritebatch);
                s.Draw(spritebatch);
            }
            if (z != null)
            {
                zDotted.Draw(spritebatch);
                z.Draw(spritebatch);
            }
            if (t != null)
            {
                tDotted.Draw(spritebatch);
                t.Draw(spritebatch);
            }
            if (o != null)
            {
                oDotted.Draw(spritebatch);
                o.Draw(spritebatch);
            }
        }

    }
}
