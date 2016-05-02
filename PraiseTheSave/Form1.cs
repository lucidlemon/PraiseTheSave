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

        public Form1()
        {
            BackupTimer = new Timer();
            BackupTimer.Tick += new EventHandler(doBackup);
            BackupTimer.Interval = (PraiseTheSave.Properties.Settings.Default.SaveInterval * 60 * 1000);
            BackupTimer.Enabled = false;

            if (PraiseTheSave.Properties.Settings.Default.AutomaticBackups == true)
            {
                BackupTimer.Enabled = true;
                BackupTimer.Start();
                BackupStarting = DateTime.Now.AddMinutes(PraiseTheSave.Properties.Settings.Default.SaveInterval);
            }

            InitializeComponent();

            checkSaveLocations();
            refreshInfo();

            activateAutomaticBackups.Checked = PraiseTheSave.Properties.Settings.Default.AutomaticBackups;
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
            return dir.GetFiles().OrderByDescending(f => f.LastWriteTime).First();
        }

        private FileInfo getOldestFileInDir(DirectoryInfo dir)
        {
            return dir.GetFiles().OrderByDescending(f => f.LastWriteTime).Last();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public void checkSaveLocations()
        {
            if (PraiseTheSave.Properties.Settings.Default.ds1location == "" || !Directory.Exists(PraiseTheSave.Properties.Settings.Default.ds1location))
            {
                string ds1save = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\NBGI\DarkSouls";
                PraiseTheSave.Properties.Settings.Default.ds1location = Environment.ExpandEnvironmentVariables(ds1save);
            }

            if (PraiseTheSave.Properties.Settings.Default.ds2location == "" || !Directory.Exists(PraiseTheSave.Properties.Settings.Default.ds2location))
            {
                string ds2save = @"%AppData%\DarkSoulsII";
                PraiseTheSave.Properties.Settings.Default.ds2location = Environment.ExpandEnvironmentVariables(ds2save);
            }

            if (PraiseTheSave.Properties.Settings.Default.ds3location == "" || !Directory.Exists(PraiseTheSave.Properties.Settings.Default.ds3location))
            {
                string ds3save = @"%AppData%\DarkSoulsIII";
                PraiseTheSave.Properties.Settings.Default.ds3location = Environment.ExpandEnvironmentVariables(ds3save);
            }

            PraiseTheSave.Properties.Settings.Default.Save();
        }

        public void refreshInfo()
        {
            string backupDir = PraiseTheSave.Properties.Settings.Default.SaveLocation;
            backupFolderLabel.Text = backupDir;
            backupFolderSizeLabel.Text =
                "Size of the backup folder is " +
                (DirSize(new DirectoryInfo(backupDir)) / 1024.0 / 1024.0).ToString("0.00")
                + "Mb";

            saveAmountInput.Value = PraiseTheSave.Properties.Settings.Default.SaveAmount;
            saveIntervalInput.Value = PraiseTheSave.Properties.Settings.Default.SaveInterval;

            if (PraiseTheSave.Properties.Settings.Default.AutomaticBackups)
            {
                lastBackupLabel.Text = "Next Backup at " + BackupStarting.ToString("HH:mm:ss") + Environment.NewLine + "(if there were changes to the file)";
            } else
            {
                lastBackupLabel.Text = "No automatic Backups active.";
            }
            


            string ds1save = PraiseTheSave.Properties.Settings.Default.ds1location;
            if (Directory.Exists(ds1save))
            {
                long ds1_found_folder_size = DirSize(new DirectoryInfo(ds1save));
                DateTime ds1_last_change = File.GetLastWriteTime(getLatestFileInDir(new DirectoryInfo(ds1save)).FullName);

                ds1_found_folder.Text = "found ds1 saves! total size is " + (ds1_found_folder_size / 1024.0 / 1024.0).ToString("0.00") + "Mb";
                ds1_last_change_label.Text = "last change was at: " + ds1_last_change.ToString();
            }
            else
            {
                ds3_found_folder.Text = "found no ds3 saves.";
            }



            string ds2save = PraiseTheSave.Properties.Settings.Default.ds2location;
            if (Directory.Exists(ds2save))
            {
                long ds2_found_folder_size = DirSize(new DirectoryInfo(ds2save));
                DateTime ds2_last_change = File.GetLastWriteTime(getLatestFileInDir(new DirectoryInfo(ds2save)).FullName);

                ds2_found_folder.Text = "found ds2 saves! total size is " + (ds2_found_folder_size / 1024.0 / 1024.0).ToString("0.00") + "Mb";
                ds2_last_change_label.Text = "last change was at: " + ds2_last_change.ToString();
            }
            else
            {
                ds3_found_folder.Text = "found no ds3 saves.";
            }


            string ds3save = PraiseTheSave.Properties.Settings.Default.ds3location;
            if (Directory.Exists(ds3save))
            {
                long ds3_found_folder_size = DirSize(new DirectoryInfo(ds3save));
                DateTime ds3_last_change = File.GetLastWriteTime(getLatestFileInDir(new DirectoryInfo(ds3save)).FullName);

                ds3_found_folder.Text = "found ds3 saves! total size is " + (ds3_found_folder_size / 1024.0 / 1024.0).ToString("0.00") + "Mb";
                ds3_last_change_label.Text = "last change was at: " + ds3_last_change.ToString();
            }
            else
            {
                ds3_found_folder.Text = "found no ds3 saves.";
            }
        }


        public void doBackup(object sender, EventArgs e)
        {
            DateTime localDate = DateTime.Now;

            Console.Write(PraiseTheSave.Properties.Settings.Default.SaveLocation);

            string ds1destination = PraiseTheSave.Properties.Settings.Default.SaveLocation + @"\ds1\";
            string ds2destination = PraiseTheSave.Properties.Settings.Default.SaveLocation + @"\ds2\";
            string ds3destination = PraiseTheSave.Properties.Settings.Default.SaveLocation + @"\ds3\";


            string ds1save = PraiseTheSave.Properties.Settings.Default.ds1location;
            if (Directory.Exists(ds1save))
            {
                if (!Directory.Exists(ds1destination))
                    Directory.CreateDirectory(ds1destination);

                if (PraiseTheSave.Properties.Settings.Default.LastDS1Change != File.GetLastWriteTime(getLatestFileInDir(new DirectoryInfo(ds1save)).FullName))
                {
                    while (PraiseTheSave.Properties.Settings.Default.SaveAmount <= Directory.GetFiles(ds1destination).Length)
                    {
                        FileInfo deleteMe = getOldestFileInDir(new DirectoryInfo(ds1destination));
                        deleteMe.Delete();
                    }

                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddDirectory(PraiseTheSave.Properties.Settings.Default.ds1location);
                        zip.Save(ds1destination + localDate.ToString("yyyyMMddHHmmss") + ".zip");
                    }
                }

                PraiseTheSave.Properties.Settings.Default.LastDS1Change = File.GetLastWriteTime(getLatestFileInDir(new DirectoryInfo(ds1save)).FullName);
            }


            string ds2save = PraiseTheSave.Properties.Settings.Default.ds2location;
            if (Directory.Exists(ds2save))
            {
                if (!Directory.Exists(ds2destination))
                    Directory.CreateDirectory(ds2destination);

                if (PraiseTheSave.Properties.Settings.Default.LastDS2Change != File.GetLastWriteTime(getLatestFileInDir(new DirectoryInfo(ds2save)).FullName))
                {

                    while (PraiseTheSave.Properties.Settings.Default.SaveAmount <= Directory.GetFiles(ds2destination).Length)
                    {
                        FileInfo deleteMe = getOldestFileInDir(new DirectoryInfo(ds2destination));
                        deleteMe.Delete();
                    }

                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddDirectory(PraiseTheSave.Properties.Settings.Default.ds2location);
                        zip.Save(ds2destination + localDate.ToString("yyyyMMddHHmmss") + ".zip");
                    }

                }

                PraiseTheSave.Properties.Settings.Default.LastDS2Change = File.GetLastWriteTime(getLatestFileInDir(new DirectoryInfo(ds2save)).FullName);
            }

            string ds3save = PraiseTheSave.Properties.Settings.Default.ds3location;
            if (Directory.Exists(ds3save))
            {
                if (!Directory.Exists(ds3destination))
                    Directory.CreateDirectory(ds3destination);

                if (PraiseTheSave.Properties.Settings.Default.LastDS3Change != File.GetLastWriteTime(getLatestFileInDir(new DirectoryInfo(ds3save)).FullName))
                {

                    while (PraiseTheSave.Properties.Settings.Default.SaveAmount <= Directory.GetFiles(ds3destination).Length)
                    {
                        FileInfo deleteMe = getOldestFileInDir(new DirectoryInfo(ds3destination));
                        deleteMe.Delete();
                    }

                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddDirectory(PraiseTheSave.Properties.Settings.Default.ds3location);
                        zip.Save(ds3destination + localDate.ToString("yyyyMMddHHmmss") + ".zip");
                    }
                }

                PraiseTheSave.Properties.Settings.Default.LastDS3Change = File.GetLastWriteTime(getLatestFileInDir(new DirectoryInfo(ds3save)).FullName);
            }

            PraiseTheSave.Properties.Settings.Default.Save();
            refreshInfo();
        }


        private void chooseDirectory(object sender, EventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                PraiseTheSave.Properties.Settings.Default.SaveLocation = dialog.SelectedPath;
                PraiseTheSave.Properties.Settings.Default.Save();
                refreshInfo();
            }
        }

        private void saveAmountInput_ValueChanged(object sender, EventArgs e)
        {
            PraiseTheSave.Properties.Settings.Default.SaveAmount = (int) saveAmountInput.Value;
            PraiseTheSave.Properties.Settings.Default.Save();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            PraiseTheSave.Properties.Settings.Default.SaveInterval = (int) saveIntervalInput.Value;
            PraiseTheSave.Properties.Settings.Default.Save();

           BackupTimer.Interval = (PraiseTheSave.Properties.Settings.Default.SaveInterval * 60 * 1000);
        }

        private void activateAutomaticBackups_CheckedChanged(object sender, EventArgs e)
        {
            PraiseTheSave.Properties.Settings.Default.AutomaticBackups = activateAutomaticBackups.Checked;
            PraiseTheSave.Properties.Settings.Default.Save();

            BackupTimer.Stop();
            BackupTimer.Enabled = false;

            if (activateAutomaticBackups.Checked)
            {
                BackupTimer.Start();
                BackupTimer.Enabled = true;
                BackupStarting = DateTime.Now.AddMinutes(PraiseTheSave.Properties.Settings.Default.SaveInterval);
            }

            refreshInfo();
        }

        private void ds1link_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(PraiseTheSave.Properties.Settings.Default.ds1location);
        }

        private void ds2link_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(PraiseTheSave.Properties.Settings.Default.ds2location);
        }

        private void ds3link_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(PraiseTheSave.Properties.Settings.Default.ds3location);
        }

        private void backupFolderLabel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(PraiseTheSave.Properties.Settings.Default.SaveLocation);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Git Gud u fucking casul. No Souls for u.", "WTF!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
