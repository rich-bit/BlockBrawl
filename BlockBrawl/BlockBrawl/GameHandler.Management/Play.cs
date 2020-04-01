using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BlockBrawl.Blocks;
using BlockBrawl.Objects;
using System.Collections.Generic;

namespace BlockBrawl
{
    class Play
    {
        GameTime gameTime;
        Random rnd = new Random();

        Texture2D[] playerColors;

        GameObject[,] playfield;
        int marginRight = 3; // to Avoid spawn outside map, will be changed.

        int[] score;

        Vector2 tileSize;
        Vector2[] spawnPositions;

        //Each block has its own class. The array will use index 1, 2 for player 1 and 2. Index 0 is not used atm.
        I[] iArray;
        J[] jArray;

        //Nextblock, will switch this array for spawn.
        string[] nextBlock;

        //Will be the stack of stopped blockes.
        TetrisObject[,] stackedBlocks;

        //Our own inputmanager, for the NES replicas used (other gamepads will work i think).
        InputManager iM;

        int playerOneIndex, playerTwoIndex;

        enum PlayState
        {
            play,
            pause,
            gameover,
        }
        PlayState currentPlayState;

        public Play(Point tiles, Vector2 tileSize, int gameWidth, int playerOneIndex, int playerTwoIndex, Texture2D playerOneColor, Texture2D playerTwoColor)
        {
            this.tileSize = tileSize;
            this.playerOneIndex = playerOneIndex;
            this.playerTwoIndex = playerTwoIndex;

            playerColors = new Texture2D[3];
            playerColors[playerOneIndex] = playerOneColor;
            playerColors[playerTwoIndex] = playerTwoColor;//[0] not used atm, 1 and 2 as a playerindex.

            playfield = new GameObject[tiles.X, tiles.Y];
            PopulatePlayfield(tiles.X, tiles.Y, tileSize, gameWidth);

            iM = new InputManager(SettingsManager.playerIndexOne, SettingsManager.playerIndexTwo);

            nextBlock = new string[3];
            nextBlock[playerOneIndex] = RandomBlock();
            nextBlock[playerTwoIndex] = RandomBlock();

            spawnPositions = new Vector2[3];
            spawnPositions[playerOneIndex] = GetSpawnPos(playerOneIndex);
            spawnPositions[playerTwoIndex] = GetSpawnPos(playerTwoIndex);

            stackedBlocks = new TetrisObject[playfield.GetLength(0), playfield.GetLength(1)];

            jArray = new J[3];//Again, index 0 is not used atm.
            iArray = new I[3];

            score = new int[3];

            currentPlayState = PlayState.play;
        }
        private void PopulatePlayfield(int tilesX, int tilesY, Vector2 tileSize, int gameWidth)//Get drawable textures and pos for the playfield
        {
            float startPointX = gameWidth / 2 - tilesX * tileSize.X / 2;
            for (int i = 0; i < tilesX; i++)
            {
                for (int j = 0; j < tilesY; j++)
                {
                    playfield[i, j] = new GameObject(new Vector2(startPointX + i * tileSize.X, j * tileSize.Y), TextureManager.transBlock);
                }
            }
        }
        public void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
            iM.Update();
            switch (currentPlayState)
            {
                case PlayState.play:
                    CheckStackRows();
                    AvoidDubbleSpawn();
                    PlayerSteering(gameTime, playerOneIndex);
                    PlayerSteering(gameTime, playerTwoIndex);
                    GetBlocks(playerOneIndex);
                    GetBlocks(playerTwoIndex);
                    if (iM.JustPressed(Buttons.Start, playerOneIndex) || iM.JustPressed(Buttons.Start, playerTwoIndex)) { currentPlayState = PlayState.pause; }
                    break;
                case PlayState.pause:
                    if (iM.JustPressed(Buttons.Start, playerOneIndex) || iM.JustPressed(Buttons.Start, playerTwoIndex)) { currentPlayState = PlayState.play; }
                    break;
                case PlayState.gameover:
                    break;
            }
        }
        private void CheckStackRows()
        {
            for (int y = 0; y < stackedBlocks.GetLength(1); y++)
            {
                for (int x = 0; x < stackedBlocks.GetLength(0); x++)
                {
                    if (stackedBlocks[x, y] == null) { break; }
                    else if (x == stackedBlocks.GetLength(0) - 1)
                    {
                        score[playerOneIndex] += RowScore(playerOneIndex, y);
                        score[playerTwoIndex] += RowScore(playerTwoIndex, y);
                        int i = 0;
                        do
                        {
                            stackedBlocks[i, y] = null;
                            i++;
                        } while (i != x + 1);
                        UpdateStack(y);
                    }
                }
            }
        }
        private int RowScore(int playerIndex, int row)
        {
            List<Texture2D> colors = new List<Texture2D>();
            int score = 0;
            for (int x = 0; x < stackedBlocks.GetLength(0); x++)
            {
                colors.Add(stackedBlocks[x, row].tex);
            }
            foreach (Texture2D color in colors)
            {
                if (playerColors[playerIndex] == color) { score += 10; }
            }
            return score;
        }
        private TetrisObject[,] UpdateStack(int deletedRow)
        {
            for (int x = 0; x < stackedBlocks.GetLength(0); x++)
            {
                for (int y = stackedBlocks.GetLength(1); y > 0; y--)
                {
                    if (stackedBlocks[x, y - 1] != null && y <= deletedRow)
                    {
                        stackedBlocks[x, y] = new TetrisObject(playfield[x, y].Pos, stackedBlocks[x, y - 1].tex);
                        stackedBlocks[x, y - 1] = null;
                    }
                }
            }
            return stackedBlocks;
        }
        private Vector2 GetSpawnPos(int playerIndex)
        {
            return spawnPositions[playerIndex] = playfield[rnd.Next(playfield.GetLength(0) - marginRight), 0].Pos;
        }
        private int OtherPlayerIndex(int playerIndex)
        {
            if (playerIndex == playerOneIndex) { return playerTwoIndex; }
            else return playerOneIndex;
        }
        private bool VerifySpawn()
        {
            I desiredBlockOne = new I(TextureManager.transBlock, spawnPositions[playerOneIndex]);
            I desiredBlockTwo = new I(TextureManager.transBlock, spawnPositions[playerTwoIndex]);
            return desiredBlockOne.MaxValues().X < desiredBlockTwo.iMatrix[0, 0].PosX
                || desiredBlockOne.MinValues().X > desiredBlockTwo.iMatrix[0, 0].PosX;
        }
        private TetrisObject[,] LocateOtherPlayer(int playerIndex)
        {
            if (iArray[OtherPlayerIndex(playerIndex)] != null && iArray[OtherPlayerIndex(playerIndex)].iMatrix != null) { return iArray[OtherPlayerIndex(playerIndex)].iMatrix; }
            else if (jArray[OtherPlayerIndex(playerIndex)] != null && jArray[OtherPlayerIndex(playerIndex)].jMatrix != null) { return jArray[OtherPlayerIndex(playerIndex)].jMatrix; }
            else return null;
        }
        private bool PlayerIntersect(int playerIndex, bool clockwise)
        {
            if (iArray[playerIndex] != null)
            {
                foreach (TetrisObject newPosition in iArray[playerIndex].NextRotatePosition(clockwise))
                {
                    if (LocateOtherPlayer(playerIndex) != null)
                    {
                        foreach (TetrisObject otherPlayer in LocateOtherPlayer(playerIndex))
                        {
                            if (newPosition.Pos == otherPlayer.Pos && newPosition.alive && otherPlayer.alive)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            if (jArray[playerIndex] != null)
            {
                foreach (TetrisObject newPosition in jArray[playerIndex].NextRotatePosition(clockwise))
                {
                    if (LocateOtherPlayer(playerIndex) != null)
                    {
                        foreach (TetrisObject otherPlayer in LocateOtherPlayer(playerIndex))
                        {
                            if (newPosition.Pos == otherPlayer.Pos && newPosition.alive && otherPlayer.alive)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        private bool PlayerMovementRightIntersect(int playerIndex, TetrisObject[,] tetrisObjects)
        {
            foreach (TetrisObject playerPosition in tetrisObjects)
            {
                if (LocateOtherPlayer(playerIndex) != null)
                {
                    foreach (TetrisObject otherPlayer in LocateOtherPlayer(playerIndex))
                    {
                        if (playerPosition.PosX + tileSize.X == otherPlayer.PosX && playerPosition.PosY == otherPlayer.PosY && playerPosition.alive && otherPlayer.alive)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private bool PlayerMovementLeftIntersect(int playerIndex, TetrisObject[,] tetrisObjects)
        {
            foreach (TetrisObject playerPosition in tetrisObjects)
            {
                if (LocateOtherPlayer(playerIndex) != null)
                {
                    foreach (TetrisObject otherPlayer in LocateOtherPlayer(playerIndex))
                    {
                        if (playerPosition.PosX - tileSize.X == otherPlayer.PosX && playerPosition.PosY == otherPlayer.PosY && playerPosition.alive && otherPlayer.alive)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private bool PlayerMovementDownIntersect(int playerIndex, TetrisObject[,] tetrisObjects)
        {
            foreach (TetrisObject playerPosition in tetrisObjects)
            {
                if (LocateOtherPlayer(playerIndex) != null)
                {
                    foreach (TetrisObject otherPlayer in LocateOtherPlayer(playerIndex))
                    {
                        if (playerPosition.PosX == otherPlayer.PosX && playerPosition.PosY + tileSize.Y == otherPlayer.PosY && playerPosition.alive && otherPlayer.alive)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private void AvoidDubbleSpawn()
        {
            if (spawnPositions[playerOneIndex].X == spawnPositions[playerTwoIndex].X) { spawnPositions[playerOneIndex] = playfield[rnd.Next(playfield.GetLength(0) - marginRight), 0].Pos; }
            if (spawnPositions[playerOneIndex].X + tileSize.X == spawnPositions[playerTwoIndex].X) { spawnPositions[playerOneIndex] = playfield[rnd.Next(playfield.GetLength(0) - marginRight), 0].Pos; }
            if (spawnPositions[playerOneIndex].X + tileSize.X + tileSize.X == spawnPositions[playerTwoIndex].X) { spawnPositions[playerOneIndex] = playfield[rnd.Next(playfield.GetLength(0) - marginRight), 0].Pos; }
            if (spawnPositions[playerOneIndex].X + tileSize.X + tileSize.X + tileSize.X == spawnPositions[playerTwoIndex].X) { spawnPositions[playerOneIndex] = playfield[rnd.Next(playfield.GetLength(0) - marginRight), 0].Pos; }
            if (spawnPositions[playerTwoIndex].X == spawnPositions[playerOneIndex].X) { spawnPositions[playerTwoIndex] = playfield[rnd.Next(playfield.GetLength(0) - marginRight), 0].Pos; }
            if (spawnPositions[playerTwoIndex].X + tileSize.X == spawnPositions[playerOneIndex].X) { spawnPositions[playerTwoIndex] = playfield[rnd.Next(playfield.GetLength(0) - marginRight), 0].Pos; }
            if (spawnPositions[playerTwoIndex].X + tileSize.X + tileSize.X == spawnPositions[playerOneIndex].X) { spawnPositions[playerTwoIndex] = playfield[rnd.Next(playfield.GetLength(0) - marginRight), 0].Pos; }
            if (spawnPositions[playerTwoIndex].X + tileSize.X + tileSize.X + tileSize.X == spawnPositions[playerOneIndex].X) { spawnPositions[playerTwoIndex] = playfield[rnd.Next(playfield.GetLength(0) - marginRight), 0].Pos; }
        }
        private void GetBlocks(int playerIndex)
        {
            Random rnd = new Random();
            if (jArray[playerIndex] == null && iArray[playerIndex] == null)
            {
                switch (nextBlock[playerIndex])
                {
                    case "J":
                        if (VerifySpawn())
                        {

                            jArray[playerIndex] = new J(playerColors[playerIndex], spawnPositions[playerIndex]);
                            if (GameOver(jArray[playerIndex].jMatrix)) { currentPlayState = PlayState.gameover; }
                            spawnPositions[playerIndex] = GetSpawnPos(playerIndex);
                            nextBlock[playerIndex] = null;
                        }
                        break;
                    case "I":
                        if (VerifySpawn())
                        {
                            iArray[playerIndex] = new I(playerColors[playerIndex], spawnPositions[playerIndex]);
                            if (GameOver(iArray[playerIndex].iMatrix)) { currentPlayState = PlayState.gameover; }
                            spawnPositions[playerIndex] = GetSpawnPos(playerIndex);
                            nextBlock[playerIndex] = null;
                        }
                        break;
                }
            }
            if (nextBlock[playerIndex] == null) { nextBlock[playerIndex] = RandomBlock(); }
        }
        private bool GameOver(TetrisObject[,] tetrisObjects)
        {
            foreach (TetrisObject aliveTetrisObject in tetrisObjects)
            {
                foreach (TetrisObject stackObject in stackedBlocks)
                {
                    if (stackObject != null && aliveTetrisObject.alive && aliveTetrisObject.Pos == stackObject.Pos)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool CheckFloor(TetrisObject[,] tetrisobjects)
        {
            foreach (TetrisObject item in tetrisobjects)
            {
                if (item.PosY == playfield[0, playfield.GetLength(1) - 1].PosY && item.alive == true)
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckOnStack(TetrisObject[,] tetrisobjects)
        {
            foreach (TetrisObject aliveblock in tetrisobjects)
            {
                foreach (TetrisObject stackblock in stackedBlocks)
                {
                    if (stackblock != null)
                    {
                        if (aliveblock.PosY + tileSize.Y == stackblock.PosY && aliveblock.PosX == stackblock.PosX && aliveblock.alive)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private bool CheckLeftSide(TetrisObject[,] tetrisObjects)
        {
            foreach (TetrisObject item in tetrisObjects)
            {
                if (item.PosX == playfield[0, 0].PosX && item.alive) { return true; }
            }
            return false;
        }
        private bool CheckRightSide(TetrisObject[,] tetrisObjects)
        {
            foreach (TetrisObject item in tetrisObjects)
            {
                if (item.PosX == playfield[playfield.GetLength(0) - 1, 0].PosX && item.alive)
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckStackLeft(TetrisObject[,] tetrisObjects)
        {
            foreach (TetrisObject aliveblock in tetrisObjects)
            {
                foreach (TetrisObject stackblock in stackedBlocks)
                {
                    if (stackblock != null)
                    {
                        if (aliveblock.PosX - tileSize.X == stackblock.PosX && aliveblock.PosY == stackblock.PosY && aliveblock.alive)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private bool CheckStackRight(TetrisObject[,] tetrisObjects)
        {
            foreach (TetrisObject aliveblock in tetrisObjects)
            {
                foreach (TetrisObject stackblock in stackedBlocks)
                {
                    if (stackblock != null)
                    {
                        if (aliveblock.PosX + tileSize.X == stackblock.PosX && aliveblock.PosY == stackblock.PosY && aliveblock.alive)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private bool StackIntersect(TetrisObject[,] tetrisObjects)
        {
            foreach (TetrisObject aliveblock in tetrisObjects)
            {
                foreach (TetrisObject stackblock in stackedBlocks)
                {
                    if (stackblock != null)
                    {
                        if (aliveblock.Pos == stackblock.Pos && aliveblock.alive)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private void AddDeadBlock(TetrisObject[,] tetrisObjects)
        {
            foreach (TetrisObject item in tetrisObjects)
            {
                for (int i = 0; i < stackedBlocks.GetLength(0); i++)
                {
                    for (int j = 0; j < stackedBlocks.GetLength(1); j++)
                    {
                        if (item.Pos == playfield[i, j].Pos && item.alive)
                        {
                            stackedBlocks[i, j] = new TetrisObject(item.Pos, item.tex);
                        }
                    }
                }
            }
        }
        private void PlayerSteering(GameTime gameTime, int playerIndex)//unfortunately not a very practical/nice method but it works ( we will se what can be done later about this )
        {
            if (jArray[playerIndex] != null && CheckFloor(jArray[playerIndex].jMatrix))
            {
                AddDeadBlock(jArray[playerIndex].jMatrix);
                jArray[playerIndex] = null;
            }
            else if (jArray[playerIndex] != null)
            {
                jArray[playerIndex].time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (jArray[playerIndex].time > 1f && !CheckFloor(jArray[playerIndex].jMatrix) && !PlayerMovementDownIntersect(playerIndex, jArray[playerIndex].jMatrix))
                {
                    jArray[playerIndex].Fall(tileSize.X); jArray[playerIndex].time = 0f;
                }
                if (iM.JustPressed(Buttons.B, playerIndex)
                    && jArray[playerIndex].AllowRotation(true, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                    && !StackIntersect(jArray[playerIndex].NextRotatePosition(true))
                    && !PlayerIntersect(playerIndex, true))
                {
                    jArray[playerIndex].Rotate(true);
                }
                if (iM.JustPressed(Buttons.Y, playerIndex)
                    && jArray[playerIndex].AllowRotation(false, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                    && !StackIntersect(jArray[playerIndex].NextRotatePosition(false))
                    && !PlayerIntersect(playerIndex, false))
                { jArray[playerIndex].Rotate(false); }
                if (iM.JustPressed(Buttons.DPadLeft, playerIndex)
                    && !CheckLeftSide(jArray[playerIndex].jMatrix)
                    && !CheckStackLeft(jArray[playerIndex].jMatrix)
                    && !PlayerMovementLeftIntersect(playerIndex, jArray[playerIndex].jMatrix))
                { jArray[playerIndex].Move(-tileSize.X); }
                if (iM.JustPressed(Buttons.DPadRight, playerIndex)
                    && !CheckRightSide(jArray[playerIndex].jMatrix)
                    && !CheckStackRight(jArray[playerIndex].jMatrix)
                    && !PlayerMovementRightIntersect(playerIndex, jArray[playerIndex].jMatrix))
                { jArray[playerIndex].Move(tileSize.X); }
                if (iM.JustPressed(Buttons.DPadDown, playerIndex)
                    && !PlayerMovementDownIntersect(playerIndex, jArray[playerIndex].jMatrix))
                { if (!CheckFloor(jArray[playerIndex].jMatrix)) { jArray[playerIndex].Fall(tileSize.Y); } }
                if (iM.IsHeld(Buttons.DPadDown, playerIndex)
                    && !PlayerMovementDownIntersect(playerIndex, jArray[playerIndex].jMatrix))
                {
                    if (!CheckFloor(jArray[playerIndex].jMatrix))
                    {
                        jArray[playerIndex].time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (jArray[playerIndex].time > 0.4f)
                        {
                            jArray[playerIndex].Fall(tileSize.Y);
                            jArray[playerIndex].time = 0f;
                        }
                    }
                }

                if (jArray[playerIndex] != null && jArray[playerIndex].jMatrix != null && stackedBlocks.Length > 0)
                {
                    if (CheckOnStack(jArray[playerIndex].jMatrix))
                    {
                        AddDeadBlock(jArray[playerIndex].jMatrix);
                        jArray[playerIndex] = null;
                    }
                }
            }
            if (iArray[playerIndex] != null && CheckFloor(iArray[playerIndex].iMatrix))
            {
                AddDeadBlock(iArray[playerIndex].iMatrix);
                iArray[playerIndex] = null;
            }
            else if (iArray[playerIndex] != null)
            {

                iArray[playerIndex].time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (iArray[playerIndex].time > 1f && !CheckFloor(iArray[playerIndex].iMatrix) && !PlayerMovementDownIntersect(playerIndex, iArray[playerIndex].iMatrix))
                {
                    iArray[playerIndex].Fall(tileSize.X); iArray[playerIndex].time = 0f;
                }

                if (iM.JustPressed(Buttons.B, playerIndex)
                    && iArray[playerIndex].AllowRotation(true, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                    && !StackIntersect(iArray[playerIndex].NextRotatePosition(true))
                    && !PlayerIntersect(playerIndex, true))
                {
                    iArray[playerIndex].Rotate(true);
                }
                if (iM.JustPressed(Buttons.Y, playerIndex)
                    && iArray[playerIndex].AllowRotation(false, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                    && !StackIntersect(iArray[playerIndex].NextRotatePosition(false))
                    && !PlayerIntersect(playerIndex, false))
                { iArray[playerIndex].Rotate(false); }
                if (iM.JustPressed(Buttons.DPadLeft, playerIndex)
                    && !CheckLeftSide(iArray[playerIndex].iMatrix)
                    && !CheckStackLeft(iArray[playerIndex].iMatrix)
                    && !PlayerMovementLeftIntersect(playerIndex, iArray[playerIndex].iMatrix))
                { iArray[playerIndex].Move(-tileSize.X); }
                if (iM.JustPressed(Buttons.DPadRight, playerIndex)
                    && !CheckRightSide(iArray[playerIndex].iMatrix)
                    && !CheckStackRight(iArray[playerIndex].iMatrix)
                    && !PlayerMovementRightIntersect(playerIndex, iArray[playerIndex].iMatrix))
                { iArray[playerIndex].Move(tileSize.X); }
                if (iM.JustPressed(Buttons.DPadDown, playerIndex)
                    && !PlayerMovementDownIntersect(playerIndex, iArray[playerIndex].iMatrix))
                { if (!CheckFloor(iArray[playerIndex].iMatrix)) { iArray[playerIndex].Fall(tileSize.Y); } }
                if (iM.IsHeld(Buttons.DPadDown, playerIndex)
                    && !PlayerMovementDownIntersect(playerIndex, iArray[playerIndex].iMatrix))
                {
                    if (!CheckFloor(iArray[playerIndex].iMatrix))
                    {
                        iArray[playerIndex].time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (iArray[playerIndex].time > 0.4f)
                        {
                            iArray[playerIndex].Fall(tileSize.Y);
                            iArray[playerIndex].time = 0f;
                        }
                    }
                }

                if (iArray[playerIndex] != null && iArray[playerIndex].iMatrix != null && stackedBlocks.Length > 0)
                {
                    if (CheckOnStack(iArray[playerIndex].iMatrix))
                    {
                        AddDeadBlock(iArray[playerIndex].iMatrix);
                        iArray[playerIndex] = null;
                    }
                }
            }
        }
        private string RandomBlock()
        {
            Random rnd = new Random();
            string[] feedRandomMachine = new string[] { "J", "I" };
            return feedRandomMachine[rnd.Next(0, feedRandomMachine.Length)];
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject item in playfield)
            {
                item.Draw(spriteBatch);
            }
            if (jArray[playerOneIndex] != null) { jArray[playerOneIndex].Draw(spriteBatch); }
            if (iArray[playerOneIndex] != null) { iArray[playerOneIndex].Draw(spriteBatch); }
            if (jArray[playerTwoIndex] != null) { jArray[playerTwoIndex].Draw(spriteBatch); }
            if (iArray[playerTwoIndex] != null) { iArray[playerTwoIndex].Draw(spriteBatch); }
            if (stackedBlocks.Length > 0) { foreach (TetrisObject item in stackedBlocks) { if (item != null) { item.Draw(spriteBatch, Color.White); } } }
            if (currentPlayState == PlayState.gameover) { spriteBatch.DrawString(FontManager.menuText, "GameOver!", Vector2.Zero, Color.IndianRed); }
            if (currentPlayState == PlayState.pause) { spriteBatch.DrawString(FontManager.menuText, "Pause!", Vector2.Zero, Color.IndianRed); }

            spriteBatch.DrawString(FontManager.menuText,
            $"{SettingsManager.playerOneName.ToString()}\nscore: " + score[playerOneIndex].ToString(),
            new Vector2(0, 80),
            Color.Black);

            spriteBatch.DrawString(FontManager.menuText,
            $"{SettingsManager.playerTwoName.ToString()}\nscore: " + score[playerTwoIndex].ToString(),
            new Vector2(SettingsManager.windowSize.X - FontManager.scoreText.MeasureString($"{SettingsManager.playerTwoName.ToString()}\nscore: ").X - 160,
            80),
            Color.Black);

        }
    }
}
