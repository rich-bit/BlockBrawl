using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BlockBrawl.Blocks;
using BlockBrawl.Objects;
using System.Collections.Generic;

namespace BlockBrawl
{
    class Menu
    {
        private enum MenuChoice
        {
            play,
            settings,
            highscore,
        }
        GameObject blockBrawlMenu, settingsMenu, playMenu, highScoreMenu;

        List<GameObject> menuObjs = new List<GameObject>();
        public Menu()
        {
            blockBrawlMenu = new GameObject(Vector2.Zero, TextureManager.menuBlockBrawl);
            playMenu = new GameObject(Vector2.Zero, TextureManager.menuPlay);
            settingsMenu = new GameObject(Vector2.Zero, TextureManager.menuSettings);
            highScoreMenu = new GameObject(Vector2.Zero, TextureManager.menuHighScore);
            menuObjs.Add(blockBrawlMenu); menuObjs.Add(playMenu); menuObjs.Add(settingsMenu); menuObjs.Add(highScoreMenu);
            AssignPos();
        }
        private void AssignPos()
        {
            float lengthOfPics = 0f;
            float heightCount = 0f;
            float arbitraryMargin = 25f;
            for (int i = 0; i < menuObjs.Count; i++)
            {
                lengthOfPics += menuObjs[i].tex.Height;
                lengthOfPics += arbitraryMargin;
            }
            for (int j = 0; j < menuObjs.Count; j++)
            {
                menuObjs[j].Pos = new Vector2(
                    SettingsManager.gameWidth / 2 - menuObjs[j].tex.Width / 2,
                    SettingsManager.gameHeight / 2
                    - lengthOfPics / 2
                    + heightCount
                    );
                heightCount += menuObjs[j].tex.Height;
                heightCount += arbitraryMargin;
            }
        }
        public void Update() { }
        public void Draw(SpriteBatch sb)
        {
            blockBrawlMenu.Draw(sb);
            playMenu.Draw(sb);
            settingsMenu.Draw(sb);
            highScoreMenu.Draw(sb);
        }
    }
}
