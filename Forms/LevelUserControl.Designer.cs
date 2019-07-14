namespace RatchetEdit
{
    partial class LevelUserControl
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.glControl = new RatchetEdit.CustomGLControl();
            this.toolstrip1 = new System.Windows.Forms.ToolStrip();
            this.cloneBtn = new System.Windows.Forms.ToolStripButton();
            this.deleteBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.translateToolBtn = new System.Windows.Forms.ToolStripButton();
            this.rotateToolBtn = new System.Windows.Forms.ToolStripButton();
            this.scaleToolBtn = new System.Windows.Forms.ToolStripButton();
            this.splineToolBtn = new System.Windows.Forms.ToolStripButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.objectTree = new RatchetEdit.ObjectTreeView();
            this.properties = new System.Windows.Forms.PropertyGrid();
            this.camYLabel = new System.Windows.Forms.Label();
            this.yawLabel = new System.Windows.Forms.Label();
            this.camXLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.camZLabel = new System.Windows.Forms.Label();
            this.pitchLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolstrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.glControl);
            this.splitContainer1.Panel1.Controls.Add(this.toolstrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1269, 681);
            this.splitContainer1.SplitterDistance = 1000;
            this.splitContainer1.TabIndex = 17;
            // 
            // glControl
            // 
            this.glControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glControl.BackColor = System.Drawing.Color.Black;
            this.glControl.Location = new System.Drawing.Point(3, 28);
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size(994, 653);
            this.glControl.TabIndex = 16;
            this.glControl.VSync = false;
            this.glControl.ObjectClick += new System.EventHandler<RatchetEdit.RatchetEventArgs>(this.glControl_ObjectClick);
            this.glControl.ObjectDeleted += new System.EventHandler<RatchetEdit.RatchetEventArgs>(this.glControl_ObjectDeleted);
            this.glControl.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseDoubleClick);
            // 
            // toolstrip1
            // 
            this.toolstrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.toolstrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cloneBtn,
            this.deleteBtn,
            this.toolStripSeparator2,
            this.translateToolBtn,
            this.rotateToolBtn,
            this.scaleToolBtn,
            this.splineToolBtn});
            this.toolstrip1.Location = new System.Drawing.Point(0, 0);
            this.toolstrip1.Name = "toolstrip1";
            this.toolstrip1.Size = new System.Drawing.Size(1000, 25);
            this.toolstrip1.TabIndex = 15;
            this.toolstrip1.Text = "toolStrip1";
            // 
            // cloneBtn
            // 
            this.cloneBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cloneBtn.Image = global::RatchetEdit.Properties.Resources.add_button;
            this.cloneBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cloneBtn.Name = "cloneBtn";
            this.cloneBtn.Size = new System.Drawing.Size(23, 22);
            this.cloneBtn.Text = "Clone Moby";
            this.cloneBtn.Click += new System.EventHandler(this.cloneBtn_Click);
            // 
            // deleteBtn
            // 
            this.deleteBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteBtn.Image = global::RatchetEdit.Properties.Resources.delete_button;
            this.deleteBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(23, 22);
            this.deleteBtn.Text = "Delete Moby (DEL)";
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // translateToolBtn
            // 
            this.translateToolBtn.CheckOnClick = true;
            this.translateToolBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.translateToolBtn.Image = global::RatchetEdit.Properties.Resources.translate_tool;
            this.translateToolBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.translateToolBtn.Name = "translateToolBtn";
            this.translateToolBtn.Size = new System.Drawing.Size(23, 22);
            this.translateToolBtn.Text = "Translate Tool (F1)";
            this.translateToolBtn.Click += new System.EventHandler(this.translateToolBtn_Click);
            // 
            // rotateToolBtn
            // 
            this.rotateToolBtn.CheckOnClick = true;
            this.rotateToolBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rotateToolBtn.Image = global::RatchetEdit.Properties.Resources.rotate_tool;
            this.rotateToolBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rotateToolBtn.Name = "rotateToolBtn";
            this.rotateToolBtn.Size = new System.Drawing.Size(23, 22);
            this.rotateToolBtn.Text = "Rotate Tool (F2)";
            this.rotateToolBtn.Click += new System.EventHandler(this.rotateToolBtn_Click);
            // 
            // scaleToolBtn
            // 
            this.scaleToolBtn.CheckOnClick = true;
            this.scaleToolBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.scaleToolBtn.Image = global::RatchetEdit.Properties.Resources.scale_tool;
            this.scaleToolBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.scaleToolBtn.Name = "scaleToolBtn";
            this.scaleToolBtn.Size = new System.Drawing.Size(23, 22);
            this.scaleToolBtn.Text = "Scale Tool (F3)";
            this.scaleToolBtn.Click += new System.EventHandler(this.scaleToolBtn_Click);
            // 
            // splineToolBtn
            // 
            this.splineToolBtn.CheckOnClick = true;
            this.splineToolBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.splineToolBtn.Image = global::RatchetEdit.Properties.Resources.spline_tool;
            this.splineToolBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.splineToolBtn.Name = "splineToolBtn";
            this.splineToolBtn.Size = new System.Drawing.Size(23, 22);
            this.splineToolBtn.Text = "Spline Tool (F4)";
            this.splineToolBtn.Click += new System.EventHandler(this.splineToolBtn_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.objectTree);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.properties);
            this.splitContainer2.Panel2.Controls.Add(this.camYLabel);
            this.splitContainer2.Panel2.Controls.Add(this.yawLabel);
            this.splitContainer2.Panel2.Controls.Add(this.camXLabel);
            this.splitContainer2.Panel2.Controls.Add(this.label10);
            this.splitContainer2.Panel2.Controls.Add(this.label18);
            this.splitContainer2.Panel2.Controls.Add(this.camZLabel);
            this.splitContainer2.Panel2.Controls.Add(this.pitchLabel);
            this.splitContainer2.Size = new System.Drawing.Size(265, 681);
            this.splitContainer2.SplitterDistance = 279;
            this.splitContainer2.TabIndex = 21;
            // 
            // objectTree
            // 
            this.objectTree.Location = new System.Drawing.Point(4, 3);
            this.objectTree.Name = "objectTree";
            this.objectTree.Size = new System.Drawing.Size(258, 264);
            this.objectTree.TabIndex = 0;
            this.objectTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.objectTreeView1_AfterSelect);
            // 
            // properties
            // 
            this.properties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.properties.HelpVisible = false;
            this.properties.Location = new System.Drawing.Point(0, 3);
            this.properties.Name = "properties";
            this.properties.Size = new System.Drawing.Size(262, 332);
            this.properties.TabIndex = 19;
            this.properties.ToolbarVisible = false;
            this.properties.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // camYLabel
            // 
            this.camYLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.camYLabel.AutoSize = true;
            this.camYLabel.Location = new System.Drawing.Point(4, 364);
            this.camYLabel.Name = "camYLabel";
            this.camYLabel.Size = new System.Drawing.Size(13, 13);
            this.camYLabel.TabIndex = 12;
            this.camYLabel.Text = "0";
            // 
            // yawLabel
            // 
            this.yawLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.yawLabel.AutoSize = true;
            this.yawLabel.Location = new System.Drawing.Point(81, 351);
            this.yawLabel.Name = "yawLabel";
            this.yawLabel.Size = new System.Drawing.Size(13, 13);
            this.yawLabel.TabIndex = 11;
            this.yawLabel.Text = "0";
            // 
            // camXLabel
            // 
            this.camXLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.camXLabel.AutoSize = true;
            this.camXLabel.Location = new System.Drawing.Point(4, 351);
            this.camXLabel.Name = "camXLabel";
            this.camXLabel.Size = new System.Drawing.Size(13, 13);
            this.camXLabel.TabIndex = 11;
            this.camXLabel.Text = "0";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 338);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Camera";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(81, 338);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(47, 13);
            this.label18.TabIndex = 15;
            this.label18.Text = "Rotation";
            // 
            // camZLabel
            // 
            this.camZLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.camZLabel.AutoSize = true;
            this.camZLabel.Location = new System.Drawing.Point(4, 377);
            this.camZLabel.Name = "camZLabel";
            this.camZLabel.Size = new System.Drawing.Size(13, 13);
            this.camZLabel.TabIndex = 13;
            this.camZLabel.Text = "0";
            // 
            // pitchLabel
            // 
            this.pitchLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pitchLabel.AutoSize = true;
            this.pitchLabel.Location = new System.Drawing.Point(81, 364);
            this.pitchLabel.Name = "pitchLabel";
            this.pitchLabel.Size = new System.Drawing.Size(13, 13);
            this.pitchLabel.TabIndex = 12;
            this.pitchLabel.Text = "0";
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 681);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Test";
            this.Text = "Replanetizer";
            this.Load += new System.EventHandler(this.Main_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolstrip1.ResumeLayout(false);
            this.toolstrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label camXLabel;
        private System.Windows.Forms.Label camYLabel;
        private System.Windows.Forms.Label camZLabel;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label yawLabel;
        private System.Windows.Forms.Label pitchLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid properties;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStrip toolstrip1;
        private System.Windows.Forms.ToolStripButton cloneBtn;
        private System.Windows.Forms.ToolStripButton deleteBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton translateToolBtn;
        private System.Windows.Forms.ToolStripButton rotateToolBtn;
        private System.Windows.Forms.ToolStripButton scaleToolBtn;
        private System.Windows.Forms.ToolStripButton splineToolBtn;
        private ObjectTreeView objectTree;
        private CustomGLControl glControl;
    }
}

