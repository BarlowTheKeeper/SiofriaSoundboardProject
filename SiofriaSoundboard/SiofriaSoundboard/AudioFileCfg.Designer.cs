namespace SiofriaSoundboard
{
    partial class AudioFileCfg
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cb_loop = new CheckBox();
            tracker_volume = new TrackBar();
            cb_fadeout = new CheckBox();
            cb_fadein = new CheckBox();
            bt_browse = new Button();
            groupBox1 = new GroupBox();
            label4 = new Label();
            label3 = new Label();
            tb_fadeout = new TextBox();
            tb_fadein = new TextBox();
            gr_range = new GroupBox();
            label2 = new Label();
            label6 = new Label();
            cb_cut_enabled = new CheckBox();
            tb_end = new TextBox();
            tb_start = new TextBox();
            lb_file = new Label();
            openFileDialog1 = new OpenFileDialog();
            bt_preview = new Button();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            button1 = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)tracker_volume).BeginInit();
            groupBox1.SuspendLayout();
            gr_range.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // cb_loop
            // 
            cb_loop.Anchor = AnchorStyles.None;
            cb_loop.AutoSize = true;
            cb_loop.Location = new Point(226, 42);
            cb_loop.Name = "cb_loop";
            cb_loop.Size = new Size(53, 19);
            cb_loop.TabIndex = 0;
            cb_loop.Text = "Loop";
            cb_loop.UseVisualStyleBackColor = true;
            cb_loop.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // tracker_volume
            // 
            tracker_volume.Location = new Point(6, 24);
            tracker_volume.Maximum = 100;
            tracker_volume.Name = "tracker_volume";
            tracker_volume.Size = new Size(450, 45);
            tracker_volume.TabIndex = 1;
            tracker_volume.Value = 100;
            tracker_volume.Scroll += tracker_volume_Scroll;
            // 
            // cb_fadeout
            // 
            cb_fadeout.AutoSize = true;
            cb_fadeout.Location = new Point(18, 62);
            cb_fadeout.Name = "cb_fadeout";
            cb_fadeout.Size = new Size(74, 19);
            cb_fadeout.TabIndex = 2;
            cb_fadeout.Text = "Fade Out";
            cb_fadeout.UseVisualStyleBackColor = true;
            cb_fadeout.CheckedChanged += cb_fadeout_CheckedChanged;
            // 
            // cb_fadein
            // 
            cb_fadein.AutoSize = true;
            cb_fadein.Location = new Point(18, 28);
            cb_fadein.Name = "cb_fadein";
            cb_fadein.Size = new Size(64, 19);
            cb_fadein.TabIndex = 3;
            cb_fadein.Text = "Fade In";
            cb_fadein.UseVisualStyleBackColor = true;
            cb_fadein.CheckedChanged += cb_fadein_CheckedChanged;
            // 
            // bt_browse
            // 
            bt_browse.Location = new Point(373, 21);
            bt_browse.Name = "bt_browse";
            bt_browse.Size = new Size(75, 23);
            bt_browse.TabIndex = 5;
            bt_browse.Text = "Browse";
            bt_browse.UseVisualStyleBackColor = true;
            bt_browse.Click += bt_browse_Click;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.None;
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(tb_fadeout);
            groupBox1.Controls.Add(tb_fadein);
            groupBox1.Controls.Add(cb_fadeout);
            groupBox1.Controls.Add(cb_fadein);
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(173, 97);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Fade";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(144, 64);
            label4.Name = "label4";
            label4.Size = new Size(13, 15);
            label4.TabIndex = 14;
            label4.Text = "S";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(144, 31);
            label3.Name = "label3";
            label3.Size = new Size(13, 15);
            label3.TabIndex = 12;
            label3.Text = "S";
            // 
            // tb_fadeout
            // 
            tb_fadeout.Location = new Point(110, 59);
            tb_fadeout.MaxLength = 8;
            tb_fadeout.Name = "tb_fadeout";
            tb_fadeout.PlaceholderText = "100";
            tb_fadeout.Size = new Size(32, 23);
            tb_fadeout.TabIndex = 13;
            tb_fadeout.TextChanged += tb_fadeout_TextChanged;
            // 
            // tb_fadein
            // 
            tb_fadein.Location = new Point(110, 26);
            tb_fadein.MaxLength = 8;
            tb_fadein.Name = "tb_fadein";
            tb_fadein.PlaceholderText = "100";
            tb_fadein.Size = new Size(32, 23);
            tb_fadein.TabIndex = 10;
            tb_fadein.TextChanged += tb_fadein_TextChanged;
            // 
            // gr_range
            // 
            gr_range.Anchor = AnchorStyles.None;
            gr_range.Controls.Add(label2);
            gr_range.Controls.Add(label6);
            gr_range.Controls.Add(cb_cut_enabled);
            gr_range.Controls.Add(tb_end);
            gr_range.Controls.Add(tb_start);
            gr_range.Location = new Point(329, 3);
            gr_range.Name = "gr_range";
            gr_range.Size = new Size(195, 97);
            gr_range.TabIndex = 7;
            gr_range.TabStop = false;
            gr_range.Text = "Cut range";
            gr_range.Enter += groupBox2_Enter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(81, 68);
            label2.Name = "label2";
            label2.Size = new Size(13, 15);
            label2.TabIndex = 17;
            label2.Text = "S";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(161, 68);
            label6.Name = "label6";
            label6.Size = new Size(13, 15);
            label6.TabIndex = 16;
            label6.Text = "S";
            // 
            // cb_cut_enabled
            // 
            cb_cut_enabled.AutoSize = true;
            cb_cut_enabled.Location = new Point(63, 28);
            cb_cut_enabled.Name = "cb_cut_enabled";
            cb_cut_enabled.Size = new Size(61, 19);
            cb_cut_enabled.TabIndex = 2;
            cb_cut_enabled.Text = "Enable";
            cb_cut_enabled.UseVisualStyleBackColor = true;
            cb_cut_enabled.CheckedChanged += cb_cut_enabled_CheckedChanged;
            // 
            // tb_end
            // 
            tb_end.Location = new Point(112, 63);
            tb_end.MaxLength = 8;
            tb_end.Name = "tb_end";
            tb_end.PlaceholderText = "Take";
            tb_end.Size = new Size(48, 23);
            tb_end.TabIndex = 1;
            tb_end.TextChanged += tb_end_TextChanged;
            // 
            // tb_start
            // 
            tb_start.Location = new Point(30, 63);
            tb_start.MaxLength = 8;
            tb_start.Name = "tb_start";
            tb_start.PlaceholderText = "Skip";
            tb_start.Size = new Size(48, 23);
            tb_start.TabIndex = 0;
            tb_start.TextChanged += tb_start_TextChanged;
            // 
            // lb_file
            // 
            lb_file.AutoSize = true;
            lb_file.Location = new Point(18, 26);
            lb_file.Name = "lb_file";
            lb_file.Size = new Size(0, 15);
            lb_file.TabIndex = 8;
            // 
            // bt_preview
            // 
            bt_preview.Anchor = AnchorStyles.None;
            bt_preview.Location = new Point(85, 19);
            bt_preview.Name = "bt_preview";
            bt_preview.Size = new Size(92, 42);
            bt_preview.TabIndex = 0;
            bt_preview.Text = "Apply";
            bt_preview.UseVisualStyleBackColor = true;
            bt_preview.Click += button1_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.None;
            groupBox2.Controls.Add(bt_browse);
            groupBox2.Controls.Add(lb_file);
            groupBox2.Location = new Point(44, 137);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(462, 57);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "File";
            groupBox2.DragDrop += groupBox2_DragDrop;
            groupBox2.DragEnter += groupBox2_DragEnter;
            groupBox2.Enter += groupBox2_Enter_1;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.None;
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(tracker_volume);
            groupBox3.Location = new Point(44, 233);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(462, 77);
            groupBox3.TabIndex = 10;
            groupBox3.TabStop = false;
            groupBox3.Text = "Volume";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 26);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 8;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(groupBox2, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 3);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(groupBox3, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 92F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 86F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(550, 419);
            tableLayoutPanel1.TabIndex = 11;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint_1;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.Anchor = AnchorStyles.None;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(button1, 1, 0);
            tableLayoutPanel3.Controls.Add(bt_preview, 0, 0);
            tableLayoutPanel3.Location = new Point(11, 335);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(527, 81);
            tableLayoutPanel3.TabIndex = 12;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.None;
            button1.Location = new Point(349, 19);
            button1.Name = "button1";
            button1.Size = new Size(92, 42);
            button1.TabIndex = 1;
            button1.Text = "Preview";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 54.9079742F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45.0920258F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            tableLayoutPanel2.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel2.Controls.Add(cb_loop, 1, 0);
            tableLayoutPanel2.Controls.Add(gr_range, 2, 0);
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(527, 103);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // AudioFileCfg
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            Controls.Add(tableLayoutPanel1);
            Name = "AudioFileCfg";
            Size = new Size(550, 419);
            Load += AudioFileCfg_Load;
            ((System.ComponentModel.ISupportInitialize)tracker_volume).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            gr_range.ResumeLayout(false);
            gr_range.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private CheckBox cb_loop;
        private TrackBar tracker_volume;
        private CheckBox cb_fadeout;
        private CheckBox cb_fadein;
        private Button bt_browse;
        private GroupBox groupBox1;
        private GroupBox gr_range;
        private Button bt_preview;
        private Label lb_file;
        private OpenFileDialog openFileDialog1;
        private CheckBox cb_cut_enabled;
        private TextBox tb_start;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label1;
        private Label label4;
        private Label label3;
        private TextBox tb_fadeout;
        private TextBox tb_fadein;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label6;
        private TableLayoutPanel tableLayoutPanel3;
        private Button button1;
        private TextBox tb_end;
        private Label label2;
    }
}
