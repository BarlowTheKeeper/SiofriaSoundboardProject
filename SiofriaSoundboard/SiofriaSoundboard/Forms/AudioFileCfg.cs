﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SiofriaSoundboard.AudioStuff;
using SiofriaSoundboard.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace SiofriaSoundboard
{
    public partial class AudioFileCfg : UserControl
    {
        public SoundClip clip;
        public static string lastBrowseLocation = "";

        public AudioFileCfg(SoundClip clip)
        {
            this.clip = clip;

            InitializeComponent();

            ApplyValuesFromSoundclip();
            groupBox2.AllowDrop = true;
        }

        public void ApplyValuesToSoundclip()
        {
            ApplyValuesToSoundclip(clip);
        }

        public void ApplyValuesToSoundclip(SoundClip soundClip)
        {
            soundClip.Volume = ((float)tracker_volume.Value) / 100.0f;
            soundClip.CutRangeEnabled = cb_cut_enabled.Checked;
            soundClip.FadeInEnabled = cb_fadein.Checked;
            soundClip.FadeOutEnabled = cb_fadeout.Checked;
            soundClip.Loop = cb_loop.Checked;
            soundClip.Stream = cb_stream.Checked;

            try
            {
                soundClip.CutRangeBegin = float.Parse(tb_start.Text);
                soundClip.CutRangeTake = float.Parse(tb_end.Text);
                soundClip.FadeInAmount = float.Parse(tb_fadein.Text);
                soundClip.FadeOutAmount = float.Parse(tb_fadeout.Text);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                MessageBox.Show("One of the textboxes does not contain valid numbers. Please Check again.");
            }
        }

        private void ApplyValuesFromSoundclip()
        {
            tracker_volume.Value = (int)(clip.Volume * 100);
            lb_file.Text = clip.Filepath;
            cb_cut_enabled.Checked = clip.CutRangeEnabled;
            cb_fadein.Checked = clip.FadeInEnabled;
            cb_fadeout.Checked = clip.FadeOutEnabled;
            cb_loop.Checked = clip.Loop;
            cb_stream.Checked = clip.Stream;
            tb_start.Text = clip.CutRangeBegin.ToString();
            tb_end.Text = clip.CutRangeTake.ToString();
            tb_fadein.Text = clip.FadeInAmount.ToString();
            tb_fadeout.Text = clip.FadeOutAmount.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_loop.Checked) //disable unsupported combinations of settings
            {
                clip.CutRangeEnabled = false;
                clip.FadeInEnabled = false;
                cb_cut_enabled.Enabled = false;
                cb_fadein.Enabled = false;
                tb_start.Enabled = false;
                tb_end.Enabled = false;
                tb_fadein.Enabled = false;
            }
            else
            {
                clip.CutRangeEnabled = cb_cut_enabled.Checked;
                clip.FadeInEnabled = cb_fadein.Checked;

                cb_cut_enabled.Enabled = true;
                cb_fadein.Enabled = true;
                tb_start.Enabled = true;
                tb_end.Enabled = true;
                tb_fadein.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ApplyValuesToSoundclip();
        }


        private void AudioFileCfg_Load(object sender, EventArgs e)
        {
            Show();
        }

        private void bt_browse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Audio File";
            openFileDialog1.Filter = "Audio files|*.mp3;*.wav;";

            string dirToOpen = @"C:\";
            if (lastBrowseLocation.Length != 0 && Directory.Exists(lastBrowseLocation))
                dirToOpen = lastBrowseLocation;

            openFileDialog1.InitialDirectory = dirToOpen;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                clip.Filepath = openFileDialog1.FileName;
                lb_file.Text = openFileDialog1.FileName;

                lastBrowseLocation = new FileInfo(openFileDialog1.FileName).Directory.FullName;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (clip.IsPlaying())
            { clip.Stop(); }
            else { clip.Play(); }
        }

        internal SoundClip GetSoundClip()
        {
            return clip;
        }

        private void groupBox2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {

                string[] droppedFilePaths = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                if (droppedFilePaths == null)
                    return;


                FileInfo info = new FileInfo(droppedFilePaths.First());

                if (!info.Extension.ToLower().Contains("mp3") && !info.Extension.ToLower().Contains("wav"))
                {
                    MessageBox.Show("Unsupported file format, please try .mp3 or .wav instead!");
                    return;
                }

                clip.Filepath = info.FullName;
                lb_file.Text = info.FullName;
                lastBrowseLocation = info.Directory.FullName;
            }
        }

        private void groupBox2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; // Okay
            else
                e.Effect = DragDropEffects.None; // Unknown data, ignore it
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
