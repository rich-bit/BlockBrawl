using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;

namespace BlockBrawl
{
    class SoundManager
    {
        private List<SoundEffect> noCopySounds;
        private List<SoundEffect> otherSounds;
        private List<SoundEffect> soundEffects;

        public static SoundEffectInstance explosion, rowFilled, menuChoice, laserShot;

        public SoundManager(ContentManager content)
        {
            noCopySounds = new List<SoundEffect>();
            otherSounds = new List<SoundEffect>();

            NoCopySounds = new List<SoundEffectInstance>();
            OtherSounds = new List<SoundEffectInstance>();

            InitiateSoundEffects(content);

            List<string> tempList = new Fileread().NoCopySounds();
            if (tempList.Count > 0)
            {
                for (int i = 0; i < tempList.Count; i++)
                {
                    var filestream = new FileStream(tempList[i], FileMode.Open);//Monogame community R0x
                    using (filestream)
                    {
                        noCopySounds.Add(SoundEffect.FromStream(filestream));
                    }
                    NoCopySounds.Add(noCopySounds[i].CreateInstance());
                }
            }

            tempList = new Fileread().OtherSounds();
            if (tempList.Count > 0)
            {
                for (int i = 0; i < tempList.Count; i++)
                {
                    var filestream = new FileStream(tempList[i], FileMode.Open);
                    using (filestream)
                    {
                        otherSounds.Add(SoundEffect.FromStream(filestream));
                    }
                    OtherSounds.Add(otherSounds[i].CreateInstance());
                }
            }
        }
        public static List<SoundEffectInstance> NoCopySounds { get; set; }
        public static List<SoundEffectInstance> OtherSounds { get; set; }

        private void InitiateSoundEffects(ContentManager content)
        {
            soundEffects = new List<SoundEffect>();
            soundEffects.Add(content.Load<SoundEffect>(@"GameSounds/explosion"));
            explosion = soundEffects[0].CreateInstance();
            soundEffects.Add(content.Load<SoundEffect>(@"GameSounds/rowfilled"));
            rowFilled = soundEffects[1].CreateInstance();
            soundEffects.Add(content.Load<SoundEffect>(@"GameSounds/menuChoice"));
            menuChoice = soundEffects[2].CreateInstance();
            soundEffects.Add(content.Load<SoundEffect>(@"GameSounds/laserShot"));
            laserShot = soundEffects[3].CreateInstance();
        }
    }
}
