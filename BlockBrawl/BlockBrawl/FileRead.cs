using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBrawl
{
    class FileRead
    {
        private readonly string rootPath;
        private readonly string noCopySoundsPath;
        private readonly string otherSoundsPath;

        public FileRead()
        {
            rootPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            rootPath = rootPath.Substring(6);
            string directoryNoCopySounds = "/noCopySounds";
            string directoryOtherSounds = "/otherSounds";
            if (!Directory.Exists(rootPath + directoryNoCopySounds))
            {
                Directory.CreateDirectory(rootPath + directoryNoCopySounds);
            }
            else if (!Directory.Exists(rootPath + directoryOtherSounds))
            {
                Directory.CreateDirectory(rootPath + directoryOtherSounds);
            }
            noCopySoundsPath = rootPath + directoryNoCopySounds;
            otherSoundsPath = rootPath + otherSoundsPath;
        }
        public List<string> NoCopySounds()
        {
            List<string> noCopySounds = new List<string>();

            foreach (string item in Directory.GetFiles(noCopySoundsPath))
            {
                if (item.Contains(".wav"))
                {
                    noCopySounds.Add(item);
                    noCopySounds.Sort();
                }
            }
            return noCopySounds;
        }
        public List<string> OtherSounds()
        {
            List<string> otherSounds = new List<string>();

            foreach (string item in Directory.GetFiles(otherSoundsPath))
            {
                if (item.Contains(".wav"))
                {
                    otherSounds.Add(item);
                    otherSounds.Sort();
                }
            }
            return otherSounds;
        }
    }
}
