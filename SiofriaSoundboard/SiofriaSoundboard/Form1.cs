using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Security.Principal;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SiofriaSoundboard
{
    public partial class Form1 : Form
    {

        private InputManager inputManager;
        private DictionaryBindingList<KeyPress, SoundClip> soundBindings;
        private KeyPress lastPress_color;

        private string saveFilePath = "";
        private const string lastSaveFileCache = "last_used_file.txt";
        private const string exportPackageName = "SbPkg";
        private const string audioDirName = "Audio";
        private const string exportConfigName = "config.sbcfg";
        private string exportAudioPath = Path.Combine(exportPackageName, audioDirName);

        public Form1()
        {
            InitializeComponent();
            soundBindings = new DictionaryBindingList<KeyPress, SoundClip>();
            dataGridView1.DataSource = soundBindings;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;

            try
            {
                inputManager = new InputManager("blocked_input.txt", fileCheckTimer);
                inputManager.OnInput += OnInput;
                loadLastFile();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Something went wrong while initializing inputManager. Check the logs.");
                Log.Write(exc);
                Application.Exit();
            }
        }


        private void loadLastFile()
        {
            try
            {
                string file = File.ReadLines(lastSaveFileCache).ElementAt<string>(0).Trim();
                if (File.Exists(file))
                {
                    saveFilePath = file;
                    LoadFile();
                }
            }
            catch (Exception ex) { Log.Write(ex); }

        }

        private void OnInput(int keycode)
        {
            KeyPress key = new KeyPress(keycode);
            if (keycode == 13) //Enter (return) should be a hotkey for applying settings
            {
                AudioFileCfg settings = getCurrentSettingWindow();
                if (settings == null)
                    return;

                settings.ApplyValuesToSoundclip();
                return;
            }

            if (!soundBindings.ContainsKey(key))
            {
                soundBindings.Add(key, new SoundClip());
                return;
            }

            if (cb_Play.Checked)
            {
                SoundClip clip = soundBindings.GetValue(key);
                if (clip.IsPlaying() && (getCurrentSettingWindowClip() == clip))
                    clip.Stop();
                else
                    clip.Play();
            }

            dataGridView1.ClearSelection();
            lastPress_color = key;
            ColorRowWithKey(key, dataGridView1.Rows[0].InheritedStyle.SelectionBackColor);
            OpenSoundSettings(key);

        }


        private AudioFileCfg getCurrentSettingWindow()
        {
            try
            {
                foreach (Component item in splitContainer1.Panel2.Controls)
                {
                    if (item is AudioFileCfg)
                    {
                        return ((AudioFileCfg)item);
                    }
                }
                return null;
            }
            catch (Exception) { return null; }
        }

        private SoundClip getCurrentSettingWindowClip()
        {
            return getCurrentSettingWindow()?.GetSoundClip();
        }

        private void restartInputProcess()
        {
            if (!inputManager.InputProcessRunning())
            {
                status_strip.Text = "No Input Process!";
                status_strip.ForeColor = Color.Red;
                try
                {
                    inputManager.StartInputProcess();
                    Log.Write("InputManager process started successfully");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Something went wrong while starting the Input intercepting process! Check the logs.");
                    Log.Write(exc);
                    keepAliveTimer.Stop();
                }
                return;
            }

            status_strip.Text = "All Good!";
            status_strip.ForeColor = Color.DarkGreen;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            restartInputProcess();
        }

        private void status_strip_Click(object sender, EventArgs e)
        {
            if (inputManager.InputProcessRunning())
            {
                inputManager.StopAllInputProcesses();
                return;
            }

            keepAliveTimer.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit?", "Exit? (reminder to save!)",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);

            e.Cancel = (result == DialogResult.No);

            if (result == DialogResult.Yes)
            {
                keepAliveTimer.Stop();
                inputManager.StopAllInputProcesses();
            }
        }

        private void fileCheckTimer_Tick(object sender, EventArgs e)
        {

        }

        private void OpenSoundSettings(KeyPress key)
        {
            try
            {
                SoundClip selected = soundBindings.GetValue(key);

                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(new AudioFileCfg(selected));
                splitContainer1.Panel2.Refresh();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Failed to open sound settings, check the logs!");
                Log.Write(exc);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                KeyPress key = (KeyPress)dataGridView1.SelectedRows[0].Cells[0].Value;
                if (lastPress_color != null && lastPress_color != key)
                {
                    ColorRowWithKey(lastPress_color, dataGridView1.Rows[0].InheritedStyle.BackColor);
                    lastPress_color = null;
                }

                OpenSoundSettings(key);
            }
            catch { }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ColorRowWithKey(KeyPress key, Color c)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value.Equals(key))
                {
                    row.Cells[0].Style.BackColor = c;
                    row.Cells[1].Style.BackColor = c;

                }
                else
                {
                    row.Cells[0].Style.BackColor = dataGridView1.Rows[0].InheritedStyle.BackColor;
                    row.Cells[1].Style.BackColor = dataGridView1.Rows[0].InheritedStyle.BackColor;
                }
            }
        }

        private void alwaysOnOpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
        }

        private void SaveAs()
        {
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Title = "Save soundboard config file";
            saveFileDialog1.DefaultExt = "sbcfg";
            saveFileDialog1.Filter = "Soundboard config files (*.sbcfg)|*.sbcfg";
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveFilePath = saveFileDialog1.FileName;
                Save(saveFilePath);
            }

        }

        private void Save(string file, bool export = false)
        {
            using (StreamWriter writer = new StreamWriter(file))
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    KeyPress key = (KeyPress)row.Cells[0].Value;
                    SoundClip clip = (SoundClip)row.Cells[1].Value;

                    string json;
                    if (export)
                    {
                        SoundClip clipClone = (SoundClip)clip.Clone();
                        clipClone.Filepath = Path.Combine(exportAudioPath, Path.GetFileName(clipClone.Filepath));
                        json = JsonConvert.SerializeObject(clipClone, Formatting.None);
                    }
                    else
                        json = JsonConvert.SerializeObject(clip, Formatting.None);

                    writer.WriteLine("[" + key.keycode.ToString() + "][" + key.ToString() + "]:" + json);
                }
            }
        }

        private void LoadFile()
        {
            try
            {
                foreach (string line in File.ReadLines(saveFilePath))
                {
                    int kyecode = Int32.Parse(line.Split("][")[0].Substring(1));
                    KeyPress key = new KeyPress(kyecode);

                    string clip_json = line.Split("]:")[1].Trim();
                    SoundClip clip = JsonConvert.DeserializeObject<SoundClip>(clip_json);

                    soundBindings.Add(key, clip);
                }

                using (StreamWriter writer = new StreamWriter(lastSaveFileCache))
                {
                    writer.Write(saveFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load that file, check the logs! " + ex.Message);
                Log.Write(ex);
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFilePath.Length == 0)
                SaveAs();
            else
                Save(saveFilePath);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFilePath.Length == 0)
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = @"C:\";
                openFileDialog1.Title = "Load soundboard config file";
                openFileDialog1.DefaultExt = "sbcfg";
                openFileDialog1.Filter = "Soundboard config files (*.sbcfg)|*.sbcfg";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    saveFilePath = openFileDialog1.FileName;
                    LoadFile();
                }
                return;
            }

            LoadFile();
        }

        private void cb_Play_CheckedChanged(object sender, EventArgs e)
        {
            cb_Play.ForeColor = cb_Play.Checked ? Color.DarkBlue : Color.DarkSlateGray;
        }


        private void CopyAllAudioFilesToExportDir(string path)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string clipSource = ((SoundClip)row.Cells[1].Value).Filepath;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string clipDest = Path.Combine(path, Path.GetFileName(clipSource));
                File.Copy(clipSource, clipDest, true);
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Title = "Export soundboard package";
            saveFileDialog1.DefaultExt = "zip";
            saveFileDialog1.Filter = "Soundboard package archive (*.zip)|*.zip";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string exportDir = new FileInfo(saveFileDialog1.FileName).Directory.FullName;
                string exportFile = saveFileDialog1.FileName;
                string packagePath = Path.Combine(exportDir, exportPackageName);

                try
                {
                    string packageAudioPath = Path.Combine(packagePath, audioDirName);
                    CopyAllAudioFilesToExportDir(packageAudioPath);

                    string packageConfig = Path.Combine(packagePath, exportConfigName);
                    Save(packageConfig, true);

                    ZipFile.CreateFromDirectory(packagePath, exportFile, CompressionLevel.Fastest, true);
                    MessageBox.Show("Export Complete!\nYou will find your file at: " + exportFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to copy all audio files to the package! Exported package might be unusable. Check the logs for more info about the error.\n" + ex.Message);
                    Log.Write(ex);
                }

                try
                {
                    if (Directory.Exists(packagePath))
                        Directory.Delete(packagePath, true);
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                }
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string packageExtractionPath = new FileInfo(Application.ExecutablePath).Directory.FullName;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Load soundboard package archive";
            openFileDialog1.DefaultExt = "zip";
            openFileDialog1.Filter = "Soundboard package archive (*.zip)|*.zip";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string packageArchive = openFileDialog1.FileName;
                ZipFile.ExtractToDirectory(packageArchive, packageExtractionPath, true);
                saveFilePath = Path.Combine(packageExtractionPath, exportPackageName, exportConfigName);
                LoadFile();
            }

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (soundBindings.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save before creating a new file?", "Save?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (saveFilePath.Length == 0)
                        SaveAs();
                    else
                        Save(saveFilePath);
                }
            }

            soundBindings.Clear();
            soundBindings = new DictionaryBindingList<KeyPress, SoundClip>();
            dataGridView1.DataSource = soundBindings;
            saveFilePath = "";
            splitContainer1.Panel2.Controls.Clear();
            lastPress_color = null;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(new AboutMe());
                splitContainer1.Panel2.Refresh();
            }
            catch (Exception exc)
            {
                MessageBox.Show("It seems there was an error opening the About me page\n...well...If that is the will of The Lake...so be it.");
                Log.Write(exc);
            }
        }
    }
}