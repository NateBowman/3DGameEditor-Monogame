namespace WindowsFormsApplication1
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BUTTON_EDITOR_MODE_SELECT = new System.Windows.Forms.ToolStripButton();
            this.BUTTON_EDITOR_MODE_PLACE = new System.Windows.Forms.ToolStripButton();
            this.BUTTON_EDITOR_MODE_ROTATE = new System.Windows.Forms.ToolStripButton();
            this.BUTTON_EDITOR_MODE_SCALE_NONUNIFORM = new System.Windows.Forms.ToolStripButton();
            this.BUTTON_EDITOR_MODE_SCALE = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.listBox_Assets = new System.Windows.Forms.ListBox();
            this.button_ImportAsset = new System.Windows.Forms.Button();
            this.editorControl1 = new WinFormsGraphicsDevice.EditorControl();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1082, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BUTTON_EDITOR_MODE_SELECT,
            this.BUTTON_EDITOR_MODE_PLACE,
            this.BUTTON_EDITOR_MODE_ROTATE,
            this.BUTTON_EDITOR_MODE_SCALE_NONUNIFORM,
            this.BUTTON_EDITOR_MODE_SCALE});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1082, 27);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // BUTTON_EDITOR_MODE_SELECT
            // 
            this.BUTTON_EDITOR_MODE_SELECT.Checked = true;
            this.BUTTON_EDITOR_MODE_SELECT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.BUTTON_EDITOR_MODE_SELECT.Image = ((System.Drawing.Image)(resources.GetObject("BUTTON_EDITOR_MODE_SELECT.Image")));
            this.BUTTON_EDITOR_MODE_SELECT.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BUTTON_EDITOR_MODE_SELECT.Name = "BUTTON_EDITOR_MODE_SELECT";
            this.BUTTON_EDITOR_MODE_SELECT.Size = new System.Drawing.Size(156, 24);
            this.BUTTON_EDITOR_MODE_SELECT.Text = "SELECT && MOVE MODE";
            this.BUTTON_EDITOR_MODE_SELECT.ToolTipText = "SELECT & MOVE MODE";
            this.BUTTON_EDITOR_MODE_SELECT.Click += new System.EventHandler(this.BUTTON_EDITOR_MODE_SELECT_Click);
            // 
            // BUTTON_EDITOR_MODE_PLACE
            // 
            this.BUTTON_EDITOR_MODE_PLACE.Image = ((System.Drawing.Image)(resources.GetObject("BUTTON_EDITOR_MODE_PLACE.Image")));
            this.BUTTON_EDITOR_MODE_PLACE.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BUTTON_EDITOR_MODE_PLACE.Name = "BUTTON_EDITOR_MODE_PLACE";
            this.BUTTON_EDITOR_MODE_PLACE.Size = new System.Drawing.Size(103, 24);
            this.BUTTON_EDITOR_MODE_PLACE.Text = "PLACE MODE";
            this.BUTTON_EDITOR_MODE_PLACE.Click += new System.EventHandler(this.BUTTON_EDITOR_MODE_PLACE_Click);
            // 
            // BUTTON_EDITOR_MODE_ROTATE
            // 
            this.BUTTON_EDITOR_MODE_ROTATE.Image = ((System.Drawing.Image)(resources.GetObject("BUTTON_EDITOR_MODE_ROTATE.Image")));
            this.BUTTON_EDITOR_MODE_ROTATE.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BUTTON_EDITOR_MODE_ROTATE.Name = "BUTTON_EDITOR_MODE_ROTATE";
            this.BUTTON_EDITOR_MODE_ROTATE.Size = new System.Drawing.Size(109, 24);
            this.BUTTON_EDITOR_MODE_ROTATE.Text = "ROTATE MODE";
            this.BUTTON_EDITOR_MODE_ROTATE.Click += new System.EventHandler(this.BUTTON_EDITOR_MODE_ROTATE_Click);
            // 
            // BUTTON_EDITOR_MODE_SCALE_NONUNIFORM
            // 
            this.BUTTON_EDITOR_MODE_SCALE_NONUNIFORM.Image = ((System.Drawing.Image)(resources.GetObject("BUTTON_EDITOR_MODE_SCALE_NONUNIFORM.Image")));
            this.BUTTON_EDITOR_MODE_SCALE_NONUNIFORM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BUTTON_EDITOR_MODE_SCALE_NONUNIFORM.Name = "BUTTON_EDITOR_MODE_SCALE_NONUNIFORM";
            this.BUTTON_EDITOR_MODE_SCALE_NONUNIFORM.Size = new System.Drawing.Size(188, 24);
            this.BUTTON_EDITOR_MODE_SCALE_NONUNIFORM.Text = "NON UNIFORM SCALE MODE";
            this.BUTTON_EDITOR_MODE_SCALE_NONUNIFORM.Click += new System.EventHandler(this.BUTTON_EDITOR_MODE_SCALE_NONUNIFORM_Click);
            // 
            // BUTTON_EDITOR_MODE_SCALE
            // 
            this.BUTTON_EDITOR_MODE_SCALE.Image = ((System.Drawing.Image)(resources.GetObject("BUTTON_EDITOR_MODE_SCALE.Image")));
            this.BUTTON_EDITOR_MODE_SCALE.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BUTTON_EDITOR_MODE_SCALE.Name = "BUTTON_EDITOR_MODE_SCALE";
            this.BUTTON_EDITOR_MODE_SCALE.Size = new System.Drawing.Size(158, 24);
            this.BUTTON_EDITOR_MODE_SCALE.Text = "UNIFORM SCALE MODE";
            this.BUTTON_EDITOR_MODE_SCALE.Click += new System.EventHandler(this.BUTTON_EDITOR_MODE_SCALE_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGrid1.Location = new System.Drawing.Point(842, 98);
            this.propertyGrid1.Margin = new System.Windows.Forms.Padding(2);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(230, 195);
            this.propertyGrid1.TabIndex = 7;
            // 
            // listBox_Assets
            // 
            this.listBox_Assets.FormattingEnabled = true;
            this.listBox_Assets.Location = new System.Drawing.Point(842, 299);
            this.listBox_Assets.Margin = new System.Windows.Forms.Padding(2);
            this.listBox_Assets.Name = "listBox_Assets";
            this.listBox_Assets.Size = new System.Drawing.Size(231, 225);
            this.listBox_Assets.TabIndex = 8;
            // 
            // button_ImportAsset
            // 
            this.button_ImportAsset.Location = new System.Drawing.Point(842, 534);
            this.button_ImportAsset.Margin = new System.Windows.Forms.Padding(2);
            this.button_ImportAsset.Name = "button_ImportAsset";
            this.button_ImportAsset.Size = new System.Drawing.Size(230, 64);
            this.button_ImportAsset.TabIndex = 9;
            this.button_ImportAsset.Text = "Import assets";
            this.button_ImportAsset.UseVisualStyleBackColor = true;
            this.button_ImportAsset.Click += new System.EventHandler(this.button_ImportAsset_Click);
            // 
            // editorControl1
            // 
            this.editorControl1.Location = new System.Drawing.Point(12, 51);
            this.editorControl1.Name = "editorControl1";
            this.editorControl1.Size = new System.Drawing.Size(800, 600);
            this.editorControl1.TabIndex = 0;
            this.editorControl1.Text = "editorControl1";
            this.editorControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.editorControl1_MouseDown);
            this.editorControl1.MouseEnter += new System.EventHandler(this.editorControl1_MouseEnter);
            this.editorControl1.MouseLeave += new System.EventHandler(this.editorControl1_MouseLeave);
            this.editorControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.editorControl1_MouseMove);
            this.editorControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.editorControl1_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 688);
            this.Controls.Add(this.button_ImportAsset);
            this.Controls.Add(this.listBox_Assets);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.editorControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "MonoGame Lecture 10";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WinFormsGraphicsDevice.EditorControl editorControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ListBox listBox_Assets;
        private System.Windows.Forms.Button button_ImportAsset;
        private System.Windows.Forms.ToolStripButton BUTTON_EDITOR_MODE_ROTATE;
        private System.Windows.Forms.ToolStripButton BUTTON_EDITOR_MODE_SCALE_NONUNIFORM;
        private System.Windows.Forms.ToolStripButton BUTTON_EDITOR_MODE_SCALE;
        private System.Windows.Forms.ToolStripButton BUTTON_EDITOR_MODE_SELECT;
        private System.Windows.Forms.ToolStripButton BUTTON_EDITOR_MODE_PLACE;

    }
}

