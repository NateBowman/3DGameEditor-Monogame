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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BUTTON_EDITOR_MODE_SELECT = new System.Windows.Forms.ToolStripButton();
            this.BUTTON_EDITOR_MODE_PLACE = new System.Windows.Forms.ToolStripButton();
            this.BUTTON_EDITOR_MODE_ROTATE = new System.Windows.Forms.ToolStripButton();
            this.BUTTON_EDITOR_MODE_SCALE_NONUNIFORM = new System.Windows.Forms.ToolStripButton();
            this.BUTTON_EDITOR_MODE_SCALE = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripButtonSnap = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButtonDrawMode = new System.Windows.Forms.ToolStripDropDownButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.listBox_Models = new System.Windows.Forms.ListBox();
            this.assetPanel = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listBox_Heights = new System.Windows.Forms.ListBox();
            this.rightContainer = new System.Windows.Forms.SplitContainer();
            this.leftContainer = new System.Windows.Forms.SplitContainer();
            this.hierarchyTreeView = new System.Windows.Forms.TreeView();
            this.meshTreeView = new System.Windows.Forms.TreeView();
            this.propertyGrid2 = new System.Windows.Forms.PropertyGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnImportHeight = new System.Windows.Forms.Button();
            this.btnDeleteHeight = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnImportModel = new System.Windows.Forms.Button();
            this.btnDeleteModel = new System.Windows.Forms.Button();
            this.toolStripButtonShowGrid = new System.Windows.Forms.ToolStripButton();
            this.contextMenuDelete = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.editorControl1 = new WinFormsGraphicsDevice.EditorControl();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.assetPanel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightContainer)).BeginInit();
            this.rightContainer.Panel1.SuspendLayout();
            this.rightContainer.Panel2.SuspendLayout();
            this.rightContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftContainer)).BeginInit();
            this.leftContainer.Panel1.SuspendLayout();
            this.leftContainer.Panel2.SuspendLayout();
            this.leftContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.contextMenuDelete.SuspendLayout();
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
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
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
            this.BUTTON_EDITOR_MODE_SCALE,
            this.toolStripDropDownButton1,
            this.toolStripDropDownButtonDrawMode,
            this.toolStripButtonSnap,
            this.toolStripButtonShowGrid});
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
            this.BUTTON_EDITOR_MODE_SELECT.Size = new System.Drawing.Size(108, 24);
            this.BUTTON_EDITOR_MODE_SELECT.Text = "Select && Move";
            this.BUTTON_EDITOR_MODE_SELECT.ToolTipText = "Select & Move";
            this.BUTTON_EDITOR_MODE_SELECT.Click += new System.EventHandler(this.BUTTON_EDITOR_MODE_SELECT_Click);
            // 
            // BUTTON_EDITOR_MODE_PLACE
            // 
            this.BUTTON_EDITOR_MODE_PLACE.Image = ((System.Drawing.Image)(resources.GetObject("BUTTON_EDITOR_MODE_PLACE.Image")));
            this.BUTTON_EDITOR_MODE_PLACE.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BUTTON_EDITOR_MODE_PLACE.Name = "BUTTON_EDITOR_MODE_PLACE";
            this.BUTTON_EDITOR_MODE_PLACE.Size = new System.Drawing.Size(59, 24);
            this.BUTTON_EDITOR_MODE_PLACE.Text = "Place";
            this.BUTTON_EDITOR_MODE_PLACE.Click += new System.EventHandler(this.BUTTON_EDITOR_MODE_PLACE_Click);
            // 
            // BUTTON_EDITOR_MODE_ROTATE
            // 
            this.BUTTON_EDITOR_MODE_ROTATE.Image = ((System.Drawing.Image)(resources.GetObject("BUTTON_EDITOR_MODE_ROTATE.Image")));
            this.BUTTON_EDITOR_MODE_ROTATE.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BUTTON_EDITOR_MODE_ROTATE.Name = "BUTTON_EDITOR_MODE_ROTATE";
            this.BUTTON_EDITOR_MODE_ROTATE.Size = new System.Drawing.Size(65, 24);
            this.BUTTON_EDITOR_MODE_ROTATE.Text = "Rotate";
            this.BUTTON_EDITOR_MODE_ROTATE.Click += new System.EventHandler(this.BUTTON_EDITOR_MODE_ROTATE_Click);
            // 
            // BUTTON_EDITOR_MODE_SCALE_NONUNIFORM
            // 
            this.BUTTON_EDITOR_MODE_SCALE_NONUNIFORM.Image = ((System.Drawing.Image)(resources.GetObject("BUTTON_EDITOR_MODE_SCALE_NONUNIFORM.Image")));
            this.BUTTON_EDITOR_MODE_SCALE_NONUNIFORM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BUTTON_EDITOR_MODE_SCALE_NONUNIFORM.Name = "BUTTON_EDITOR_MODE_SCALE_NONUNIFORM";
            this.BUTTON_EDITOR_MODE_SCALE_NONUNIFORM.Size = new System.Drawing.Size(58, 24);
            this.BUTTON_EDITOR_MODE_SCALE_NONUNIFORM.Text = "Scale";
            this.BUTTON_EDITOR_MODE_SCALE_NONUNIFORM.Click += new System.EventHandler(this.BUTTON_EDITOR_MODE_SCALE_NONUNIFORM_Click);
            // 
            // BUTTON_EDITOR_MODE_SCALE
            // 
            this.BUTTON_EDITOR_MODE_SCALE.Image = ((System.Drawing.Image)(resources.GetObject("BUTTON_EDITOR_MODE_SCALE.Image")));
            this.BUTTON_EDITOR_MODE_SCALE.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BUTTON_EDITOR_MODE_SCALE.Name = "BUTTON_EDITOR_MODE_SCALE";
            this.BUTTON_EDITOR_MODE_SCALE.Size = new System.Drawing.Size(105, 24);
            this.BUTTON_EDITOR_MODE_SCALE.Text = "Uniform Scale";
            this.BUTTON_EDITOR_MODE_SCALE.Click += new System.EventHandler(this.BUTTON_EDITOR_MODE_SCALE_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.Image = global::WindowsFormsApplication1.Properties.Resources.arrows;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(98, 24);
            this.toolStripDropDownButton1.Text = "PivotMode";
            // 
            // toolStripButtonSnap
            // 
            this.toolStripButtonSnap.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSnap.Image")));
            this.toolStripButtonSnap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSnap.Name = "toolStripButtonSnap";
            this.toolStripButtonSnap.Size = new System.Drawing.Size(57, 24);
            this.toolStripButtonSnap.Text = "Snap";
            this.toolStripButtonSnap.Click += new System.EventHandler(this.toolStripButtonSnap_Click);
            // 
            // toolStripDropDownButtonDrawMode
            // 
            this.toolStripDropDownButtonDrawMode.Image = global::WindowsFormsApplication1.Properties.Resources.arrows;
            this.toolStripDropDownButtonDrawMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonDrawMode.Name = "toolStripDropDownButtonDrawMode";
            this.toolStripDropDownButtonDrawMode.Size = new System.Drawing.Size(115, 24);
            this.toolStripDropDownButtonDrawMode.Text = "DrawingMode";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(2, 2);
            this.propertyGrid1.Margin = new System.Windows.Forms.Padding(2);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(196, 444);
            this.propertyGrid1.TabIndex = 7;
            // 
            // listBox_Models
            // 
            this.listBox_Models.ContextMenuStrip = this.contextMenuDelete;
            this.listBox_Models.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Models.FormattingEnabled = true;
            this.listBox_Models.Location = new System.Drawing.Point(3, 3);
            this.listBox_Models.Margin = new System.Windows.Forms.Padding(2);
            this.listBox_Models.Name = "listBox_Models";
            this.listBox_Models.Size = new System.Drawing.Size(182, 149);
            this.listBox_Models.TabIndex = 8;
            // 
            // assetPanel
            // 
            this.assetPanel.Controls.Add(this.tabControl1);
            this.assetPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.assetPanel.Location = new System.Drawing.Point(2, 2);
            this.assetPanel.Name = "assetPanel";
            this.assetPanel.Size = new System.Drawing.Size(196, 181);
            this.assetPanel.TabIndex = 11;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(196, 181);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.listBox_Models);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(188, 155);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Mesh";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.listBox_Heights);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(188, 155);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Terrain";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listBox_Heights
            // 
            this.listBox_Heights.ContextMenuStrip = this.contextMenuDelete;
            this.listBox_Heights.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Heights.FormattingEnabled = true;
            this.listBox_Heights.Location = new System.Drawing.Point(3, 3);
            this.listBox_Heights.Name = "listBox_Heights";
            this.listBox_Heights.Size = new System.Drawing.Size(182, 149);
            this.listBox_Heights.TabIndex = 0;
            // 
            // rightContainer
            // 
            this.rightContainer.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightContainer.Location = new System.Drawing.Point(882, 51);
            this.rightContainer.Name = "rightContainer";
            this.rightContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // rightContainer.Panel1
            // 
            this.rightContainer.Panel1.Controls.Add(this.propertyGrid1);
            this.rightContainer.Panel1.Padding = new System.Windows.Forms.Padding(2);
            // 
            // rightContainer.Panel2
            // 
            this.rightContainer.Panel2.Controls.Add(this.assetPanel);
            this.rightContainer.Panel2.Padding = new System.Windows.Forms.Padding(2);
            this.rightContainer.Size = new System.Drawing.Size(200, 637);
            this.rightContainer.SplitterDistance = 448;
            this.rightContainer.TabIndex = 11;
            // 
            // leftContainer
            // 
            this.leftContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftContainer.Location = new System.Drawing.Point(0, 51);
            this.leftContainer.Name = "leftContainer";
            this.leftContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // leftContainer.Panel1
            // 
            this.leftContainer.Panel1.Controls.Add(this.hierarchyTreeView);
            this.leftContainer.Panel1.Padding = new System.Windows.Forms.Padding(2);
            // 
            // leftContainer.Panel2
            // 
            this.leftContainer.Panel2.Controls.Add(this.meshTreeView);
            this.leftContainer.Panel2.Controls.Add(this.propertyGrid2);
            this.leftContainer.Panel2.Padding = new System.Windows.Forms.Padding(2);
            this.leftContainer.Size = new System.Drawing.Size(200, 637);
            this.leftContainer.SplitterDistance = 318;
            this.leftContainer.TabIndex = 12;
            // 
            // hierarchyTreeView
            // 
            this.hierarchyTreeView.AllowDrop = true;
            this.hierarchyTreeView.ContextMenuStrip = this.contextMenuDelete;
            this.hierarchyTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hierarchyTreeView.FullRowSelect = true;
            this.hierarchyTreeView.HideSelection = false;
            this.hierarchyTreeView.Location = new System.Drawing.Point(2, 2);
            this.hierarchyTreeView.Name = "hierarchyTreeView";
            this.hierarchyTreeView.Size = new System.Drawing.Size(196, 314);
            this.hierarchyTreeView.TabIndex = 0;
            this.hierarchyTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_ItemDrag);
            this.hierarchyTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            this.hierarchyTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView_DragDrop);
            this.hierarchyTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView_DragEnter);
            // 
            // meshTreeView
            // 
            this.meshTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.meshTreeView.Location = new System.Drawing.Point(2, 2);
            this.meshTreeView.Name = "meshTreeView";
            this.meshTreeView.Size = new System.Drawing.Size(196, 181);
            this.meshTreeView.TabIndex = 0;
            this.meshTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.meshTreeView_NodeMouseClick);
            // 
            // propertyGrid2
            // 
            this.propertyGrid2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.propertyGrid2.HelpVisible = false;
            this.propertyGrid2.Location = new System.Drawing.Point(2, 183);
            this.propertyGrid2.Name = "propertyGrid2";
            this.propertyGrid2.Size = new System.Drawing.Size(196, 130);
            this.propertyGrid2.TabIndex = 1;
            this.propertyGrid2.ToolbarVisible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.editorControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(200, 51);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(2);
            this.panel1.Size = new System.Drawing.Size(682, 637);
            this.panel1.TabIndex = 13;
            // 
            // btnImportHeight
            // 
            this.btnImportHeight.AutoSize = true;
            this.btnImportHeight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnImportHeight.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnImportHeight.Location = new System.Drawing.Point(0, 0);
            this.btnImportHeight.Margin = new System.Windows.Forms.Padding(2);
            this.btnImportHeight.Name = "btnImportHeight";
            this.btnImportHeight.Size = new System.Drawing.Size(100, 32);
            this.btnImportHeight.TabIndex = 10;
            this.btnImportHeight.Text = "Import Heightmap";
            this.btnImportHeight.UseVisualStyleBackColor = true;
            this.btnImportHeight.Click += new System.EventHandler(this.btnImportHeight_Click);
            // 
            // btnDeleteHeight
            // 
            this.btnDeleteHeight.AutoSize = true;
            this.btnDeleteHeight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDeleteHeight.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeleteHeight.Location = new System.Drawing.Point(110, 0);
            this.btnDeleteHeight.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteHeight.Name = "btnDeleteHeight";
            this.btnDeleteHeight.Size = new System.Drawing.Size(72, 32);
            this.btnDeleteHeight.TabIndex = 11;
            this.btnDeleteHeight.Text = "Delete Map";
            this.btnDeleteHeight.UseVisualStyleBackColor = true;
            this.btnDeleteHeight.Click += new System.EventHandler(this.btnDeleteHeight_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnImportHeight);
            this.panel2.Controls.Add(this.btnDeleteHeight);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 120);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(182, 32);
            this.panel2.TabIndex = 12;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnImportModel);
            this.panel3.Controls.Add(this.btnDeleteModel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 120);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(182, 32);
            this.panel3.TabIndex = 13;
            // 
            // btnImportModel
            // 
            this.btnImportModel.AutoSize = true;
            this.btnImportModel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnImportModel.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnImportModel.Location = new System.Drawing.Point(0, 0);
            this.btnImportModel.Margin = new System.Windows.Forms.Padding(2);
            this.btnImportModel.Name = "btnImportModel";
            this.btnImportModel.Size = new System.Drawing.Size(78, 32);
            this.btnImportModel.TabIndex = 10;
            this.btnImportModel.Text = "Import Model";
            this.btnImportModel.UseVisualStyleBackColor = true;
            this.btnImportModel.Click += new System.EventHandler(this.btnImportModel_Click);
            // 
            // btnDeleteModel
            // 
            this.btnDeleteModel.AutoSize = true;
            this.btnDeleteModel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDeleteModel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeleteModel.Location = new System.Drawing.Point(102, 0);
            this.btnDeleteModel.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteModel.Name = "btnDeleteModel";
            this.btnDeleteModel.Size = new System.Drawing.Size(80, 32);
            this.btnDeleteModel.TabIndex = 11;
            this.btnDeleteModel.Text = "Delete Model";
            this.btnDeleteModel.UseVisualStyleBackColor = true;
            this.btnDeleteModel.Click += new System.EventHandler(this.btnDeleteModel_Click);
            // 
            // toolStripButtonShowGrid
            // 
            this.toolStripButtonShowGrid.Checked = true;
            this.toolStripButtonShowGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonShowGrid.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonShowGrid.Image")));
            this.toolStripButtonShowGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowGrid.Name = "toolStripButtonShowGrid";
            this.toolStripButtonShowGrid.Size = new System.Drawing.Size(53, 24);
            this.toolStripButtonShowGrid.Text = "Grid";
            this.toolStripButtonShowGrid.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // contextMenuDelete
            // 
            this.contextMenuDelete.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuDelete});
            this.contextMenuDelete.Name = "contextMenuDelete";
            this.contextMenuDelete.Size = new System.Drawing.Size(165, 26);
            this.contextMenuDelete.Text = "Manage";
            this.contextMenuDelete.Opened += new System.EventHandler(this.contextMenuDelete_Opened);
            // 
            // toolStripMenuDelete
            // 
            this.toolStripMenuDelete.Name = "toolStripMenuDelete";
            this.toolStripMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.toolStripMenuDelete.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuDelete.Text = "&Delete Entity";
            this.toolStripMenuDelete.Click += new System.EventHandler(this.toolStripMenuDelete_Click);
            // 
            // editorControl1
            // 
            this.editorControl1.ContextMenuStrip = this.contextMenuDelete;
            this.editorControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorControl1.DrawDrid = true;
            this.editorControl1.DrawingMode = WinFormsGraphicsDevice.EditorControl.EditorDrawMode.Shaded;
            this.editorControl1.Location = new System.Drawing.Point(2, 2);
            this.editorControl1.Name = "editorControl1";
            this.editorControl1.Size = new System.Drawing.Size(678, 633);
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
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.leftContainer);
            this.Controls.Add(this.rightContainer);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "3D Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.assetPanel.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.rightContainer.Panel1.ResumeLayout(false);
            this.rightContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rightContainer)).EndInit();
            this.rightContainer.ResumeLayout(false);
            this.leftContainer.Panel1.ResumeLayout(false);
            this.leftContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftContainer)).EndInit();
            this.leftContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.contextMenuDelete.ResumeLayout(false);
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
        private System.Windows.Forms.ListBox listBox_Models;
        private System.Windows.Forms.ToolStripButton BUTTON_EDITOR_MODE_ROTATE;
        private System.Windows.Forms.ToolStripButton BUTTON_EDITOR_MODE_SCALE_NONUNIFORM;
        private System.Windows.Forms.ToolStripButton BUTTON_EDITOR_MODE_SCALE;
        private System.Windows.Forms.ToolStripButton BUTTON_EDITOR_MODE_SELECT;
        private System.Windows.Forms.ToolStripButton BUTTON_EDITOR_MODE_PLACE;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.Panel assetPanel;
        private System.Windows.Forms.SplitContainer rightContainer;
        private System.Windows.Forms.SplitContainer leftContainer;
        private System.Windows.Forms.TreeView hierarchyTreeView;
        private System.Windows.Forms.TreeView meshTreeView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PropertyGrid propertyGrid2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSnap;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox listBox_Heights;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonDrawMode;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnImportModel;
        private System.Windows.Forms.Button btnDeleteModel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnImportHeight;
        private System.Windows.Forms.Button btnDeleteHeight;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowGrid;
        private System.Windows.Forms.ContextMenuStrip contextMenuDelete;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuDelete;
    }
}

