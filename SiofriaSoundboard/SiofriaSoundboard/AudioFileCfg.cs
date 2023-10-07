﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiofriaSoundboard
{
    public partial class AudioFileCfg : UserControl
    {
        public SoundClip clip;

        public AudioFileCfg(SoundClip clip)
        {
            this.clip = clip;
            InitializeComponent();

            ApplyValuesFromSoundclip();
        }

        public void ApplyValuesToSoundclip()
        {
            clip.Volume = ((float)tracker_volume.Value) / 100.0f;
            clip.CutRangeEnabled = cb_cut_enabled.Checked;
            clip.FadeInEnabled = cb_fadein.Checked;
            clip.FadeOutEnabled = cb_fadeout.Checked;
            clip.Loop = cb_loop.Checked;

            try
            {
                clip.CutRangeBegin = float.Parse(tb_start.Text);
                clip.CutRangeTake = float.Parse(tb_end.Text);
                clip.FadeInAmount = float.Parse(tb_fadein.Text);
                clip.FadeOutAmount = float.Parse(tb_fadeout.Text);
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
            lb_file.Text = clip.ToString();
            cb_cut_enabled.Checked = clip.CutRangeEnabled;
            cb_fadein.Checked = clip.FadeInEnabled;
            cb_fadeout.Checked = clip.FadeOutEnabled;
            cb_loop.Checked = clip.Loop;

            tb_start.Text = clip.CutRangeBegin.ToString();
            tb_end.Text = clip.CutRangeTake.ToString();
            tb_fadein.Text = clip.FadeInAmount.ToString();
            tb_fadeout.Text = clip.FadeOutAmount.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ApplyValuesToSoundclip();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AudioFileCfg_Load(object sender, EventArgs e)
        {
            Show();
        }

        private void cb_fadein_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cb_fadeout_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tb_fadein_TextChanged(object sender, EventArgs e)
        {

        }

        private void tb_fadeout_TextChanged(object sender, EventArgs e)
        {

        }

        private void cb_cut_enabled_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tb_start_TextChanged(object sender, EventArgs e)
        {

        }

        private void tb_end_TextChanged(object sender, EventArgs e)
        {

        }

        private void tracker_volume_Scroll(object sender, EventArgs e)
        {

        }

        private void bt_browse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Audio File";
            openFileDialog1.Filter = "Audio files|*.mp3;*.wav;";
            openFileDialog1.InitialDirectory = @"C:\";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                clip.Filepath = openFileDialog1.FileName;
                lb_file.Text = openFileDialog1.FileName;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(clip.IsPlaying())
            { clip.Stop(); }
            else { clip.Play(); }
        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        internal SoundClip GetSoundClip()
        {
            return clip;
        }
    }
}
