using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        string[] playerBlocks;
        Vector2 tileSizeQTE, gameBoundaries;
        string playerOneName, playerTwoName;
        int playerOneIndex, playerTwoIndex;
        float timeLeft, textTransparecy;
        public bool Cleared { get; set; }
        public int Winner { get; set; }
        public QTE()
        {
            timeLeft = 5;
            textTransparecy = 0f;
            Winner = int.MinValue;

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
                        foreach (TetrisObject item in j.jMatrix) { item.transparency = 0f; }
                        foreach (TetrisObject item in jDotted.jMatrix) { item.transparency = 0f; }
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
                        foreach (TetrisObject item in i.iMatrix) { item.transparency = 0f; }
                        foreach (TetrisObject item in iDotted.iMatrix) { item.transparency = 0f; }
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
                        foreach (TetrisObject item in t.tMatrix) { item.transparency = 0f; }
                        foreach (TetrisObject item in tDotted.tMatrix) { item.transparency = 0f; }
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
                        foreach (TetrisObject item in o.oMatrix) { item.transparency = 0f; }
                        foreach (TetrisObject item in oDotted.oMatrix) { item.transparency = 0f; }
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
                        foreach (TetrisObject item in l.lMatrix) { item.transparency = 0f; }
                        foreach (TetrisObject item in lDotted.lMatrix) { item.transparency = 0f; }
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
                        foreach (TetrisObject item in s.sMatrix) { item.transparency = 0f; }
                        foreach (TetrisObject item in sDotted.sMatrix) { item.transparency = 0f; }
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
                        foreach (TetrisObject item in zDotted.zMatrix) { item.transparency = 0f; }
                        foreach (TetrisObject item in z.zMatrix) { item.transparency = 0f; }
                        for (int i = 0; i < rotateFewTiles; i++)
                        {
                            z.Rotate(true);
                        }
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
                x -= playerIndexCorrection;
            }
            else
            {
                x += playerIndexCorrection;
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
            if (timeLeft >= 0f && Winner != playerOneIndex && Winner != playerTwoIndex)
            {
                timeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
        public void Update(InputManager iM, int playerIndex, bool gamePad, GameTime gameTime)
        {
            if (timeLeft > 0f && Winner != playerOneIndex && Winner != playerTwoIndex)
            {
                FadeIn(gameTime, playerIndex);
            }
            if (Winner != playerOneIndex || Winner != playerTwoIndex)
            {
                CheckWinner(playerIndex);
            }
            if (gamePad && playerBlocks[playerIndex] != null && timeLeft > 0f && Winner == int.MinValue)
            {
                CheckGamePadInputs(playerIndex, iM);
            }
            else if (Winner == int.MinValue && timeLeft > 0f)
            {
                CheckKeyboardInput(playerIndex, iM);
            }
            if (timeLeft <= 0f || Winner == playerOneIndex || Winner == playerTwoIndex)
            {
                FadeOut(gameTime, playerIndex);
            }
        }
        private void CheckCleared(TetrisObject[,] tetrisObjects)
        {
            int count = 0;
            foreach (TetrisObject item in tetrisObjects)
            {
                if(item.transparency <= 0f)
                {
                    count++;
                }
            }
            if(count == tetrisObjects.GetLength(0) * tetrisObjects.GetLength(1))
            {
                Cleared = true;
            }
        }
        private void FadeOut(GameTime gameTime, int playerIndex)
        {
            float decrement = 0.1f;
            float timeBetweenIncr = 0.1f;

            switch (playerBlocks[playerIndex])
            {
                case "J":
                    for (int i = 0; i < j.jMatrix.GetLength(0); i++)
                    {
                        for (int k = 0; k < j.jMatrix.GetLength(1); k++)
                        {
                            if (j.jMatrix[i, k].transparency > 0f)
                            {
                                j.Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                if (j.Time >= timeBetweenIncr)
                                {
                                    j.jMatrix[i, k].transparency -= decrement;
                                    jDotted.jMatrix[i, k].transparency -= decrement;
                                    j.Time = 0f;
                                }
                            }
                        }
                    }
                    FadeOutTexts(j.Time, decrement, gameTime);
                    CheckCleared(j.jMatrix);
                    break;
                case "L":
                    for (int i = 0; i < l.lMatrix.GetLength(0); i++)
                    {
                        for (int k = 0; k < l.lMatrix.GetLength(1); k++)
                        {
                            if (l.lMatrix[i, k].transparency > 0f)
                            {
                                l.Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                if (l.Time >= timeBetweenIncr)
                                {
                                    l.lMatrix[i, k].transparency -= decrement;
                                    lDotted.lMatrix[i, k].transparency -= decrement;
                                    l.Time = 0f;
                                }
                            }
                        }
                    }
                    FadeOutTexts(l.Time, decrement, gameTime);
                    CheckCleared(l.lMatrix);
                    break;
                case "T":
                    for (int i = 0; i < t.tMatrix.GetLength(0); i++)
                    {
                        for (int k = 0; k < t.tMatrix.GetLength(1); k++)
                        {
                            if (t.tMatrix[i, k].transparency > 0f)
                            {
                                t.Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                if (t.Time >= timeBetweenIncr)
                                {
                                    t.tMatrix[i, k].transparency -= decrement;
                                    tDotted.tMatrix[i, k].transparency -= decrement;
                                    t.Time = 0f;
                                }
                            }
                        }
                    }
                    FadeOutTexts(t.Time, decrement, gameTime);
                    CheckCleared(t.tMatrix);
                    break;
                case "I":
                    for (int f = 0; f < i.iMatrix.GetLength(0); f++)
                    {
                        for (int k = 0; k < i.iMatrix.GetLength(1); k++)
                        {
                            if (i.iMatrix[f, k].transparency > 0f)
                            {
                                i.Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                if (i.Time >= timeBetweenIncr)
                                {
                                    i.iMatrix[f, k].transparency -= decrement;
                                    iDotted.iMatrix[f, k].transparency -= decrement;
                                    i.Time = 0f;
                                }
                            }
                        }
                    }
                    FadeOutTexts(i.Time, decrement, gameTime);
                    CheckCleared(i.iMatrix);
                    break;
                case "Z":
                    for (int i = 0; i < z.zMatrix.GetLength(0); i++)
                    {
                        for (int k = 0; k < z.zMatrix.GetLength(1); k++)
                        {
                            if (z.zMatrix[i, k].transparency > 0f)
                            {
                                z.Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                if (z.Time >= timeBetweenIncr)
                                {
                                    z.zMatrix[i, k].transparency -= decrement;
                                    zDotted.zMatrix[i, k].transparency -= decrement;
                                    z.Time = 0f;
                                }
                            }
                        }
                    }
                    FadeOutTexts(z.Time, decrement, gameTime);
                    CheckCleared(z.zMatrix);
                    break;
                case "S":
                    for (int i = 0; i < s.sMatrix.GetLength(0); i++)
                    {
                        for (int k = 0; k < s.sMatrix.GetLength(1); k++)
                        {
                            if (s.sMatrix[i, k].transparency > 0f)
                            {
                                s.Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                if (s.Time >= timeBetweenIncr)
                                {
                                    s.sMatrix[i, k].transparency -= decrement;
                                    sDotted.sMatrix[i, k].transparency -= decrement;
                                    s.Time = 0f;
                                }
                            }
                        }
                    }
                    FadeOutTexts(s.Time, decrement, gameTime);
                    CheckCleared(s.sMatrix);
                    break;
                case "O":
                    for (int i = 0; i < o.oMatrix.GetLength(0); i++)
                    {
                        for (int k = 0; k < o.oMatrix.GetLength(1); k++)
                        {
                            if (o.oMatrix[i, k].transparency > 0f)
                            {
                                o.Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                if (o.Time >= timeBetweenIncr)
                                {
                                    o.oMatrix[i, k].transparency -= decrement;
                                    oDotted.oMatrix[i, k].transparency -= decrement;
                                    o.Time = 0f;
                                }
                            }
                        }
                    }
                    FadeOutTexts(o.Time, decrement, gameTime);
                    CheckCleared(o.oMatrix);
                    break;
            }
        }
        private void FadeIn(GameTime gameTime, int playerIndex)
        {
            float increment = 0.1f;
            float timeBetweenIncr = 0.1f;

            switch (playerBlocks[playerIndex])
            {
                case "J":
                    for (int i = 0; i < j.jMatrix.GetLength(0); i++)
                    {
                        for (int k = 0; k < j.jMatrix.GetLength(1); k++)
                        {
                            if (j.jMatrix[i, k].transparency < 1f)
                            {
                                j.Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                if (j.Time >= timeBetweenIncr)
                                {
                                    j.jMatrix[i, k].transparency += increment;
                                    jDotted.jMatrix[i, k].transparency += increment;
                                    j.Time = 0f;
                                }
                            }
                        }
                    }
                    FadeInTexts(j.Time, increment, gameTime);
                    break;
                case "L":
                    for (int i = 0; i < l.lMatrix.GetLength(0); i++)
                    {
                        for (int k = 0; k < l.lMatrix.GetLength(1); k++)
                        {
                            if (l.lMatrix[i, k].transparency < 1f)
                            {
                                l.Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                if (l.Time >= timeBetweenIncr)
                                {
                                    l.lMatrix[i, k].transparency += increment;
                                    lDotted.lMatrix[i, k].transparency += increment;
                                    l.Time = 0f;
                                }
                            }
                        }
                    }
                    FadeInTexts(l.Time, increment, gameTime);
                    break;
                case "T":
                    for (int i = 0; i < t.tMatrix.GetLength(0); i++)
                    {
                        for (int k = 0; k < t.tMatrix.GetLength(1); k++)
                        {
                            if (t.tMatrix[i, k].transparency < 1f)
                            {
                                t.Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                if (t.Time >= timeBetweenIncr)
                                {
                                    t.tMatrix[i, k].transparency += increment;
                                    tDotted.tMatrix[i, k].transparency += increment;
                                    t.Time = 0f;
                                }
                            }
                        }
                    }
                    FadeInTexts(t.Time, increment, gameTime);
                    break;
                case "I":
                    for (int f = 0; f < i.iMatrix.GetLength(0); f++)
                    {
                        for (int k = 0; k < i.iMatrix.GetLength(1); k++)
                        {
                            if (i.iMatrix[f, k].transparency < 1f)
                            {
                                i.Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                if (i.Time >= timeBetweenIncr)
                                {
                                    i.iMatrix[f, k].transparency += increment;
                                    iDotted.iMatrix[f, k].transparency += increment;
                                    i.Time = 0f;
                                }
                            }
                        }
                    }
                    FadeInTexts(i.Time, increment, gameTime);
                    break;
                case "Z":
                    for (int i = 0; i < z.zMatrix.GetLength(0); i++)
                    {
                        for (int k = 0; k < z.zMatrix.GetLength(1); k++)
                        {
                            if (z.zMatrix[i, k].transparency < 1f)
                            {
                                z.Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                if (z.Time >= timeBetweenIncr)
                                {
                                    z.zMatrix[i, k].transparency += increment;
                                    zDotted.zMatrix[i, k].transparency += increment;
                                    z.Time = 0f;
                                }
                            }
                        }
                    }
                    FadeInTexts(z.Time, increment, gameTime);
                    break;
                case "S":
                    for (int i = 0; i < s.sMatrix.GetLength(0); i++)
                    {
                        for (int k = 0; k < s.sMatrix.GetLength(1); k++)
                        {
                            if (s.sMatrix[i, k].transparency < 1f)
                            {
                                s.Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                if (s.Time >= timeBetweenIncr)
                                {
                                    s.sMatrix[i, k].transparency += increment;
                                    sDotted.sMatrix[i, k].transparency += increment;
                                    s.Time = 0f;
                                }
                            }
                        }
                    }
                    FadeInTexts(s.Time, increment, gameTime);
                    break;
                case "O":
                    for (int i = 0; i < o.oMatrix.GetLength(0); i++)
                    {
                        for (int k = 0; k < o.oMatrix.GetLength(1); k++)
                        {
                            if (o.oMatrix[i, k].transparency < 1f)
                            {
                                o.Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                if (o.Time >= timeBetweenIncr)
                                {
                                    o.oMatrix[i, k].transparency += increment;
                                    oDotted.oMatrix[i, k].transparency += increment;
                                    o.Time = 0f;
                                }
                            }
                        }
                    }
                    FadeInTexts(o.Time, increment, gameTime);
                    break;
            }
        }
        private void FadeInTexts(float timeCounter, float increment, GameTime gameTime)
        {
            timeCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeCounter > 0.1f)
            {
                textTransparecy += increment;
                timeCounter = 0f;
            }
        }
        private void FadeOutTexts(float timeCounter, float increment, GameTime gameTime)
        {
            timeCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeCounter > 0.1f)
            {
                textTransparecy -= increment * 2;
                timeCounter = 0f;
            }
        }
        private void CheckKeyboardInput(int playerIndex, InputManager iM)
        {
            Keys up = Keys.W;
            Keys left = Keys.A;
            Keys down = Keys.S;
            Keys right = Keys.D;
            Keys rotateCW = Keys.F;
            Keys rotateCC = Keys.LeftShift;

            if (playerIndex == playerTwoIndex)
            {
                up = Keys.Up;
                left = Keys.Left;
                down = Keys.Down;
                right = Keys.Right;
                rotateCW = Keys.NumPad0;
                rotateCC = Keys.RightControl;
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
            if (iM.JustPressed(Buttons.DPadUp, playerIndex) ||
                iM.JustPressed(Buttons.DPadLeft, playerIndex) ||
                iM.JustPressed(Buttons.DPadRight, playerIndex) ||
                iM.JustPressed(Buttons.DPadDown, playerIndex) ||
                iM.JustPressed(Buttons.B, playerIndex) ||
                iM.JustPressed(Buttons.Y, playerIndex))
            {
                CheckWinner(playerIndex);
            }
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
        public void AssignWinner(TetrisObject[,] pArray, TetrisObject[,] pDottedArray, int playerIndex)
        {
            List<Vector2> correctPositions = new List<Vector2>();
            foreach (TetrisObject item in pDottedArray) { if (item.alive) { correctPositions.Add(item.Pos); } }
            int bricks = 4;
            int count = 0;
            for (int i = 0; i < pArray.GetLength(0); i++)
            {
                for (int j = 0; j < pArray.GetLength(1); j++)
                {
                    foreach (Vector2 pos in correctPositions)
                    {
                        if (pArray[i, j].alive && pArray[i, j].Pos == pos)
                        {
                            count++;
                        }
                    }
                }
            }
            if (bricks == count)
            {
                Winner = playerIndex;
                //Cleared = true;
            }
        }
        public void CheckWinner(int playerIndex)
        {
            switch (playerBlocks[playerIndex])
            {
                case "J":
                    AssignWinner(j.jMatrix, jDotted.jMatrix, playerIndex);
                    break;
                case "L":
                    AssignWinner(l.lMatrix, lDotted.lMatrix, playerIndex);
                    break;
                case "T":
                    AssignWinner(t.tMatrix, tDotted.tMatrix, playerIndex);
                    break;
                case "I":
                    AssignWinner(i.iMatrix, iDotted.iMatrix, playerIndex);
                    break;
                case "Z":
                    AssignWinner(z.zMatrix, zDotted.zMatrix, playerIndex);
                    break;
                case "S":
                    AssignWinner(s.sMatrix, sDotted.sMatrix, playerIndex);
                    break;
                case "O":
                    AssignWinner(o.oMatrix, oDotted.oMatrix, playerIndex);
                    break;
            }
        }
        public void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {

            if (i != null)
            {
                iDotted.Draw(spritebatch, Color.White, gameTime);
                i.Draw(spritebatch, Color.White, gameTime);
            }
            if (j != null)
            {
                jDotted.Draw(spritebatch, Color.White, gameTime);
                j.Draw(spritebatch, Color.White, gameTime);
            }
            if (l != null)
            {
                lDotted.Draw(spritebatch, Color.White, gameTime);
                l.Draw(spritebatch, Color.White, gameTime);
            }
            if (s != null)
            {
                sDotted.Draw(spritebatch, Color.White, gameTime);
                s.Draw(spritebatch, Color.White, gameTime);
            }
            if (z != null)
            {
                zDotted.Draw(spritebatch, Color.White, gameTime);
                z.Draw(spritebatch, Color.White, gameTime);
            }
            if (t != null)
            {
                tDotted.Draw(spritebatch, Color.White, gameTime);
                t.Draw(spritebatch, Color.White, gameTime);
            }
            if (o != null)
            {
                oDotted.Draw(spritebatch, Color.White, gameTime);
                o.Draw(spritebatch, Color.White, gameTime);
            }
            spritebatch.DrawString(FontManager.MenuText, playerOneName,
                TextPosition(
                playerOneIndex, gameBoundaries, FontManager.MenuText, playerOneName
                ),
                Color.Red * textTransparecy);
            spritebatch.DrawString(FontManager.MenuText, "VS",
            TextPosition(
            gameBoundaries, FontManager.MenuText, "VS"
                ),
            Color.Red * textTransparecy);
            spritebatch.DrawString(FontManager.MenuText, playerTwoName,
                TextPosition(
                playerTwoIndex, gameBoundaries, FontManager.MenuText, playerTwoName
                ),
                Color.Red * textTransparecy);
            spritebatch.DrawString(FontManager.MenuText, "Time Left " + Convert.ToInt32(timeLeft).ToString(),
            TextPosition(
                gameBoundaries,
                FontManager.MenuText, "Time Left " + Convert.ToInt32(timeLeft).ToString(),
                SettingsManager.gameHeight / 2
                ),
            Color.Red * textTransparecy);
            if (Winner != int.MinValue)
            {
                spritebatch.DrawString(FontManager.MenuText, "Player " + (Winner + 1).ToString() + " wins!!",
                    TextPosition(
                        gameBoundaries,
                        FontManager.MenuText, "Player " + (Winner + 1).ToString() + " wins!!",
                        SettingsManager.gameHeight - tileSizeQTE.Y
                        ),
                            Color.Yellow * textTransparecy);
            }
        }
    }
}
