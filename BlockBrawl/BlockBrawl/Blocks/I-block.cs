using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using BlockBrawl.Objects;

namespace BlockBrawl.Blocks
{
    class I//This class will basically just contain a matrix and handle the textures of each element in it, amongst other usefull things.
    {
        public TetrisObject[,] iMatrix;


        public Texture2D Color { get; set; }
        public float Time { get; set; }
        public enum IblockState
        {
            one,
            two,
            three,
            four,
        }
        public IblockState Formation { get; set; }
        public I(Texture2D color, Vector2 startPos)
        {
            Color = color;
            
            iMatrix = new TetrisObject[4, 4];

            //Contents of the I, looping through the matrix setting positions and textures
            for (int i = 0; i < iMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < iMatrix.GetLength(1); j++)
                {
                    iMatrix[i, j] = new TetrisObject(Vector2.Zero, color)
                    {
                        PosX = startPos.X + i * color.Width,
                        PosY = startPos.Y + j * color.Height
                    };
                }
            }
            Formation = IblockState.one;//Default Formation of the I figure
            UpdateFormation();
        }
        private void UpdateFormation()
        {
            switch (Formation)
            {
                case IblockState.one:
                    foreach (TetrisObject item in iMatrix) { item.ChangeState(true); }
                    iMatrix[0, 1].ChangeState(false);
                    iMatrix[1, 1].ChangeState(false);
                    iMatrix[2, 1].ChangeState(false);
                    iMatrix[3, 1].ChangeState(false);
                    break;
                case IblockState.two:
                    foreach (TetrisObject item in iMatrix) { item.ChangeState(true); }
                    iMatrix[2, 0].ChangeState(false);
                    iMatrix[2, 1].ChangeState(false);
                    iMatrix[2, 2].ChangeState(false);
                    iMatrix[2, 3].ChangeState(false);
                    break;
                case IblockState.three:
                    foreach (TetrisObject item in iMatrix) { item.ChangeState(true); }
                    iMatrix[0, 2].ChangeState(false);
                    iMatrix[1, 2].ChangeState(false);
                    iMatrix[2, 2].ChangeState(false);
                    iMatrix[3, 2].ChangeState(false);
                    break;
                case IblockState.four:
                    foreach (TetrisObject item in iMatrix) { item.ChangeState(true); }
                    iMatrix[1, 0].ChangeState(false);
                    iMatrix[1, 1].ChangeState(false);
                    iMatrix[1, 2].ChangeState(false);
                    iMatrix[1, 3].ChangeState(false);
                    break;
            }
        }
        public void Rotate(bool Clockwise)
        {
            if (Clockwise)
            {
                switch (Formation)
                {
                    case IblockState.one:
                        Formation = IblockState.two;
                        UpdateFormation();
                        break;
                    case IblockState.two:
                        Formation = IblockState.three;
                        UpdateFormation();
                        break;
                    case IblockState.three:
                        Formation = IblockState.four;
                        UpdateFormation();
                        break;
                    case IblockState.four:
                        Formation = IblockState.one;
                        UpdateFormation();
                        break;
                }
            }
            else if (!Clockwise)
            {
                switch (Formation)
                {
                    case IblockState.one:
                        Formation = IblockState.four;
                        UpdateFormation();
                        break;
                    case IblockState.two:
                        Formation = IblockState.one;
                        UpdateFormation();
                        break;
                    case IblockState.three:
                        Formation = IblockState.two;
                        UpdateFormation();
                        break;
                    case IblockState.four:
                        Formation = IblockState.three;
                        UpdateFormation();
                        break;
                }
            }
        }
        public bool AllowRotation(bool clockwise, Vector2 maxValues, Vector2 minValues)
        {
            TetrisObject[,] newPosition = new TetrisObject[iMatrix.GetLength(0), iMatrix.GetLength(1)];
            for (int i = 0; i < newPosition.GetLength(0); i++)
            {
                for (int j = 0; j < newPosition.GetLength(1); j++)
                {
                    newPosition[i, j] = new TetrisObject(Vector2.Zero, Color);
                    newPosition[i, j].PosX = iMatrix[0,0].PosX + i * Color.Width;
                    newPosition[i, j].PosY = iMatrix[0,0].PosY + j * Color.Height;
                }
            }

            if (clockwise)
            {
                switch (Formation)
                {
                    case IblockState.one:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[2, 0].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        newPosition[2, 3].ChangeState(false);
                        break;
                    case IblockState.two:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 2].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        newPosition[3, 2].ChangeState(false);
                        break;
                    case IblockState.three:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        newPosition[1, 3].ChangeState(false);
                        break;
                    case IblockState.four:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[3, 1].ChangeState(false);
                        break;
                }
            }
            else if (!clockwise)
            {
                switch (Formation)
                {
                    case IblockState.one:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        newPosition[1, 3].ChangeState(false);
                        break;
                    case IblockState.two:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[3, 1].ChangeState(false);
                        break;
                    case IblockState.three:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[2, 0].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        newPosition[2, 3].ChangeState(false);
                        break;
                    case IblockState.four:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 2].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        newPosition[3, 2].ChangeState(false);
                        break;
                }
            }
            return minValues.X <= MinValues(newPosition).X && maxValues.X >= MaxValues(newPosition).X && maxValues.Y >= MaxValues(newPosition).Y; 
        }
        public void Fall(float lenght)
        {
            foreach (TetrisObject item in iMatrix)
            {
                item.PosY += lenght;
            }
        }
        public void Move(float lenght)
        {
            foreach (TetrisObject item in iMatrix)
            {
                item.PosX += lenght;
            }
        }
        public Vector2 MinValues()
        {
            float x = float.MaxValue;
            float y = float.MaxValue;
            for (int i = 0; i < iMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < iMatrix.GetLength(1); j++)
                {
                    if (iMatrix[i, j].PosX < x && iMatrix[i, j].alive) { x = iMatrix[i, j].PosX; }
                    if (iMatrix[i, j].PosY < y && iMatrix[i, j].alive) { y = iMatrix[i, j].PosY; }
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
            for (int i = 0; i < iMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < iMatrix.GetLength(1); j++)
                {
                    if (iMatrix[i, j].PosX > x && iMatrix[i, j].alive) { x = iMatrix[i, j].PosX; }
                    if (iMatrix[i, j].PosY > y && iMatrix[i, j].alive) { y = iMatrix[i, j].PosY; }
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
            TetrisObject[,] newPosition = new TetrisObject[iMatrix.GetLength(0), iMatrix.GetLength(1)];
            for (int i = 0; i < newPosition.GetLength(0); i++)
            {
                for (int j = 0; j < newPosition.GetLength(1); j++)
                {
                    newPosition[i, j] = new TetrisObject(Vector2.Zero, Color);
                    newPosition[i, j].PosX = iMatrix[0, 0].PosX + i * Color.Width;
                    newPosition[i, j].PosY = iMatrix[0, 0].PosY + j * Color.Height;
                }
            }

            if (clockwise)
            {
                switch (Formation)
                {
                    case IblockState.one:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[2, 0].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        newPosition[2, 3].ChangeState(false);
                        break;
                    case IblockState.two:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 2].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        newPosition[3, 2].ChangeState(false);
                        break;
                    case IblockState.three:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        newPosition[1, 3].ChangeState(false);
                        break;
                    case IblockState.four:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[3, 1].ChangeState(false);
                        break;
                }
            }
            else if (!clockwise)
            {
                switch (Formation)
                {
                    case IblockState.one:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        newPosition[1, 3].ChangeState(false);
                        break;
                    case IblockState.two:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[3, 1].ChangeState(false);
                        break;
                    case IblockState.three:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[2, 0].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        newPosition[2, 3].ChangeState(false);
                        break;
                    case IblockState.four:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 2].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        newPosition[3, 2].ChangeState(false);
                        break;
                }
            }
            return newPosition;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (TetrisObject item in iMatrix)
            {
                item.Draw(spriteBatch);
            }
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            foreach (TetrisObject item in iMatrix)
            {
                item.Draw(spriteBatch, color);
            }
        }
    }
}
