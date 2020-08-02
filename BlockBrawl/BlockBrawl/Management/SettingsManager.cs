using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace BlockBrawl
{
    class SettingsManager
    {
        public static Vector2 tileSize, bazookaShotSpeed, pistolShotSpeed, effectPositionBloodSpatter;
        public static float speed, fallTime, newSpeedCounter, qteWaitTime, spawnBlockBazooka;
        public static Texture2D playerOneColor, playerTwoColor;
        public static string playerOneName, playerTwoName;
        public static int playerIndexOne, playerIndexTwo;
        public static int gameWidth, gameHeight;
        public static bool gamePadVersion;
        public static Point tiles, arrowsInMenuMaxX;
        public static Buttons p1MoveLeft, p1MoveRight, p1MoveDown, p1MoveUp, p1PowerUp, p1RotateCW, p1RotateCC, p1Start;
        public static Buttons p2MoveLeft, p2MoveRight, p2MoveDown, p2MoveUp, p2PowerUp, p2RotateCW, p2RotateCC, p2Start;
        public static string connectionString = new DbLogin().connectionString;
        public SettingsManager(GraphicsDeviceManager graphicsDeviceManager, InputManager iM)
        {
            SetDefaultButtons(iM);
            gameWidth = PreConfigurations.gameWidth;
            gameHeight = PreConfigurations.gameHeight;
            graphicsDeviceManager.PreferredBackBufferWidth = gameWidth;
            graphicsDeviceManager.PreferredBackBufferHeight = gameHeight;

            //graphicsDeviceManager.HardwareModeSwitch = false;
            graphicsDeviceManager.IsFullScreen = PreConfigurations.fullScreen;
            graphicsDeviceManager.ApplyChanges();

            gamePadVersion = PreConfigurations.gamePadVersion;

            speed = tileSize.X;
            fallTime = 1.3f;
            newSpeedCounter = 15f;


            playerOneColor = TextureManager.blueBlock1920;
            playerTwoColor = TextureManager.greenBlock1920;

            qteWaitTime = 40f;

            bazookaShotSpeed = new Vector2(10, 10);
            pistolShotSpeed = new Vector2(17, 17);
            spawnBlockBazooka = 5f;

            playerIndexOne = 0;
            playerIndexTwo = 1;
            tileSize = new Vector2(60, 60);//Note
            tiles = new Point(20, gameHeight / 60);
            effectPositionBloodSpatter = new Vector2(0, tileSize.Y / 2);

            arrowsInMenuMaxX = new Point(10, 0); // Set y to 0 please.

            playerOneName = "Player One";
            playerTwoName = "Player Two";
        }
        public void SetDefaultButtons(InputManager iM)
        {
            p1MoveDown = Buttons.DPadDown;
            p1MoveLeft = Buttons.DPadLeft;
            p1MoveRight = Buttons.DPadRight;
            p1MoveUp = Buttons.DPadUp;
            p1PowerUp = Buttons.Back;
            p1RotateCC = Buttons.B;
            p1RotateCW = Buttons.Y;
            p1Start = Buttons.Start;

            p2MoveDown = Buttons.DPadDown;
            p2MoveLeft = Buttons.DPadLeft;
            p2MoveRight = Buttons.DPadRight;
            p2MoveUp = Buttons.DPadUp;
            p2PowerUp = Buttons.Back;
            p2RotateCC = Buttons.B;
            p2RotateCW = Buttons.Y;
            p2Start = Buttons.Start;
            CheckForGamePadConfig(iM);
        }
        private void CheckForGamePadConfig(InputManager iM)
        {
            try
            {
                List<string> preGamePadConfigs = new Fileread().LookForGamePadConfig();
                List<string> p1Config = new List<string>();
                List<string> p2Config = new List<string>();
                for (int i = 0; i < preGamePadConfigs.Count; i++)
                {
                    if (preGamePadConfigs[i] != "Player two:")
                    {
                        if (preGamePadConfigs[i] != "Player one:")
                            p1Config.Add(preGamePadConfigs[i]);
                    }
                    else { break; }
                }

                bool leave = false;
                for (int i = 0; i < preGamePadConfigs.Count; i++)
                {
                    if (preGamePadConfigs[i] == "Player two:")
                    {
                        for (int j = i; j < preGamePadConfigs.Count; j++)
                        {
                            if (preGamePadConfigs[j] != "Player two:")
                            {
                                p2Config.Add(preGamePadConfigs[j]);
                            }
                            if(j == preGamePadConfigs.Count - 1) { leave = true; break; }
                        }
                    }
                    if (leave) { break; }
                }
                SetPreConfig(iM, p1Config, p2Config);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        private void SetPreConfig(InputManager iM, List<string> config1, List<string> config2)
        {
            p1MoveDown = FromString(iM, FindStringRepresentation(config1[0], ": "));            
            p1MoveLeft = FromString(iM, FindStringRepresentation(config1[1], ": "));
            p1MoveRight = FromString(iM, FindStringRepresentation(config1[2], ": "));
            p1MoveUp = FromString(iM, FindStringRepresentation(config1[3], ": "));
            p1PowerUp = FromString(iM, FindStringRepresentation(config1[4], ": "));
            p1RotateCC = FromString(iM, FindStringRepresentation(config1[5], ": "));
            p1RotateCW = FromString(iM, FindStringRepresentation(config1[6], ": "));
            p1Start = FromString(iM, FindStringRepresentation(config1[7], ": "));

            p2MoveDown = FromString(iM, FindStringRepresentation(config2[0], ": "));
            p2MoveLeft = FromString(iM, FindStringRepresentation(config2[1], ": "));
            p2MoveRight = FromString(iM, FindStringRepresentation(config2[2], ": "));
            p2MoveUp = FromString(iM, FindStringRepresentation(config2[3], ": "));
            p2PowerUp = FromString(iM, FindStringRepresentation(config2[4], ": "));
            p2RotateCC = FromString(iM, FindStringRepresentation(config2[5], ": "));
            p2RotateCW = FromString(iM, FindStringRepresentation(config2[6], ": "));
            p2Start = FromString(iM, FindStringRepresentation(config2[7], ": "));
        }
        private string FindStringRepresentation(string s, string splitter)
        {
            string[] split = s.Split(new[] { $"{splitter}" }, StringSplitOptions.RemoveEmptyEntries);
            return split[1];
        }
        private Buttons FromString(InputManager iM, string s)
        {
            if (s == "Back") { return Buttons.Back; }
            else if (s == "A") { return Buttons.A; }
            else if (s == "B") { return Buttons.B; }
            else if (s == "BigButton") { return Buttons.BigButton; }
            else if (s == "DPadDown") { return Buttons.DPadDown; }
            else if (s == "DPadLeft") { return Buttons.DPadLeft; }
            else if (s == "DPadRight") { return Buttons.DPadRight; }
            else if (s == "DPadUp") { return Buttons.DPadUp; }
            else if (s == "LeftShoulder") { return Buttons.LeftShoulder; }
            else if (s == "LeftStick") { return Buttons.LeftStick; }
            else if (s == "LeftThumbstickDown") { return Buttons.LeftThumbstickDown; }
            else if (s == "LeftThumbstickLeft") { return Buttons.LeftThumbstickLeft; }
            else if (s == "LeftThumbstickRight") { return Buttons.LeftThumbstickRight; }
            else if (s == "LeftThumbstickUp") { return Buttons.LeftThumbstickUp; }
            else if (s == "LeftTrigger") { return Buttons.LeftTrigger; }
            else if (s == "RightShoulder") { return Buttons.RightShoulder; }
            else if (s == "RightStick") { return Buttons.RightStick; }
            else if (s == "RightThumbstickDown") { return Buttons.RightThumbstickDown; }
            else if (s == "RightThumbstickLeft") { return Buttons.RightThumbstickLeft; }
            else if (s == "RightThumbstickRight") { return Buttons.RightThumbstickRight; }
            else if (s == "RightThumbstickUp") { return Buttons.RightThumbstickUp; }
            else if (s == "RightTrigger") { return Buttons.RightTrigger; }
            else if (s == "Start") { return Buttons.Start; }
            else if (s == "X") { return Buttons.X; }
            else if (s == "Y") { return Buttons.Y; }
            else { return Buttons.Start; }//must return smth...:/
        }
    }
}
