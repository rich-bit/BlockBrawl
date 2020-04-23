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
        GameObject blockBrawlMenu, settingsMenu, playMenu;

        int menuObjectCount;
        public Menu()
        {
            blockBrawlMenu = new GameObject(AssignPos(TextureManager.menuBlockBrawl), TextureManager.menuBlockBrawl);
            settingsMenu = new GameObject(AssignPos(TextureManager.menuSettings), TextureManager.menuSettings);
            playMenu = new GameObject(AssignPos(TextureManager.menuPlay), TextureManager.menuPlay);
        }
        private Vector2 AssignPos(Texture2D tex)
        {
            menuObjectCount++;
            float arbitraryObjectMargin = 60;
            return new Vector2(SettingsManager.gameWidth / 2 - tex.Width / 2, SettingsManager.gameHeight / 2 - tex.Height + menuObjectCount * arbitraryObjectMargin);
        }
        public void Update() { }
        public void Draw(SpriteBatch sb) 
        {
            blockBrawlMenu.Draw(sb);
            settingsMenu.Draw(sb);
            playMenu.Draw(sb);
        }
    }
}
