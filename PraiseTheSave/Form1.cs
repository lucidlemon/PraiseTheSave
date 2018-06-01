using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Ionic.Zip;

namespace PraiseTheSave
{
    public partial class Form1 : Form
    {
        public Timer backupTimer;
        public DateTime backupStarting;

        public DateTime? lastDS1Change;
        public DateTime? lastDS2Change;
        public DateTime? lastDS3Change;
        public DateTime? lastDS1RChange;

        private Properties.Settings DefSettings => Properties.Settings.Default;

        public Form1()
        {
            backupTimer = new Timer();
            backupTimer.Tick += new EventHandler(DoBackup);
            backupTimer.Interval = (DefSettings.SaveInterval * 60 * 1000);
            backupTimer.Enabled = false;

            if (DefSettings.AutomaticBackups == true)
            {
                backupTimer.Enabled = true;
                backupTimer.Start();
                backupStarting = DateTime.Now.AddMinutes(DefSettings.SaveInterval);
            }

            InitializeComponent();

            CheckSaveLocations();
            RefreshInfo();

            activateAutomaticBackups.Checked = DefSettings.AutomaticBackups;
        }

        private string BytesToMbStr(long numBytes)
        {
            return (numBytes / 1024.0 / 1024.0).ToString("N2");
        }

        public static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }


        private FileInfo GetLatestFileInDir(DirectoryInfo dir)
        {
            FileInfo[] files = dir.GetFiles("*.*", SearchOption.AllDirectories);
            if (files.Length == 0) return null;
            return files.Aggregate((current, next) => next.LastWriteTime > current.LastWriteTime ? next : current);
        }

        private FileInfo GetOldestFileInDir(DirectoryInfo dir)
        {
            FileInfo[] files = dir.GetFiles();
            if (files.Length == 0) return null;
            return files.Aggregate((current, next) => next.LastWriteTime < current.LastWriteTime ? next : current);
        }

        public bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public void CheckSaveLocations()
        {
            if (DefSettings.ds1location == "" || !Directory.Exists(DefSettings.ds1location))
            {
                string ds1save = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\NBGI\DarkSouls";
                DefSettings.ds1location = Environment.ExpandEnvironmentVariables(ds1save);
            }

            if (DefSettings.ds2location == "" || !Directory.Exists(DefSettings.ds2location))
            {
                string ds2save = @"%AppData%\DarkSoulsII";
                DefSettings.ds2location = Environment.ExpandEnvironmentVariables(ds2save);
            }

            if (DefSettings.ds3location == "" || !Directory.Exists(DefSettings.ds3location))
            {
                string ds3save = @"%AppData%\DarkSoulsIII";
                DefSettings.ds3location = Environment.ExpandEnvironmentVariables(ds3save);
            }

            if (DefSettings.ds1Rlocation == "" || !Directory.Exists(DefSettings.ds1location))
            {
                string ds1save = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\NBGI\DARK SOULS REMASTERED";
                DefSettings.ds1Rlocation = Environment.ExpandEnvironmentVariables(ds1save);
            }

            DefSettings.Save();
        }

        private void DispSaveStats(string saveLocation, Label foundFolder, Label lastChange, string gameInitials)
        {
            if (Directory.Exists(saveLocation))
            {
                long folderSize = DirSize(new DirectoryInfo(saveLocation));
                DateTime changeTime = File.GetLastWriteTime(GetLatestFileInDir(new DirectoryInfo(saveLocation)).FullName);

                foundFolder.Text = string.Format(Resource1.msgFoundSaves,
                    gameInitials, BytesToMbStr(folderSize), Resource1.mebibyteUnit);
                lastChange.Text = string.Format(Resource1.msgLastChange, changeTime.ToString("F"));
            }
            else
            {
                foundFolder.Text = string.Format(Resource1.msgFoundNoSaves, gameInitials);
            }
        }

        public void RefreshInfo()
        {
            string backupDir = DefSettings.SaveLocation;
            backupFolderLabel.Text = backupDir;
            if (Directory.Exists(backupDir))
            {
                backupFolderSizeLabel.Text = string.Format(Resource1.msgBackupFolderSize,
                    BytesToMbStr(DirSize(new DirectoryInfo(backupDir))), Resource1.mebibyteUnit);
            }
            

            saveAmountInput.Value = DefSettings.SaveAmount;
            saveIntervalInput.Value = DefSettings.SaveInterval;

            if (DefSettings.AutomaticBackups)
            {
                lastBackupLabel.Text = string.Format(Resource1.msgNextBackup, backupStarting.ToString("T"));
            }
            else
            {
                lastBackupLabel.Text = Resource1.msgBackupsInactive;
            }


            DispSaveStats(DefSettings.ds1location, ds1_found_folder, ds1_last_change_label, Resource1.initialsDs1);
            DispSaveStats(DefSettings.ds2location, ds2_found_folder, ds2_last_change_label, Resource1.initialsDs2);
            DispSaveStats(DefSettings.ds3location, ds3_found_folder, ds3_last_change_label, Resource1.initialsDs3);
            DispSaveStats(DefSettings.ds1Rlocation, ds1R_found_folder, ds1R_last_change_label, Resource1.initialsDsr);
        }

        private void BackupGame(string saveLocation, string destination, DateTime? lastChange, DateTime lastChangeSetting)
        {
            if (Directory.Exists(saveLocation))
            {
                if (!Directory.Exists(destination))
                    Directory.CreateDirectory(destination);


                if (
                    IsDirectoryEmpty(destination)
                    ||
                    !lastChange.HasValue
                    ||
                    lastChange != File.GetLastWriteTime(GetLatestFileInDir(new DirectoryInfo(saveLocation)).FullName)
                    )
                {
                    while (DefSettings.SaveAmount <= Directory.GetFiles(destination).Length)
                    {
                        FileInfo deleteMe = GetOldestFileInDir(new DirectoryInfo(destination));
                        deleteMe.Delete();
                    }

                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddDirectory(saveLocation);
                        zip.Save(destination + DateTime.Now.ToString("yyyyMMddHHmmss") + ".zip");
                    }
                }

                lastChangeSetting = File.GetLastWriteTime(GetLatestFileInDir(new DirectoryInfo(saveLocation)).FullName);
            }
        }

        public void DoBackup(object sender, EventArgs e)
        {
            DateTime localDate = DateTime.Now;

            Console.Write(DefSettings.SaveLocation);

            string ds1destination = DefSettings.SaveLocation + @"\ds1\";
            string ds2destination = DefSettings.SaveLocation + @"\ds2\";
            string ds3destination = DefSettings.SaveLocation + @"\ds3\";
            string ds1Rdestination = DefSettings.SaveLocation + @"\ds1_remastered\";


            if (!Directory.Exists(DefSettings.SaveLocation))
            {
                Directory.CreateDirectory(DefSettings.SaveLocation);
            }


            BackupGame(DefSettings.ds1location, ds1destination, lastDS1Change, DefSettings.LastDS1Change);
            BackupGame(DefSettings.ds2location, ds2destination, lastDS2Change, DefSettings.LastDS2Change);
            BackupGame(DefSettings.ds3location, ds3destination, lastDS3Change, DefSettings.LastDS3Change);
            BackupGame(DefSettings.ds1Rlocation, ds1Rdestination, lastDS1RChange, DefSettings.LastDS1RChange);

            DefSettings.Save();
            RefreshInfo();
        }


        private void ChooseDirectory(object sender, EventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                DefSettings.SaveLocation = dialog.SelectedPath;
                DefSettings.Save();
                RefreshInfo();
            }
        }

        private void SaveAmountInput_ValueChanged(object sender, EventArgs e)
        {
            DefSettings.SaveAmount = (int) saveAmountInput.Value;
            DefSettings.Save();
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            DefSettings.SaveInterval = (int) saveIntervalInput.Value;
            DefSettings.Save();

            backupTimer.Interval = (DefSettings.SaveInterval * 60 * 1000);
            ResetTimer();
        }

        private void ActivateAutomaticBackups_CheckedChanged(object sender, EventArgs e)
        {
            DefSettings.AutomaticBackups = activateAutomaticBackups.Checked;
            DefSettings.Save();

            ResetTimer();
        }

        private void ResetTimer()
        {
            backupTimer.Stop();
            backupTimer.Enabled = false;

            if (activateAutomaticBackups.Checked)
            {
                backupTimer.Start();
                backupTimer.Enabled = true;
                backupStarting = DateTime.Now.AddMinutes(DefSettings.SaveInterval);
            }

            RefreshInfo();
        }

        private void Ds1link_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(DefSettings.ds1location);
        }

        private void Ds2link_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(DefSettings.ds2location);
        }

        private void Ds3link_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(DefSettings.ds3location);
        }

        private void Ds1Rlink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(DefSettings.ds1Rlocation);
        }

        private void BackupFolderLabel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(DefSettings.SaveLocation);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resource1.msgNoSouls, Resource1.titleNoSouls,
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
