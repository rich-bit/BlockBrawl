using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using BlockBrawl.Objects;

namespace BlockBrawl.Blocks
{
    class I//This class will basically just contain a matrix and handle the textures of each element in it, amongst other usefull things.
    {
        public TetrisObject[,] iMatrix;


        public Texture2D color { get; set; }
        public float time { get; set; }
        enum iblockState
        {
            one,
            two,
            three,
            four,
        }
        iblockState formation;
        public I(Texture2D color, Vector2 startPos)
        {
            this.color = color;
            
            iMatrix = new TetrisObject[4, 4];

            //Contents of the I, looping through the dubbelarray setting positions and textures
            for (int i = 0; i < iMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < iMatrix.GetLength(1); j++)
                {
                    iMatrix[i, j] = new TetrisObject(Vector2.Zero, color);
                    iMatrix[i, j].PosX = startPos.X + i * color.Width;
                    iMatrix[i, j].PosY = startPos.Y + j * color.Height;
                }
            }
            formation = iblockState.one;//Default formation of the I figure
            UpdateFormation();
        }
        private void UpdateFormation()
        {
            switch (formation)
            {
                case iblockState.one:
                    foreach (TetrisObject item in iMatrix) { item.ChangeState(true); }
                    iMatrix[0, 1].ChangeState(false);
                    iMatrix[1, 1].ChangeState(false);
                    iMatrix[2, 1].ChangeState(false);
                    iMatrix[3, 1].ChangeState(false);
                    break;
                case iblockState.two:
                    foreach (TetrisObject item in iMatrix) { item.ChangeState(true); }
                    iMatrix[2, 0].ChangeState(false);
                    iMatrix[2, 1].ChangeState(false);
                    iMatrix[2, 2].ChangeState(false);
                    iMatrix[2, 3].ChangeState(false);
                    break;
                case iblockState.three:
                    foreach (TetrisObject item in iMatrix) { item.ChangeState(true); }
                    iMatrix[0, 2].ChangeState(false);
                    iMatrix[1, 2].ChangeState(false);
                    iMatrix[2, 2].ChangeState(false);
                    iMatrix[3, 2].ChangeState(false);
                    break;
                case iblockState.four:
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
                switch (formation)
                {
                    case iblockState.one:
                        formation = iblockState.two;
                        UpdateFormation();
                        break;
                    case iblockState.two:
                        formation = iblockState.three;
                        UpdateFormation();
                        break;
                    case iblockState.three:
                        formation = iblockState.four;
                        UpdateFormation();
                        break;
                    case iblockState.four:
                        formation = iblockState.one;
                        UpdateFormation();
                        break;
                }
            }
            else if (!Clockwise)
            {
                switch (formation)
                {
                    case iblockState.one:
                        formation = iblockState.four;
                        UpdateFormation();
                        break;
                    case iblockState.two:
                        formation = iblockState.one;
                        UpdateFormation();
                        break;
                    case iblockState.three:
                        formation = iblockState.two;
                        UpdateFormation();
                        break;
                    case iblockState.four:
                        formation = iblockState.three;
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
                    newPosition[i, j] = new TetrisObject(Vector2.Zero, color);
                    newPosition[i, j].PosX = iMatrix[0,0].PosX + i * color.Width;
                    newPosition[i, j].PosY = iMatrix[0,0].PosY + j * color.Height;
                }
            }

            if (clockwise)
            {
                switch (formation)
                {
                    case iblockState.one:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[2, 0].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        newPosition[2, 3].ChangeState(false);
                        break;
                    case iblockState.two:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 2].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        newPosition[3, 2].ChangeState(false);
                        break;
                    case iblockState.three:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        newPosition[1, 3].ChangeState(false);
                        break;
                    case iblockState.four:
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
                switch (formation)
                {
                    case iblockState.one:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        newPosition[1, 3].ChangeState(false);
                        break;
                    case iblockState.two:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[3, 1].ChangeState(false);
                        break;
                    case iblockState.three:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[2, 0].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        newPosition[2, 3].ChangeState(false);
                        break;
                    case iblockState.four:
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
                    newPosition[i, j] = new TetrisObject(Vector2.Zero, color);
                    newPosition[i, j].PosX = iMatrix[0, 0].PosX + i * color.Width;
                    newPosition[i, j].PosY = iMatrix[0, 0].PosY + j * color.Height;
                }
            }

            if (clockwise)
            {
                switch (formation)
                {
                    case iblockState.one:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[2, 0].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        newPosition[2, 3].ChangeState(false);
                        break;
                    case iblockState.two:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 2].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        newPosition[3, 2].ChangeState(false);
                        break;
                    case iblockState.three:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        newPosition[1, 3].ChangeState(false);
                        break;
                    case iblockState.four:
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
                switch (formation)
                {
                    case iblockState.one:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[1, 0].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[1, 2].ChangeState(false);
                        newPosition[1, 3].ChangeState(false);
                        break;
                    case iblockState.two:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[0, 1].ChangeState(false);
                        newPosition[1, 1].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[3, 1].ChangeState(false);
                        break;
                    case iblockState.three:
                        foreach (TetrisObject item in newPosition) { item.ChangeState(true); }
                        newPosition[2, 0].ChangeState(false);
                        newPosition[2, 1].ChangeState(false);
                        newPosition[2, 2].ChangeState(false);
                        newPosition[2, 3].ChangeState(false);
                        break;
                    case iblockState.four:
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
    }
}
