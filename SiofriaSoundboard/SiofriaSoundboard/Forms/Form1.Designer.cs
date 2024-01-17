namespace SiofriaSoundboard
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            splitContainer1 = new SplitContainer();
            dataGridView1 = new DataGridView();
            statusStrip1 = new StatusStrip();
            status_strip = new ToolStripStatusLabel();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            managePackagesToolStripMenuItem = new ToolStripMenuItem();
            importToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            manageToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            alwaysOnOpToolStripMenuItem = new ToolStripMenuItem();
            clearCacheToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            showAboutPageToolStripMenuItem = new ToolStripMenuItem();
            checkForUpdatesToolStripMenuItem = new ToolStripMenuItem();
            keepAliveTimer = new System.Windows.Forms.Timer(components);
            fileCheckTimer = new System.Windows.Forms.Timer(components);
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            button1 = new Button();
            cb_Play = new CheckBox();
            saveFileDialog1 = new SaveFileDialog();
            timercolor = new System.Windows.Forms.Timer(components);
            toolTip1 = new ToolTip(components);
            applyToOtherSoundsToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(3, 28);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dataGridView1);
            splitContainer1.Size = new Size(793, 417);
            splitContainer1.SplitterDistance = 236;
            splitContainer1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.RowTemplate.ReadOnly = true;
            dataGridView1.RowTemplate.Resizable = DataGridViewTriState.True;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(236, 417);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellContentClick;
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { status_strip });
            statusStrip1.Location = new Point(0, 448);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(799, 21);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // status_strip
            // 
            status_strip.ActiveLinkColor = Color.Yellow;
            status_strip.Font = new Font("Consolas", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            status_strip.ForeColor = Color.Blue;
            status_strip.Name = "status_strip";
            status_strip.Size = new Size(104, 16);
            status_strip.Text = "Initializing";
            status_strip.Click += status_strip_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ButtonFace;
            menuStrip1.Dock = DockStyle.Fill;
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, viewToolStripMenuItem, toolStripMenuItem1 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(148, 25);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, loadToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, managePackagesToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 21);
            fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(186, 22);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.L;
            loadToolStripMenuItem.Size = new Size(186, 22);
            loadToolStripMenuItem.Text = "Load";
            loadToolStripMenuItem.Click += loadToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.Size = new Size(186, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            saveAsToolStripMenuItem.Size = new Size(186, 22);
            saveAsToolStripMenuItem.Text = "Save As";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // managePackagesToolStripMenuItem
            // 
            managePackagesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { importToolStripMenuItem, exportToolStripMenuItem, manageToolStripMenuItem });
            managePackagesToolStripMenuItem.Name = "managePackagesToolStripMenuItem";
            managePackagesToolStripMenuItem.Size = new Size(186, 22);
            managePackagesToolStripMenuItem.Text = "Packages";
            // 
            // importToolStripMenuItem
            // 
            importToolStripMenuItem.Name = "importToolStripMenuItem";
            importToolStripMenuItem.Size = new Size(117, 22);
            importToolStripMenuItem.Text = "Import";
            importToolStripMenuItem.Click += importToolStripMenuItem_Click_1;
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new Size(117, 22);
            exportToolStripMenuItem.Text = "Export";
            exportToolStripMenuItem.Click += exportToolStripMenuItem_Click_1;
            // 
            // manageToolStripMenuItem
            // 
            manageToolStripMenuItem.Name = "manageToolStripMenuItem";
            manageToolStripMenuItem.Size = new Size(117, 22);
            manageToolStripMenuItem.Text = "Manage";
            manageToolStripMenuItem.Click += manageToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { alwaysOnOpToolStripMenuItem, clearCacheToolStripMenuItem, applyToOtherSoundsToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(46, 21);
            viewToolStripMenuItem.Text = "Tools";
            // 
            // alwaysOnOpToolStripMenuItem
            // 
            alwaysOnOpToolStripMenuItem.Name = "alwaysOnOpToolStripMenuItem";
            alwaysOnOpToolStripMenuItem.Size = new Size(191, 22);
            alwaysOnOpToolStripMenuItem.Text = "Always On Top";
            alwaysOnOpToolStripMenuItem.Click += alwaysOnOpToolStripMenuItem_Click;
            // 
            // clearCacheToolStripMenuItem
            // 
            clearCacheToolStripMenuItem.Name = "clearCacheToolStripMenuItem";
            clearCacheToolStripMenuItem.Size = new Size(191, 22);
            clearCacheToolStripMenuItem.Text = "Clear Cache";
            clearCacheToolStripMenuItem.Click += clearCacheToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { showAboutPageToolStripMenuItem, checkForUpdatesToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(52, 21);
            toolStripMenuItem1.Text = "About";
            // 
            // showAboutPageToolStripMenuItem
            // 
            showAboutPageToolStripMenuItem.Name = "showAboutPageToolStripMenuItem";
            showAboutPageToolStripMenuItem.Size = new Size(173, 22);
            showAboutPageToolStripMenuItem.Text = "Show About Page";
            showAboutPageToolStripMenuItem.Click += showAboutPageToolStripMenuItem_Click;
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            checkForUpdatesToolStripMenuItem.Size = new Size(173, 22);
            checkForUpdatesToolStripMenuItem.Text = "Check For Updates";
            checkForUpdatesToolStripMenuItem.Click += checkForUpdatesToolStripMenuItem_Click;
            // 
            // keepAliveTimer
            // 
            keepAliveTimer.Enabled = true;
            keepAliveTimer.Interval = 3000;
            keepAliveTimer.Tick += timer1_Tick;
            // 
            // fileCheckTimer
            // 
            fileCheckTimer.Enabled = true;
            fileCheckTimer.Interval = 200;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(splitContainer1, 0, 1);
            tableLayoutPanel1.Controls.Add(statusStrip1, 0, 2);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.33049059F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90.1918945F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4.44642162F));
            tableLayoutPanel1.Size = new Size(799, 469);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62.447258F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 37.552742F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 486F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75F));
            tableLayoutPanel2.Controls.Add(button1, 3, 0);
            tableLayoutPanel2.Controls.Add(menuStrip1, 0, 0);
            tableLayoutPanel2.Controls.Add(cb_Play, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(799, 25);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Left;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button1.ForeColor = Color.Firebrick;
            button1.Location = new Point(723, 0);
            button1.Margin = new Padding(0);
            button1.Name = "button1";
            button1.Size = new Size(70, 25);
            button1.TabIndex = 3;
            button1.Text = "STOP!";
            toolTip1.SetToolTip(button1, "Stop all playback [backspace on soundboard]");
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // cb_Play
            // 
            cb_Play.Appearance = Appearance.Button;
            cb_Play.AutoSize = true;
            cb_Play.FlatStyle = FlatStyle.Popup;
            cb_Play.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            cb_Play.ForeColor = Color.FromArgb(64, 64, 64);
            cb_Play.Location = new Point(148, 0);
            cb_Play.Margin = new Padding(0);
            cb_Play.Name = "cb_Play";
            cb_Play.Size = new Size(48, 25);
            cb_Play.TabIndex = 2;
            cb_Play.Text = "Play!";
            toolTip1.SetToolTip(cb_Play, "Toggle to enable soundboard mode");
            cb_Play.UseVisualStyleBackColor = true;
            cb_Play.CheckedChanged += cb_Play_CheckedChanged;
            // 
            // timercolor
            // 
            timercolor.Enabled = true;
            timercolor.Tick += timercolor_Tick;
            // 
            // applyToOtherSoundsToolStripMenuItem
            // 
            applyToOtherSoundsToolStripMenuItem.Name = "applyToOtherSoundsToolStripMenuItem";
            applyToOtherSoundsToolStripMenuItem.Size = new Size(191, 22);
            applyToOtherSoundsToolStripMenuItem.Text = "Apply to other sounds";
            applyToOtherSoundsToolStripMenuItem.Click += applyToOtherSoundsToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(799, 469);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "Form1";
            ShowIcon = false;
            Text = "Siofria Soundboard";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private DataGridView dataGridView1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel status_strip;
        private System.Windows.Forms.Timer keepAliveTimer;
        private System.Windows.Forms.Timer fileCheckTimer;
        private TableLayoutPanel tableLayoutPanel1;
        private ToolStripMenuItem alwaysOnOpToolStripMenuItem;
        private SaveFileDialog saveFileDialog1;
        private TableLayoutPanel tableLayoutPanel2;
        private CheckBox cb_Play;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Timer timercolor;
        private Button button1;
        private ToolTip toolTip1;
        private ToolStripMenuItem managePackagesToolStripMenuItem;
        private ToolStripMenuItem manageToolStripMenuItem;
        private ToolStripMenuItem importToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem clearCacheToolStripMenuItem;
        private ToolStripMenuItem showAboutPageToolStripMenuItem;
        private ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private ToolStripMenuItem applyToOtherSoundsToolStripMenuItem;
    }
}