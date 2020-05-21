using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Install_Template
{
    public partial class Installation : Form//Some stuff here could be simplyfied
    {
        string rootPath;
        string installFilesPath;
        string installPath = "";
        int filesToCopy;
        List<string> files = new List<string>();
        List<string> folders = new List<string>();
        public Installation()
        {
            chkShortCDesk.Enabled = false;
            chkShortCStartMenu.Enabled = false;// Cant get them to work atm
            InitializeComponent();
            FindInstallDir();
            rootPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            rootPath = rootPath.Substring(6);

            installFilesPath = rootPath + "\\Release\\";
            string[] subFolders = Directory.GetDirectories($"{installFilesPath}", "*", SearchOption.AllDirectories);//I <3 StackOverFlow


            if (System.IO.File.Exists(rootPath + "/installation.txt"))
            {
                rtxInfoBox.Text = System.IO.File.ReadAllText(rootPath + "/installation.txt");
            }
            else { rtxInfoBox.Text += "No install info to show..."; }

            new Thread(() => UpdateButtons()).Start();
            new Thread(() => LocateFilesFolders(subFolders)).Start();
        }
        private void LocateFilesFolders(string[] subFolders)
        {
            if (Directory.Exists(installFilesPath))
            {
                foreach (string item in Directory.GetFiles(installFilesPath))
                {
                    string[] split = item.Split(new[] { "Release\\" }, StringSplitOptions.RemoveEmptyEntries);
                    files.Add(installFilesPath + split[1]);
                    filesToCopy++;
                }
                for (int i = 0; i < subFolders.Length; i++)
                {
                    string[] folderNameSplit = subFolders[i].Split(new[] { "Release\\" }, StringSplitOptions.RemoveEmptyEntries);
                    folders.Add(folderNameSplit[1]);
                    foreach (string item in Directory.GetFiles(subFolders[i]))
                    {
                        string[] fileNameSplit = item.Split(new[] { "Release\\" }, StringSplitOptions.RemoveEmptyEntries);
                        files.Add(installFilesPath + fileNameSplit[1]);
                        filesToCopy++;
                    }
                }
                new Thread(() =>
                {
                    rtxInfoBox.Invoke(new Action(() =>
                    {
                        rtxInfoBox.Text += $"\n{filesToCopy} file(s) will be copied over to your hard drive.";

                    }));
                }).Start();
            }
            else
            { rtxInfoBox.Invoke(new Action(() => { rtxInfoBox.Text += "Found no release to install..."; })); }
        }

        private void FindInstallDir()
        {
            if (Directory.Exists("C:/"))
            {
                if (Directory.Exists("C:/Program Files (x86)"))
                {
                    installPath = "C:/Program Files (x86)/BlockBrawl";
                }
                else if (Directory.Exists("C:/Program Files"))
                {
                    installPath = "C:/Program Files/BlockBrawl";
                }
            }
            rtxInstallDir.Text += installPath;
        }
        private void UpdateButtons()
        {
            while (installPath == "" || filesToCopy == 0)
            {
                if (btnInstall.Created)
                {
                    btnInstall.Invoke(new Action(() => { btnInstall.Enabled = false; }));
                    if (installPath == "")
                    {
                        btnInstall.Invoke(new Action(() => { btnChangeDir.Text = "Select Directory"; }));
                    }
                    Thread.Sleep(200);
                }
            }
            if (btnInstall.Created)
            {
                btnInstall.Invoke(new Action(() => { btnInstall.Enabled = true; }));
            }
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            btnInstall.Enabled = false;
            btnInstall.Text = "Install\n(Disabled)";
            System.IO.File.WriteAllText(installPath + "\\installConfig.txt", $"{installPath}");
            new Thread(() =>
            {
                if (!Directory.Exists(installPath))
                {
                    Directory.CreateDirectory(installPath);

                }
                int i = 0;
                while (i != folders.Count)
                {
                    Directory.CreateDirectory(installPath + $"\\{folders[i]}");
                    rtxInfoBox.Invoke(new Action(() => { rtxInfoBox.Text += $"\nCreated folder {folders[i]} in install directory..."; }));
                    i++;
                }
                try
                {
                    int j = 0;
                    while (j != files.Count)
                    {
                        string[] split = files[j].Split(new[] { "Release\\" }, StringSplitOptions.RemoveEmptyEntries);
                        System.IO.File.Copy(files[j], installPath + $"/{split[1]}");
                        rtxInfoBox.Invoke(new Action(() => { rtxInfoBox.Text += $"\nCopied {split[1]} to directory..."; }));
                        j++;
                    }

                    //Verify everything is created...
                    if (i == folders.Count) { rtxInfoBox.Invoke(new Action(() => { rtxInfoBox.Text += $"\nAll folders(s) created!"; })); }
                    else { rtxInfoBox.Invoke(new Action(() => { rtxInfoBox.Text += $"\n{i} folders(s) created! NOT all folders could be created by install!"; })); }

                    if (j == files.Count)
                    {
                        rtxInfoBox.Invoke(new Action(() => { rtxInfoBox.Text += $"\nAll file(s) copied!"; }));
                    }
                    else { rtxInfoBox.Invoke(new Action(() => { rtxInfoBox.Text += $"\n{j} file(s) copied! NOT all files could be created by install!"; })); }
                    HandleShortCuts(chkShortCDesk.Checked, chkShortCStartMenu.Checked);
                }
                catch (Exception ex)
                {
                    string errorMsg = ex.ToString();
                    rtxInfoBox.Invoke(new Action(() =>
                    {
                        rtxInfoBox.ForeColor = Color.Yellow;
                        rtxInfoBox.Text += $"\nUnable to finish install!\n\nSend me this error code please:\n";
                        rtxInfoBox.ForeColor = Color.Red;
                        rtxInfoBox.Text += $"\n{errorMsg}";
                        rtxInfoBox.ForeColor = Color.White;
                    }));
                }
            }).Start();
        }
        private void HandleShortCuts(bool desktopShortcut, bool startmenuShortcut)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Logical Desktop - stackoverflow
            string startmenuPath = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu); // Start Menu
            ;
            if (desktopShortcut)
            {
                CreateShortcut("BlockBrawl", desktopPath, installPath + "\\BlockBrawl.exe");
                rtxInfoBox.Invoke(new Action(() =>
                {
                    rtxInfoBox.Text += $"\nDesktop shortcut created:\n";
                    rtxInfoBox.Text += desktopPath;
                }));
            }
            if (startmenuShortcut)
            {
                if (!Directory.Exists(startmenuPath + "\\Programs\\BlockBrawl"))
                {
                    Directory.CreateDirectory(startmenuPath + "\\Programs\\BlockBrawl");
                }
                CreateShortcut("BlockBrawl", startmenuPath + "\\Programs\\BlockBrawl", installPath + "\\BlockBrawl.exe");
                rtxInfoBox.Invoke(new Action(() =>
                {
                    rtxInfoBox.Text += $"\nStartmenu shortcut created:\n";
                    rtxInfoBox.Text += startmenuPath + "\\Programs\\BlockBrawl\n";
                }));
                CreateShortcut("BlockBrawl.Uninstall", startmenuPath + "\\Programs\\BlockBrawl", installPath + "\\RunUninstall.bat");
            }
        }
        private void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation)
        {//Thanks to www.fluxbytes.com/
            string shortcutLocation = Path.Combine(shortcutPath, shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = "BlockBrawl Game";   // The description of the shortcut
            shortcut.IconLocation = $"{rootPath}" + "\\blockbrawl.ico";           // The icon of the shortcut
            shortcut.TargetPath = targetFileLocation;                 // The path of the file that will launch when the shortcut is run
            shortcut.Save();                                    // Save the shortcut
        }

        private void Installation_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }

        private void btnChangeDir_Click(object sender, EventArgs e)
        {
            DialogResult result = fldBrowser.ShowDialog();

            while (result != DialogResult.OK || result != DialogResult.Cancel)
            {

            }
            installPath = fldBrowser.SelectedPath;
            rtxInstallDir.Text = installPath;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }

        private void rtxInfoBox_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            rtxInfoBox.SelectionStart = rtxInfoBox.Text.Length;
            // scroll it automatically
            rtxInfoBox.ScrollToCaret();//Ty stackOverflow
        }
    }
}
