﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BlockBrawl
{
    class Menu
    {
        public enum MenuChoice
        {
            play,
            settings,
            highscore,
            credits,
            quit,
        }
        public MenuChoice menuChoiceSwitch;
        int CurrentMenuChoice { get; set; }
        public bool EnterChoice { get; set; }
        int badImgMarginFix = 3;
        Point marginFromMenuObj;
        GameObject blockBrawlMenu, settingsMenu, playMenu, highScoreMenu, arrowOneLeft, arrowTwoLeft, arrowOneRight, arrowTwoRight, quit, creditsMenu;

        List<GameObject> menuObjs = new List<GameObject>();

        Buttons p1Start, p1MoveDown, p1MoveUp, p2Start, p2MoveDown, p2MoveUp;
        public Menu()
        {
            p1Start = SettingsManager.p1Start;
            p1MoveDown = SettingsManager.p1MoveDown;
            p1MoveUp = SettingsManager.p1MoveUp;

            p2Start = SettingsManager.p2Start;
            p2MoveDown = SettingsManager.p2MoveDown;
            p2MoveUp = SettingsManager.p2MoveUp;

            CurrentMenuChoice = 1;
            marginFromMenuObj = SettingsManager.arrowsInMenuMaxX;
            arrowOneLeft = new GameObject(Vector2.Zero, TextureManager.menuArrowLeft);
            arrowTwoLeft = new GameObject(Vector2.Zero, TextureManager.menuArrowLeft);

            arrowOneRight = new GameObject(Vector2.Zero, TextureManager.menuArrowRight);
            arrowTwoRight = new GameObject(Vector2.Zero, TextureManager.menuArrowRight);

            blockBrawlMenu = new GameObject(Vector2.Zero, TextureManager.menuBlockBrawl);
            playMenu = new GameObject(Vector2.Zero, TextureManager.menuPlay);
            settingsMenu = new GameObject(Vector2.Zero, TextureManager.menuSettings);
            highScoreMenu = new GameObject(Vector2.Zero, TextureManager.menuHighScore);
            creditsMenu = new GameObject(Vector2.Zero, TextureManager.menuCredits);
            quit = new GameObject(Vector2.Zero, TextureManager.menuQuit);
            menuObjs.Add(blockBrawlMenu); 
            menuObjs.Add(playMenu); 
            menuObjs.Add(settingsMenu); 
            menuObjs.Add(highScoreMenu); 
            menuObjs.Add(creditsMenu); 
            menuObjs.Add(quit);
            AssignPos();
        }
        private void AssignPos()
        {
            float lengthOfPics = 0f;
            float heightCount = 0f;
            float arbitraryMargin = 5f;
            for (int i = 0; i < menuObjs.Count; i++)
            {
                lengthOfPics += menuObjs[i].Tex.Height;
                lengthOfPics += arbitraryMargin;
            }
            for (int j = 0; j < menuObjs.Count; j++)
            {
                menuObjs[j].Pos = new Vector2(
                    SettingsManager.gameWidth / 2 - menuObjs[j].Tex.Width / 2,
                    SettingsManager.gameHeight / 2
                    - lengthOfPics / 2
                    + heightCount
                    );
                heightCount += menuObjs[j].Tex.Height;
                heightCount += arbitraryMargin;
            }
        }
        public void Update(InputManager iM, int playerOneIndex, int playerTwoIndex, GameTime gameTime)
        {
            BackAndForwardNumber(gameTime);//Changin the margin from menu objects with time..
            SetLeftArrowPos();
            SetRightArrowPos();
            PresentMenuChoice();
            if (iM.JustPressed(p1MoveDown, playerOneIndex)
                    || iM.JustPressed(p2MoveDown, playerTwoIndex)
                    || iM.JustPressed(Keys.S)
                    || iM.JustPressed(Keys.Down))
            {
                if (CurrentMenuChoice == menuObjs.Count - 1)
                {
                    CurrentMenuChoice = 1;
                }
                else
                {
                    CurrentMenuChoice++;
                }
            }
            if (iM.JustPressed(p1MoveUp, playerOneIndex)
                || iM.JustPressed(p2MoveUp, playerTwoIndex)
                || iM.JustPressed(Keys.W)
                || iM.JustPressed(Keys.Up))
            {
                if (CurrentMenuChoice == 1)
                {
                    CurrentMenuChoice = menuObjs.Count - 1;
                }
                else
                {
                    CurrentMenuChoice--;
                }
            }
            if (iM.JustPressed(p1Start, playerOneIndex) || iM.JustPressed(p2Start, playerTwoIndex)
                || iM.JustPressed(Keys.Enter) || iM.JustPressed(Keys.Space))
            {
                EnterChoice = true;
                SoundManager.menuChoice.Play();
            }

        }
        private void PresentMenuChoice()
        {
            if(CurrentMenuChoice == 1) { menuChoiceSwitch = MenuChoice.play; }
            else if (CurrentMenuChoice == 2) { menuChoiceSwitch = MenuChoice.settings; }
            else if (CurrentMenuChoice == 3) { menuChoiceSwitch = MenuChoice.highscore; }
            else if (CurrentMenuChoice == 4) { menuChoiceSwitch = MenuChoice.credits; }
            else if (CurrentMenuChoice == 5) { menuChoiceSwitch = MenuChoice.quit; }
        }
        private void SetLeftArrowPos()
        {
            arrowOneLeft.PosX = menuObjs[CurrentMenuChoice].PosX + menuObjs[CurrentMenuChoice].Rect.Width + marginFromMenuObj.X;
            arrowOneLeft.PosY = menuObjs[CurrentMenuChoice].PosY - (arrowOneLeft.Rect.Height / 2) + badImgMarginFix + (menuObjs[CurrentMenuChoice].Rect.Height / 2);

            arrowTwoLeft.PosX = menuObjs[CurrentMenuChoice].PosX + menuObjs[CurrentMenuChoice].Rect.Width + arrowOneLeft.Rect.Width + marginFromMenuObj.X * 2;
            arrowTwoLeft.PosY = menuObjs[CurrentMenuChoice].PosY - (arrowTwoLeft.Rect.Height / 2) + badImgMarginFix + (menuObjs[CurrentMenuChoice].Rect.Height / 2);
        }
        private void SetRightArrowPos()
        {
            arrowOneRight.PosX = menuObjs[CurrentMenuChoice].PosX - arrowOneRight.Rect.Width - marginFromMenuObj.X;
            arrowOneRight.PosY = menuObjs[CurrentMenuChoice].PosY - (arrowOneRight.Rect.Height / 2) + badImgMarginFix + (menuObjs[CurrentMenuChoice].Rect.Height / 2);

            arrowTwoRight.PosX = menuObjs[CurrentMenuChoice].PosX - arrowOneRight.Rect.Width - arrowTwoRight.Rect.Width - marginFromMenuObj.X * 2;
            arrowTwoRight.PosY = menuObjs[CurrentMenuChoice].PosY - (arrowTwoRight.Rect.Height / 2) + badImgMarginFix + (menuObjs[CurrentMenuChoice].Rect.Height / 2);
        }
        private void BackAndForwardNumber(GameTime gameTime)
        {
            arrowOneLeft.Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            float betweenIncrements = 0.08f;
            int marginChange = 1;
            if(arrowOneLeft.Time > betweenIncrements && marginFromMenuObj.Y == 0) 
            {
                marginFromMenuObj.X -= marginChange;
                if(marginFromMenuObj.X == 0) { marginFromMenuObj.Y = 1; }
                arrowOneLeft.Time = 0f;
            }
            if(arrowOneLeft.Time > betweenIncrements && marginFromMenuObj.Y == 1)
            {
                marginFromMenuObj.X += marginChange;
                if (marginFromMenuObj.X == 10) { marginFromMenuObj.Y = 0; }
                arrowOneLeft.Time = 0f;
            }
        }
        public void Draw(SpriteBatch sb)
        {
            foreach(GameObject item in menuObjs)
            {
                item.Draw(sb);
            }
            arrowOneLeft.Draw(sb);
            arrowTwoLeft.Draw(sb);
            arrowOneRight.Draw(sb);
            arrowTwoRight.Draw(sb);
        }
    }
}
