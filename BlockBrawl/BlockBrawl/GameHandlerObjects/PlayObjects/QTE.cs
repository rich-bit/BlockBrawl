using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BlockBrawl.Blocks;


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
        string[] playerBlocks;
        Vector2 tileSizeQTE, gameBoundaries;
        string playerOneName, playerTwoName;
        int playerOneIndex, playerTwoIndex;
        float timeLeft;
        bool Cleared { get; set; }
        int Winner { get; set; }
        public int WinnerIndex { get; }
        public QTE()
        {
            timeLeft = 5;

            playerOneName = SettingsManager.playerOneName;
            playerTwoName = SettingsManager.playerTwoName;

            playerOneIndex = SettingsManager.playerIndexOne;
            playerTwoIndex = SettingsManager.playerIndexTwo;

            gameBoundaries = new Vector2(SettingsManager.gameWidth, SettingsManager.gameHeight);
            tileSizeQTE = new Vector2(TextureManager.qteColor.Width, TextureManager.qteColor.Height);
            Vector2 randomOffset = RandomOffset(3);
            int count = 0;
            int rotateFewTiles = rnd.Next(1, 6);
            playerBlocks = new string[2];
            string oldString = null;

            do
            {
                string myString = SpawnRandomBlock(oldString);
                switch (myString)
                {
                    case "J":
                        jDotted = new J(TextureManager.qteDotted,
                            GetMiddlePosition(
                            gameBoundaries,
                            new Vector2(
                            new J(TextureManager.transBlock, Vector2.Zero).jMatrix.GetLength(0) * tileSizeQTE.X,
                            new J(TextureManager.transBlock, Vector2.Zero).jMatrix.GetLength(1) * tileSizeQTE.X
                            ), count)
                            );
                        j = new J(TextureManager.qteColor, jDotted.jMatrix[0, 0].Pos + randomOffset);
                        for (int i = 0; i < rotateFewTiles; i++)
                        {
                            j.Rotate(true);
                        }
                        break;
                    case "I":
                        iDotted = new I(TextureManager.qteDotted,
                            GetMiddlePosition(
                            gameBoundaries,
                            new Vector2(
                            new I(TextureManager.transBlock, Vector2.Zero).iMatrix.GetLength(0) * tileSizeQTE.X,
                            new I(TextureManager.transBlock, Vector2.Zero).iMatrix.GetLength(1) * tileSizeQTE.X
                            ), count)
                            );
                        i = new I(TextureManager.qteColor, iDotted.iMatrix[0, 0].Pos + randomOffset);
                        for (int j = 0; j < rotateFewTiles; j++)
                        {
                            i.Rotate(true);
                        }
                        break;
                    case "T":
                        tDotted = new T(TextureManager.qteDotted,
                            GetMiddlePosition(
                            gameBoundaries,
                            new Vector2(
                            new T(TextureManager.transBlock, Vector2.Zero).tMatrix.GetLength(0) * tileSizeQTE.X,
                            new T(TextureManager.transBlock, Vector2.Zero).tMatrix.GetLength(1) * tileSizeQTE.X
                            ), count)
                            );
                        t = new T(TextureManager.qteColor, tDotted.tMatrix[0, 0].Pos + randomOffset);
                        for (int i = 0; i < rotateFewTiles; i++)
                        {
                            t.Rotate(true);
                        }
                        break;
                    case "O":
                        oDotted = new O(TextureManager.qteDotted,
                            GetMiddlePosition(
                            gameBoundaries,
                            new Vector2(
                            new O(TextureManager.transBlock, Vector2.Zero).oMatrix.GetLength(0) * tileSizeQTE.X,
                            new O(TextureManager.transBlock, Vector2.Zero).oMatrix.GetLength(1) * tileSizeQTE.X
                            ), count)
                            );
                        o = new O(TextureManager.qteColor, oDotted.oMatrix[0, 0].Pos + randomOffset);
                        for (int i = 0; i < rotateFewTiles; i++)
                        {
                            o.Rotate(true);
                        }
                        break;
                    case "L":
                        lDotted = new L(TextureManager.qteDotted,
                            GetMiddlePosition(
                            gameBoundaries,
                            new Vector2(
                            new L(TextureManager.transBlock, Vector2.Zero).lMatrix.GetLength(0) * tileSizeQTE.X,
                            new L(TextureManager.transBlock, Vector2.Zero).lMatrix.GetLength(1) * tileSizeQTE.X
                            ), count)
                            );
                        l = new L(TextureManager.qteColor, lDotted.lMatrix[0, 0].Pos + randomOffset);
                        for (int i = 0; i < rotateFewTiles; i++)
                        {
                            l.Rotate(true);
                        }
                        break;
                    case "S":
                        sDotted = new S(TextureManager.qteDotted,
                            GetMiddlePosition(
                            gameBoundaries,
                            new Vector2(
                            new S(TextureManager.transBlock, Vector2.Zero).sMatrix.GetLength(0) * tileSizeQTE.X,
                            new S(TextureManager.transBlock, Vector2.Zero).sMatrix.GetLength(1) * tileSizeQTE.X
                            ), count)
                            );
                        s = new S(TextureManager.qteColor, sDotted.sMatrix[0, 0].Pos + randomOffset);
                        for (int i = 0; i < rotateFewTiles; i++)
                        {
                            s.Rotate(true);
                        }
                        break;
                    case "Z":
                        zDotted = new Z(TextureManager.qteDotted,
                            GetMiddlePosition(
                            gameBoundaries,
                            new Vector2(
                            new Z(TextureManager.transBlock, Vector2.Zero).zMatrix.GetLength(0) * tileSizeQTE.X,
                            new Z(TextureManager.transBlock, Vector2.Zero).zMatrix.GetLength(1) * tileSizeQTE.X
                            ), count)
                            );
                        z = new Z(TextureManager.qteColor, zDotted.zMatrix[0, 0].Pos + randomOffset);
                        break;
                }
                playerBlocks[count] = myString;
                count++;
                oldString = myString;
            } while (count < 2);
        }
        private Vector2 GetMiddlePosition(Vector2 gameBoundaries, Vector2 lenght, int playerIndex)
        {
            float playerIndexCorrection = gameBoundaries.X / 4;
            float x = gameBoundaries.X / 2;
            float y = gameBoundaries.Y / 2;
            if (playerIndex == 0)
            {
                x += playerIndexCorrection;
            }
            else
            {
                x -= playerIndexCorrection;
            }

            x -= lenght.X / 2;
            y -= lenght.Y / 2;

            return new Vector2(x, y);
        }
        private Vector2 RandomOffset(int maxOffset)
        {
            int offsetX = rnd.Next(1, maxOffset + 1);
            if (rnd.Next(2) == 0)
            {
                offsetX *= -1;
            }
            int offsetY = rnd.Next(1, maxOffset + 1);
            if (rnd.Next(2) == 0)
            {
                offsetY *= -1;
            }
            return new Vector2(offsetX * tileSizeQTE.X, offsetY * tileSizeQTE.Y);
        }
        public void Status(GameTime gameTime)
        {
            if (timeLeft >= 0f)
            {
                timeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Cleared = true;
            }
        }
        public void Update(InputManager iM, int playerIndex, bool gamePad)
        {

            if (gamePad && playerBlocks[playerIndex] != null)
            {
                CheckGamePadInputs(playerIndex, iM);
            }
            else
            {
                CheckKeyboardInput(playerIndex, iM);
            }
        }
        private void CheckKeyboardInput(int playerIndex, InputManager iM)
        {
            Keys up = Keys.W;
            Keys left = Keys.A;
            Keys down = Keys.S;
            Keys right = Keys.D;
            Keys rotateCW = Keys.Space;
            Keys rotateCC = Keys.LeftShift;

            if (playerIndex == playerTwoIndex)
            {
                up = Keys.Up;
                left = Keys.Left;
                down = Keys.Down;
                right = Keys.Right;
                rotateCW = Keys.NumPad0;
                rotateCC = Keys.Enter;
            }

            switch (playerBlocks[playerIndex])
            {
                case "J":
                    if (iM.JustPressed(rotateCW))
                    {
                        j.Rotate(true);
                    }
                    if (iM.JustPressed(rotateCC))
                    {
                        j.Rotate(false);
                    }
                    if (iM.JustPressed(up))
                    {
                        j.Fall(-tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(down))
                    {
                        j.Fall(tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(left))
                    {
                        j.Move(-tileSizeQTE.X);
                    }
                    if (iM.JustPressed(right))
                    {
                        j.Move(tileSizeQTE.X);
                    }
                    break;
                case "I":
                    if (iM.JustPressed(rotateCW))
                    {
                        i.Rotate(true);
                    }
                    if (iM.JustPressed(rotateCC))
                    {
                        i.Rotate(false);
                    }
                    if (iM.JustPressed(up))
                    {
                        i.Fall(-tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(down))
                    {
                        i.Fall(tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(left))
                    {
                        i.Move(-tileSizeQTE.X);
                    }
                    if (iM.JustPressed(right))
                    {
                        i.Move(tileSizeQTE.X);
                    }
                    break;
                case "T":
                    if (iM.JustPressed(rotateCW))
                    {
                        t.Rotate(true);
                    }
                    if (iM.JustPressed(rotateCC))
                    {
                        t.Rotate(false);
                    }
                    if (iM.JustPressed(up))
                    {
                        t.Fall(-tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(down))
                    {
                        t.Fall(tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(left))
                    {
                        t.Move(-tileSizeQTE.X);
                    }
                    if (iM.JustPressed(right))
                    {
                        t.Move(tileSizeQTE.X);
                    }
                    break;
                case "O":
                    if (iM.JustPressed(rotateCW))
                    {
                        o.Rotate(true);
                    }
                    if (iM.JustPressed(rotateCC))
                    {
                        o.Rotate(false);
                    }
                    if (iM.JustPressed(up))
                    {
                        o.Fall(-tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(down))
                    {
                        o.Fall(tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(left))
                    {
                        o.Move(-tileSizeQTE.X);
                    }
                    if (iM.JustPressed(right))
                    {
                        o.Move(tileSizeQTE.X);
                    }
                    break;
                case "L":
                    if (iM.JustPressed(rotateCW))
                    {
                        l.Rotate(true);
                    }
                    if (iM.JustPressed(rotateCC))
                    {
                        l.Rotate(false);
                    }
                    if (iM.JustPressed(up))
                    {
                        l.Fall(-tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(down))
                    {
                        l.Fall(tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(left))
                    {
                        l.Move(-tileSizeQTE.X);
                    }
                    if (iM.JustPressed(right))
                    {
                        l.Move(tileSizeQTE.X);
                    }
                    break;
                case "S":
                    if (iM.JustPressed(rotateCW))
                    {
                        s.Rotate(true);
                    }
                    if (iM.JustPressed(rotateCC))
                    {
                        s.Rotate(false);
                    }
                    if (iM.JustPressed(up))
                    {
                        s.Fall(-tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(down))
                    {
                        s.Fall(tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(left))
                    {
                        s.Move(-tileSizeQTE.X);
                    }
                    if (iM.JustPressed(right))
                    {
                        s.Move(tileSizeQTE.X);
                    }
                    break;
                case "Z":
                    if (iM.JustPressed(rotateCW))
                    {
                        z.Rotate(true);
                    }
                    if (iM.JustPressed(rotateCC))
                    {
                        z.Rotate(false);
                    }
                    if (iM.JustPressed(up))
                    {
                        z.Fall(-tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(down))
                    {
                        z.Fall(tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(left))
                    {
                        z.Move(-tileSizeQTE.X);
                    }
                    if (iM.JustPressed(right))
                    {
                        z.Move(tileSizeQTE.X);
                    }
                    break;
            }
        }
        private void CheckGamePadInputs(int playerIndex, InputManager iM)
        {
            switch (playerBlocks[playerIndex])
            {
                case "J":
                    if (iM.JustPressed(Buttons.B, playerIndex))
                    {
                        j.Rotate(true);
                    }
                    if (iM.JustPressed(Buttons.Y, playerIndex))
                    {
                        j.Rotate(false);
                    }
                    if (iM.JustPressed(Buttons.DPadUp, playerIndex))
                    {
                        j.Fall(-tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(Buttons.DPadDown, playerIndex))
                    {
                        j.Fall(tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(Buttons.DPadLeft, playerIndex))
                    {
                        j.Move(-tileSizeQTE.X);
                    }
                    if (iM.JustPressed(Buttons.DPadRight, playerIndex))
                    {
                        j.Move(tileSizeQTE.X);
                    }
                    break;
                case "I":
                    if (iM.JustPressed(Buttons.B, playerIndex))
                    {
                        i.Rotate(true);
                    }
                    if (iM.JustPressed(Buttons.Y, playerIndex))
                    {
                        i.Rotate(false);
                    }
                    if (iM.JustPressed(Buttons.DPadUp, playerIndex))
                    {
                        i.Fall(-tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(Buttons.DPadDown, playerIndex))
                    {
                        i.Fall(tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(Buttons.DPadLeft, playerIndex))
                    {
                        i.Move(-tileSizeQTE.X);
                    }
                    if (iM.JustPressed(Buttons.DPadRight, playerIndex))
                    {
                        i.Move(tileSizeQTE.X);
                    }
                    break;
                case "T":
                    if (iM.JustPressed(Buttons.B, playerIndex))
                    {
                        t.Rotate(true);
                    }
                    if (iM.JustPressed(Buttons.Y, playerIndex))
                    {
                        t.Rotate(false);
                    }
                    if (iM.JustPressed(Buttons.DPadUp, playerIndex))
                    {
                        t.Fall(-tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(Buttons.DPadDown, playerIndex))
                    {
                        t.Fall(tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(Buttons.DPadLeft, playerIndex))
                    {
                        t.Move(-tileSizeQTE.X);
                    }
                    if (iM.JustPressed(Buttons.DPadRight, playerIndex))
                    {
                        t.Move(tileSizeQTE.X);
                    }
                    break;
                case "O":
                    if (iM.JustPressed(Buttons.B, playerIndex))
                    {
                        o.Rotate(true);
                    }
                    if (iM.JustPressed(Buttons.Y, playerIndex))
                    {
                        o.Rotate(false);
                    }
                    if (iM.JustPressed(Buttons.DPadUp, playerIndex))
                    {
                        o.Fall(-tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(Buttons.DPadDown, playerIndex))
                    {
                        o.Fall(tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(Buttons.DPadLeft, playerIndex))
                    {
                        o.Move(-tileSizeQTE.X);
                    }
                    if (iM.JustPressed(Buttons.DPadRight, playerIndex))
                    {
                        o.Move(tileSizeQTE.X);
                    }
                    break;
                case "L":
                    if (iM.JustPressed(Buttons.B, playerIndex))
                    {
                        l.Rotate(true);
                    }
                    if (iM.JustPressed(Buttons.Y, playerIndex))
                    {
                        l.Rotate(false);
                    }
                    if (iM.JustPressed(Buttons.DPadUp, playerIndex))
                    {
                        l.Fall(-tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(Buttons.DPadDown, playerIndex))
                    {
                        l.Fall(tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(Buttons.DPadLeft, playerIndex))
                    {
                        l.Move(-tileSizeQTE.X);
                    }
                    if (iM.JustPressed(Buttons.DPadRight, playerIndex))
                    {
                        l.Move(tileSizeQTE.X);
                    }
                    break;
                case "S":
                    if (iM.JustPressed(Buttons.B, playerIndex))
                    {
                        s.Rotate(true);
                    }
                    if (iM.JustPressed(Buttons.Y, playerIndex))
                    {
                        s.Rotate(false);
                    }
                    if (iM.JustPressed(Buttons.DPadUp, playerIndex))
                    {
                        s.Fall(-tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(Buttons.DPadDown, playerIndex))
                    {
                        s.Fall(tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(Buttons.DPadLeft, playerIndex))
                    {
                        s.Move(-tileSizeQTE.X);
                    }
                    if (iM.JustPressed(Buttons.DPadRight, playerIndex))
                    {
                        s.Move(tileSizeQTE.X);
                    }
                    break;
                case "Z":
                    if (iM.JustPressed(Buttons.B, playerIndex))
                    {
                        z.Rotate(true);
                    }
                    if (iM.JustPressed(Buttons.Y, playerIndex))
                    {
                        z.Rotate(false);
                    }
                    if (iM.JustPressed(Buttons.DPadUp, playerIndex))
                    {
                        z.Fall(-tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(Buttons.DPadDown, playerIndex))
                    {
                        z.Fall(tileSizeQTE.Y);
                    }
                    if (iM.JustPressed(Buttons.DPadLeft, playerIndex))
                    {
                        z.Move(-tileSizeQTE.X);
                    }
                    if (iM.JustPressed(Buttons.DPadRight, playerIndex))
                    {
                        z.Move(tileSizeQTE.X);
                    }
                    break;
            }
        }
        public string SpawnRandomBlock(string lastRandom)
        {
            Random rnd = new Random();
            List<string> randomStrings = new List<string>
            {
                "J",
                "I",
                "T",
                "O",
                "L",
                "S",
                "Z"
            };
            if (lastRandom != null)
            {
                randomStrings.Remove(lastRandom);
                randomStrings.Sort();
            }
            string randomString = randomStrings[rnd.Next(randomStrings.Count)];
            return randomString;
        }
        private Vector2 TextPosition(Vector2 gameBoundaries, SpriteFont spriteFont, string text)
        {
            float x = gameBoundaries.X / 2;
            float margin = tileSizeQTE.Y;
            float y = 0 + margin;

            x -= spriteFont.MeasureString(text).X / 2;

            return new Vector2(x, y);
        }
        private Vector2 TextPosition(Vector2 gameBoundaries, SpriteFont spriteFont, string text, float margin)
        {
            float x = gameBoundaries.X / 2;
            float y = 0 + margin;

            x -= spriteFont.MeasureString(text).X / 2;

            return new Vector2(x, y);
        }
        private Vector2 TextPosition(int playerIndex, Vector2 gameBoundaries, SpriteFont spriteFont, string text)
        {
            float playerIndexCorrection = gameBoundaries.X / 4;
            float x = gameBoundaries.X / 2;
            float margin = tileSizeQTE.Y;
            float y = 0 + margin;
            if (playerIndex == 0)
            {
                x -= playerIndexCorrection;
            }
            else
            {
                x += playerIndexCorrection;
            }

            x -= spriteFont.MeasureString(text).X / 2;

            return new Vector2(x, y);
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
            spritebatch.DrawString(FontManager.MenuText, playerOneName,
                TextPosition(
                playerOneIndex, gameBoundaries, FontManager.MenuText, playerOneName
                ),
                Color.Red);
            spritebatch.DrawString(FontManager.MenuText, "VS",
            TextPosition(
            gameBoundaries, FontManager.MenuText, "VS"
                ),
            Color.Red);
            spritebatch.DrawString(FontManager.MenuText, playerTwoName,
                TextPosition(
                playerTwoIndex, gameBoundaries, FontManager.MenuText, playerTwoName
                ),
                Color.Red);
            spritebatch.DrawString(FontManager.MenuText, "Time Left " + Convert.ToInt32(timeLeft).ToString(),
            TextPosition(
                gameBoundaries,
                FontManager.MenuText, "Time Left " + Convert.ToInt32(timeLeft).ToString(),
                SettingsManager.gameHeight / 2
                ),
            Color.Red);
        }
    }
}
