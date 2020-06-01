using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace BlockBrawl
{
    public partial class PreConfigurations : Form
    {
        Fileread fileRead = new Fileread();
        public static bool fullScreen;
        public static bool gamePadVersion;
        public bool ShowPreConfigWindow { get; }
        public static int gameWidth;
        public static int gameHeight;
        //string programPath = "";
        public PreConfigurations()
        {
            InitializeComponent();
            LocateASettingsFile();

            //try
            //{
            //    if (File.Exists(@"installConfig.txt)"))
            //        programPath = File.ReadAllText(@"installConfig.txt");
            //}
            //catch (Exception e)
            //{
            //    string s = e.ToString();
            //}
            resolutionslst.DataSource = fileRead.Resolutions();

            ShowPreConfigWindow = fileRead.ShowConfigWindowAtStart().Contains("True");

            fullScreen = fileRead.StartInFullScreen().Contains("True");
            if (fullScreen) { fullscreenchk.Checked = true; }
            gamePadVersion = fileRead.StartWithGamePads().Contains("True");
            if (gamePadVersion) { chkGamePad.Checked = true; }

            string preferredResolution = fileRead.PreferredResolution();

            if (preferredResolution != "NotRead")
            {
                for (int i = 0; i < fileRead.Resolutions().Count; i++)
                {
                    if (preferredResolution != resolutionslst.Text)
                    {
                        resolutionslst.SelectedIndex++;
                    }
                }
                string[] split = preferredResolution.Split(new[] { "x" }, StringSplitOptions.RemoveEmptyEntries);
                gameWidth = Convert.ToInt32(split[0]);
                gameHeight = Convert.ToInt32(split[1]);
            }
        }
        private void LocateASettingsFile()
        {
            if (!fileRead.SettingsExist())
            {
                //if (programPath != "")
                    File.WriteAllLines(/*programPath + */"settings.txt", fileRead.SettingsFile());
            }
        }
        private void runGamebtn_Click(object sender, EventArgs e)
        {
            List<string> oldSettings = fileRead.SettingsFile();
            List<string> newSettings = new List<string>();

            for (int i = 0; i < oldSettings.Count; i++)
            {
                if (oldSettings[i].Contains("showPreConfig="))
                {
                    string newString = "showPreConfig=" + (!dontShowAgainlbl.Checked).ToString() + ";";
                    newSettings.Add(newString);
                }
                else if (oldSettings[i].Contains("fullscreen="))
                {
                    string newString = "fullscreen=" + fullscreenchk.Checked.ToString() + ";";
                    newSettings.Add(newString);
                }
                else if (oldSettings[i].Contains("startWithGamePad="))
                {
                    string newString = "startWithGamePad=" + chkGamePad.Checked.ToString() + ";";
                    newSettings.Add(newString);
                }
                else if (oldSettings[i].Contains("PreferredResolution="))
                {
                    string newString = "PreferredResolution=" + resolutionslst.Text + ";";
                    newSettings.Add(newString);
                }
                else
                {
                    newSettings.Add(oldSettings[i]);
                }
            }
            string choosenRes = resolutionslst.Text;
            string[] split = choosenRes.Split(new[] { "x" }, StringSplitOptions.RemoveEmptyEntries);
            gameWidth = Convert.ToInt32(split[0]);
            gameHeight = Convert.ToInt32(split[1]);
            fullScreen = fullscreenchk.Checked;
            gamePadVersion = chkGamePad.Checked;
            //if (programPath != "")
                File.WriteAllLines(/*programPath + */"settings.txt", newSettings);
            Close();
        }
    }
}
