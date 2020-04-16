﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using BlockBrawl.Objects;

namespace BlockBrawl.Blocks
{
    class S
    {
        public TetrisObject[,] tMatrix;


        public Texture2D Color { get; set; }
        public float Time { get; set; }
        enum IblockState
        {
            one,
            two,
            three,
            four,
        }
        IblockState formation;
        public S(Texture2D color, Vector2 startPos)
        {
            Color = color;

            tMatrix = new TetrisObject[3, 3];

            //Contents of the I, looping through the matrix setting positions and textures
            for (int i = 0; i < tMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < tMatrix.GetLength(1); j++)
                {
                    tMatrix[i, j] = new TetrisObject(Vector2.Zero, color)
                    {
                        PosX = startPos.X + i * color.Width,
                        PosY = startPos.Y + j * color.Height
                    };
                }
            }
            formation = IblockState.one;//Default formation of the T figure
            UpdateFormation();
        }
        private void UpdateFormation()
        {
            switch (formation)
            {
                case IblockState.one:
                    foreach (TetrisObject item in tMatrix) { item.ChangeState(true); }
                    tMatrix[0, 1].ChangeState(false);
                    tMatrix[1, 0].ChangeState(false);
                    tMatrix[1, 1].ChangeState(false);
                    tMatrix[0, 2].ChangeState(false);
                    break;
                case IblockState.two:
                    foreach (TetrisObject item in tMatrix) { item.ChangeState(true); }
                    tMatrix[1, 0].ChangeState(false);
                    tMatrix[1, 1].ChangeState(false);
                    tMatrix[2, 1].ChangeState(false);
                    tMatrix[2, 2].ChangeState(false);
                    break;
                case IblockState.three:
                    foreach (TetrisObject item in tMatrix) { item.ChangeState(true); }
                    tMatrix[1, 1].ChangeState(false);
                    tMatrix[2, 1].ChangeState(false);
                    tMatrix[0, 2].ChangeState(false);
                    tMatrix[1, 2].ChangeState(false);
                    break;
                case IblockState.four:
                    foreach (TetrisObject item in tMatrix) { item.ChangeState(true); }
                    tMatrix[0, 0].ChangeState(false);
                    tMatrix[0, 1].ChangeState(false);
                    tMatrix[1, 1].ChangeState(false);
                    tMatrix[1, 2].ChangeState(false);
                    break;
            }
        }
        public void Rotate(bool Clockwise)
        {
            if (Clockwise)
            {
                switch (formation)
                {
                    case IblockState.one:
                        formation = IblockState.two;
                        UpdateFormation();
                        break;
                    case IblockState.two:
                        formation = IblockState.three;
                        UpdateFormation();
                        break;
                    case IblockState.three:
                        formation = IblockState.four;
                        UpdateFormation();
                        break;
                    case IblockState.four:
                        formation = IblockState.one;
                        UpdateFormation();
                        break;
                }
            }
            else if (!Clockwise)
            {
                switch (formation)
                {
                    case IblockState.one:
                        formation = IblockState.four;
                        UpdateFormation();
                        break;
                    case IblockState.two:
                        formation = IblockState.one;
                        UpdateFormation();
                        break;
                    case IblockState.three:
                        formation = IblockState.two;
                        UpdateFormation();
                        break;
                    case IblockState.four:
                        formation = IblockState.three;
                        UpdateFormation();
                        break;
                }
            }
        }
        public bool AllowRotation(bool clockwise, Vector2 maxValues, Vector2 minValues)
        {
            TetrisObject[,] newPosition = new TetrisObject[tMatrix.GetLength(0), tMatrix.GetLength(1)];
            for (int i = 0; i < newPosition.GetLength(0); i++)
            {
                for (int j = 0; j < newPosition.GetLength(1); j++)
                {
                    newPosition[i, j] = new TetrisObject(Vector2.Zero, Color);
                    newPosition[i, j].PosX = tMatrix[0, 0].PosX + i * Color.Width;
                    newPosition[i, j].PosY = tMatrix[0, 0].PosY + j * Color.Height;
                }
            }

            if (clockwise)
            {
                switch (formation)
                {
                    case IblockState.one:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        break;
                    case IblockState.two:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[0, 2].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        break;
                    case IblockState.three:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 0].ChangeState(false);
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        break;
                    case IblockState.four:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[2, 0].ChangeState(false);
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        break;
                }
            }
            else if (!clockwise)
            {
                switch (formation)
                {
                    case IblockState.one:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 0].ChangeState(false);
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        break;
                    case IblockState.two:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[2, 0].ChangeState(false);
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        break;
                    case IblockState.three:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        break;
                    case IblockState.four:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[0, 2].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        break;
                }
            }
            return minValues.X <= MinValues(newPosition).X && maxValues.X >= MaxValues(newPosition).X && maxValues.Y >= MaxValues(newPosition).Y;
        }
        public void Fall(float lenght)
        {
            foreach (TetrisObject item in tMatrix)
            {
                item.PosY += lenght;
            }
        }
        public void Move(float lenght)
        {
            foreach (TetrisObject item in tMatrix)
            {
                item.PosX += lenght;
            }
        }
        public Vector2 MinValues()
        {
            float x = float.MaxValue;
            float y = float.MaxValue;
            for (int i = 0; i < tMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < tMatrix.GetLength(1); j++)
                {
                    if (tMatrix[i, j].PosX < x && tMatrix[i, j].alive) { x = tMatrix[i, j].PosX; }
                    if (tMatrix[i, j].PosY < y && tMatrix[i, j].alive) { y = tMatrix[i, j].PosY; }
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
            for (int i = 0; i < tMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < tMatrix.GetLength(1); j++)
                {
                    if (tMatrix[i, j].PosX > x && tMatrix[i, j].alive) { x = tMatrix[i, j].PosX; }
                    if (tMatrix[i, j].PosY > y && tMatrix[i, j].alive) { y = tMatrix[i, j].PosY; }
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
            TetrisObject[,] newPosition = new TetrisObject[tMatrix.GetLength(0), tMatrix.GetLength(1)];
            for (int i = 0; i < newPosition.GetLength(0); i++)
            {
                for (int j = 0; j < newPosition.GetLength(1); j++)
                {
                    newPosition[i, j] = new TetrisObject(Vector2.Zero, Color);
                    newPosition[i, j].PosX = tMatrix[0, 0].PosX + i * Color.Width;
                    newPosition[i, j].PosY = tMatrix[0, 0].PosY + j * Color.Height;
                }
            }

            if (clockwise)
            {
                switch (formation)
                {
                    case IblockState.one:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        break;
                    case IblockState.two:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[0, 2].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        break;
                    case IblockState.three:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 0].ChangeState(false);
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        break;
                    case IblockState.four:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[2, 0].ChangeState(false);
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        break;
                }
            }
            else if (!clockwise)
            {
                switch (formation)
                {
                    case IblockState.one:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 0].ChangeState(false);
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        break;
                    case IblockState.two:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[2, 0].ChangeState(false);
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        break;
                    case IblockState.three:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        break;
                    case IblockState.four:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[0, 2].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        break;
                }
            }
            return newPosition;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (TetrisObject item in tMatrix)
            {
                item.Draw(spriteBatch);
            }
        }
    }
}