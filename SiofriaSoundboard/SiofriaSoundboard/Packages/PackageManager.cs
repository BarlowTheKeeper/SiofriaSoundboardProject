using SiofriaSoundboard.AudioStuff;
using SiofriaSoundboard.Input;
using SiofriaSoundboard.Utils;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiofriaSoundboard.Packages
{
    public class PackageManager
    {
        private const string packageDirName = "packages";
        private string packagesPath = Path.Combine(new FileInfo(Application.ExecutablePath).Directory.FullName, packageDirName);
        private const string audioDirName = "Audio";
        private const string exportConfigName = "config.sbcfg";
        private const string lastSaveFileCache = "last_used_file.txt";

        private SoundPackage current = null;

        public SoundPackage Current
        {
            get { return current; }
            private set
            {
                current = value;
                SaveToLastFileCache(Current.Config);
            } 
        }

        public PackageManager()
        {
            LoadLast();
            if (Current == null || !Current.Valid)
                Current = new SoundPackage();
        }

        public void Save()
        {
            if (Current.Config.Length == 0)
                SaveAs();
            else
                Current.Save();
        }

        public void SaveAs()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Title = "Save soundboard config file";
            saveFileDialog1.DefaultExt = "sbcfg";
            saveFileDialog1.Filter = "Soundboard config files (*.sbcfg)|*.sbcfg";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Current.SaveAs(saveFileDialog1.FileName);
                SaveToLastFileCache(Current.Config);
            }
        }

        public void Load()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Load soundboard config file";
            openFileDialog1.DefaultExt = "sbcfg";
            openFileDialog1.Filter = "Soundboard config files (*.sbcfg)|*.sbcfg";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Current = new SoundPackage(openFileDialog1.FileName);
            }
        }

        internal void LoadLast()
        {
            try
            {
                string file = File.ReadLines(lastSaveFileCache).ElementAt<string>(0).Trim();
                if (File.Exists(file))
                    Current = new SoundPackage(file);
            }
            catch (Exception ex) 
            { 
                Log.Write(ex);
                Current = new SoundPackage();
            }
        }

        public void NewPackage()
        {
            if (Current.GetSoundBindings().Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save before creating a new file?", "Save?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                    Save();
            }

            Current = new SoundPackage();
        }

        public void Import()
        {
            string previouslyOpen = Current.Config;
            NewPackage();

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Load soundboard package archive";
            openFileDialog1.DefaultExt = "zip";
            openFileDialog1.Filter = "Soundboard package archive (*.zip)|*.zip";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string packageArchive = openFileDialog1.FileName;
                string imporedPakageName = Path.GetFileNameWithoutExtension(packageArchive);
                string packageExtractionPath = Path.Combine(packagesPath, imporedPakageName);

                if (Directory.Exists(packageExtractionPath))
                {
                    DialogResult dialogResult = MessageBox.Show(
                        "There is already a package with that name!\nDo you want to override it?",
                        "Action needed!",
                        MessageBoxButtons.OKCancel
                        );

                    if (dialogResult == DialogResult.Cancel)
                    {
                        Current = new SoundPackage(previouslyOpen);
                        return;
                    }
                }

                ZipFile.ExtractToDirectory(packageArchive, packageExtractionPath, true);
                string saveFilePath = Path.Combine(packageExtractionPath, exportConfigName);

                Current = new SoundPackage(saveFilePath);
            }
        }

        public void Export()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Title = "Export soundboard package";
            saveFileDialog1.DefaultExt = "zip";
            saveFileDialog1.Filter = "Soundboard package archive (*.zip)|*.zip";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string exportDir = new FileInfo(saveFileDialog1.FileName).Directory.FullName; 

                string exportFilePath = saveFileDialog1.FileName;
                string exportFileName = Path.GetFileNameWithoutExtension(exportFilePath);
                 
                string exportedPackagePath = Path.Combine(exportDir, exportFileName);

                try
                {
                    string packageAudioPath = Path.Combine(exportedPackagePath, audioDirName);
                    CopyAllAudioFilesToExportDir(packageAudioPath);

                    string relativeAutioDirPath = Path.Combine(packageDirName, exportFileName, audioDirName);
                    string packageConfig = Path.Combine(exportedPackagePath, exportConfigName);
                    Current.SerializeConfigForExport(packageConfig, relativeAutioDirPath);

                    ZipFile.CreateFromDirectory(exportedPackagePath, exportFilePath, CompressionLevel.Fastest, false);
                    MessageBox.Show("Export Complete!\nYou will find your file at: " + exportFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to copy all audio files to the package! Exported package might be unusable. Check the logs for more info about the error.\n" + ex.Message);
                    Log.Write(ex);
                }

                try
                {
                    if (Directory.Exists(exportedPackagePath))
                    {
                        Directory.Delete(exportedPackagePath, true);
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                }
            }
        }

        private void CopyAllAudioFilesToExportDir(string path)
        {
            List<string> failedFiles = new List<string>();
            foreach (string clipSource in Current.GetAllSounds())
            {
                if (clipSource.Length == 0)
                    continue;

                if (!clipSource.Contains(".mp3") && !clipSource.Contains(".wav"))
                    continue;

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string clipDest = Path.Combine(path, Path.GetFileName(clipSource));

                try
                {
                    File.Copy(clipSource, clipDest, true);
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    failedFiles.Add(clipSource);
                }
            }

            if (failedFiles.Count > 0)
            {
                MessageBox.Show("These files failed to export [check the logs]: \n" + String.Join(", ", failedFiles.ToArray()));
            }
        }

        public void SaveToLastFileCache(string configPath)
        {
            using (StreamWriter writer = new StreamWriter(lastSaveFileCache))
            {
                writer.Write(configPath);
            }
        }

        public string GetCurrentPackageName()
        {
            try
            {
                if (Current.Config.Contains(packagesPath))
                    return Directory.GetParent(Current.Config).Name;

                return "Packageless Config [" +
                Path.Combine(
                    Directory.GetParent(Current.Config).Name,
                    Path.GetFileNameWithoutExtension(Current.Config)
                    )
                + "]";

            }
            catch { }

            return "Packageless Config";
        }

        public List<string> GetPackageList()
        {
            List<string> directories = new List<string>();
            foreach (var directory in Directory.EnumerateDirectories(packagesPath, "*", SearchOption.TopDirectoryOnly))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(directory);
                directories.Add(dirInfo.Name);
            }
            return directories;
        }

        public bool DeletePackage(string name)
        {
            string packageDirName = Path.Combine(packagesPath, name);

            if (Current.Config.Contains(packageDirName) && Current.GetSoundBindings().Count > 0)
            {
                 DialogResult dialogResult = MessageBox.Show("You are removing your currently open package?", "Are you sure?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                    return false;
            }

            if (Directory.Exists(packageDirName))
                Directory.Delete(packageDirName, true);

            return true;
        }

        public void LoadPackageByName(string name)
        {
            if (Current.GetSoundBindings().Count > 0) //TODO: I can probably move this check for saving to the setter or smth...as it is redundant code
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save before opening another package?", "Save?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                    Save();
            }

            Current = new SoundPackage(Path.Combine(packagesPath, name, exportConfigName));
        }

        public void DuplicatePackage(string pkgName, string newPkgName)
        {
            if(pkgName.Length == 0 || newPkgName.Length == 0)
            { return; }

            string src = Path.Combine(packagesPath, pkgName);
            string dest = Path.Combine(packagesPath, newPkgName);

            try
            {
                FileUtils.CopyDirectory(src, dest, true);
                string srcCfg = Path.Combine(src, exportConfigName);
                string destCfg = Path.Combine(dest, exportConfigName);

                string text = File.ReadAllText(srcCfg);
                text = text.Replace("\\" + pkgName + "\\", "\\" + newPkgName + "\\");
                File.WriteAllText(destCfg, text);
            }
            catch   (Exception ex) {

                MessageBox.Show("Failed to duplicate package. New package might be unusable. Check the logs for more info about the error.\n" + ex.Message);
                Log.Write(ex);
            }
        }
    }
}
