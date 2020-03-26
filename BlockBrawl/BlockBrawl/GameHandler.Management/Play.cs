using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BlockBrawl.Blocks;
using BlockBrawl.Objects;

namespace BlockBrawl
{
    class Play
    {
        GameTime gameTime;

        GameObject[,] playfield;
        Vector2 tileSize;

        //Alla olika blocks hamnar i resp array. Insert görs i array med playerIndex för att igenkänna vems som är vems.
        I[] iArray;
        J[] jArray;

        //Nextblock
        string[] nextBlock;

        //Blocks på spelplanen hamnar i en lista
        TetrisObject[,] stoppedblocks;

        //Hemmagjort inputManager
        InputManager iM;

        public Play(int tilesX, int tilesY, Vector2 tileSize, int gameWidth)
        {
            this.tileSize = tileSize;

            playfield = new GameObject[tilesX, tilesY];
            PopulatePlayfield(tilesX, tilesY, tileSize, gameWidth);

            //Im
            iM = new InputManager();

            //Nextblock med playerindex som karta för nästa block (plats 0 används aldrig i praktiken).
            nextBlock = new string[3];
            nextBlock[1] = RandomBlock();
            nextBlock[2] = RandomBlock();

            //Alla blocks som gått hela vägen ner ska delas upp och hamna i en lista.
            stoppedblocks = new TetrisObject[playfield.GetLength(0), playfield.GetLength(1)];

            //Initierar Arrayer, plats 0 används inte i praktiken
            jArray = new J[3];
            iArray = new I[3];
        }
        private void PopulatePlayfield(int tilesX, int tilesY, Vector2 tileSize, int gameWidth)
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
            PlayerStearing(gameTime, 1);
            PlayerStearing(gameTime, 2);
            GetBlocks(1);
            GetBlocks(2);
        }
        private void GetBlocks(int playerIndex)
        {
            Random rnd = new Random();
            if (jArray[playerIndex] == null && iArray[playerIndex] == null)
            {
                switch (nextBlock[playerIndex])
                {
                    case "J":
                        jArray[playerIndex] = new J(TextureManager.blueBlock, playfield[rnd.Next(0, playfield.GetLength(0) - 3), 0].Pos);
                        nextBlock[playerIndex] = null;
                        break;
                    case "I":
                        iArray[playerIndex] = new I(TextureManager.lightgreenBlock, playfield[rnd.Next(0, playfield.GetLength(0) - 3), 0].Pos);
                        nextBlock[playerIndex] = null;
                        break;
                }
            }
            if (nextBlock[playerIndex] == null) { nextBlock[playerIndex] = RandomBlock(); }
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
                foreach (TetrisObject stackblock in stoppedblocks)
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

        private void AddDeadBlock(TetrisObject[,] tetrisObjects)
        {
            foreach (TetrisObject item in tetrisObjects)
            {
                for (int i = 0; i < stoppedblocks.GetLength(0); i++)
                {
                    for (int j = 0; j < stoppedblocks.GetLength(1); j++)
                    {
                        if (item.Pos == playfield[i, j].Pos && item.alive)
                        {
                            stoppedblocks[i, j] = new TetrisObject(item.Pos, item.tex);
                        }
                    }
                }
            }
        }
        private void PlayerStearing(GameTime gameTime, int playerIndex)//Massa JustPressed tyvärr
        {
            if (jArray[playerIndex] != null && CheckFloor(jArray[playerIndex].jMatrix))//Bryt ut till metod när den funkar[2 dagar senare->] FAN GÅR JU INTE!!!!!!!!!!!
            {
                AddDeadBlock(jArray[playerIndex].jMatrix);
                jArray[playerIndex] = null;
            }
            else if (jArray[playerIndex] != null)
            {
                jArray[playerIndex].time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (jArray[playerIndex].time > 1f && !CheckFloor(jArray[playerIndex].jMatrix))
                {
                    jArray[playerIndex].Fall(tileSize.X); jArray[playerIndex].time = 0f;
                }
                if (iM.JustPressed(Buttons.B, playerIndex) && jArray[playerIndex].AllowRotation(true, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos))
                {
                    jArray[playerIndex].Rotate(true);
                }
                if (iM.JustPressed(Buttons.Y, playerIndex) && jArray[playerIndex].AllowRotation(false, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos))
                { jArray[playerIndex].Rotate(false); }
                if (iM.JustPressed(Buttons.DPadLeft, playerIndex) && !CheckLeftSide(jArray[playerIndex].jMatrix))
                { jArray[playerIndex].Move(-tileSize.X); }
                if (iM.JustPressed(Buttons.DPadRight, playerIndex) && !CheckRightSide(jArray[playerIndex].jMatrix))
                { jArray[playerIndex].Move(tileSize.X); }
                if (iM.JustPressed(Buttons.DPadDown, playerIndex) || iM.IsHeld(Buttons.DPadDown, playerIndex))
                { if (!CheckFloor(jArray[playerIndex].jMatrix)) { jArray[playerIndex].Fall(tileSize.Y); } }


                if (jArray[playerIndex] != null && jArray[playerIndex].jMatrix != null && stoppedblocks.Length > 0)
                {
                    if (CheckOnStack(jArray[playerIndex].jMatrix))
                    {
                        AddDeadBlock(jArray[playerIndex].jMatrix);
                        jArray[playerIndex] = null;
                    }
                }
            }
            if (iArray[playerIndex] != null && CheckFloor(iArray[playerIndex].iMatrix))//Bryt ut till metod när den funkar
            {
                AddDeadBlock(iArray[playerIndex].iMatrix);
                iArray[playerIndex] = null;
            }
            else if (iArray[playerIndex] != null)
            {

                iArray[playerIndex].time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (iArray[playerIndex].time > 1f && !CheckFloor(iArray[playerIndex].iMatrix))
                {
                    iArray[playerIndex].Fall(tileSize.X); iArray[playerIndex].time = 0f;
                }

                if (iM.JustPressed(Buttons.B, playerIndex) && iArray[playerIndex].AllowRotation(true, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos))
                {
                    iArray[playerIndex].Rotate(true);
                }
                if (iM.JustPressed(Buttons.Y, playerIndex) && iArray[playerIndex].AllowRotation(false, playfield[playfield.GetLength(0) - 1, playfield.GetLength(1) - 1].Pos, playfield[0, 0].Pos))
                { iArray[playerIndex].Rotate(false); }
                if (iM.JustPressed(Buttons.DPadLeft, playerIndex) && !CheckLeftSide(iArray[playerIndex].iMatrix))
                { iArray[playerIndex].Move(-tileSize.X); }
                if (iM.JustPressed(Buttons.DPadRight, playerIndex) && !CheckRightSide(iArray[playerIndex].iMatrix))
                { iArray[playerIndex].Move(tileSize.X); }
                if (iM.JustPressed(Buttons.DPadDown, playerIndex) || iM.IsHeld(Buttons.DPadDown, playerIndex))
                { if (!CheckFloor(iArray[playerIndex].iMatrix)) { iArray[playerIndex].Fall(tileSize.Y); } }

                if (iArray[playerIndex] != null && iArray[playerIndex].iMatrix != null && stoppedblocks.Length > 0)
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
            if (jArray[1] != null) { jArray[1].Draw(spriteBatch); }
            if (iArray[1] != null) { iArray[1].Draw(spriteBatch); }
            if (jArray[2] != null) { jArray[2].Draw(spriteBatch); }
            if (iArray[2] != null) { iArray[2].Draw(spriteBatch); }
            if (stoppedblocks.Length > 0) { foreach (TetrisObject item in stoppedblocks) { if (item != null) { item.Draw(spriteBatch); } } }
        }
    }
}
