using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;

namespace BlockBrawl
{
    class SoundManager
    {
        private readonly List<SoundEffect> noCopySounds;
        private readonly List<SoundEffect> otherSounds;
        private List<SoundEffect> soundEffects;

        public static SoundEffectInstance explosion, rowFilled, menuChoice;

        public SoundManager(ContentManager content)
        {
            noCopySounds = new List<SoundEffect>();
            otherSounds = new List<SoundEffect>();

            NoCopySounds = new List<SoundEffectInstance>();
            OtherSounds = new List<SoundEffectInstance>();

            InitiateSoundEffects(content);

            List<string> tempList = new FileRead().NoCopySounds();
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

            tempList = new FileRead().OtherSounds();
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
        public List<SoundEffectInstance> NoCopySounds { get; }
        public List<SoundEffectInstance> OtherSounds { get; }

        private void InitiateSoundEffects(ContentManager content)
        {
            soundEffects = new List<SoundEffect>();
            soundEffects.Add(content.Load<SoundEffect>(@"GameSounds/explosion"));
            explosion = soundEffects[0].CreateInstance();
            soundEffects.Add(content.Load<SoundEffect>(@"GameSounds/rowfilled"));
            rowFilled = soundEffects[1].CreateInstance();
            soundEffects.Add(content.Load<SoundEffect>(@"GameSounds/menuChoice"));
            menuChoice = soundEffects[2].CreateInstance();
        }
    }
}
