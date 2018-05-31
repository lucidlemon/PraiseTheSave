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
        public Timer BackupTimer;
        public DateTime BackupStarting;

        public DateTime? lastDS1Change;
        public DateTime? lastDS2Change;
        public DateTime? lastDS3Change;
        public DateTime? lastDS1RChange;

        private Properties.Settings DefSettings => Properties.Settings.Default;

        public Form1()
        {
            BackupTimer = new Timer();
            BackupTimer.Tick += new EventHandler(doBackup);
            BackupTimer.Interval = (DefSettings.SaveInterval * 60 * 1000);
            BackupTimer.Enabled = false;

            if (DefSettings.AutomaticBackups == true)
            {
                BackupTimer.Enabled = true;
                BackupTimer.Start();
                BackupStarting = DateTime.Now.AddMinutes(DefSettings.SaveInterval);
            }

            InitializeComponent();

            checkSaveLocations();
            refreshInfo();

            activateAutomaticBackups.Checked = DefSettings.AutomaticBackups;
        }

        private string BytesToMbStr(long numBytes)
        {
            return (numBytes / 1024.0 / 1024.0).ToString("0.00") + "Mb";
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


        private FileInfo getLatestFileInDir(DirectoryInfo dir)
        {
            return dir.GetFiles("*.*", SearchOption.AllDirectories).OrderByDescending(f => f.LastWriteTime).First();
        }

        private FileInfo getOldestFileInDir(DirectoryInfo dir)
        {
            return dir.GetFiles().OrderByDescending(f => f.LastWriteTime).Last();
        }

        public bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public void checkSaveLocations()
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
                DateTime changeTime = File.GetLastWriteTime(getLatestFileInDir(new DirectoryInfo(saveLocation)).FullName);

                foundFolder.Text = "found " + gameInitials + " saves! total size is " + BytesToMbStr(folderSize);
                lastChange.Text = "last change was at: " + changeTime.ToString();
            }
            else
            {
                foundFolder.Text = "found no " + gameInitials + " saves.";
            }
        }

        public void refreshInfo()
        {
            string backupDir = DefSettings.SaveLocation;
            backupFolderLabel.Text = backupDir;
            if (Directory.Exists(backupDir))
            {
                backupFolderSizeLabel.Text =
                "Size of the backup folder is " + BytesToMbStr(DirSize(new DirectoryInfo(backupDir)));
            }
            

            saveAmountInput.Value = DefSettings.SaveAmount;
            saveIntervalInput.Value = DefSettings.SaveInterval;

            if (DefSettings.AutomaticBackups)
            {
                lastBackupLabel.Text = "Next Backup at " + BackupStarting.ToString("HH:mm:ss") + Environment.NewLine + "(if there were changes to the file)";
            } else
            {
                lastBackupLabel.Text = "No automatic Backups active.";
            }


            DispSaveStats(DefSettings.ds1location, ds1_found_folder, ds1_last_change_label, "ds1");
            DispSaveStats(DefSettings.ds2location, ds2_found_folder, ds2_last_change_label, "ds2");
            DispSaveStats(DefSettings.ds3location, ds3_found_folder, ds3_last_change_label, "ds3");
            DispSaveStats(DefSettings.ds1Rlocation, ds1R_found_folder, ds1R_last_change_label, "ds1 remastered");
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
                    lastChange != File.GetLastWriteTime(getLatestFileInDir(new DirectoryInfo(saveLocation)).FullName)
                    )
                {
                    while (DefSettings.SaveAmount <= Directory.GetFiles(destination).Length)
                    {
                        FileInfo deleteMe = getOldestFileInDir(new DirectoryInfo(destination));
                        deleteMe.Delete();
                    }

                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddDirectory(saveLocation);
                        zip.Save(destination + DateTime.Now.ToString("yyyyMMddHHmmss") + ".zip");
                    }
                }

                lastChangeSetting = File.GetLastWriteTime(getLatestFileInDir(new DirectoryInfo(saveLocation)).FullName);
            }
        }

        public void doBackup(object sender, EventArgs e)
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
            refreshInfo();
        }


        private void chooseDirectory(object sender, EventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                DefSettings.SaveLocation = dialog.SelectedPath;
                DefSettings.Save();
                refreshInfo();
            }
        }

        private void saveAmountInput_ValueChanged(object sender, EventArgs e)
        {
            DefSettings.SaveAmount = (int) saveAmountInput.Value;
            DefSettings.Save();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            DefSettings.SaveInterval = (int) saveIntervalInput.Value;
            DefSettings.Save();

            BackupTimer.Interval = (DefSettings.SaveInterval * 60 * 1000);
            resetTimer();
        }

        private void activateAutomaticBackups_CheckedChanged(object sender, EventArgs e)
        {
            DefSettings.AutomaticBackups = activateAutomaticBackups.Checked;
            DefSettings.Save();

            resetTimer();
        }

        private void resetTimer()
        {
            BackupTimer.Stop();
            BackupTimer.Enabled = false;

            if (activateAutomaticBackups.Checked)
            {
                BackupTimer.Start();
                BackupTimer.Enabled = true;
                BackupStarting = DateTime.Now.AddMinutes(DefSettings.SaveInterval);
            }

            refreshInfo();
        }

        private void ds1link_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(DefSettings.ds1location);
        }

        private void ds2link_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(DefSettings.ds2location);
        }

        private void ds3link_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(DefSettings.ds3location);
        }

        private void ds1Rlink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(DefSettings.ds1Rlocation);
        }

        private void backupFolderLabel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(DefSettings.SaveLocation);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Git Gud u fucking casul. No Souls for u.", "WTF!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
