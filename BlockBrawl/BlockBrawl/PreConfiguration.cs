using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BlockBrawl
{
    public partial class PreConfigurations : Form
    {
        FileRead fileRead = new FileRead();
        public bool Fullscreen { get; set; }
        public bool ShowPreConfigWindow { get; }
        public int GameWidth { get; set; }
        public int GameHeight { get; set; }
        public PreConfigurations()
        {
            InitializeComponent();
            LocateASettingsFile();

            resolutionslst.DataSource = fileRead.Resolutions();

            ShowPreConfigWindow = fileRead.ShowConfigWindowAtStart().Contains("True");

            Fullscreen = fileRead.StartInFullScreen().Contains("True");
            if (Fullscreen) { fullscreenchk.Checked = true; }

            string preferredResolution = fileRead.PreferredResolution();

            if (preferredResolution != "NotRead")
            {
                for(int i = 0; i < fileRead.Resolutions().Count; i++)
                {
                    if(preferredResolution != resolutionslst.Text)
                    {
                        resolutionslst.SelectedIndex++;
                    }
                }
                string[] split = preferredResolution.Split(new[] { "x" }, StringSplitOptions.RemoveEmptyEntries);
                GameWidth = Convert.ToInt32(split[0]);
                GameHeight = Convert.ToInt32(split[1]);
            }
        }
        private void LocateASettingsFile()
        {
            if (!fileRead.SettingsExist())
            {
                File.WriteAllLines("settings.txt", fileRead.SettingsFile());
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
            GameWidth = Convert.ToInt32(split[0]);
            GameHeight = Convert.ToInt32(split[1]);
            Fullscreen = fullscreenchk.Checked;
            File.WriteAllLines("settings.txt", newSettings);
            Close();
        }
    }
}
