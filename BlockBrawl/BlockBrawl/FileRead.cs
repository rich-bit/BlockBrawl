using System;
using System.Collections.Generic;
using System.IO;

namespace BlockBrawl
{
    class Fileread
    {
        private readonly string rootPath;
        private readonly string noCopySoundsPath;
        private readonly string otherSoundsPath;
        private readonly List<string> supportedResolutions;

        public Fileread()
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
            otherSoundsPath = rootPath + directoryOtherSounds;

            supportedResolutions = new List<string>();
            supportedResolutions.Add("1920x1080");
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
        public List<string> LookForGamePadConfig()
        {
            List<string> preGamePadConfig = new List<string>();
            try
            {
                if (File.Exists("gamepadConfig.txt"))
                {
                    preGamePadConfig.AddRange(File.ReadAllLines("gamepadConfig.txt"));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return preGamePadConfig;
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
        public List<string> SettingsFile()
        {
            string settingsFile = "settings.txt";
            List<string> settings = new List<string>();
            if (!File.Exists(settingsFile))
            {
                settings.Add("fullscreen=False;");
                settings.Add("startWithGamePad=True");
                settings.Add("showPreConfig=True;");
                foreach (string item in supportedResolutions) { settings.Add("Resolution: " + item); }
                settings.Add("PreferredResolution=");
            }
            else if (File.Exists(settingsFile))
            {
                string[] settingsArray = File.ReadAllLines(settingsFile);
                foreach (string item in settingsArray)
                {
                    settings.Add(item);
                }
            }
            return settings;
        }
        public List<string> Resolutions()
        {
            return supportedResolutions;
        }
        public bool SettingsExist()
        {
            return File.Exists("settings.txt");
        }
        public string ShowConfigWindowAtStart()
        {
            string dataRead = "NotRead";
            string settingsFile = "settings.txt";
            if (File.Exists(settingsFile))
            {
                string[] settings = File.ReadAllLines(settingsFile);
                foreach (string item in settings)
                {
                    if (item.Contains("showPreConfig="))
                    {
                        string[] a = item.Split(new[] { "showPreConfig=", ";" }, StringSplitOptions.RemoveEmptyEntries);
                        dataRead = a[0];
                    }
                }
            }
            else if (!File.Exists(settingsFile)) { dataRead = "unsuccessful"; }
            return dataRead;
        }
        public string StartInFullScreen()
        {
            string dataRead = "NotRead";
            string settingsFile = "settings.txt";
            if (File.Exists(settingsFile))
            {
                string[] settings = File.ReadAllLines(settingsFile);
                foreach (string item in settings)
                {
                    if (item.Contains("fullscreen="))
                    {
                        string[] a = item.Split(new[] { "fullscreen=", ";" }, StringSplitOptions.RemoveEmptyEntries);
                        dataRead = a[0];
                    }
                }
            }
            else if (!File.Exists(settingsFile)) { dataRead = "unsuccessful"; }
            return dataRead;
        }
        public string StartWithGamePads()
        {
            string dataRead = "NotRead";
            string settingsFile = "settings.txt";
            if (File.Exists(settingsFile))
            {
                string[] settings = File.ReadAllLines(settingsFile);
                foreach (string item in settings)
                {
                    if (item.Contains("startWithGamePad="))
                    {
                        string[] a = item.Split(new[] { "startWithGamePad=", ";" }, StringSplitOptions.RemoveEmptyEntries);
                        dataRead = a[0];
                    }
                }
            }
            else if (!File.Exists(settingsFile)) { dataRead = "unsuccessful"; }
            return dataRead;
        }
        public string PreferredResolution()
        {
            string dataRead = "NotRead";
            string settingsFile = "settings.txt";
            if (File.Exists(settingsFile))
            {
                string[] settings = File.ReadAllLines(settingsFile);
                foreach (string item in settings)
                {
                    if (item.Contains("PreferredResolution="))
                    {
                        string[] a = item.Split(new[] { "PreferredResolution=", ";" }, StringSplitOptions.RemoveEmptyEntries);
                        if (a.Length > 0)
                        {
                            dataRead = a[0];
                        }
                    }
                }
            }
            else if (!File.Exists(settingsFile)) { dataRead = "unsuccessful"; }
            return dataRead;
        }
    }
}
