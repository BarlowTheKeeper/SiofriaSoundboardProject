using SiofriaSoundboard.AudioStuff;
using SiofriaSoundboard.Input;
using SiofriaSoundboard.Network;
using SiofriaSoundboard.Packages;
using SiofriaSoundboard.Utils;
using System.ComponentModel;

namespace SiofriaSoundboard
{
    public partial class Form1 : Form
    {
        private InputManager inputManager;
        private KeyPress lastPress_color;
        private bool regKeyboardMessageShown = false;
        private PackageManager packageMgr;
        private string baseTitle = "";


        //Form1 stuff
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;

            try
            {
                inputManager = new InputManager("blocked_input.txt", fileCheckTimer);
                inputManager.OnInput += OnInput;

                UpdateChecker.CheckForNewVersionAsync();
                this.Text += " " + UpdateChecker.Tag;
                baseTitle = this.Text;

                packageMgr = new PackageManager();
                packageMgr.LoadLast();
                refreshDatagridAndPanels();

            }
            catch (Exception exc)
            {
                MessageBox.Show("Something went wrong while initializing inputManager. Check the logs.");
                Log.Write(exc);
                Application.Exit();
            }
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


        //Input Process comm and keepalive
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
            if (keycode == 8)
            {
                StopAll();
                return;
            }

            var soundBindings = packageMgr.Current.GetSoundBindings();
            if (!soundBindings.ContainsKey(key))
            {
                soundBindings.Add(key, new SoundClip());
                return;
            }

            if (cb_Play.Checked)
            {
                SoundClip clip = soundBindings.GetValue(key);
                if (clip.IsPlaying())
                    clip.Stop();

                else if (!(clip.Filepath.Length == 0))
                    clip.Play();
            }

            dataGridView1.ClearSelection();
            lastPress_color = key;
            ColorRowWithKey(key, dataGridView1.Rows[0].InheritedStyle.SelectionBackColor);
            OpenSoundSettings(key);

        }

        private void restartInputProcess()
        {
            if (!inputManager.InputProcessRunning())
            {
                status_strip.Text = "No Input Process!";
                status_strip.ForeColor = Color.Red;
                regKeyboardMessageShown = false;

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

            if (!regKeyboardMessageShown)
            {
                regKeyboardMessageShown = true;
                status_strip.Text = "Press Enter to register the keyboard!";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                restartInputProcess();
            }
            catch (Exception exc)
            {
                Log.Write(exc);
                MessageBox.Show("Something went wrong! The problem might resolve itself, but I advise restarting the application.");
            }
        }


        //Gridview stuff
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete)
                return;

            SoundClip clip = (SoundClip)dataGridView1.SelectedRows[0].Cells[1].Value;

            DialogResult r = MessageBox.Show("Deleting " + clip, "Are you sure?", MessageBoxButtons.YesNo);
            if (r == DialogResult.No)
                return;

            getCurrentSettingWindow().Dispose();
            KeyPress key = (KeyPress)dataGridView1.SelectedRows[0].Cells[0].Value;
            clip.Dispose();

            packageMgr.Current.GetSoundBindings().Remove(key);
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

        private void refreshDatagridAndPanels()
        {
            dataGridView1.DataSource = packageMgr.Current.GetSoundBindings();
            splitContainer1.Panel2.Controls.Clear();
            lastPress_color = null;
            this.Text = baseTitle + " - " + packageMgr.GetCurrentPackageName();
        }

        //Color Gridview Elements
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

        private void timercolor_Tick(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                Color c = dataGridView1.Rows[0].InheritedStyle.ForeColor;
                if (((SoundClip)row.Cells[1].Value).IsPlaying())
                    c = Color.Red;

                row.Cells[0].Style.ForeColor = c;
                row.Cells[1].Style.ForeColor = c;
            }
        }


        //Audio Controls Play/Stop
        private void cb_Play_CheckedChanged(object sender, EventArgs e)
        {
            cb_Play.ForeColor = cb_Play.Checked ? Color.DarkBlue : Color.DarkSlateGray;
        }

        private void StopAll()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                SoundClip clip = (SoundClip)row.Cells[1].Value;
                if (clip.IsPlaying())
                    clip.Stop();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StopAll();
        }


        //Sound Settings panel stuff
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

        private void OpenSoundSettings(KeyPress key)
        {
            try
            {

                SoundClip selected = packageMgr.Current.GetSoundBindings().GetValue(key);

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


        //Status strip
        private void status_strip_Click(object sender, EventArgs e)
        {
            if (inputManager.InputProcessRunning())
            {
                inputManager.StopAllInputProcesses();
                return;
            }

            keepAliveTimer.Start();
        }


        //Strip Clicks
        private void managePackagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open Form with packages
            //Open/Delete
        }

        private void importToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            packageMgr.Import();
            refreshDatagridAndPanels();
        }

        private void exportToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            packageMgr.Export();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            packageMgr.SaveAs();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            packageMgr.NewPackage();
            refreshDatagridAndPanels();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            packageMgr.Save();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            packageMgr.Load();
            refreshDatagridAndPanels();
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

        private void alwaysOnOpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
        }


    }
}