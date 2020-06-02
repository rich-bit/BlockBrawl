using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using BlockBrawl.Gamehandler.Settings;

namespace BlockBrawl
{
    class Settings
    {
        public enum SettingsChoice
        {
            settings,
            configureGamePad,
            resetPreConfig,
            viewSteerings,
        }
        public SettingsChoice settingsChoiceSwitcher;
        int CurrentSettingsChoice { get; set; }
        public bool GoToMenu { get; set; }
        int badImgMarginFix = 3;
        Point marginFromMenuObj;
        GameObject configureGamePad, viewControls, resetPreConfig, arrowOneRight, arrowTwoRight, arrowOneLeft, arrowTwoLeft;

        List<GameObject> menuObjs = new List<GameObject>();

        ViewControls showControls;
        public Settings()
        {
            settingsChoiceSwitcher = SettingsChoice.settings;

            CurrentSettingsChoice = 0;
            marginFromMenuObj = SettingsManager.arrowsInMenuMaxX;
            arrowOneLeft = new GameObject(Vector2.Zero, TextureManager.menuArrowLeft);
            arrowTwoLeft = new GameObject(Vector2.Zero, TextureManager.menuArrowLeft);

            arrowOneRight = new GameObject(Vector2.Zero, TextureManager.menuArrowRight);
            arrowTwoRight = new GameObject(Vector2.Zero, TextureManager.menuArrowRight);

            configureGamePad = new GameObject(Vector2.Zero, TextureManager.settingsConfigureGamePad);
            viewControls = new GameObject(Vector2.Zero, TextureManager.settingsViewControls);
            resetPreConfig = new GameObject(Vector2.Zero, TextureManager.settingsShowPreConfig);

            menuObjs.Add(configureGamePad); menuObjs.Add(viewControls); menuObjs.Add(resetPreConfig);
            AssignPos();

            showControls = new ViewControls();
        }
        private void AssignPos()
        {
            float lengthOfPics = 0f;
            float heightCount = 0f;
            float arbitraryMargin = 25f;
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
            switch (settingsChoiceSwitcher)
            {
                case SettingsChoice.settings:
                    if (iM.JustPressed(Buttons.Start, playerOneIndex) || iM.JustPressed(Buttons.Start, playerTwoIndex)
                    || iM.JustPressed(Keys.Enter) || iM.JustPressed(Keys.Space))
                    {
                    SoundManager.menuChoice.Play();
                        ToggleChoice();

                    }
                    if (iM.JustPressed(Buttons.Back, playerOneIndex) || iM.JustPressed(Buttons.Back, playerTwoIndex)
                    || iM.JustPressed(Keys.Escape))
                    {
                        GoToMenu = true;
                        SoundManager.menuChoice.Play();
                    }
                    BackAndForwardNumber(gameTime);//Changin the margin from menu objects with time..
                    SetLeftArrowPos();
                    SetRightArrowPos();
                    if (iM.JustPressed(Buttons.DPadDown, playerOneIndex)
                            || iM.JustPressed(Buttons.DPadDown, playerTwoIndex)
                            || iM.JustPressed(Keys.S)
                            || iM.JustPressed(Keys.Down))
                    {
                        if (CurrentSettingsChoice == menuObjs.Count - 1)
                        {
                            CurrentSettingsChoice = 0;
                        }
                        else
                        {
                            CurrentSettingsChoice++;
                        }
                    }
                    if (iM.JustPressed(Buttons.DPadUp, playerOneIndex)
                        || iM.JustPressed(Buttons.DPadUp, playerTwoIndex)
                        || iM.JustPressed(Keys.W)
                        || iM.JustPressed(Keys.Up))
                    {
                        if (CurrentSettingsChoice == 0)
                        {
                            CurrentSettingsChoice = menuObjs.Count - 1;
                        }
                        else
                        {
                            CurrentSettingsChoice--;
                        }
                    }
                    break;
                case SettingsChoice.configureGamePad:
                    break;
                case SettingsChoice.resetPreConfig:
                    if (File.Exists("settings.txt"))
                    {
                        File.Delete("settings.txt");
                    }
                    settingsChoiceSwitcher = SettingsChoice.settings;
                    break;
                case SettingsChoice.viewSteerings:
                    showControls.Update(iM, playerOneIndex, playerTwoIndex);
                    if (showControls.GoToSettings) { settingsChoiceSwitcher = SettingsChoice.settings; showControls.GoToSettings = false; }
                    break;
            }
        }
        private void ToggleChoice()
        {
            if (CurrentSettingsChoice == 0) { settingsChoiceSwitcher = SettingsChoice.configureGamePad; }
            else if (CurrentSettingsChoice == 1) { settingsChoiceSwitcher = SettingsChoice.viewSteerings; }
            else if (CurrentSettingsChoice == 2) { settingsChoiceSwitcher = SettingsChoice.resetPreConfig; }
        }
        private void SetLeftArrowPos()
        {
            arrowOneLeft.PosX = menuObjs[CurrentSettingsChoice].PosX + menuObjs[CurrentSettingsChoice].Rect.Width + marginFromMenuObj.X;
            arrowOneLeft.PosY = menuObjs[CurrentSettingsChoice].PosY - (arrowOneLeft.Rect.Height / 2) + badImgMarginFix + (menuObjs[CurrentSettingsChoice].Rect.Height / 2);

            arrowTwoLeft.PosX = menuObjs[CurrentSettingsChoice].PosX + menuObjs[CurrentSettingsChoice].Rect.Width + arrowOneLeft.Rect.Width + marginFromMenuObj.X * 2;
            arrowTwoLeft.PosY = menuObjs[CurrentSettingsChoice].PosY - (arrowTwoLeft.Rect.Height / 2) + badImgMarginFix + (menuObjs[CurrentSettingsChoice].Rect.Height / 2);
        }
        private void SetRightArrowPos()
        {
            arrowOneRight.PosX = menuObjs[CurrentSettingsChoice].PosX - arrowOneRight.Rect.Width - marginFromMenuObj.X;
            arrowOneRight.PosY = menuObjs[CurrentSettingsChoice].PosY - (arrowOneRight.Rect.Height / 2) + badImgMarginFix + (menuObjs[CurrentSettingsChoice].Rect.Height / 2);

            arrowTwoRight.PosX = menuObjs[CurrentSettingsChoice].PosX - arrowOneRight.Rect.Width - arrowTwoRight.Rect.Width - marginFromMenuObj.X * 2;
            arrowTwoRight.PosY = menuObjs[CurrentSettingsChoice].PosY - (arrowTwoRight.Rect.Height / 2) + badImgMarginFix + (menuObjs[CurrentSettingsChoice].Rect.Height / 2);
        }
        private void BackAndForwardNumber(GameTime gameTime)
        {
            arrowOneLeft.Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            float betweenIncrements = 0.08f;
            int marginChange = 1;
            if (arrowOneLeft.Time > betweenIncrements && marginFromMenuObj.Y == 0)
            {
                marginFromMenuObj.X -= marginChange;
                if (marginFromMenuObj.X == 0) { marginFromMenuObj.Y = 1; }
                arrowOneLeft.Time = 0f;
            }
            if (arrowOneLeft.Time > betweenIncrements && marginFromMenuObj.Y == 1)
            {
                marginFromMenuObj.X += marginChange;
                if (marginFromMenuObj.X == 10) { marginFromMenuObj.Y = 0; }
                arrowOneLeft.Time = 0f;
            }
        }
        public void Draw(SpriteBatch sb)
        {
            switch (settingsChoiceSwitcher)
            {
                case SettingsChoice.settings:
                    foreach (GameObject item in menuObjs)
                    {
                        item.Draw(sb);
                    }
                    arrowOneLeft.Draw(sb);
                    arrowTwoLeft.Draw(sb);
                    arrowOneRight.Draw(sb);
                    arrowTwoRight.Draw(sb);
                    sb.DrawString(FontManager.GeneralText, "To go menu? Use ESC / Select", new Vector2(0,
                        SettingsManager.gameHeight - FontManager.GeneralText.MeasureString("To go menu? Use ESC / Select").Y), Color.Yellow);
                    break;
                case SettingsChoice.viewSteerings:
                    showControls.Draw(sb);
                    sb.DrawString(FontManager.GeneralText, "To go menu? Use ESC / Select", new Vector2(0,
    SettingsManager.gameHeight - FontManager.GeneralText.MeasureString("To go menu? Use ESC / Select").Y), Color.Yellow);
                    break;
                case SettingsChoice.resetPreConfig:
                    break;
                case SettingsChoice.configureGamePad:
                    break;
            }
        }
    }
}
