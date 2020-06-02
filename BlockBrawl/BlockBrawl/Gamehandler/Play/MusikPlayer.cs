using System;
using Microsoft.Xna.Framework.Audio;

namespace BlockBrawl.Gamehandler.Play
{
    class MusikPlayer
    {
        Random random;
        public bool SongsToPlay { get; set; }
        public MusikPlayer(Random random)
        {
            this.random = random;
            if (SoundManager.NoCopySounds.Count > 0 || SoundManager.OtherSounds.Count > 0)
            {
                SongsToPlay = true;
            }
        }
        public void PlayRandomSong()
        {
            foreach(SoundEffectInstance item in SoundManager.NoCopySounds)
            {
                item.Stop();
            }
            foreach (SoundEffectInstance item in SoundManager.OtherSounds)
            {
                item.Stop();
            }
            try
            {
                if (SoundManager.NoCopySounds.Count > 0 && SoundManager.OtherSounds.Count > 0)
                {
                    int i = random.Next(2);
                    if (i == 0)
                    {
                        SoundManager.NoCopySounds[random.Next(SoundManager.NoCopySounds.Count)].Play();
                    }
                    else
                    {
                        SoundManager.OtherSounds[random.Next(SoundManager.OtherSounds.Count)].Play();
                    }
                }
                else if (SoundManager.NoCopySounds.Count > 0)
                {
                    SoundManager.NoCopySounds[random.Next(SoundManager.NoCopySounds.Count)].Play();
                }
                else if (SoundManager.OtherSounds.Count > 0)
                {
                    SoundManager.OtherSounds[random.Next(SoundManager.OtherSounds.Count)].Play();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
