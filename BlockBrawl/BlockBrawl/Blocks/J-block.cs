using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using BlockBrawl.Objects;

namespace BlockBrawl.Blocks
{
    class J//This class will basically just contain a matrix and handle the textures of each element in it, amongst other usefull things.
    {
        public TetrisObject[,] jMatrix { get; set; }

        Vector2 startPos;

        public Texture2D color { get; set; }
        public float Time { get; set; }
        public enum blockState
        {
            one,
            two,
            three,
            four,
        }
        public blockState Formation { get; set; }
        public J(Texture2D color, Vector2 startPos)
        {
            this.color = color;
            this.startPos = startPos;
            jMatrix = new TetrisObject[3, 3];

            ////Contents of the I, looping through the matrix setting positions and textures
            for (int i = 0; i < jMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < jMatrix.GetLength(1); j++)
                {
                    jMatrix[i, j] = new TetrisObject(Vector2.Zero, color);
                    jMatrix[i, j].PosX = startPos.X + i * color.Width;
                    jMatrix[i, j].PosY = startPos.Y + j * color.Height;
                }
            }

            Formation = blockState.one;//Default formation of the J figure
            UpdateFormation();
        }
        private void UpdateFormation()
        {
            switch (Formation)
            {
                case blockState.one:
                    foreach (TetrisObject item in jMatrix) { item.ChangeState(true); }
                    jMatrix[0, 0].ChangeState(false);
                    jMatrix[0, 1].ChangeState(false);
                    jMatrix[1, 1].ChangeState(false);
                    jMatrix[2, 1].ChangeState(false);
                    break;
                case blockState.two:
                    foreach (TetrisObject item in jMatrix) { item.ChangeState(true); }
                    jMatrix[1, 0].ChangeState(false);
                    jMatrix[2, 0].ChangeState(false);
                    jMatrix[1, 1].ChangeState(false);
                    jMatrix[1, 2].ChangeState(false);
                    break;
                case blockState.three:
                    foreach (TetrisObject item in jMatrix) { item.ChangeState(true); }
                    jMatrix[0, 1].ChangeState(false);
                    jMatrix[1, 1].ChangeState(false);
                    jMatrix[2, 1].ChangeState(false);
                    jMatrix[2, 2].ChangeState(false);
                    break;
                case blockState.four:
                    foreach (TetrisObject item in jMatrix) { item.ChangeState(true); }
                    jMatrix[1, 0].ChangeState(false);
                    jMatrix[1, 1].ChangeState(false);
                    jMatrix[0, 2].ChangeState(false);
                    jMatrix[1, 2].ChangeState(false);
                    break;
            }
        }
        public void Rotate(bool Clockwise)
        {
            if (Clockwise)
            {
                switch (Formation)
                {
                    case blockState.one:
                        Formation = blockState.two;
                        UpdateFormation();
                        break;
                    case blockState.two:
                        Formation = blockState.three;
                        UpdateFormation();
                        break;
                    case blockState.three:
                        Formation = blockState.four;
                        UpdateFormation();
                        break;
                    case blockState.four:
                        Formation = blockState.one;
                        UpdateFormation();
                        break;
                }
            }
            else if (!Clockwise)
            {
                switch (Formation)
                {
                    case blockState.one:
                        Formation = blockState.four;
                        UpdateFormation();
                        break;
                    case blockState.two:
                        Formation = blockState.one;
                        UpdateFormation();
                        break;
                    case blockState.three:
                        Formation = blockState.two;
                        UpdateFormation();
                        break;
                    case blockState.four:
                        Formation = blockState.three;
                        UpdateFormation();
                        break;
                }
            }
        }
        public bool AllowRotation(bool clockwise, Vector2 maxValues, Vector2 minValues)
        {
            TetrisObject[,] newPosition = new TetrisObject[3, 3];
            for (int i = 0; i < newPosition.GetLength(0); i++)
            {
                for (int j = 0; j < newPosition.GetLength(1); j++)
                {
                    newPosition[i, j] = new TetrisObject(Vector2.Zero, color);
                    newPosition[i, j].PosX = jMatrix[0, 0].PosX + i * color.Width;
                    newPosition[i, j].PosY = jMatrix[0, 0].PosY + j * color.Height;
                }
            }
            if (clockwise)
            {
                switch (Formation)
                {
                    case blockState.one:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[2, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        break;
                    case blockState.two:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        break;
                    case blockState.three:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[0, 2].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        break;
                    case blockState.four:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 0].ChangeState(false);
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        break;
                }
            }
            else if (!clockwise)
            {
                switch (Formation)
                {
                    case blockState.one:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[0, 2].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        break;
                    case blockState.two:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 0].ChangeState(false);
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        break;
                    case blockState.three:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[2, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        break;
                    case blockState.four:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        break;
                }
            }
            return minValues.X <= MinValues(newPosition).X && maxValues.X >= MaxValues(newPosition).X && maxValues.Y >= MaxValues(newPosition).Y;
        }
        public void Fall(float lenght)
        {
            foreach (TetrisObject item in jMatrix)
            {
                item.PosY += lenght;
            }
        }
        public void Move(float lenght)
        {
            foreach (TetrisObject item in jMatrix)
            {
                item.PosX += lenght;
            }
        }
        public Vector2 MinValues()
        {
            float x = float.MaxValue;
            float y = float.MaxValue;
            for (int i = 0; i < jMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < jMatrix.GetLength(1); j++)
                {
                    if (jMatrix[i, j].PosX < x && jMatrix[i, j].alive) { x = jMatrix[i, j].PosX; }
                    if (jMatrix[i, j].PosY < y && jMatrix[i, j].alive) { y = jMatrix[i, j].PosY; }
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
            for (int i = 0; i < jMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < jMatrix.GetLength(1); j++)
                {
                    if (jMatrix[i, j].PosX > x && jMatrix[i, j].alive) { x = jMatrix[i, j].PosX; }
                    if (jMatrix[i, j].PosY > y && jMatrix[i, j].alive) { y = jMatrix[i, j].PosY; }
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
            TetrisObject[,] newPosition = new TetrisObject[jMatrix.GetLength(0), jMatrix.GetLength(1)];
            for (int i = 0; i < newPosition.GetLength(0); i++)
            {
                for (int j = 0; j < newPosition.GetLength(1); j++)
                {
                    newPosition[i, j] = new TetrisObject(Vector2.Zero, color);
                    newPosition[i, j].PosX = jMatrix[0, 0].PosX + i * color.Width;
                    newPosition[i, j].PosY = jMatrix[0, 0].PosY + j * color.Height;
                }
            }

            if (clockwise)
            {
                switch (Formation)
                {
                    case blockState.one:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[2, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        break;
                    case blockState.two:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        break;
                    case blockState.three:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[0, 2].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        break;
                    case blockState.four:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 0].ChangeState(false);
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        break;
                }
            }
            else if (!clockwise)
            {
                switch (Formation)
                {
                    case blockState.one:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[0, 2].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        break;
                    case blockState.two:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 0].ChangeState(false);
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        break;
                    case blockState.three:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[2, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        break;
                    case blockState.four:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        break;
                }
            }
            return newPosition;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (TetrisObject item in jMatrix)
            {
                item.Draw(spriteBatch);
            }
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            foreach (TetrisObject item in jMatrix)
            {
                item.Draw(spriteBatch, color);
            }
        }
    }
}
