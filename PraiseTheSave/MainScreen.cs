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
using AutoUpdaterDotNET;

namespace PraiseTheSave
{
    public partial class MainScreen : Form
    {
        private class Game
        {
            public string SavePath { get; }
            private readonly string backupDir;

            private readonly string dispNameShort;
            private readonly Label lblFoundFolder;
            private readonly Label lblLastChange;

            public Game(string savePath, string backupDir, string dispNameShort, Label lblFoundFolder, Label lblLastChange)
            {
                savePath = savePath ?? throw new ArgumentNullException(nameof(savePath));
                SavePath = Environment.ExpandEnvironmentVariables(savePath);
                this.backupDir = backupDir ?? throw new ArgumentNullException(nameof(backupDir));
                this.dispNameShort = dispNameShort ?? throw new ArgumentNullException(nameof(dispNameShort));
                this.lblFoundFolder = lblFoundFolder ?? throw new ArgumentNullException(nameof(lblFoundFolder));
                this.lblLastChange = lblLastChange ?? throw new ArgumentNullException(nameof(lblLastChange));
            }

            private bool DoSavesExist()
            {
                return Directory.Exists(SavePath) && DirHasFiles(SavePath, true);
            }

            public void DispSaveStats()
            {
                if (DoSavesExist())
                {
                    DateTime changeTime = GetLastChanged(SavePath, true);

                    lblFoundFolder.Text = string.Format(Resource1.msgFoundSaves,
                        dispNameShort, BytesToMbStr(DirSize(new DirectoryInfo(SavePath), true)), Resource1.mebibyteUnit);
                    lblLastChange.Text = string.Format(Resource1.msgLastChange, changeTime.ToString("F"));
                }
                else
                {
                    lblFoundFolder.Text = string.Format(Resource1.msgFoundNoSaves, dispNameShort);
                }
            }

            public void Backup()
            {
                if (!DoSavesExist()) return; //Do nothing if there are no saves to backup

                string baseDir = DefSettings.SaveLocation;
                if (!Directory.Exists(baseDir)) Directory.CreateDirectory(baseDir);

                string destination = baseDir;
                if (!destination.EndsWith(@"\")) destination += @"\";
                destination += backupDir;
                if (!destination.EndsWith(@"\")) destination += @"\";

                if (!Directory.Exists(destination)) Directory.CreateDirectory(destination);

                //At this point, we know this game has saves and that the destination folder for their backups exists

                DateTime lastBackup;
                if (
                    !Directory.GetFiles(destination).Any()
                    ||
                    GetLastChanged(SavePath, true) > (lastBackup = GetLastChanged(destination, false))
                    ) //If the destination directory is empty, or if there is a save that was changed since the latest backup
                {
                    //Delete backups until there are one fewer than the user-set max number
                    DirectoryInfo dDestination = new DirectoryInfo(destination);
                    while (DefSettings.SaveAmount <= CountFilesInDir(dDestination, false))
                    {
                        GetOldestFileInDir(dDestination, false).Delete();
                    }

                    //Make a new backup
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddDirectory(SavePath);
                        zip.Save(destination + DateTime.Now.ToString("yyyyMMddHHmmss") + ".zip");
                    }
                }
            }
        }

        private Timer backupTimer;
        private DateTime nextBackup;

        private static Properties.Settings DefSettings => Properties.Settings.Default;

        private readonly Dictionary<string, Game> games;

        public MainScreen()
        {
            backupTimer = new Timer();
            backupTimer.Tick += new EventHandler(DoBackup);
            backupTimer.Interval = (DefSettings.SaveInterval * 60 * 1000);
            backupTimer.Enabled = false;

            if (DefSettings.AutomaticBackups == true)
            {
                backupTimer.Enabled = true;
                backupTimer.Start();
                nextBackup = DateTime.Now.AddMinutes(DefSettings.SaveInterval);
            }

            InitializeComponent();

            string DS1_Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\NBGI\DarkSouls";
            string DSR_Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\NBGI\DARK SOULS REMASTERED";

            if(!Directory.Exists(DSR_Path))
            {
                DSR_Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\FromSoftware\DARK SOULS REMASTERED";
            }

            games = new Dictionary<string, Game>
            {
                {
                    "DS1",
                    new Game(DS1_Path, DefSettings.dirDS1, Resource1.initialsDs1, ds1_found_folder, ds1_last_change_label)
                },
                {
                    "DS2",
                    new Game(@"%AppData%\DarkSoulsII", DefSettings.dirDS2, Resource1.initialsDs2, ds2_found_folder, ds2_last_change_label)
                },
                {
                    "DS3",
                    new Game(@"%AppData%\DarkSoulsIII", DefSettings.dirDS3, Resource1.initialsDs3, ds3_found_folder, ds3_last_change_label)
                },
                {
                    "DSR",
                    new Game(DSR_Path, DefSettings.dirDSR, Resource1.initialsDsr, ds1R_found_folder, ds1R_last_change_label)
                }
            };

            RefreshInfo();

            activateAutomaticBackups.Checked = DefSettings.AutomaticBackups;
        }

        private static string BytesToMbStr(long numBytes)
        {
            return (numBytes / 1024.0 / 1024.0).ToString("N2");
        }

        private static long DirSize(DirectoryInfo d, bool checkSubdirs)
        {
            return d.GetFiles("*", checkSubdirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                .Sum(f => f.Length);
        }

        private static bool DirHasFiles(string path, bool checkSubdirs)
        {
            return Directory.GetFiles(path, "*",
                checkSubdirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                .Any();
        }

        private static int CountFilesInDir(DirectoryInfo dir, bool checkSubdirs)
        {
            return dir.GetFiles("*",
                checkSubdirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                .Length;
        }

        private static DateTime GetLastChanged(string path, bool checkSubdirs)
        {
            FileInfo[] files = (new DirectoryInfo(path)).GetFiles("*",
                checkSubdirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            if (!files.Any()) return new DateTime(0);
            return files.Aggregate(
                    (current, next) => next.LastWriteTime > current.LastWriteTime ? next : current
                    ).LastWriteTime;
        }

        private static FileInfo GetOldestFileInDir(DirectoryInfo dir, bool checkSubdirs)
        {
            FileInfo[] files = dir.GetFiles("*",
                checkSubdirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            if (files.Length == 0) return null;
            return files.Aggregate(
                (current, next) => next.LastWriteTime < current.LastWriteTime ? next : current);
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            AutoUpdater.Start("https://lucidlemon.github.io/PraiseTheSave/AutoUpdater.xml");

            System.Timers.Timer timer = new System.Timers.Timer
            {
                Interval = 2 * 60 * 1000,
                SynchronizingObject = this
            };
            timer.Elapsed += delegate
            {
                AutoUpdater.Start("https://lucidlemon.github.io/PraiseTheSave/AutoUpdater.xml");
            };
            timer.Start();
        }

        public void RefreshInfo()
        {
            string backupDir = DefSettings.SaveLocation;
            backupFolderLabel.Text = backupDir;
            if (Directory.Exists(backupDir))
            {
                backupFolderSizeLabel.Text = string.Format(Resource1.msgBackupFolderSize,
                    BytesToMbStr(DirSize(new DirectoryInfo(backupDir), true)), Resource1.mebibyteUnit);
            }
            else
            {
                backupFolderSizeLabel.Text = Resource1.msgBackupFolderDNE;
            }


            saveAmountInput.Value = DefSettings.SaveAmount;
            saveIntervalInput.Value = DefSettings.SaveInterval;

            if (DefSettings.AutomaticBackups)
            {
                lastBackupLabel.Text = string.Format(Resource1.msgNextBackup, nextBackup.ToString("T"));
            }
            else
            {
                lastBackupLabel.Text = Resource1.msgBackupsInactive;
            }

            foreach (Game g in games.Values)
            {
                g.DispSaveStats();
            }
        }

        public void DoBackup(object sender, EventArgs e)
        {
            nextBackup = nextBackup.AddMilliseconds(backupTimer.Interval);

            Console.Write(DefSettings.SaveLocation);

            foreach (Game g in games.Values)
            {
                g.Backup();
            }

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
            DefSettings.SaveAmount = (int)saveAmountInput.Value;
            DefSettings.Save();
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            DefSettings.SaveInterval = (int)saveIntervalInput.Value;
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
                nextBackup = DateTime.Now.AddMinutes(DefSettings.SaveInterval);
            }

            RefreshInfo();
        }

        private void Ds1link_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(games["DS1"].SavePath);
        }

        private void Ds2link_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(games["DS2"].SavePath);
        }

        private void Ds3link_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(games["DS3"].SavePath);
        }

        private void Ds1Rlink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(games["DSR"].SavePath);
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
