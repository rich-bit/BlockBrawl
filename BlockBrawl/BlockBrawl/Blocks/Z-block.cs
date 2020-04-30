﻿using BlockBrawl.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockBrawl.Blocks
{
    class Z
    {
        public TetrisObject[,] zMatrix;


        public Texture2D Color { get; set; }
        public float Time { get; set; }
        public enum BlockState
        {
            one,
            two,
            three,
            four,
        }
        public BlockState Formation { get; set; }
        public Z(Texture2D color, Vector2 startPos)
        {
            Color = color;

            zMatrix = new TetrisObject[3, 3];

            //Contents of the I, looping through the matrix setting positions and textures
            for (int i = 0; i < zMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < zMatrix.GetLength(1); j++)
                {
                    zMatrix[i, j] = new TetrisObject(Vector2.Zero, color)
                    {
                        PosX = startPos.X + i * color.Width,
                        PosY = startPos.Y + j * color.Height
                    };
                }
            }
            Formation = BlockState.one;//Default formation of the T figure
            UpdateFormation();
        }
        private void UpdateFormation()
        {
            switch (Formation)
            {
                case BlockState.one:
                    foreach (TetrisObject item in zMatrix) { item.ChangeState(true); }
                    zMatrix[0, 0].ChangeState(false);
                    zMatrix[1, 0].ChangeState(false);
                    zMatrix[1, 1].ChangeState(false);
                    zMatrix[2, 1].ChangeState(false);
                    break;
                case BlockState.two:
                    foreach (TetrisObject item in zMatrix) { item.ChangeState(true); }
                    zMatrix[2, 0].ChangeState(false);
                    zMatrix[1, 1].ChangeState(false);
                    zMatrix[2, 1].ChangeState(false);
                    zMatrix[1, 2].ChangeState(false);
                    break;
                case BlockState.three:
                    foreach (TetrisObject item in zMatrix) { item.ChangeState(true); }
                    zMatrix[0, 1].ChangeState(false);
                    zMatrix[1, 1].ChangeState(false);
                    zMatrix[1, 2].ChangeState(false);
                    zMatrix[2, 2].ChangeState(false);
                    break;
                case BlockState.four:
                    foreach (TetrisObject item in zMatrix) { item.ChangeState(true); }
                    zMatrix[1, 0].ChangeState(false);
                    zMatrix[0, 1].ChangeState(false);
                    zMatrix[1, 1].ChangeState(false);
                    zMatrix[0, 2].ChangeState(false);
                    break;
            }
        }
        public void Rotate(bool Clockwise)
        {
            if (Clockwise)
            {
                switch (Formation)
                {
                    case BlockState.one:
                        Formation = BlockState.two;
                        UpdateFormation();
                        break;
                    case BlockState.two:
                        Formation = BlockState.three;
                        UpdateFormation();
                        break;
                    case BlockState.three:
                        Formation = BlockState.four;
                        UpdateFormation();
                        break;
                    case BlockState.four:
                        Formation = BlockState.one;
                        UpdateFormation();
                        break;
                }
            }
            else if (!Clockwise)
            {
                switch (Formation)
                {
                    case BlockState.one:
                        Formation = BlockState.four;
                        UpdateFormation();
                        break;
                    case BlockState.two:
                        Formation = BlockState.one;
                        UpdateFormation();
                        break;
                    case BlockState.three:
                        Formation = BlockState.two;
                        UpdateFormation();
                        break;
                    case BlockState.four:
                        Formation = BlockState.three;
                        UpdateFormation();
                        break;
                }
            }
        }
        public bool AllowRotation(bool clockwise, Vector2 maxValues, Vector2 minValues)
        {
            TetrisObject[,] newPosition = new TetrisObject[zMatrix.GetLength(0), zMatrix.GetLength(1)];
            for (int i = 0; i < newPosition.GetLength(0); i++)
            {
                for (int j = 0; j < newPosition.GetLength(1); j++)
                {
                    newPosition[i, j] = new TetrisObject(Vector2.Zero, Color);
                    newPosition[i, j].PosX = zMatrix[0, 0].PosX + i * Color.Width;
                    newPosition[i, j].PosY = zMatrix[0, 0].PosY + j * Color.Height;
                }
            }

            if (clockwise)
            {
                switch (Formation)
                {
                    case BlockState.one:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[2, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        break;
                    case BlockState.two:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        break;
                    case BlockState.three:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[0, 2].ChangeState(false);
                        break;
                    case BlockState.four:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 0].ChangeState(false);
                        newPosition[1, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        break;
                }
            }
            else if (!clockwise)
            {
                switch (Formation)
                {
                    case BlockState.one:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[0, 2].ChangeState(false);
                        break;
                    case BlockState.two:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 0].ChangeState(false);
                        newPosition[1, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        break;
                    case BlockState.three:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[2, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        break;
                    case BlockState.four:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        break;
                }
            }
            return minValues.X <= MinValues(newPosition).X && maxValues.X >= MaxValues(newPosition).X && maxValues.Y >= MaxValues(newPosition).Y;
        }
        public void Fall(float lenght)
        {
            foreach (TetrisObject item in zMatrix)
            {
                item.PosY += lenght;
            }
        }
        public void Move(float lenght)
        {
            foreach (TetrisObject item in zMatrix)
            {
                item.PosX += lenght;
            }
        }
        public Vector2 MinValues()
        {
            float x = float.MaxValue;
            float y = float.MaxValue;
            for (int i = 0; i < zMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < zMatrix.GetLength(1); j++)
                {
                    if (zMatrix[i, j].PosX < x && zMatrix[i, j].alive) { x = zMatrix[i, j].PosX; }
                    if (zMatrix[i, j].PosY < y && zMatrix[i, j].alive) { y = zMatrix[i, j].PosY; }
                }
            }
            return new Vector2(x, y);
        }
        public Vector2 MinValues(TetrisObject[,] tetrisObjects)
        {
            float x = float.MaxValue;
            float y = float.MaxValue;
            for (int i = 0; i < tetrisObjects.GetLength(0); i++)
            {
                for (int j = 0; j < tetrisObjects.GetLength(1); j++)
                {
                    if (tetrisObjects[i, j].PosX < x && tetrisObjects[i, j].alive) { x = tetrisObjects[i, j].PosX; }
                    if (tetrisObjects[i, j].PosY < y && tetrisObjects[i, j].alive) { y = tetrisObjects[i, j].PosY; }
                }
            }
            return new Vector2(x, y);
        }
        public Vector2 MaxValues()
        {
            float x = float.MinValue;
            float y = float.MinValue;
            for (int i = 0; i < zMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < zMatrix.GetLength(1); j++)
                {
                    if (zMatrix[i, j].PosX > x && zMatrix[i, j].alive) { x = zMatrix[i, j].PosX; }
                    if (zMatrix[i, j].PosY > y && zMatrix[i, j].alive) { y = zMatrix[i, j].PosY; }
                }
            }
            return new Vector2(x, y);
        }
        public Vector2 MaxValues(TetrisObject[,] tetrisObjects)
        {
            float x = float.MinValue;
            float y = float.MinValue;
            for (int i = 0; i < tetrisObjects.GetLength(0); i++)
            {
                for (int j = 0; j < tetrisObjects.GetLength(1); j++)
                {
                    if (tetrisObjects[i, j].PosX > x && tetrisObjects[i, j].alive) { x = tetrisObjects[i, j].PosX; }
                    if (tetrisObjects[i, j].PosY > y && tetrisObjects[i, j].alive) { y = tetrisObjects[i, j].PosY; }
                }
            }
            return new Vector2(x, y);
        }
        public TetrisObject[,] NextRotatePosition(bool clockwise)
        {
            TetrisObject[,] newPosition = new TetrisObject[zMatrix.GetLength(0), zMatrix.GetLength(1)];
            for (int i = 0; i < newPosition.GetLength(0); i++)
            {
                for (int j = 0; j < newPosition.GetLength(1); j++)
                {
                    newPosition[i, j] = new TetrisObject(Vector2.Zero, Color);
                    newPosition[i, j].PosX = zMatrix[0, 0].PosX + i * Color.Width;
                    newPosition[i, j].PosY = zMatrix[0, 0].PosY + j * Color.Height;
                }
            }

            if (clockwise)
            {
                switch (Formation)
                {
                    case BlockState.one:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[2, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        break;
                    case BlockState.two:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        break;
                    case BlockState.three:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[0, 2].ChangeState(false);
                        break;
                    case BlockState.four:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 0].ChangeState(false);
                        newPosition[1, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        break;
                }
            }
            else if (!clockwise)
            {
                switch (Formation)
                {
                    case BlockState.one:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[0, 2].ChangeState(false);
                        break;
                    case BlockState.two:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 0].ChangeState(false);
                        newPosition[1, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        break;
                    case BlockState.three:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[2, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        break;
                    case BlockState.four:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        break;
                }
            }
            return newPosition;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (TetrisObject item in zMatrix)
            {
                item.Draw(spriteBatch);
            }
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            foreach (TetrisObject item in zMatrix)
            {
                item.Draw(spriteBatch, color);
            }
        }
    }
}
