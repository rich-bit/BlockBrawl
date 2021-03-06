﻿using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BlockBrawl.Blocks;
using BlockBrawl.Objects;
using System.Collections.Generic;
using BlockBrawl.Gamehandler.Play;

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
        float[] waitForSpawn;
        int[,] bonusRecieved;
        float playedTime;

        Vector2 tileSize;
        Vector2[] spawnPositions;

        I[] iArray;
        J[] jArray;
        T[] tArray;
        O[] oArray;
        L[] lArray;
        S[] sArray;
        Z[] zArray;

        QTE qte;

        //Nextblock, will switch this array for spawn.
        readonly string[] nextBlock;

        //Will be the stack of stopped blockes.
        readonly TetrisObject[,] stackedBlocks;

        private readonly int playerOneIndex;
        private readonly int playerTwoIndex;
        private float fallWaitTime, qteWaitTime;
        private readonly float newSpeedCounter;
        readonly bool gamePadVersion;
        readonly SideBars sideBars;

        //Power-Ups
        BloodOrb bloodOrb;
        Pistol pistol;
        MusikPlayer musikPlayer;
        //Special-Effects
        AnimatedObject explosion;
        enum PlayState
        {
            play,
            pause,
            gameover,
            qte,
        }
        PlayState currentPlayState;

        //Gamestate obj.
        CheckGameOver gameOver;
        public Pause Pause { get; set; }

        public bool GoToMenu { get; set; }

        Buttons p1MoveLeft, p1MoveRight, p1MoveDown, p1PowerUp, p1RotateCW, p1RotateCC, p1Start;
        Buttons p2MoveLeft, p2MoveRight, p2MoveDown, p2PowerUp, p2RotateCW, p2RotateCC, p2Start;

        public Play(bool gamePadVersion, Point tiles, Vector2 tileSize, int gameWidth, int gameHeight, int playerOneIndex, int playerTwoIndex, Texture2D playerOneColor, Texture2D playerTwoColor)
        {
            this.tileSize = tileSize;
            this.playerOneIndex = playerOneIndex;
            this.playerTwoIndex = playerTwoIndex;
            this.gamePadVersion = gamePadVersion;

            //Assigning buttons

            p1MoveLeft = SettingsManager.p1MoveLeft;
            p1MoveRight = SettingsManager.p1MoveRight;
            p1MoveDown = SettingsManager.p1MoveDown;
            p1PowerUp = SettingsManager.p1PowerUp;
            p1RotateCC = SettingsManager.p1RotateCC;
            p1RotateCW = SettingsManager.p1RotateCW;
            p1Start = SettingsManager.p1Start;

            p2MoveLeft = SettingsManager.p2MoveLeft;
            p2MoveRight = SettingsManager.p2MoveRight;
            p2MoveDown = SettingsManager.p2MoveDown;
            p2PowerUp = SettingsManager.p2PowerUp;
            p2RotateCC = SettingsManager.p2RotateCC;
            p2RotateCW = SettingsManager.p2RotateCW;
            p2Start = SettingsManager.p2Start;

            fallWaitTime = SettingsManager.fallTime;
            newSpeedCounter = SettingsManager.newSpeedCounter;
            qteWaitTime = SettingsManager.qteWaitTime;

            playerColors = new Texture2D[2];
            playerColors[playerOneIndex] = playerOneColor;
            playerColors[playerTwoIndex] = playerTwoColor;//player 1 = playercolors[0], player 1 = playercolors[1], same with other arrays

            playfield = new GameObject[tiles.X, tiles.Y];
            PopulatePlayfield(tiles.X, tiles.Y, tileSize, gameWidth, gameHeight);

            nextBlock = new string[2];
            nextBlock[playerOneIndex] = RandomBlock();
            nextBlock[playerTwoIndex] = RandomBlock();

            waitForSpawn = new float[2];

            spawnPositions = new Vector2[2];
            spawnPositions[playerOneIndex] = GetSpawnPos(playerOneIndex);
            spawnPositions[playerTwoIndex] = GetSpawnPos(playerTwoIndex);

            stackedBlocks = new TetrisObject[playfield.GetLength(0), playfield.GetLength(1)];

            jArray = new J[2];//Again: player 1 = jArray[0], player 1 = jArray[1], same with other arrays
            iArray = new I[2];
            tArray = new T[2];
            oArray = new O[2];
            lArray = new L[2];
            sArray = new S[2];
            zArray = new Z[2];

            sideBars = new SideBars(playerColors, waitForSpawn, gamePadVersion);

            score = new int[2];
            bonusRecieved = new int[2, stackedBlocks.GetLength(1)];

            currentPlayState = PlayState.play;

            gameOver = new CheckGameOver();
            Pause = new Pause();

            musikPlayer = new MusikPlayer(rnd);
        }
        private void PopulatePlayfield(int tilesX, int tilesY, Vector2 tileSize, int gameWidth, int gameHeight)//Get drawable textures and pos for the playfield
        {
            for (int y = tilesY - 1; y >= 0; y--)
            {
                for (int x = tilesX - 1; x >= 0; x--)// Fixa
                {
                    playfield[x, y] = new GameObject(new Vector2(
                        ((tilesX * tileSize.X)
                        + (-tilesX * tileSize.X + x * tileSize.X)/* - tileSize.X*/)
                        + (gameWidth / 2)
                        - (tilesX * tileSize.X / 2),
                        gameHeight
                        + (-tilesY * tileSize.Y + y * tileSize.Y)//it works atm
                        ), TextureManager.transBlock);
                }
            }
        }
        private void EventTrigger(GameTime gameTime)
        {
            playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            float time = playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Time;
            if(time > qteWaitTime - 6 && qte == null) { SoundManager.qteWarning.Play(); }
            if (time > qteWaitTime && qte == null)
            {
                qte = new QTE();
                currentPlayState = PlayState.qte;
                playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Time = 0f;
            }
        }
        private void EventHandler(GameTime gameTime)
        {
            if (qte.Cleared && qte.Winner != int.MinValue)
            {
                int numberPowerUps;
                if (musikPlayer.SongsToPlay)
                {
                    numberPowerUps = 3;
                }
                else
                {
                    numberPowerUps = 2;
                }

                int randomPowerUp = rnd.Next(numberPowerUps);
                
                switch (randomPowerUp)
                {
                    case 0:
                        bloodOrb = new BloodOrb(5f, qte.Winner, playerOneIndex, playerTwoIndex);
                        sideBars.QTEWinner = qte.Winner;
                        break;
                    case 1:
                        pistol = new Pistol(5f, qte.Winner, playerOneIndex, playerTwoIndex);
                        sideBars.QTEWinner = qte.Winner;
                        break;
                    case 2:
                        musikPlayer.PlayRandomSong();
                        sideBars.Music = true;
                        break;
                }
                qte = null;
                currentPlayState = PlayState.play;
            }
            else if (qte.Cleared && qte.Winner == int.MinValue)
            {
                qte = null;
                currentPlayState = PlayState.play;
            }
        }
        public void Update(GameTime gameTime, InputManager iM)
        {
            this.gameTime = gameTime;
            playedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            switch (currentPlayState)
            {
                case PlayState.play:
                    EventTrigger(gameTime);
                    DrawBonus();
                    AvoidDubbleSpawn();
                    IncreseFallSpeed();
                    HandleWaitForSpawn(gameTime);
                    sideBars.Update(score, bonusRecieved, nextBlock);
                    if (gamePadVersion)
                    {
                        GamePadSteering(gameTime, playerOneIndex, iM);
                        GamePadSteering(gameTime, playerTwoIndex, iM);
                    }
                    else
                    {
                        KeyboardSteering(playerOneIndex, iM);
                        KeyboardSteering(playerTwoIndex, iM);
                    }
                    FallDownAddStack(playerOneIndex);
                    FallDownAddStack(playerTwoIndex);
                    GetBlocks(playerOneIndex);
                    GetBlocks(playerTwoIndex);
                    BloodOrbLogic(gameTime, iM);
                    PistolLogic(gameTime, iM);
                    ResetPowerUpNotice(gamePadVersion, iM);
                    if (gamePadVersion && iM.JustPressed(p1Start, playerOneIndex) || iM.JustPressed(p2Start, playerTwoIndex)) { currentPlayState = PlayState.pause; }
                    else if (!gamePadVersion && iM.JustPressed(Keys.Escape) || iM.JustPressed(Keys.NumLock)) { currentPlayState = PlayState.pause; }
                    break;
                case PlayState.pause:
                    Pause.Update(iM, playerOneIndex, playerTwoIndex);
                    if (gamePadVersion && iM.JustPressed(p1Start, playerOneIndex) || iM.JustPressed(p2Start, playerTwoIndex)) { currentPlayState = PlayState.play; }
                    else if (!gamePadVersion && iM.JustPressed(Keys.Enter) || iM.JustPressed(Keys.NumLock)) { currentPlayState = PlayState.play; }
                    break;
                case PlayState.gameover:
                    gameOver.Update(iM, gamePadVersion, playerOneIndex, playerTwoIndex);
                    if (gameOver.RequestGoToMenu) { GoToMenu = true; gameOver.RequestGoToMenu = false; }
                    break;
                case PlayState.qte:
                    qte.Status(gameTime);
                    qte.Update(iM, playerOneIndex, gamePadVersion, gameTime);
                    qte.Update(iM, playerTwoIndex, gamePadVersion, gameTime);
                    EventHandler(gameTime);
                    break;
            }
        }
        private void ResetPowerUpNotice(bool gamepadVersion, InputManager iM)
        {
            if (gamepadVersion)
            {
                if (sideBars.QTEWinner == playerOneIndex && pistol == null)
                {
                    if (iM.JustPressed(p1PowerUp, playerOneIndex)) { sideBars.QTEWinner = int.MinValue; }
                }
                else
                {
                    if (iM.JustPressed(p2PowerUp, playerTwoIndex)) { sideBars.QTEWinner = int.MinValue; }
                }
            }
            else
            {
                if (sideBars.QTEWinner == playerOneIndex && pistol == null)
                {
                    if (iM.JustPressed(Keys.W)) { sideBars.QTEWinner = int.MinValue; }
                }
                else
                {
                    if (iM.JustPressed(Keys.RightControl)) { sideBars.QTEWinner = int.MinValue; }
                }
            }
        }
        private void BloodOrbLogic(GameTime gameTime, InputManager iM)
        {
            if (bloodOrb != null)
            {
                bloodOrb.Action(
                    LocateOtherPlayerMatrix(OtherPlayerIndex(playerOneIndex)),
                    LocateOtherPlayerMatrix(OtherPlayerIndex(playerTwoIndex)),
                    iM, gamePadVersion, gameTime);
                if (bloodOrb.TargetHit)
                {
                    explosion = new AnimatedObject(bloodOrb.ShotPos, TextureManager.spriteSheetExplosion1920x1080, new Point(4, 4));
                    RemoveOtherPlayerBlock(OtherPlayerIndex(bloodOrb.PlayerIndexBazooka));
                    waitForSpawn[OtherPlayerIndex(bloodOrb.PlayerIndexBazooka)] += SettingsManager.spawnBlockBazooka;
                    bloodOrb = null;
                }
            }
            if (explosion != null)
            {
                SoundManager.explosion.Play();
                explosion.CycleSpriteSheetOnce(gameTime);
                if (explosion.Done)
                {
                    explosion = null;
                    sideBars.QTEWinner = int.MinValue;
                }
            }
        }

        private void PistolLogic(GameTime gameTime, InputManager iM)
        {
            if (pistol != null)
            {
                float pistolTTL = 6f;
                playfield[playfield.GetLength(0) - 1, 0].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                float pistolTime = playfield[playfield.GetLength(0) - 1, 0].Time; // Just a measure of time...
                int pistolIndex = pistol.PlayerIndexPistol;

                pistol.Action(
                    LocateOtherPlayerMatrix(OtherPlayerIndex(playerOneIndex)),
                    LocateOtherPlayerMatrix(OtherPlayerIndex(playerTwoIndex)),
                    iM, gamePadVersion, gameTime);
                if (pistol.TargetHit && pistolTime < pistolTTL)
                {
                    if (!pistol.EnemyDown)
                    {
                        pistol = new Pistol(5f, pistolIndex, playerOneIndex, playerTwoIndex);
                    }
                    else
                    {
                        RemoveOtherPlayerBlock(OtherPlayerIndex(pistol.PlayerIndexPistol));
                        pistol = new Pistol(5f, pistolIndex, playerOneIndex, playerTwoIndex);
                    }
                }
                else if (pistolTime > pistolTTL)
                {
                    pistol = null;
                    sideBars.QTEWinner = int.MinValue;
                    playfield[playfield.GetLength(0) - 1, 0].Time = 0f;
                }
            }
        }

        private void DrawBonus()
        {
            for (int playerIndex = 0; playerIndex < bonusRecieved.GetLength(0); playerIndex++)
            {
                for (int bonusRow = 0; bonusRow < bonusRecieved.GetLength(1); bonusRow++)
                {
                    if (bonusRecieved[playerIndex, bonusRow] != 0)
                    {
                        if (playerIndex == playerOneIndex)
                        {
                            playfield[playerIndex, bonusRow].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            if (playfield[playerIndex, bonusRow].Time > 2f)
                            {
                                bonusRecieved[playerIndex, bonusRow] = 0;
                                playfield[playerIndex, bonusRow].Time = 0f;
                            }
                        }
                        else if (playerIndex == playerTwoIndex)
                        {
                            playfield[playerIndex, bonusRow].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            if (playfield[playerIndex, bonusRow].Time > 2f)
                            {
                                bonusRecieved[playerIndex, bonusRow] = 0;
                                playfield[playerIndex, bonusRow].Time = 0f;
                            }
                        }
                    }
                }
            }
        }
        private void RemoveOtherPlayerBlock(int playerIndex)
        {
            if (jArray[playerIndex] != null) { jArray[playerIndex] = null; }
            else if (iArray[playerIndex] != null) { iArray[playerIndex] = null; }
            else if (tArray[playerIndex] != null) { tArray[playerIndex] = null; }
            else if (oArray[playerIndex] != null) { oArray[playerIndex] = null; }
            else if (lArray[playerIndex] != null) { lArray[playerIndex] = null; }
            else if (sArray[playerIndex] != null) { sArray[playerIndex] = null; }
            else if (zArray[playerIndex] != null) { zArray[playerIndex] = null; }
        }
        private void HandleWaitForSpawn(GameTime gameTime)
        {
            if (waitForSpawn[playerOneIndex] > 0f)
            {
                waitForSpawn[playerOneIndex] -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (waitForSpawn[playerTwoIndex] > 0f)
            {
                waitForSpawn[playerTwoIndex] -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
        private int RowScore(int playerIndex, int row, TetrisObject[,] tetrisObjects)
        {
            List<Texture2D> colors = new List<Texture2D>();
            int score = 0;
            for (int x = 0; x < tetrisObjects.GetLength(0); x++)
            {
                if (tetrisObjects[x, row] != null)
                {
                    colors.Add(tetrisObjects[x, row].Tex);
                }
            }
            foreach (Texture2D color in colors)
            {
                if (playerColors[playerIndex] == color) { score += 10; }
            }
            return score;
        }
        private TetrisObject[,] UpdatePositionsStack(int deletedRow)
        {
            for (int x = 0; x < stackedBlocks.GetLength(0); x++)
            {
                for (int y = stackedBlocks.GetLength(1); y > 0; y--)
                {
                    if (stackedBlocks[x, y - 1] != null && y <= deletedRow)
                    {
                        stackedBlocks[x, y] = new TetrisObject(playfield[x, y].Pos, stackedBlocks[x, y - 1].Tex);
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

            I currentPosP1 = null;
            if (LocateOtherPlayerMatrix(OtherPlayerIndex(playerOneIndex)) != null)
            {
                currentPosP1 = new I(TextureManager.transBlock, LocateOtherPlayerMatrix(OtherPlayerIndex(playerOneIndex))[0, 0].Pos);
                foreach (TetrisObject item in currentPosP1.iMatrix) { item.alive = true; }
            }
            I currentPosP2 = null;
            if (LocateOtherPlayerMatrix(OtherPlayerIndex(playerTwoIndex)) != null)
            {
                currentPosP2 = new I(TextureManager.transBlock, LocateOtherPlayerMatrix(OtherPlayerIndex(playerTwoIndex))[0, 0].Pos);
                foreach (TetrisObject item in currentPosP2.iMatrix) { item.alive = true; }
            }


            return (desiredBlockOne.MaxValues().X < desiredBlockTwo.iMatrix[0, 0].PosX
                || desiredBlockOne.MinValues().X > desiredBlockTwo.iMatrix[0, 0].PosX)
                && (currentPosP2 == null ||
                desiredBlockOne.MaxValues().X < currentPosP2.MinValues().X
                || desiredBlockOne.MinValues().X > currentPosP2.MaxValues().X
                || desiredBlockOne.MaxValues().Y < currentPosP2.MinValues().Y)
                && (currentPosP1 == null
                || desiredBlockTwo.MaxValues().X < currentPosP1.MinValues().X
                || desiredBlockTwo.MinValues().X > currentPosP1.MaxValues().X
                || desiredBlockTwo.MaxValues().Y < currentPosP1.MinValues().Y
                );
        }
        private TetrisObject[,] LocateOtherPlayerMatrix(int playerIndex)
        {
            if (iArray[OtherPlayerIndex(playerIndex)] != null && iArray[OtherPlayerIndex(playerIndex)].iMatrix != null) { return iArray[OtherPlayerIndex(playerIndex)].iMatrix; }
            else if (jArray[OtherPlayerIndex(playerIndex)] != null && jArray[OtherPlayerIndex(playerIndex)].jMatrix != null) { return jArray[OtherPlayerIndex(playerIndex)].jMatrix; }
            else if (tArray[OtherPlayerIndex(playerIndex)] != null && tArray[OtherPlayerIndex(playerIndex)].tMatrix != null) { return tArray[OtherPlayerIndex(playerIndex)].tMatrix; }
            else if (oArray[OtherPlayerIndex(playerIndex)] != null && oArray[OtherPlayerIndex(playerIndex)].oMatrix != null) { return oArray[OtherPlayerIndex(playerIndex)].oMatrix; }
            else if (lArray[OtherPlayerIndex(playerIndex)] != null && lArray[OtherPlayerIndex(playerIndex)].lMatrix != null) { return lArray[OtherPlayerIndex(playerIndex)].lMatrix; }
            else if (sArray[OtherPlayerIndex(playerIndex)] != null && sArray[OtherPlayerIndex(playerIndex)].sMatrix != null) { return sArray[OtherPlayerIndex(playerIndex)].sMatrix; }
            else if (zArray[OtherPlayerIndex(playerIndex)] != null && zArray[OtherPlayerIndex(playerIndex)].zMatrix != null) { return zArray[OtherPlayerIndex(playerIndex)].zMatrix; }
            else return null;
        }
        private bool PlayerIntersect(int playerIndex, bool clockwise)
        {
            if (iArray[playerIndex] != null)
            {
                foreach (TetrisObject newPosition in iArray[playerIndex].NextRotatePosition(clockwise))
                {
                    if (LocateOtherPlayerMatrix(playerIndex) != null)
                    {
                        foreach (TetrisObject otherPlayer in LocateOtherPlayerMatrix(playerIndex))
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
                    if (LocateOtherPlayerMatrix(playerIndex) != null)
                    {
                        foreach (TetrisObject otherPlayer in LocateOtherPlayerMatrix(playerIndex))
                        {
                            if (newPosition.Pos == otherPlayer.Pos && newPosition.alive && otherPlayer.alive)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            if (tArray[playerIndex] != null)
            {
                foreach (TetrisObject newPosition in tArray[playerIndex].NextRotatePosition(clockwise))
                {
                    if (LocateOtherPlayerMatrix(playerIndex) != null)
                    {
                        foreach (TetrisObject otherPlayer in LocateOtherPlayerMatrix(playerIndex))
                        {
                            if (newPosition.Pos == otherPlayer.Pos && newPosition.alive && otherPlayer.alive)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            if (oArray[playerIndex] != null)
            {
                foreach (TetrisObject newPosition in oArray[playerIndex].NextRotatePosition(clockwise))
                {
                    if (LocateOtherPlayerMatrix(playerIndex) != null)
                    {
                        foreach (TetrisObject otherPlayer in LocateOtherPlayerMatrix(playerIndex))
                        {
                            if (newPosition.Pos == otherPlayer.Pos && newPosition.alive && otherPlayer.alive)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            if (lArray[playerIndex] != null)
            {
                foreach (TetrisObject newPosition in lArray[playerIndex].NextRotatePosition(clockwise))
                {
                    if (LocateOtherPlayerMatrix(playerIndex) != null)
                    {
                        foreach (TetrisObject otherPlayer in LocateOtherPlayerMatrix(playerIndex))
                        {
                            if (newPosition.Pos == otherPlayer.Pos && newPosition.alive && otherPlayer.alive)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            if (sArray[playerIndex] != null)
            {
                foreach (TetrisObject newPosition in sArray[playerIndex].NextRotatePosition(clockwise))
                {
                    if (LocateOtherPlayerMatrix(playerIndex) != null)
                    {
                        foreach (TetrisObject otherPlayer in LocateOtherPlayerMatrix(playerIndex))
                        {
                            if (newPosition.Pos == otherPlayer.Pos && newPosition.alive && otherPlayer.alive)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            if (zArray[playerIndex] != null)
            {
                foreach (TetrisObject newPosition in zArray[playerIndex].NextRotatePosition(clockwise))
                {
                    if (LocateOtherPlayerMatrix(playerIndex) != null)
                    {
                        foreach (TetrisObject otherPlayer in LocateOtherPlayerMatrix(playerIndex))
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
                if (LocateOtherPlayerMatrix(playerIndex) != null)
                {
                    foreach (TetrisObject otherPlayer in LocateOtherPlayerMatrix(playerIndex))
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
                if (LocateOtherPlayerMatrix(playerIndex) != null)
                {
                    foreach (TetrisObject otherPlayer in LocateOtherPlayerMatrix(playerIndex))
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
                if (LocateOtherPlayerMatrix(playerIndex) != null)
                {
                    foreach (TetrisObject otherPlayer in LocateOtherPlayerMatrix(playerIndex))
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
        private void AvoidDubbleSpawn()//Fixa
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
            if (jArray[playerIndex] == null && iArray[playerIndex] == null && tArray[playerIndex] == null && oArray[playerIndex] == null
                && lArray[playerIndex] == null && sArray[playerIndex] == null && zArray[playerIndex] == null && waitForSpawn[playerIndex] <= 0f)
            {
                switch (nextBlock[playerIndex])
                {
                    case "J":
                        if (VerifySpawn())
                        {
                            jArray[playerIndex] = new J(playerColors[playerIndex], spawnPositions[playerIndex]);
                            new Thread(() => {
                            HandleGameOver(playerIndex, jArray[playerIndex].jMatrix);
                            }).Start();
                            spawnPositions[playerIndex] = GetSpawnPos(playerIndex);
                            nextBlock[playerIndex] = null;
                        }
                        break;
                    case "I":
                        if (VerifySpawn())
                        {
                            iArray[playerIndex] = new I(playerColors[playerIndex], spawnPositions[playerIndex]);
                            new Thread(() => {
                                HandleGameOver(playerIndex, iArray[playerIndex].iMatrix);
                            }).Start();
                            spawnPositions[playerIndex] = GetSpawnPos(playerIndex);
                            nextBlock[playerIndex] = null;
                        }
                        break;
                    case "T":
                        if (VerifySpawn())
                        {
                            tArray[playerIndex] = new T(playerColors[playerIndex], spawnPositions[playerIndex]);
                            new Thread(() => {
                                HandleGameOver(playerIndex, tArray[playerIndex].tMatrix);
                            }).Start();
                            spawnPositions[playerIndex] = GetSpawnPos(playerIndex);
                            nextBlock[playerIndex] = null;
                        }
                        break;
                    case "O":
                        if (VerifySpawn())
                        {
                            oArray[playerIndex] = new O(playerColors[playerIndex], spawnPositions[playerIndex]);
                            new Thread(() => {
                                HandleGameOver(playerIndex, oArray[playerIndex].oMatrix);
                            }).Start();
                            spawnPositions[playerIndex] = GetSpawnPos(playerIndex);
                            nextBlock[playerIndex] = null;
                        }
                        break;
                    case "L":
                        if (VerifySpawn())
                        {
                            lArray[playerIndex] = new L(playerColors[playerIndex], spawnPositions[playerIndex]);
                            new Thread(() => {
                                HandleGameOver(playerIndex, lArray[playerIndex].lMatrix);
                            }).Start();
                            spawnPositions[playerIndex] = GetSpawnPos(playerIndex);
                            nextBlock[playerIndex] = null;
                        }
                        break;
                    case "S":
                        if (VerifySpawn())
                        {
                            sArray[playerIndex] = new S(playerColors[playerIndex], spawnPositions[playerIndex]);
                            new Thread(() => {
                                HandleGameOver(playerIndex, sArray[playerIndex].sMatrix);
                            }).Start();
                            spawnPositions[playerIndex] = GetSpawnPos(playerIndex);
                            nextBlock[playerIndex] = null;
                        }
                        break;
                    case "Z":
                        if (VerifySpawn())
                        {
                            zArray[playerIndex] = new Z(playerColors[playerIndex], spawnPositions[playerIndex]);
                            new Thread(() => {
                                HandleGameOver(playerIndex, zArray[playerIndex].zMatrix);
                            }).Start();
                            spawnPositions[playerIndex] = GetSpawnPos(playerIndex);
                            nextBlock[playerIndex] = null;
                        }
                        break;
                }
            }
            if (nextBlock[playerIndex] == null) { nextBlock[playerIndex] = RandomBlock(); }
        }

        private void HandleGameOver(int playerIndex, TetrisObject[,] tetrisObjects)
        {
            if (CheckGameOver(tetrisObjects))
            {
                currentPlayState = PlayState.gameover;
                if (score[playerOneIndex] > score[playerTwoIndex])
                {
                    gameOver.CheckHighScore(score[playerOneIndex], SettingsManager.playerOneName, score[playerTwoIndex], SettingsManager.playerTwoName, (int)playedTime, gamePadVersion);
                }
                else
                {
                    gameOver.CheckHighScore(score[playerTwoIndex], SettingsManager.playerTwoName, score[playerOneIndex], SettingsManager.playerOneName, (int)playedTime, gamePadVersion);
                }
            }
        }

        private bool CheckGameOver(TetrisObject[,] tetrisObjects)
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
        private void RemoveClearedRow()
        {
            for (int y = 0; y < stackedBlocks.GetLength(1); y++)
            {
                for (int x = 0; x < stackedBlocks.GetLength(0); x++)
                {
                    if (stackedBlocks[x, y] == null) { break; }
                    else if (x == stackedBlocks.GetLength(0) - 1)
                    {
                        int i = 0;
                        do
                        {
                            stackedBlocks[i, y] = null;
                            i++;
                        } while (i != x + 1);
                        UpdatePositionsStack(y);
                        SoundManager.rowFilled.Play();
                    }
                }
            }
        }
        private void CheckRowHandleScore(TetrisObject[,] tetrisObjects, int playerIndex)
        {
            TetrisObject[,] cloneArray = new TetrisObject[stackedBlocks.GetLength(0), stackedBlocks.GetLength(1)];
            for (int i = 0; i < stackedBlocks.GetLength(0); i++)
            {
                for (int j = 0; j < stackedBlocks.GetLength(1); j++)
                {
                    if (stackedBlocks[i, j] != null)
                    {
                        cloneArray[i, j] = new TetrisObject(stackedBlocks[i, j].Pos, stackedBlocks[i, j].Tex);
                    }
                }
            }
            for (int x = 0; x < playfield.GetLength(0); x++)
            {
                for (int y = 0; y < playfield.GetLength(1); y++)
                {
                    foreach (TetrisObject tetrisobject in tetrisObjects)
                    {
                        if (tetrisobject.Pos == playfield[x, y].Pos && tetrisobject.alive)
                        {
                            cloneArray[x, y] = new TetrisObject(tetrisobject.Pos, tetrisobject.Tex);
                        }
                    }
                }
            }
            for (int y = 0; y < cloneArray.GetLength(1); y++)
            {
                for (int x = 0; x < cloneArray.GetLength(0); x++)
                {
                    if (cloneArray[x, y] == null)
                    {
                        break;
                    }
                    else if (x == cloneArray.GetLength(0) - 1)
                    {
                        score[playerOneIndex] += RowScore(playerOneIndex, y, cloneArray);
                        score[playerTwoIndex] += RowScore(playerTwoIndex, y, cloneArray);
                        if (playerIndex == playerOneIndex)
                        {
                            score[playerOneIndex] += RowScore(playerOneIndex, y, cloneArray) / 2;
                            bonusRecieved[playerOneIndex, y] = RowScore(playerOneIndex, y, cloneArray) / 2;
                        }
                        else if (playerIndex == playerTwoIndex)
                        {
                            score[playerTwoIndex] += RowScore(playerTwoIndex, y, cloneArray) / 2;
                            bonusRecieved[playerTwoIndex, y] = RowScore(playerTwoIndex, y, cloneArray) / 2;
                        }
                    }
                }
            }
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
                            stackedBlocks[i, j] = new TetrisObject(item.Pos, item.Tex);
                        }
                    }
                }
            }
        }
        private void IncreseFallSpeed()
        {
            playfield[0, 0].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (playfield[0, 0].Time > newSpeedCounter && fallWaitTime > 0.4f)
            {
                fallWaitTime -= 0.1f;
                playfield[0, 0].Time = 0f;
            }
        }
        private void FallDownAddStack(int playerIndex)
        {
            if (jArray[playerIndex] != null && CheckFloor(jArray[playerIndex].jMatrix))
            {
                CheckRowHandleScore(jArray[playerIndex].jMatrix, playerIndex);
                AddDeadBlock(jArray[playerIndex].jMatrix);
                RemoveClearedRow();
                jArray[playerIndex] = null;
            }
            if (jArray[playerIndex] != null)
            {
                jArray[playerIndex].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (jArray[playerIndex].Time > fallWaitTime && !CheckFloor(jArray[playerIndex].jMatrix) && !PlayerMovementDownIntersect(playerIndex, jArray[playerIndex].jMatrix))
                {
                    jArray[playerIndex].Fall(tileSize.X);
                    jArray[playerIndex].Time = 0f;
                }
            }
            if (iArray[playerIndex] != null && CheckFloor(iArray[playerIndex].iMatrix))
            {
                CheckRowHandleScore(iArray[playerIndex].iMatrix, playerIndex);
                AddDeadBlock(iArray[playerIndex].iMatrix);
                RemoveClearedRow();
                iArray[playerIndex] = null;
            }
            if (iArray[playerIndex] != null)
            {
                iArray[playerIndex].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (iArray[playerIndex].Time > fallWaitTime && !CheckFloor(iArray[playerIndex].iMatrix) && !PlayerMovementDownIntersect(playerIndex, iArray[playerIndex].iMatrix))
                {
                    iArray[playerIndex].Fall(tileSize.X);
                    iArray[playerIndex].Time = 0f;
                }
            }
            if (tArray[playerIndex] != null && CheckFloor(tArray[playerIndex].tMatrix))
            {
                CheckRowHandleScore(tArray[playerIndex].tMatrix, playerIndex);
                AddDeadBlock(tArray[playerIndex].tMatrix);
                RemoveClearedRow();
                tArray[playerIndex] = null;
            }
            if (tArray[playerIndex] != null)
            {
                tArray[playerIndex].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (tArray[playerIndex].Time > fallWaitTime && !CheckFloor(tArray[playerIndex].tMatrix) && !PlayerMovementDownIntersect(playerIndex, tArray[playerIndex].tMatrix))
                {
                    tArray[playerIndex].Fall(tileSize.X); tArray[playerIndex].Time = 0f;
                }
            }
            if (oArray[playerIndex] != null && CheckFloor(oArray[playerIndex].oMatrix))
            {
                CheckRowHandleScore(oArray[playerIndex].oMatrix, playerIndex);
                AddDeadBlock(oArray[playerIndex].oMatrix);
                RemoveClearedRow();
                oArray[playerIndex] = null;
            }
            if (oArray[playerIndex] != null)
            {
                oArray[playerIndex].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (oArray[playerIndex].Time > fallWaitTime && !CheckFloor(oArray[playerIndex].oMatrix) && !PlayerMovementDownIntersect(playerIndex, oArray[playerIndex].oMatrix))
                {
                    oArray[playerIndex].Fall(tileSize.X); oArray[playerIndex].Time = 0f;
                }
            }
            if (lArray[playerIndex] != null && CheckFloor(lArray[playerIndex].lMatrix))
            {
                CheckRowHandleScore(lArray[playerIndex].lMatrix, playerIndex);
                AddDeadBlock(lArray[playerIndex].lMatrix);
                RemoveClearedRow();
                lArray[playerIndex] = null;
            }
            if (lArray[playerIndex] != null)
            {
                lArray[playerIndex].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (lArray[playerIndex].Time > fallWaitTime && !CheckFloor(lArray[playerIndex].lMatrix) && !PlayerMovementDownIntersect(playerIndex, lArray[playerIndex].lMatrix))
                {
                    lArray[playerIndex].Fall(tileSize.X); lArray[playerIndex].Time = 0f;
                }
            }
            if (sArray[playerIndex] != null && CheckFloor(sArray[playerIndex].sMatrix))
            {
                CheckRowHandleScore(sArray[playerIndex].sMatrix, playerIndex);
                AddDeadBlock(sArray[playerIndex].sMatrix);
                RemoveClearedRow();
                sArray[playerIndex] = null;
            }
            if (sArray[playerIndex] != null)
            {
                sArray[playerIndex].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (sArray[playerIndex].Time > fallWaitTime && !CheckFloor(sArray[playerIndex].sMatrix) && !PlayerMovementDownIntersect(playerIndex, sArray[playerIndex].sMatrix))
                {
                    sArray[playerIndex].Fall(tileSize.X); sArray[playerIndex].Time = 0f;
                }
            }
            if (zArray[playerIndex] != null && CheckFloor(zArray[playerIndex].zMatrix))
            {
                CheckRowHandleScore(zArray[playerIndex].zMatrix, playerIndex);
                AddDeadBlock(zArray[playerIndex].zMatrix);
                RemoveClearedRow();
                zArray[playerIndex] = null;
            }
            if (zArray[playerIndex] != null)
            {
                zArray[playerIndex].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (zArray[playerIndex].Time > fallWaitTime && !CheckFloor(zArray[playerIndex].zMatrix) && !PlayerMovementDownIntersect(playerIndex, zArray[playerIndex].zMatrix))
                {
                    zArray[playerIndex].Fall(tileSize.X); zArray[playerIndex].Time = 0f;
                }
            }


        }
        private void KeyboardSteering(int playerIndex, InputManager iM)
        {
            Keys rotateClockWise = Keys.NumPad0;
            Keys rotateCounterClockWise = Keys.RightControl;
            Keys steerLeft = Keys.Left;
            Keys steerRight = Keys.Right;
            Keys steerDown = Keys.Down;
            if (playerIndex == playerOneIndex)
            {
                rotateClockWise = Keys.F;
                rotateCounterClockWise = Keys.LeftShift;
                steerLeft = Keys.A;
                steerRight = Keys.D;
                steerDown = Keys.S;
            }
            if (jArray[playerIndex] != null)
            {
                if (iM.JustPressed(rotateClockWise)
                && jArray[playerIndex].AllowRotation(true, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                && !StackIntersect(jArray[playerIndex].NextRotatePosition(true))
                && !PlayerIntersect(playerIndex, true))
                {
                    jArray[playerIndex].Rotate(true);
                }
                if (iM.JustPressed(rotateCounterClockWise)
                && jArray[playerIndex].AllowRotation(false, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                && !StackIntersect(jArray[playerIndex].NextRotatePosition(false))
                && !PlayerIntersect(playerIndex, false))
                { jArray[playerIndex].Rotate(false); }
                if (iM.JustPressed(steerLeft)
                && !CheckLeftSide(jArray[playerIndex].jMatrix)
                && !CheckStackLeft(jArray[playerIndex].jMatrix)
                && !PlayerMovementLeftIntersect(playerIndex, jArray[playerIndex].jMatrix))
                { jArray[playerIndex].Move(-tileSize.X); }
                if (iM.JustPressed(steerRight)
                && !CheckRightSide(jArray[playerIndex].jMatrix)
                && !CheckStackRight(jArray[playerIndex].jMatrix)
                && !PlayerMovementRightIntersect(playerIndex, jArray[playerIndex].jMatrix))
                { jArray[playerIndex].Move(tileSize.X); }
                if (iM.JustPressed(steerDown)
                && !PlayerMovementDownIntersect(playerIndex, jArray[playerIndex].jMatrix))
                { if (!CheckFloor(jArray[playerIndex].jMatrix)) { jArray[playerIndex].Fall(tileSize.Y); } }
                if (iM.IsHeld(steerDown)
                && !PlayerMovementDownIntersect(playerIndex, jArray[playerIndex].jMatrix))
                {
                    if (!CheckFloor(jArray[playerIndex].jMatrix))
                    {
                        jArray[playerIndex].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (jArray[playerIndex].Time > 0.4f)
                        {
                            jArray[playerIndex].Fall(tileSize.Y);
                            jArray[playerIndex].Time = 0f;
                        }
                    }

                }
                if (jArray[playerIndex] != null && jArray[playerIndex].jMatrix != null && stackedBlocks.Length > 0)
                {
                    if (CheckOnStack(jArray[playerIndex].jMatrix))
                    {
                        CheckRowHandleScore(jArray[playerIndex].jMatrix, playerIndex);
                        AddDeadBlock(jArray[playerIndex].jMatrix);
                        RemoveClearedRow();
                        jArray[playerIndex] = null;
                    }
                }
            }
            if (iArray[playerIndex] != null)
            {

                if (iM.JustPressed(rotateClockWise)
                && iArray[playerIndex].AllowRotation(true, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                && !StackIntersect(iArray[playerIndex].NextRotatePosition(true))
                && !PlayerIntersect(playerIndex, true))
                {
                    iArray[playerIndex].Rotate(true);
                }
                if (iM.JustPressed(rotateCounterClockWise)
                && iArray[playerIndex].AllowRotation(false, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                && !StackIntersect(iArray[playerIndex].NextRotatePosition(false))
                && !PlayerIntersect(playerIndex, false))
                { iArray[playerIndex].Rotate(false); }
                if (iM.JustPressed(steerLeft)
                && !CheckLeftSide(iArray[playerIndex].iMatrix)
                && !CheckStackLeft(iArray[playerIndex].iMatrix)
                && !PlayerMovementLeftIntersect(playerIndex, iArray[playerIndex].iMatrix))
                { iArray[playerIndex].Move(-tileSize.X); }
                if (iM.JustPressed(steerRight)
                && !CheckRightSide(iArray[playerIndex].iMatrix)
                && !CheckStackRight(iArray[playerIndex].iMatrix)
                && !PlayerMovementRightIntersect(playerIndex, iArray[playerIndex].iMatrix))
                { iArray[playerIndex].Move(tileSize.X); }
                if (iM.JustPressed(steerDown)
                && !PlayerMovementDownIntersect(playerIndex, iArray[playerIndex].iMatrix))
                { if (!CheckFloor(iArray[playerIndex].iMatrix)) { iArray[playerIndex].Fall(tileSize.Y); } }
                if (iM.IsHeld(steerDown)
                && !PlayerMovementDownIntersect(playerIndex, iArray[playerIndex].iMatrix))
                {
                    if (!CheckFloor(iArray[playerIndex].iMatrix))
                    {
                        iArray[playerIndex].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (iArray[playerIndex].Time > 0.4f)
                        {
                            iArray[playerIndex].Fall(tileSize.Y);
                            iArray[playerIndex].Time = 0f;
                        }
                    }
                }
                if (iArray[playerIndex] != null && iArray[playerIndex].iMatrix != null && stackedBlocks.Length > 0)
                {
                    if (CheckOnStack(iArray[playerIndex].iMatrix))
                    {
                        CheckRowHandleScore(iArray[playerIndex].iMatrix, playerIndex);
                        AddDeadBlock(iArray[playerIndex].iMatrix);
                        RemoveClearedRow();
                        iArray[playerIndex] = null;
                    }
                }
            }
            if (tArray[playerIndex] != null)
            {

                if (iM.JustPressed(rotateClockWise)
                && tArray[playerIndex].AllowRotation(true, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                && !StackIntersect(tArray[playerIndex].NextRotatePosition(true))
                && !PlayerIntersect(playerIndex, true))
                {
                    tArray[playerIndex].Rotate(true);
                }
                if (iM.JustPressed(rotateCounterClockWise)
                && tArray[playerIndex].AllowRotation(false, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                && !StackIntersect(tArray[playerIndex].NextRotatePosition(false))
                && !PlayerIntersect(playerIndex, false))
                { tArray[playerIndex].Rotate(false); }
                if (iM.JustPressed(steerLeft)
                && !CheckLeftSide(tArray[playerIndex].tMatrix)
                && !CheckStackLeft(tArray[playerIndex].tMatrix)
                && !PlayerMovementLeftIntersect(playerIndex, tArray[playerIndex].tMatrix))
                { tArray[playerIndex].Move(-tileSize.X); }
                if (iM.JustPressed(steerRight)
                && !CheckRightSide(tArray[playerIndex].tMatrix)
                && !CheckStackRight(tArray[playerIndex].tMatrix)
                && !PlayerMovementRightIntersect(playerIndex, tArray[playerIndex].tMatrix))
                { tArray[playerIndex].Move(tileSize.X); }
                if (iM.JustPressed(steerDown)
                && !PlayerMovementDownIntersect(playerIndex, tArray[playerIndex].tMatrix))
                { if (!CheckFloor(tArray[playerIndex].tMatrix)) { tArray[playerIndex].Fall(tileSize.Y); } }
                if (iM.IsHeld(steerDown)
                && !PlayerMovementDownIntersect(playerIndex, tArray[playerIndex].tMatrix))
                {
                    if (!CheckFloor(tArray[playerIndex].tMatrix))
                    {
                        tArray[playerIndex].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (tArray[playerIndex].Time > 0.4f)
                        {
                            tArray[playerIndex].Fall(tileSize.Y);
                            tArray[playerIndex].Time = 0f;
                        }
                    }
                }
                if (tArray[playerIndex] != null && tArray[playerIndex].tMatrix != null && stackedBlocks.Length > 0)
                {
                    if (CheckOnStack(tArray[playerIndex].tMatrix))
                    {
                        CheckRowHandleScore(tArray[playerIndex].tMatrix, playerIndex);
                        AddDeadBlock(tArray[playerIndex].tMatrix);
                        RemoveClearedRow();
                        tArray[playerIndex] = null;
                    }
                }
            }
            if (oArray[playerIndex] != null)
            {
                if (iM.JustPressed(rotateClockWise)
                && oArray[playerIndex].AllowRotation(true, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                && !StackIntersect(oArray[playerIndex].NextRotatePosition(true))
                && !PlayerIntersect(playerIndex, true))
                {
                    oArray[playerIndex].Rotate(true);
                }
                if (iM.JustPressed(rotateCounterClockWise)
                && oArray[playerIndex].AllowRotation(false, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                && !StackIntersect(oArray[playerIndex].NextRotatePosition(false))
                && !PlayerIntersect(playerIndex, false))
                { oArray[playerIndex].Rotate(false); }
                if (iM.JustPressed(steerLeft)
                && !CheckLeftSide(oArray[playerIndex].oMatrix)
                && !CheckStackLeft(oArray[playerIndex].oMatrix)
                && !PlayerMovementLeftIntersect(playerIndex, oArray[playerIndex].oMatrix))
                { oArray[playerIndex].Move(-tileSize.X); }
                if (iM.JustPressed(steerRight)
                && !CheckRightSide(oArray[playerIndex].oMatrix)
                && !CheckStackRight(oArray[playerIndex].oMatrix)
                && !PlayerMovementRightIntersect(playerIndex, oArray[playerIndex].oMatrix))
                { oArray[playerIndex].Move(tileSize.X); }
                if (iM.JustPressed(steerDown)
                && !PlayerMovementDownIntersect(playerIndex, oArray[playerIndex].oMatrix))
                { if (!CheckFloor(oArray[playerIndex].oMatrix)) { oArray[playerIndex].Fall(tileSize.Y); } }
                if (iM.IsHeld(steerDown)
                && !PlayerMovementDownIntersect(playerIndex, oArray[playerIndex].oMatrix))
                {
                    if (!CheckFloor(oArray[playerIndex].oMatrix))
                    {
                        oArray[playerIndex].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (oArray[playerIndex].Time > 0.4f)
                        {
                            oArray[playerIndex].Fall(tileSize.Y);
                            oArray[playerIndex].Time = 0f;
                        }
                    }
                }
                if (oArray[playerIndex] != null && oArray[playerIndex].oMatrix != null && stackedBlocks.Length > 0)
                {
                    if (CheckOnStack(oArray[playerIndex].oMatrix))
                    {
                        CheckRowHandleScore(oArray[playerIndex].oMatrix, playerIndex);
                        AddDeadBlock(oArray[playerIndex].oMatrix);
                        RemoveClearedRow();
                        oArray[playerIndex] = null;
                    }
                }
            }
            if (lArray[playerIndex] != null)
            {
                if (iM.JustPressed(rotateClockWise)
                && lArray[playerIndex].AllowRotation(true, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                && !StackIntersect(lArray[playerIndex].NextRotatePosition(true))
                && !PlayerIntersect(playerIndex, true))
                {
                    lArray[playerIndex].Rotate(true);
                }
                if (iM.JustPressed(rotateCounterClockWise)
                && lArray[playerIndex].AllowRotation(false, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                && !StackIntersect(lArray[playerIndex].NextRotatePosition(false))
                && !PlayerIntersect(playerIndex, false))
                { lArray[playerIndex].Rotate(false); }
                if (iM.JustPressed(steerLeft)
                && !CheckLeftSide(lArray[playerIndex].lMatrix)
                && !CheckStackLeft(lArray[playerIndex].lMatrix)
                && !PlayerMovementLeftIntersect(playerIndex, lArray[playerIndex].lMatrix))
                { lArray[playerIndex].Move(-tileSize.X); }
                if (iM.JustPressed(steerRight)
                && !CheckRightSide(lArray[playerIndex].lMatrix)
                && !CheckStackRight(lArray[playerIndex].lMatrix)
                && !PlayerMovementRightIntersect(playerIndex, lArray[playerIndex].lMatrix))
                { lArray[playerIndex].Move(tileSize.X); }
                if (iM.JustPressed(steerDown)
                && !PlayerMovementDownIntersect(playerIndex, lArray[playerIndex].lMatrix))
                { if (!CheckFloor(lArray[playerIndex].lMatrix)) { lArray[playerIndex].Fall(tileSize.Y); } }
                if (iM.IsHeld(steerDown)
                && !PlayerMovementDownIntersect(playerIndex, lArray[playerIndex].lMatrix))
                {
                    if (!CheckFloor(lArray[playerIndex].lMatrix))
                    {
                        lArray[playerIndex].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (lArray[playerIndex].Time > 0.4f)
                        {
                            lArray[playerIndex].Fall(tileSize.Y);
                            lArray[playerIndex].Time = 0f;
                        }
                    }
                }
                if (lArray[playerIndex] != null && lArray[playerIndex].lMatrix != null && stackedBlocks.Length > 0)
                {
                    if (CheckOnStack(lArray[playerIndex].lMatrix))
                    {
                        CheckRowHandleScore(lArray[playerIndex].lMatrix, playerIndex);
                        AddDeadBlock(lArray[playerIndex].lMatrix);
                        RemoveClearedRow();
                        lArray[playerIndex] = null;
                    }
                }
            }
            if (sArray[playerIndex] != null)
            {

                if (iM.JustPressed(rotateClockWise)
                && sArray[playerIndex].AllowRotation(true, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                && !StackIntersect(sArray[playerIndex].NextRotatePosition(true))
                && !PlayerIntersect(playerIndex, true))
                {
                    sArray[playerIndex].Rotate(true);
                }
                if (iM.JustPressed(rotateCounterClockWise)
                && sArray[playerIndex].AllowRotation(false, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                && !StackIntersect(sArray[playerIndex].NextRotatePosition(false))
                && !PlayerIntersect(playerIndex, false))
                { sArray[playerIndex].Rotate(false); }
                if (iM.JustPressed(steerLeft)
                && !CheckLeftSide(sArray[playerIndex].sMatrix)
                && !CheckStackLeft(sArray[playerIndex].sMatrix)
                && !PlayerMovementLeftIntersect(playerIndex, sArray[playerIndex].sMatrix))
                { sArray[playerIndex].Move(-tileSize.X); }
                if (iM.JustPressed(steerRight)
                && !CheckRightSide(sArray[playerIndex].sMatrix)
                && !CheckStackRight(sArray[playerIndex].sMatrix)
                && !PlayerMovementRightIntersect(playerIndex, sArray[playerIndex].sMatrix))
                { sArray[playerIndex].Move(tileSize.X); }
                if (iM.JustPressed(steerDown)
                && !PlayerMovementDownIntersect(playerIndex, sArray[playerIndex].sMatrix))
                { if (!CheckFloor(sArray[playerIndex].sMatrix)) { sArray[playerIndex].Fall(tileSize.Y); } }
                if (iM.IsHeld(steerDown)
                && !PlayerMovementDownIntersect(playerIndex, sArray[playerIndex].sMatrix))
                {
                    if (!CheckFloor(sArray[playerIndex].sMatrix))
                    {
                        sArray[playerIndex].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (sArray[playerIndex].Time > 0.4f)
                        {
                            sArray[playerIndex].Fall(tileSize.Y);
                            sArray[playerIndex].Time = 0f;
                        }
                    }
                }
                if (sArray[playerIndex] != null && sArray[playerIndex].sMatrix != null && stackedBlocks.Length > 0)
                {
                    if (CheckOnStack(sArray[playerIndex].sMatrix))
                    {
                        CheckRowHandleScore(sArray[playerIndex].sMatrix, playerIndex);
                        AddDeadBlock(sArray[playerIndex].sMatrix);
                        RemoveClearedRow();
                        sArray[playerIndex] = null;
                    }
                }
            }
            if (zArray[playerIndex] != null)
            {
                if (iM.JustPressed(rotateClockWise)
                && zArray[playerIndex].AllowRotation(true, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                && !StackIntersect(zArray[playerIndex].NextRotatePosition(true))
                && !PlayerIntersect(playerIndex, true))
                {
                    zArray[playerIndex].Rotate(true);
                }
                if (iM.JustPressed(rotateCounterClockWise)
                && zArray[playerIndex].AllowRotation(false, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                && !StackIntersect(zArray[playerIndex].NextRotatePosition(false))
                && !PlayerIntersect(playerIndex, false))
                { zArray[playerIndex].Rotate(false); }
                if (iM.JustPressed(steerLeft)
                && !CheckLeftSide(zArray[playerIndex].zMatrix)
                && !CheckStackLeft(zArray[playerIndex].zMatrix)
                && !PlayerMovementLeftIntersect(playerIndex, zArray[playerIndex].zMatrix))
                { zArray[playerIndex].Move(-tileSize.X); }
                if (iM.JustPressed(steerRight)
                && !CheckRightSide(zArray[playerIndex].zMatrix)
                && !CheckStackRight(zArray[playerIndex].zMatrix)
                && !PlayerMovementRightIntersect(playerIndex, zArray[playerIndex].zMatrix))
                { zArray[playerIndex].Move(tileSize.X); }
                if (iM.JustPressed(steerDown)
                && !PlayerMovementDownIntersect(playerIndex, zArray[playerIndex].zMatrix))
                { if (!CheckFloor(zArray[playerIndex].zMatrix)) { zArray[playerIndex].Fall(tileSize.Y); } }
                if (iM.IsHeld(steerDown)
                && !PlayerMovementDownIntersect(playerIndex, zArray[playerIndex].zMatrix))
                {
                    if (!CheckFloor(zArray[playerIndex].zMatrix))
                    {
                        zArray[playerIndex].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (zArray[playerIndex].Time > 0.4f)
                        {
                            zArray[playerIndex].Fall(tileSize.Y);
                            zArray[playerIndex].Time = 0f;
                        }
                    }
                }
                if (zArray[playerIndex] != null && zArray[playerIndex].zMatrix != null && stackedBlocks.Length > 0)
                {
                    if (CheckOnStack(zArray[playerIndex].zMatrix))
                    {
                        CheckRowHandleScore(zArray[playerIndex].zMatrix, playerIndex);
                        AddDeadBlock(zArray[playerIndex].zMatrix);
                        RemoveClearedRow();
                        zArray[playerIndex] = null;
                    }
                }
            }
        }
        private void GamePadSteering(GameTime gameTime, int playerIndex, InputManager iM)//unfortunately not a very practical/nice method but it works ( we will se what can be done later about this )
        {
            Buttons moveLeft = p1MoveLeft;
            Buttons moveRight = p1MoveRight;
            Buttons moveDown = p1MoveDown;
            Buttons powerUp = p1PowerUp;
            Buttons start = p1Start;
            Buttons rotateCW = p1RotateCC;
            Buttons rotateCC = p1RotateCW;

            if(playerIndex == playerTwoIndex)
            {
                moveLeft = p2MoveLeft;
                moveRight = p2MoveRight;
                moveDown = p2MoveDown;
                powerUp = p2PowerUp;
                start = p2Start;
                rotateCW = p2RotateCC;
                rotateCC = p2RotateCW;
            }

            if (jArray[playerIndex] != null)
            {
                if (iM.JustPressed(rotateCW, playerIndex)
                    && jArray[playerIndex].AllowRotation(true, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                    && !StackIntersect(jArray[playerIndex].NextRotatePosition(true))
                    && !PlayerIntersect(playerIndex, true))
                {
                    jArray[playerIndex].Rotate(true);
                }
                if (iM.JustPressed(rotateCC, playerIndex)
                    && jArray[playerIndex].AllowRotation(false, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                    && !StackIntersect(jArray[playerIndex].NextRotatePosition(false))
                    && !PlayerIntersect(playerIndex, false))
                { jArray[playerIndex].Rotate(false); }
                if (iM.JustPressed(moveLeft, playerIndex)
                    && !CheckLeftSide(jArray[playerIndex].jMatrix)
                    && !CheckStackLeft(jArray[playerIndex].jMatrix)
                    && !PlayerMovementLeftIntersect(playerIndex, jArray[playerIndex].jMatrix))
                { jArray[playerIndex].Move(-tileSize.X); }
                if (iM.JustPressed(moveRight, playerIndex)
                    && !CheckRightSide(jArray[playerIndex].jMatrix)
                    && !CheckStackRight(jArray[playerIndex].jMatrix)
                    && !PlayerMovementRightIntersect(playerIndex, jArray[playerIndex].jMatrix))
                { jArray[playerIndex].Move(tileSize.X); }
                if (iM.JustPressed(moveDown, playerIndex)
                    && !PlayerMovementDownIntersect(playerIndex, jArray[playerIndex].jMatrix))
                { if (!CheckFloor(jArray[playerIndex].jMatrix)) { jArray[playerIndex].Fall(tileSize.Y); } }
                if (iM.IsHeld(moveDown, playerIndex)
                    && !PlayerMovementDownIntersect(playerIndex, jArray[playerIndex].jMatrix))
                {
                    if (!CheckFloor(jArray[playerIndex].jMatrix))
                    {
                        jArray[playerIndex].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (jArray[playerIndex].Time > 0.4f)
                        {
                            jArray[playerIndex].Fall(tileSize.Y);
                            jArray[playerIndex].Time = 0f;
                        }
                    }
                }

                if (jArray[playerIndex] != null && jArray[playerIndex].jMatrix != null && stackedBlocks.Length > 0)
                {
                    if (CheckOnStack(jArray[playerIndex].jMatrix))
                    {
                        CheckRowHandleScore(jArray[playerIndex].jMatrix, playerIndex);
                        AddDeadBlock(jArray[playerIndex].jMatrix);
                        RemoveClearedRow();
                        jArray[playerIndex] = null;
                    }
                }
            }
            if (iArray[playerIndex] != null)
            {
                if (iM.JustPressed(rotateCW, playerIndex)
                    && iArray[playerIndex].AllowRotation(true, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                    && !StackIntersect(iArray[playerIndex].NextRotatePosition(true))
                    && !PlayerIntersect(playerIndex, true))
                {
                    iArray[playerIndex].Rotate(true);
                }
                if (iM.JustPressed(rotateCC, playerIndex)
                    && iArray[playerIndex].AllowRotation(false, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                    && !StackIntersect(iArray[playerIndex].NextRotatePosition(false))
                    && !PlayerIntersect(playerIndex, false))
                { iArray[playerIndex].Rotate(false); }
                if (iM.JustPressed(moveLeft, playerIndex)
                    && !CheckLeftSide(iArray[playerIndex].iMatrix)
                    && !CheckStackLeft(iArray[playerIndex].iMatrix)
                    && !PlayerMovementLeftIntersect(playerIndex, iArray[playerIndex].iMatrix))
                { iArray[playerIndex].Move(-tileSize.X); }
                if (iM.JustPressed(moveRight, playerIndex)
                    && !CheckRightSide(iArray[playerIndex].iMatrix)
                    && !CheckStackRight(iArray[playerIndex].iMatrix)
                    && !PlayerMovementRightIntersect(playerIndex, iArray[playerIndex].iMatrix))
                { iArray[playerIndex].Move(tileSize.X); }
                if (iM.JustPressed(moveDown, playerIndex)
                    && !PlayerMovementDownIntersect(playerIndex, iArray[playerIndex].iMatrix))
                { if (!CheckFloor(iArray[playerIndex].iMatrix)) { iArray[playerIndex].Fall(tileSize.Y); } }
                if (iM.IsHeld(moveDown, playerIndex)
                    && !PlayerMovementDownIntersect(playerIndex, iArray[playerIndex].iMatrix))
                {
                    if (!CheckFloor(iArray[playerIndex].iMatrix))
                    {
                        iArray[playerIndex].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (iArray[playerIndex].Time > 0.4f)
                        {
                            iArray[playerIndex].Fall(tileSize.Y);
                            iArray[playerIndex].Time = 0f;
                        }
                    }
                }
                if (iArray[playerIndex] != null && iArray[playerIndex].iMatrix != null && stackedBlocks.Length > 0)
                {
                    if (CheckOnStack(iArray[playerIndex].iMatrix))
                    {
                        CheckRowHandleScore(iArray[playerIndex].iMatrix, playerIndex);
                        AddDeadBlock(iArray[playerIndex].iMatrix);
                        RemoveClearedRow();
                        iArray[playerIndex] = null;
                    }
                }
            }
            if (tArray[playerIndex] != null)
            {
                if (iM.JustPressed(rotateCW, playerIndex)
                    && tArray[playerIndex].AllowRotation(true, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                    && !StackIntersect(tArray[playerIndex].NextRotatePosition(true))
                    && !PlayerIntersect(playerIndex, true))
                {
                    tArray[playerIndex].Rotate(true);
                }
                if (iM.JustPressed(rotateCC, playerIndex)
                    && tArray[playerIndex].AllowRotation(false, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                    && !StackIntersect(tArray[playerIndex].NextRotatePosition(false))
                    && !PlayerIntersect(playerIndex, false))
                { tArray[playerIndex].Rotate(false); }
                if (iM.JustPressed(moveLeft, playerIndex)
                    && !CheckLeftSide(tArray[playerIndex].tMatrix)
                    && !CheckStackLeft(tArray[playerIndex].tMatrix)
                    && !PlayerMovementLeftIntersect(playerIndex, tArray[playerIndex].tMatrix))
                { tArray[playerIndex].Move(-tileSize.X); }
                if (iM.JustPressed(moveRight, playerIndex)
                    && !CheckRightSide(tArray[playerIndex].tMatrix)
                    && !CheckStackRight(tArray[playerIndex].tMatrix)
                    && !PlayerMovementRightIntersect(playerIndex, tArray[playerIndex].tMatrix))
                { tArray[playerIndex].Move(tileSize.X); }
                if (iM.JustPressed(moveDown, playerIndex)
                    && !PlayerMovementDownIntersect(playerIndex, tArray[playerIndex].tMatrix))
                { if (!CheckFloor(tArray[playerIndex].tMatrix)) { tArray[playerIndex].Fall(tileSize.Y); } }
                if (iM.IsHeld(moveDown, playerIndex)
                    && !PlayerMovementDownIntersect(playerIndex, tArray[playerIndex].tMatrix))
                {
                    if (!CheckFloor(tArray[playerIndex].tMatrix))
                    {
                        tArray[playerIndex].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (tArray[playerIndex].Time > 0.4f)
                        {
                            tArray[playerIndex].Fall(tileSize.Y);
                            tArray[playerIndex].Time = 0f;
                        }
                    }
                }
                if (tArray[playerIndex] != null && tArray[playerIndex].tMatrix != null && stackedBlocks.Length > 0)
                {
                    if (CheckOnStack(tArray[playerIndex].tMatrix))
                    {
                        CheckRowHandleScore(tArray[playerIndex].tMatrix, playerIndex);
                        AddDeadBlock(tArray[playerIndex].tMatrix);
                        RemoveClearedRow();
                        tArray[playerIndex] = null;
                    }
                }
            }
            if (oArray[playerIndex] != null)
            {
                if (iM.JustPressed(rotateCW, playerIndex)
                    && oArray[playerIndex].AllowRotation(true, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                    && !StackIntersect(oArray[playerIndex].NextRotatePosition(true))
                    && !PlayerIntersect(playerIndex, true))
                {
                    oArray[playerIndex].Rotate(true);
                }
                if (iM.JustPressed(rotateCC, playerIndex)
                    && oArray[playerIndex].AllowRotation(false, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                    && !StackIntersect(oArray[playerIndex].NextRotatePosition(false))
                    && !PlayerIntersect(playerIndex, false))
                { oArray[playerIndex].Rotate(false); }
                if (iM.JustPressed(moveLeft, playerIndex)
                    && !CheckLeftSide(oArray[playerIndex].oMatrix)
                    && !CheckStackLeft(oArray[playerIndex].oMatrix)
                    && !PlayerMovementLeftIntersect(playerIndex, oArray[playerIndex].oMatrix))
                { oArray[playerIndex].Move(-tileSize.X); }
                if (iM.JustPressed(moveRight, playerIndex)
                    && !CheckRightSide(oArray[playerIndex].oMatrix)
                    && !CheckStackRight(oArray[playerIndex].oMatrix)
                    && !PlayerMovementRightIntersect(playerIndex, oArray[playerIndex].oMatrix))
                { oArray[playerIndex].Move(tileSize.X); }
                if (iM.JustPressed(moveDown, playerIndex)
                    && !PlayerMovementDownIntersect(playerIndex, oArray[playerIndex].oMatrix))
                { if (!CheckFloor(oArray[playerIndex].oMatrix)) { oArray[playerIndex].Fall(tileSize.Y); } }
                if (iM.IsHeld(moveDown, playerIndex)
                    && !PlayerMovementDownIntersect(playerIndex, oArray[playerIndex].oMatrix))
                {
                    if (!CheckFloor(oArray[playerIndex].oMatrix))
                    {
                        oArray[playerIndex].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (oArray[playerIndex].Time > 0.4f)
                        {
                            oArray[playerIndex].Fall(tileSize.Y);
                            oArray[playerIndex].Time = 0f;
                        }
                    }
                }
                if (oArray[playerIndex] != null && oArray[playerIndex].oMatrix != null && stackedBlocks.Length > 0)
                {
                    if (CheckOnStack(oArray[playerIndex].oMatrix))
                    {
                        CheckRowHandleScore(oArray[playerIndex].oMatrix, playerIndex);
                        AddDeadBlock(oArray[playerIndex].oMatrix);
                        RemoveClearedRow();
                        oArray[playerIndex] = null;
                    }
                }
            }
            if (lArray[playerIndex] != null)
            {
                if (iM.JustPressed(rotateCW, playerIndex)
                    && lArray[playerIndex].AllowRotation(true, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                    && !StackIntersect(lArray[playerIndex].NextRotatePosition(true))
                    && !PlayerIntersect(playerIndex, true))
                {
                    lArray[playerIndex].Rotate(true);
                }
                if (iM.JustPressed(rotateCC, playerIndex)
                    && lArray[playerIndex].AllowRotation(false, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                    && !StackIntersect(lArray[playerIndex].NextRotatePosition(false))
                    && !PlayerIntersect(playerIndex, false))
                { lArray[playerIndex].Rotate(false); }
                if (iM.JustPressed(moveLeft, playerIndex)
                    && !CheckLeftSide(lArray[playerIndex].lMatrix)
                    && !CheckStackLeft(lArray[playerIndex].lMatrix)
                    && !PlayerMovementLeftIntersect(playerIndex, lArray[playerIndex].lMatrix))
                { lArray[playerIndex].Move(-tileSize.X); }
                if (iM.JustPressed(moveRight, playerIndex)
                    && !CheckRightSide(lArray[playerIndex].lMatrix)
                    && !CheckStackRight(lArray[playerIndex].lMatrix)
                    && !PlayerMovementRightIntersect(playerIndex, lArray[playerIndex].lMatrix))
                { lArray[playerIndex].Move(tileSize.X); }
                if (iM.JustPressed(moveDown, playerIndex)
                    && !PlayerMovementDownIntersect(playerIndex, lArray[playerIndex].lMatrix))
                { if (!CheckFloor(lArray[playerIndex].lMatrix)) { lArray[playerIndex].Fall(tileSize.Y); } }
                if (iM.IsHeld(moveDown, playerIndex)
                    && !PlayerMovementDownIntersect(playerIndex, lArray[playerIndex].lMatrix))
                {
                    if (!CheckFloor(lArray[playerIndex].lMatrix))
                    {
                        lArray[playerIndex].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (lArray[playerIndex].Time > 0.4f)
                        {
                            lArray[playerIndex].Fall(tileSize.Y);
                            lArray[playerIndex].Time = 0f;
                        }
                    }
                }
                if (lArray[playerIndex] != null && lArray[playerIndex].lMatrix != null && stackedBlocks.Length > 0)
                {
                    if (CheckOnStack(lArray[playerIndex].lMatrix))
                    {
                        CheckRowHandleScore(lArray[playerIndex].lMatrix, playerIndex);
                        AddDeadBlock(lArray[playerIndex].lMatrix);
                        RemoveClearedRow();
                        lArray[playerIndex] = null;
                    }
                }
            }
            if (sArray[playerIndex] != null)
            {
                if (iM.JustPressed(rotateCW, playerIndex)
                    && sArray[playerIndex].AllowRotation(true, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                    && !StackIntersect(sArray[playerIndex].NextRotatePosition(true))
                    && !PlayerIntersect(playerIndex, true))
                {
                    sArray[playerIndex].Rotate(true);
                }
                if (iM.JustPressed(rotateCC, playerIndex)
                    && sArray[playerIndex].AllowRotation(false, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                    && !StackIntersect(sArray[playerIndex].NextRotatePosition(false))
                    && !PlayerIntersect(playerIndex, false))
                { sArray[playerIndex].Rotate(false); }
                if (iM.JustPressed(moveLeft, playerIndex)
                    && !CheckLeftSide(sArray[playerIndex].sMatrix)
                    && !CheckStackLeft(sArray[playerIndex].sMatrix)
                    && !PlayerMovementLeftIntersect(playerIndex, sArray[playerIndex].sMatrix))
                { sArray[playerIndex].Move(-tileSize.X); }
                if (iM.JustPressed(moveRight, playerIndex)
                    && !CheckRightSide(sArray[playerIndex].sMatrix)
                    && !CheckStackRight(sArray[playerIndex].sMatrix)
                    && !PlayerMovementRightIntersect(playerIndex, sArray[playerIndex].sMatrix))
                { sArray[playerIndex].Move(tileSize.X); }
                if (iM.JustPressed(moveDown, playerIndex)
                    && !PlayerMovementDownIntersect(playerIndex, sArray[playerIndex].sMatrix))
                { if (!CheckFloor(sArray[playerIndex].sMatrix)) { sArray[playerIndex].Fall(tileSize.Y); } }
                if (iM.IsHeld(moveDown, playerIndex)
                    && !PlayerMovementDownIntersect(playerIndex, sArray[playerIndex].sMatrix))
                {
                    if (!CheckFloor(sArray[playerIndex].sMatrix))
                    {
                        sArray[playerIndex].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (sArray[playerIndex].Time > 0.4f)
                        {
                            sArray[playerIndex].Fall(tileSize.Y);
                            sArray[playerIndex].Time = 0f;
                        }
                    }
                }
                if (sArray[playerIndex] != null && sArray[playerIndex].sMatrix != null && stackedBlocks.Length > 0)
                {
                    if (CheckOnStack(sArray[playerIndex].sMatrix))
                    {
                        CheckRowHandleScore(sArray[playerIndex].sMatrix, playerIndex);
                        AddDeadBlock(sArray[playerIndex].sMatrix);
                        RemoveClearedRow();
                        sArray[playerIndex] = null;
                    }
                }
            }
            if (zArray[playerIndex] != null)
            {
                if (iM.JustPressed(rotateCW, playerIndex)
                    && zArray[playerIndex].AllowRotation(true, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                    && !StackIntersect(zArray[playerIndex].NextRotatePosition(true))
                    && !PlayerIntersect(playerIndex, true))
                {
                    zArray[playerIndex].Rotate(true);
                }
                if (iM.JustPressed(rotateCC, playerIndex)
                    && zArray[playerIndex].AllowRotation(false, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos)
                    && !StackIntersect(zArray[playerIndex].NextRotatePosition(false))
                    && !PlayerIntersect(playerIndex, false))
                { zArray[playerIndex].Rotate(false); }
                if (iM.JustPressed(moveLeft, playerIndex)
                    && !CheckLeftSide(zArray[playerIndex].zMatrix)
                    && !CheckStackLeft(zArray[playerIndex].zMatrix)
                    && !PlayerMovementLeftIntersect(playerIndex, zArray[playerIndex].zMatrix))
                { zArray[playerIndex].Move(-tileSize.X); }
                if (iM.JustPressed(moveRight, playerIndex)
                    && !CheckRightSide(zArray[playerIndex].zMatrix)
                    && !CheckStackRight(zArray[playerIndex].zMatrix)
                    && !PlayerMovementRightIntersect(playerIndex, zArray[playerIndex].zMatrix))
                { zArray[playerIndex].Move(tileSize.X); }
                if (iM.JustPressed(moveDown, playerIndex)
                    && !PlayerMovementDownIntersect(playerIndex, zArray[playerIndex].zMatrix))
                { if (!CheckFloor(zArray[playerIndex].zMatrix)) { zArray[playerIndex].Fall(tileSize.Y); } }
                if (iM.IsHeld(moveDown, playerIndex)
                    && !PlayerMovementDownIntersect(playerIndex, zArray[playerIndex].zMatrix))
                {
                    if (!CheckFloor(zArray[playerIndex].zMatrix))
                    {
                        zArray[playerIndex].Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (zArray[playerIndex].Time > 0.4f)
                        {
                            zArray[playerIndex].Fall(tileSize.Y);
                            zArray[playerIndex].Time = 0f;
                        }
                    }
                }
                if (zArray[playerIndex] != null && zArray[playerIndex].zMatrix != null && stackedBlocks.Length > 0)
                {
                    if (CheckOnStack(zArray[playerIndex].zMatrix))
                    {
                        CheckRowHandleScore(zArray[playerIndex].zMatrix, playerIndex);
                        AddDeadBlock(zArray[playerIndex].zMatrix);
                        RemoveClearedRow();
                        zArray[playerIndex] = null;
                    }
                }
            }
        }

        private string RandomBlock()
        {
            Random rnd = new Random();
            string[] feedRandomMachine = new string[] { "J", "I", "T", "O", "L", "S", "Z" };
            return feedRandomMachine[rnd.Next(0, feedRandomMachine.Length)];
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            switch (currentPlayState)
            {
                case PlayState.play:
                    sideBars.Draw(spriteBatch, gameTime);
                    foreach (GameObject item in playfield)
                    {
                        item.Draw(spriteBatch);
                    }
                    //Draw for blocks
                    if (jArray[playerOneIndex] != null) { jArray[playerOneIndex].Draw(spriteBatch, gameTime); }
                    if (iArray[playerOneIndex] != null) { iArray[playerOneIndex].Draw(spriteBatch, gameTime); }
                    if (tArray[playerOneIndex] != null) { tArray[playerOneIndex].Draw(spriteBatch, gameTime); }
                    if (oArray[playerOneIndex] != null) { oArray[playerOneIndex].Draw(spriteBatch, gameTime); }
                    if (lArray[playerOneIndex] != null) { lArray[playerOneIndex].Draw(spriteBatch, gameTime); }
                    if (sArray[playerOneIndex] != null) { sArray[playerOneIndex].Draw(spriteBatch, gameTime); }
                    if (zArray[playerOneIndex] != null) { zArray[playerOneIndex].Draw(spriteBatch, gameTime); }
                    if (jArray[playerTwoIndex] != null) { jArray[playerTwoIndex].Draw(spriteBatch, gameTime); }
                    if (iArray[playerTwoIndex] != null) { iArray[playerTwoIndex].Draw(spriteBatch, gameTime); }
                    if (tArray[playerTwoIndex] != null) { tArray[playerTwoIndex].Draw(spriteBatch, gameTime); }
                    if (oArray[playerTwoIndex] != null) { oArray[playerTwoIndex].Draw(spriteBatch, gameTime); }
                    if (lArray[playerTwoIndex] != null) { lArray[playerTwoIndex].Draw(spriteBatch, gameTime); }
                    if (sArray[playerTwoIndex] != null) { sArray[playerTwoIndex].Draw(spriteBatch, gameTime); }
                    if (zArray[playerTwoIndex] != null) { zArray[playerTwoIndex].Draw(spriteBatch, gameTime); }
                    ///The stack of dead blocks
                    if (stackedBlocks.Length > 0) { foreach (TetrisObject item in stackedBlocks) { if (item != null) { item.Draw(spriteBatch, Color.White); } } }

                    //Weapons
                    if (bloodOrb != null) { bloodOrb.Draw(spriteBatch); }
                    if (pistol != null) { pistol.Draw(spriteBatch); }
                    //Effects
                    if (explosion != null) { explosion.Draw(spriteBatch); }
                    break;
                case PlayState.pause:
                    Pause.Draw(spriteBatch);
                    break;
                case PlayState.gameover:
                    gameOver.Draw(spriteBatch);
                    break;
                case PlayState.qte:
                    qte.Draw(spriteBatch, gameTime);
                    break;
            }
        }
    }
}
