using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace BlockBrawl.Gamehandler.Settings
{
    class ConfigureGamePads
    {
        InputManager iM;
        public bool GoBack { get; set; }
        private bool changeButton, saved;
        int configureChoice = 1;

        int playerOneIndex, playerTwoIndex, togglePlayerConfiguration;

        List<string> p1Buttons, p2Buttons;

        GameObject arrowOneRight, arrowTwoRight, arrowOneLeft, arrowTwoLeft;

        Point marginFromMenuObj;
        int rowMarginTop = 5;
        int badImgMarginFix = 3;

        Buttons p1Up, p1Down, p1Select, p2Up, p2Down, p2Select;

        public ConfigureGamePads(InputManager iM, int playerOneIndex, int playerTwoIndex)
        {
            this.iM = iM;
            this.playerOneIndex = playerOneIndex;
            this.playerTwoIndex = playerTwoIndex;

            p1Up = SettingsManager.p1MoveUp;
            p1Down = SettingsManager.p1MoveDown;
            p1Select = SettingsManager.p1PowerUp;

            p2Up = SettingsManager.p2MoveUp;
            p2Down = SettingsManager.p2MoveDown;
            p2Select = SettingsManager.p2PowerUp;

            togglePlayerConfiguration = 0;

            p1Buttons = new List<string>();
            p2Buttons = new List<string>();

            marginFromMenuObj = SettingsManager.arrowsInMenuMaxX;

            StringRepresentationButtons();

            arrowOneLeft = new GameObject(Vector2.Zero, TextureManager.menuArrowLeft);
            arrowTwoLeft = new GameObject(Vector2.Zero, TextureManager.menuArrowLeft);
            arrowOneRight = new GameObject(Vector2.Zero, TextureManager.menuArrowRight);
            arrowTwoRight = new GameObject(Vector2.Zero, TextureManager.menuArrowRight);
        }
        private void StringRepresentationButtons()
        {
            p1Buttons.Add("Player one:");
            p1Buttons.Add("Move Down: " + SettingsManager.p1MoveDown.ToString());
            p1Buttons.Add("Move Left: " + SettingsManager.p1MoveLeft.ToString());
            p1Buttons.Add("Move Right: " + SettingsManager.p1MoveRight.ToString());
            p1Buttons.Add("Move Up: " + SettingsManager.p1MoveUp.ToString());
            p1Buttons.Add("Power up: " + SettingsManager.p1PowerUp.ToString());
            p1Buttons.Add("Rotate clockwise: " + SettingsManager.p1RotateCC.ToString());
            p1Buttons.Add("Rotate counter clockwise: " + SettingsManager.p1RotateCW.ToString());
            p1Buttons.Add("Start / Pause: " + SettingsManager.p1Start.ToString());

            p2Buttons.Add("Player two:");
            p2Buttons.Add("Move Down: " + SettingsManager.p2MoveDown.ToString());
            p2Buttons.Add("Move Left: " + SettingsManager.p2MoveLeft.ToString());
            p2Buttons.Add("Move Right: " + SettingsManager.p2MoveRight.ToString());
            p2Buttons.Add("Move Up: " + SettingsManager.p2MoveUp.ToString());
            p2Buttons.Add("Power up: " + SettingsManager.p2PowerUp.ToString());
            p2Buttons.Add("Rotate clockwise: " + SettingsManager.p2RotateCC.ToString());
            p2Buttons.Add("Rotate counter clockwise: " + SettingsManager.p2RotateCW.ToString());
            p2Buttons.Add("Start / Pause: " + SettingsManager.p2Start.ToString());
        }
        public void Update(GameTime gameTime, SettingsManager settingsManager)
        {
            if (!changeButton && (
                iM.JustPressed(p1Select, playerOneIndex) || iM.JustPressed(p2Select, playerTwoIndex)
            || iM.JustPressed(Keys.Escape)))
            {
                GoBack = true;
                SoundManager.menuChoice.Play();
            }
            if (iM.JustPressed(Keys.Enter))
            {
                changeButton = true;
            }
            if (iM.IsHeld(Keys.LeftControl) && iM.JustPressed(Keys.S))
            {
                try
                {
                    List<string> allInputs = new List<string>();
                    allInputs.AddRange(p1Buttons);
                    allInputs.AddRange(p2Buttons);
                    File.WriteAllLines("gamepadConfig.txt", allInputs);
                    settingsManager.SetDefaultButtons(iM);
                    saved = true;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            if (changeButton)
            {
                arrowTwoLeft.Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (arrowTwoLeft.Time > 5f) { changeButton = false; arrowTwoLeft.Time = 0f; }

                if (iM.SavedButtonPress(togglePlayerConfiguration))
                {
                    if(togglePlayerConfiguration == playerOneIndex)
                    {
                        string[] split = p1Buttons[configureChoice].Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
                        string newKeyStringBuild = split[0] + ": ";
                        p1Buttons[configureChoice] = newKeyStringBuild + iM.PressedButton.ToString();
                        new Thread(() =>
                        {
                            Thread.Sleep(200);
                            changeButton = false;
                        }).Start();
                    }
                    else if(togglePlayerConfiguration == playerTwoIndex)
                    {
                        string[] split = p2Buttons[configureChoice].Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
                        string newKeyStringBuild = split[0] + ": ";
                        p2Buttons[configureChoice] = newKeyStringBuild + iM.PressedButton.ToString();
                        new Thread(() =>
                        {
                            Thread.Sleep(200);
                            changeButton = false;
                        }).Start();
                    }
                }
            }
            if (saved)
            {
                arrowOneRight.Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(arrowOneRight.Time > 3f) { arrowOneRight.Time = 0f; saved = false; }
            }
            if (iM.JustPressed(Keys.Tab))
            {
                if (togglePlayerConfiguration == 0)
                {
                    togglePlayerConfiguration = 1;
                }
                else if (togglePlayerConfiguration == 1)
                {
                    togglePlayerConfiguration = 0;
                }
            }
            if (togglePlayerConfiguration == 0) { UpdateCurrentSelected(iM, p1Buttons, gameTime); }
            if (togglePlayerConfiguration == 1) { UpdateCurrentSelected(iM, p2Buttons, gameTime); }
        }
        private void UpdateCurrentSelected(InputManager iM, List<string> currentSettings, GameTime gameTime)
        {
            SetLeftArrowPos(currentSettings);
            SetRightArrowPos(currentSettings);
            BackAndForwardNumber(gameTime);
            if (!changeButton && (iM.JustPressed(p1Down, playerOneIndex)
                            || iM.JustPressed(p2Down, playerTwoIndex)
                            || iM.JustPressed(Keys.S)
                            || iM.JustPressed(Keys.Down)))
            {
                if (configureChoice == currentSettings.Count - 1)
                {
                    configureChoice = 1;
                }
                else
                {
                    configureChoice++;
                }
            }
            if (!changeButton && (iM.JustPressed(p1Up, playerOneIndex)
                || iM.JustPressed(p2Up, playerTwoIndex)
                || iM.JustPressed(Keys.W)
                || iM.JustPressed(Keys.Up)))
            {
                if (configureChoice == 1)
                {
                    configureChoice = currentSettings.Count - 1;
                }
                else
                {
                    configureChoice--;
                }
            }
        }
        private void SetLeftArrowPos(List<string> currentConfigList)
        {
            Vector2 currentChoicePos = GetDrawPos(configureChoice + rowMarginTop, togglePlayerConfiguration, FontManager.GeneralText, currentConfigList[configureChoice]);
            float width = FontManager.GeneralText.MeasureString(currentConfigList[configureChoice]).X;
            float height = FontManager.GeneralText.MeasureString(currentConfigList[configureChoice]).Y;

            arrowOneLeft.PosX = currentChoicePos.X + width + marginFromMenuObj.X;
            arrowOneLeft.PosY = currentChoicePos.Y - (arrowOneLeft.Rect.Height / 2) + badImgMarginFix + height / 2;

            arrowTwoLeft.PosX = currentChoicePos.X + width + arrowOneLeft.Rect.Width + marginFromMenuObj.X * 2;
            arrowTwoLeft.PosY = currentChoicePos.Y - (arrowTwoLeft.Rect.Height / 2) + badImgMarginFix + height / 2;
        }
        private void SetRightArrowPos(List<string> currentConfigList)
        {
            Vector2 currentChoicePos = GetDrawPos(configureChoice + rowMarginTop, togglePlayerConfiguration, FontManager.GeneralText, currentConfigList[configureChoice]);
            float width = FontManager.GeneralText.MeasureString(currentConfigList[configureChoice]).X;
            float height = FontManager.GeneralText.MeasureString(currentConfigList[configureChoice]).Y;

            arrowOneRight.PosX = currentChoicePos.X - arrowOneRight.Rect.Width - marginFromMenuObj.X;
            arrowOneRight.PosY = currentChoicePos.Y - (arrowOneRight.Rect.Height / 2) + badImgMarginFix + height / 2;

            arrowTwoRight.PosX = currentChoicePos.X - arrowOneRight.Rect.Width - arrowTwoRight.Rect.Width - marginFromMenuObj.X * 2;
            arrowTwoRight.PosY = currentChoicePos.Y - (arrowTwoRight.Rect.Height / 2) + badImgMarginFix + height / 2;
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
        public Vector2 GetDrawPos(int row, int playerIndex, SpriteFont font, string text)
        {
            float x = SettingsManager.gameWidth / 2 - font.MeasureString(text).X / 2;
            if (playerIndex == playerOneIndex)
            {
                x -= SettingsManager.gameWidth / 4;
            }
            else
            {
                x += SettingsManager.gameWidth / 4;
            }
            float y = font.MeasureString(text).Y * row;
            return new Vector2(x, y);
        }
        private Vector2 MiddleText(int row, string text, SpriteFont spriteFont)
        {
            float x = SettingsManager.gameWidth / 2 - spriteFont.MeasureString(text).X / 2;
            float y = row * SettingsManager.tileSize.Y;
            return new Vector2(x, y);
        }
        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(FontManager.GeneralText, "Gamepad Configuration", MiddleText(1, "Gamepad Configuration", FontManager.GeneralText), Color.Purple);
            sb.DrawString(FontManager.GeneralText, "Change Player with TAB", MiddleText(2, "Change Player with TAB", FontManager.GeneralText), Color.White);
            if (changeButton)
            {
                sb.DrawString(FontManager.GeneralText, $"Press button! {Convert.ToInt32(5 - arrowTwoLeft.Time)}", MiddleText(12, $"Press button! {Convert.ToInt32(5 - arrowTwoLeft.Time)}", FontManager.GeneralText), Color.White);
            }
            if (saved)
            {
                sb.DrawString(FontManager.GeneralText, "Saved gamepadConfig.txt, ready to use! (Game might need restart)", MiddleText(15, "Saved gamepadConfig.txt, ready to use! (Game might need restart)", FontManager.GeneralText), Color.White);
            }
            sb.DrawString(FontManager.GeneralText, "Hit enter and then button on gamepad to change button binding.", MiddleText(13, "Hit enter and then button on gamepad to change button binding.", FontManager.GeneralText), Color.Purple);
            sb.DrawString(FontManager.GeneralText, "Save Configuration? Hit CTRL + S", MiddleText(14, "Save Configuration? Hit CTRL + S", FontManager.GeneralText), Color.Purple);

            for (int i = 0; i < p1Buttons.Count; i++)
            {
                sb.DrawString(FontManager.GeneralText, p1Buttons[i], GetDrawPos(i + rowMarginTop, playerOneIndex, FontManager.GeneralText, p1Buttons[i]), Color.PaleGoldenrod);
            }
            for (int j = 0; j < p2Buttons.Count; j++)
            {
                sb.DrawString(FontManager.GeneralText, p2Buttons[j], GetDrawPos(j + rowMarginTop, playerTwoIndex, FontManager.GeneralText, p2Buttons[j]), Color.Gold);
            }
            arrowOneLeft.Draw(sb);
            arrowTwoLeft.Draw(sb);
            arrowOneRight.Draw(sb);
            arrowTwoRight.Draw(sb);
        }
    }
}
