namespace RatchetEdit
{
    partial class Main
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.mapOpenBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.mapSaveBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.mapSaveAsBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewToolStipItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mobyCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.tieCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.shrubCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.collCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.terrainCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.splineCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.skyboxCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.cuboidCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.type0CCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.WindowToolStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelViewerToolBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.spriteViewerToolBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.UISpriteToolBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.levelVariablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.tickTimer = new System.Windows.Forms.Timer(this.components);
            this.mapSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.ViewToolStipItem,
            this.WindowToolStripItem,
            this.toolStripMenuItem3});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1269, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "File";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.mapOpenBtn,
            this.mapSaveBtn,
            this.mapSaveAsBtn,
            this.toolStripSeparator1,
            this.exitToolBtn});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "&File";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Enabled = false;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem4.Text = "New";
            // 
            // mapOpenBtn
            // 
            this.mapOpenBtn.Name = "mapOpenBtn";
            this.mapOpenBtn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mapOpenBtn.Size = new System.Drawing.Size(180, 22);
            this.mapOpenBtn.Text = "Open...";
            this.mapOpenBtn.Click += new System.EventHandler(this.mapOpenBtn_Click);
            // 
            // mapSaveBtn
            // 
            this.mapSaveBtn.Enabled = false;
            this.mapSaveBtn.Name = "mapSaveBtn";
            this.mapSaveBtn.Size = new System.Drawing.Size(180, 22);
            this.mapSaveBtn.Text = "Save";
            // 
            // mapSaveAsBtn
            // 
            this.mapSaveAsBtn.Enabled = false;
            this.mapSaveAsBtn.Name = "mapSaveAsBtn";
            this.mapSaveAsBtn.Size = new System.Drawing.Size(180, 22);
            this.mapSaveAsBtn.Text = "Save as...";
            this.mapSaveAsBtn.Click += new System.EventHandler(this.mapSaveAsBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // exitToolBtn
            // 
            this.exitToolBtn.Name = "exitToolBtn";
            this.exitToolBtn.Size = new System.Drawing.Size(180, 22);
            this.exitToolBtn.Text = "Exit";
            this.exitToolBtn.Click += new System.EventHandler(this.exitToolBtn_Click);
            // 
            // ViewToolStipItem
            // 
            this.ViewToolStipItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mobyCheck,
            this.tieCheck,
            this.shrubCheck,
            this.collCheck,
            this.terrainCheck,
            this.splineCheck,
            this.skyboxCheck,
            this.cuboidCheck,
            this.type0CCheck});
            this.ViewToolStipItem.Name = "ViewToolStipItem";
            this.ViewToolStipItem.Size = new System.Drawing.Size(44, 20);
            this.ViewToolStipItem.Text = "View";
            // 
            // mobyCheck
            // 
            this.mobyCheck.Checked = true;
            this.mobyCheck.CheckOnClick = true;
            this.mobyCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mobyCheck.Enabled = false;
            this.mobyCheck.Name = "mobyCheck";
            this.mobyCheck.Size = new System.Drawing.Size(120, 22);
            this.mobyCheck.Text = "Mobys";
            this.mobyCheck.CheckedChanged += new System.EventHandler(this.mobyCheck_CheckedChanged);
            // 
            // tieCheck
            // 
            this.tieCheck.Checked = true;
            this.tieCheck.CheckOnClick = true;
            this.tieCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tieCheck.Enabled = false;
            this.tieCheck.Name = "tieCheck";
            this.tieCheck.Size = new System.Drawing.Size(120, 22);
            this.tieCheck.Text = "Ties";
            this.tieCheck.CheckedChanged += new System.EventHandler(this.tieCheck_CheckedChanged);
            // 
            // shrubCheck
            // 
            this.shrubCheck.Checked = true;
            this.shrubCheck.CheckOnClick = true;
            this.shrubCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.shrubCheck.Enabled = false;
            this.shrubCheck.Name = "shrubCheck";
            this.shrubCheck.Size = new System.Drawing.Size(120, 22);
            this.shrubCheck.Text = "Shrubs";
            this.shrubCheck.CheckedChanged += new System.EventHandler(this.shrubCheck_CheckedChanged);
            // 
            // collCheck
            // 
            this.collCheck.CheckOnClick = true;
            this.collCheck.Enabled = false;
            this.collCheck.Name = "collCheck";
            this.collCheck.Size = new System.Drawing.Size(120, 22);
            this.collCheck.Text = "Collision";
            this.collCheck.CheckedChanged += new System.EventHandler(this.collCheck_CheckedChanged);
            // 
            // terrainCheck
            // 
            this.terrainCheck.Checked = true;
            this.terrainCheck.CheckOnClick = true;
            this.terrainCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.terrainCheck.Enabled = false;
            this.terrainCheck.Name = "terrainCheck";
            this.terrainCheck.Size = new System.Drawing.Size(120, 22);
            this.terrainCheck.Text = "Terrain";
            this.terrainCheck.CheckedChanged += new System.EventHandler(this.terrainCheck_CheckedChanged);
            // 
            // splineCheck
            // 
            this.splineCheck.CheckOnClick = true;
            this.splineCheck.Enabled = false;
            this.splineCheck.Name = "splineCheck";
            this.splineCheck.Size = new System.Drawing.Size(120, 22);
            this.splineCheck.Text = "Splines";
            this.splineCheck.CheckedChanged += new System.EventHandler(this.splineCheck_CheckedChanged);
            // 
            // skyboxCheck
            // 
            this.skyboxCheck.CheckOnClick = true;
            this.skyboxCheck.Enabled = false;
            this.skyboxCheck.Name = "skyboxCheck";
            this.skyboxCheck.Size = new System.Drawing.Size(120, 22);
            this.skyboxCheck.Text = "Skybox";
            this.skyboxCheck.CheckedChanged += new System.EventHandler(this.skyboxCheck_CheckedChanged);
            // 
            // cuboidCheck
            // 
            this.cuboidCheck.CheckOnClick = true;
            this.cuboidCheck.Enabled = false;
            this.cuboidCheck.Name = "cuboidCheck";
            this.cuboidCheck.Size = new System.Drawing.Size(120, 22);
            this.cuboidCheck.Text = "Cuboids";
            this.cuboidCheck.CheckedChanged += new System.EventHandler(this.cuboidCheck_CheckedChanged);
            // 
            // type0CCheck
            // 
            this.type0CCheck.CheckOnClick = true;
            this.type0CCheck.Enabled = false;
            this.type0CCheck.Name = "type0CCheck";
            this.type0CCheck.Size = new System.Drawing.Size(120, 22);
            this.type0CCheck.Text = "Type0Cs";
            this.type0CCheck.CheckedChanged += new System.EventHandler(this.type0CCheck_CheckedChanged);
            // 
            // WindowToolStripItem
            // 
            this.WindowToolStripItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelViewerToolBtn,
            this.toolStripMenuItem10,
            this.toolStripMenuItem11,
            this.spriteViewerToolBtn,
            this.UISpriteToolBtn,
            this.toolStripMenuItem14,
            this.levelVariablesToolStripMenuItem,
            this.languageDataToolStripMenuItem});
            this.WindowToolStripItem.Name = "WindowToolStripItem";
            this.WindowToolStripItem.Size = new System.Drawing.Size(63, 20);
            this.WindowToolStripItem.Text = "Window";
            // 
            // modelViewerToolBtn
            // 
            this.modelViewerToolBtn.Enabled = false;
            this.modelViewerToolBtn.Name = "modelViewerToolBtn";
            this.modelViewerToolBtn.Size = new System.Drawing.Size(174, 22);
            this.modelViewerToolBtn.Text = "Model Viewer";
            this.modelViewerToolBtn.Click += new System.EventHandler(this.modelViewerToolBtn_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Enabled = false;
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(174, 22);
            this.toolStripMenuItem10.Text = "Level object viewer";
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Enabled = false;
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(174, 22);
            this.toolStripMenuItem11.Text = "Textures";
            this.toolStripMenuItem11.Click += new System.EventHandler(this.toolStripMenuItem11_Click);
            // 
            // spriteViewerToolBtn
            // 
            this.spriteViewerToolBtn.Enabled = false;
            this.spriteViewerToolBtn.Name = "spriteViewerToolBtn";
            this.spriteViewerToolBtn.Size = new System.Drawing.Size(174, 22);
            this.spriteViewerToolBtn.Text = "Sprites";
            this.spriteViewerToolBtn.Click += new System.EventHandler(this.spriteViewerToolBtn_Click);
            // 
            // UISpriteToolBtn
            // 
            this.UISpriteToolBtn.Enabled = false;
            this.UISpriteToolBtn.Name = "UISpriteToolBtn";
            this.UISpriteToolBtn.Size = new System.Drawing.Size(174, 22);
            this.UISpriteToolBtn.Text = "UI Sprites";
            this.UISpriteToolBtn.Click += new System.EventHandler(this.UISpriteToolBtn_Click);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Enabled = false;
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(174, 22);
            this.toolStripMenuItem14.Text = "Console";
            // 
            // levelVariablesToolStripMenuItem
            // 
            this.levelVariablesToolStripMenuItem.Enabled = false;
            this.levelVariablesToolStripMenuItem.Name = "levelVariablesToolStripMenuItem";
            this.levelVariablesToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.levelVariablesToolStripMenuItem.Text = "Level Variables";
            // 
            // languageDataToolStripMenuItem
            // 
            this.languageDataToolStripMenuItem.Enabled = false;
            this.languageDataToolStripMenuItem.Name = "languageDataToolStripMenuItem";
            this.languageDataToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.languageDataToolStripMenuItem.Text = "Language Data";
            this.languageDataToolStripMenuItem.Click += new System.EventHandler(this.languageDataToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(80, 20);
            this.toolStripMenuItem3.Text = "Preferences";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // mapOpenDialog
            // 
            this.mapOpenDialog.Filter = "Engine file|engine.ps3";
            // 
            // tickTimer
            // 
            this.tickTimer.Enabled = true;
            this.tickTimer.Interval = 1;
            this.tickTimer.Tick += new System.EventHandler(this.tickTimer_Tick);
            // 
            // mapSaveDialog
            // 
            this.mapSaveDialog.FileName = "gameplay_ntsc";
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(12, 4);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1269, 657);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseUp);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 681);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Replanetizer";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem WindowToolStripItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mapOpenBtn;
        private System.Windows.Forms.ToolStripMenuItem mapSaveBtn;
        private System.Windows.Forms.ToolStripMenuItem mapSaveAsBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolBtn;
        private System.Windows.Forms.ToolStripMenuItem modelViewerToolBtn;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem spriteViewerToolBtn;
        private System.Windows.Forms.ToolStripMenuItem UISpriteToolBtn;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog mapOpenDialog;
        private System.Windows.Forms.Timer tickTimer;
        private System.Windows.Forms.ToolStripMenuItem ViewToolStipItem;
        private System.Windows.Forms.ToolStripMenuItem mobyCheck;
        private System.Windows.Forms.ToolStripMenuItem tieCheck;
        private System.Windows.Forms.ToolStripMenuItem shrubCheck;
        private System.Windows.Forms.ToolStripMenuItem collCheck;
        private System.Windows.Forms.ToolStripMenuItem terrainCheck;
        private System.Windows.Forms.ToolStripMenuItem splineCheck;
        private System.Windows.Forms.ToolStripMenuItem levelVariablesToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog mapSaveDialog;
        private System.Windows.Forms.ToolStripMenuItem skyboxCheck;
        private System.Windows.Forms.ToolStripMenuItem cuboidCheck;
        private System.Windows.Forms.ToolStripMenuItem type0CCheck;
        private System.Windows.Forms.ToolStripMenuItem languageDataToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
    }
}

