using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharedGameData;
using SharedGameData.Editor;
using SharedGameData.ExtensionMethods;
using SharedGameData.Level_Classes;
using System.Xml.Serialization;
using SharedGameData.Grid;
using System.IO;

namespace WindowsFormsApplication1 {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Properties;
    using SharedGameData.HelperFunctions;
    using WinFormsGraphicsDevice;
    using XNAGizmo;
    using Point = System.Drawing.Point;

    public partial class Form1 : Form {
        #region Members

        #endregion

        #region Constructor & Load Event

        List<SceneEntity> ActivePropertySubscriptions = new List<SceneEntity>();

        public Form1() {
            InitializeComponent();
            STATIC_EDITOR_MODE.LevelInstance = new Level();

            // manage subscriptions to selected objects propertychange notifications
            STATIC_EDITOR_MODE.SelectionChanged += OnStaticEditorModeOnSelectionChanged;

            this.MouseWheel += new MouseEventHandler(this.OnMouseWheel);

            //initialise input handlers
            STATIC_GLOBAL_INPUT.InitialiseInputHandlers(editorControl1);

            PopulateHeirarchy();
            InitPivotModes();
            InitDrawModes();

            STATIC_EDITOR_MODE.SelectionChanged += hierarchyTreeViewSelectionChanged;

            STATIC_EDITOR_MODE.SelectionChanged += OnStaticEditorModeOnSelectionChanged;

        }

        private void OnStaticEditorModeOnSelectionChanged(object o) {
            ActivePropertySubscriptions.ForEach(sceneEntity => sceneEntity.PropertyChanged -= OnSceneEntityPropertyChanged);
            if (o is List<SceneEntity> entities && entities.Count > 0) {
                entities.ForEach(entity => {
                                     entity.PropertyChanged += OnSceneEntityPropertyChanged;
                                     ActivePropertySubscriptions.Add(entity);
                                 });

                propertyGrid1.SelectedObject = entities[0];

            }
        }

        private void OnSceneEntityPropertyChanged(object sender, PropertyChangedEventArgs args) {
                propertyGrid1.Refresh();
            if (sender is SceneEntity entity) {
                UpdateTrees(entity);
                
            }
    }
        private void UpdateTrees(SceneEntity entity) { }

        private void Form1_Load(object sender, EventArgs e) {
            RefreshAllXNBLists();

            editorControl1.Translation_GUI_Update += BUTTON_EDITOR_MODE_SELECT_Click;
            editorControl1.Rotation_GUI_Update += BUTTON_EDITOR_MODE_ROTATE_Click;
            editorControl1.ScaleUniform_GUI_Update += BUTTON_EDITOR_MODE_SCALE_Click;
            editorControl1.ScaleNonUniform_GUI_Update += BUTTON_EDITOR_MODE_SCALE_NONUNIFORM_Click;
            editorControl1.SelectionChange += Handle_Property_Grid_Items;

            editorControl1.SelectionChange += () => STATIC_EDITOR_MODE.SelectedObjects = editorControl1.Gizmo.Selection.Cast<SceneEntity>().ToList();
        }

        private void InitPivotModes() {
            foreach (var pivot in Enum.GetNames(typeof(PivotType))) {
                toolStripDropDownButton1.DropDown.Items.Add(pivot, Resources.arrows, (o, args) => {
                                                                                         //set pivot mode
                                                                                         editorControl1.Gizmo.ActivePivot = (PivotType) Enum.Parse(typeof(PivotType), pivot);
                                                                                         //set text on button
                                                                                         toolStripDropDownButton1.Text = pivot;
                                                                                     });
            }

            // set starting button text
            toolStripDropDownButton1.Text = PivotType.ObjectCenter.ToString();
        }

        private void InitDrawModes()
        {
            foreach (var editorMode in Enum.GetNames(typeof(EditorControl.EditorDrawMode)))
            {
                toolStripDropDownButtonDrawMode.DropDown.Items.Add(editorMode, Resources.arrows, (o, args) => {
                                                                                         //set pivot mode
                                                                                         editorControl1.DrawingMode = (EditorControl.EditorDrawMode)Enum.Parse(typeof(EditorControl.EditorDrawMode), editorMode);
                                                                                         //set text on button
                                                                                         toolStripDropDownButtonDrawMode.Text = editorMode;
                                                                                     });
            }

            // set starting button text
            toolStripDropDownButtonDrawMode.Text = EditorControl.EditorDrawMode.Shaded.ToString();
        }

        #endregion

        #region Render Control Mouse Event Handlers

        private void editorControl1_MouseMove(object sender, MouseEventArgs e) {
            STATIC_GLOBAL_INPUT.HandleMouseMove(e);
        }

        private void editorControl1_MouseDown(object sender, MouseEventArgs e) {
            STATIC_GLOBAL_INPUT.HandleMouseButtons(e, true);
            switch (editorControl1.Gizmo.ActiveMode) {
                case GizmoMode.Placement: {
                        
                    if (tabControl1.SelectedTab == tabPage1) {
                        // we are on the model page
                            if (e.Button == MouseButtons.Left) {
                            if (listBox_Models.SelectedItem != null) {
                                if (IsValidContentFile(@"Models\" + listBox_Models.SelectedItem, typeof(Model))) {

                                    BoundingBox bb = new BoundingBox();
                                        var useTerrain = false;
                                    var terrains = STATIC_EDITOR_MODE.LevelInstance.Entities.Where(entity => entity is TerrainEntity).ToList();
                                    if (terrains.Count > 0) {
                                        bb = terrains[0].BoundingBox;
                                        terrains.Remove(terrains[0]);
                                        useTerrain = true;
                                        foreach (var terrain in terrains) {
                                            bb = BoundingBox.CreateMerged(bb, terrain.BoundingBox);
                                        }
                                    }

                                    terrains = null;
                                    
                                    var rayHitPos = RayHelpers.GetHitPoint(editorControl1.ray, (useTerrain? bb : Grid3D.BB));
                                    if (rayHitPos != null) {
                                        var fileName = listBox_Models.SelectedItem.ToString();
                                        var obj = STATIC_EDITOR_MODE.contentMan.Load<object>(Path.Combine("Models", fileName));
                                        var se = new SceneEntity(obj as Model, fileName) {Position = (Vector3) rayHitPos};
                                        se.LoadAsset(STATIC_EDITOR_MODE.contentMan);
                                        Engine.Entities.Add(se);
                                        STATIC_EDITOR_MODE.LevelInstance.Entities.Add(se);
                                        AddTreeNodeFromSceneEntity(se, hierarchyTreeView);
                                        editorControl1.SetSelectionTo(se);
                                    }
                                }
                            }
                            else {
                                MessageBox.Show("This is not a Model asset", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                    }
                    else if (tabControl1.SelectedTab == tabPage2) {
                            // we are on the texture page
                        if (listBox_Heights.SelectedItem != null) {
                            if (IsValidContentFile(@"HeightMaps\" + listBox_Heights.SelectedItem, typeof(Texture2D))) {
                                // ?? to a default position
                                    var rayHitPos = RayHelpers.GetHitPoint(editorControl1.ray, Grid3D.BB) ?? new Vector3(0, 0, 0);
                                    
                                var fileName = listBox_Heights.SelectedItem.ToString();
                                var obj = STATIC_EDITOR_MODE.contentMan.Load<object>(Path.Combine("HeightMaps", fileName));
                                var se = new TerrainEntity(obj as Texture2D, fileName) {Position = rayHitPos};
                                se.LoadAsset(STATIC_EDITOR_MODE.contentMan);
                                    //remove all terrains
                                //Engine.Entities.RemoveAll(entity => entity is TerrainEntity);

                                Engine.Entities.Add(se);
                                STATIC_EDITOR_MODE.LevelInstance.Entities.Add(se);
                                //STATIC_EDITOR_MODE.LevelInstance.Terrain = se;
                                    // rebuild the tree
                                PopulateHeirarchy();
                                editorControl1.SetSelectionTo(se);
                            }
                        }
                    }
                            break;
                }
            }
        }

        private void editorControl1_MouseUp(object sender, MouseEventArgs e) {
            STATIC_GLOBAL_INPUT.HandleMouseButtons(e, false);
        }

        private void editorControl1_MouseEnter(object sender, EventArgs e) {
            if (!editorControl1.Focused)
                editorControl1.Focus();
        }

        private void editorControl1_MouseLeave(object sender, EventArgs e) { }

        private void OnMouseWheel(object sender, MouseEventArgs e) {
            STATIC_GLOBAL_INPUT.HandleMouseWheel(e);
        }

        #endregion

        #region Editor Mode Selection Buttons

        private void BUTTON_EDITOR_MODE_SELECT_Click(object sender, EventArgs e) {
            UncheckToolStripButtons(Controls);
            BUTTON_EDITOR_MODE_SELECT.CheckState = CheckState.Checked;
            editorControl1.Gizmo.ActiveMode = XNAGizmo.GizmoMode.Translate;
        }

        private void BUTTON_EDITOR_MODE_PLACE_Click(object sender, EventArgs e) {
            UncheckToolStripButtons(Controls);
            BUTTON_EDITOR_MODE_PLACE.CheckState = CheckState.Checked;
            editorControl1.Gizmo.ActiveMode = XNAGizmo.GizmoMode.Placement;
        }

        private void BUTTON_EDITOR_MODE_ROTATE_Click(object sender, EventArgs e) {
            UncheckToolStripButtons(Controls);
            BUTTON_EDITOR_MODE_ROTATE.CheckState = CheckState.Checked;
            editorControl1.Gizmo.ActiveMode = XNAGizmo.GizmoMode.Rotate;
        }

        private void BUTTON_EDITOR_MODE_SCALE_NONUNIFORM_Click(object sender, EventArgs e) {
            UncheckToolStripButtons(Controls);
            BUTTON_EDITOR_MODE_SCALE_NONUNIFORM.CheckState = CheckState.Checked;
            editorControl1.Gizmo.ActiveMode = XNAGizmo.GizmoMode.NonUniformScale;
        }

        private void BUTTON_EDITOR_MODE_SCALE_Click(object sender, EventArgs e) {
            UncheckToolStripButtons(Controls);
            BUTTON_EDITOR_MODE_SCALE.CheckState = CheckState.Checked;
            editorControl1.Gizmo.ActiveMode = XNAGizmo.GizmoMode.UniformScale;
        }

        private void UncheckToolStripButtons(Control.ControlCollection controls) {
            BUTTON_EDITOR_MODE_SCALE.Checked = BUTTON_EDITOR_MODE_SCALE_NONUNIFORM.Checked =
                                                   BUTTON_EDITOR_MODE_ROTATE.Checked = BUTTON_EDITOR_MODE_PLACE.Checked = BUTTON_EDITOR_MODE_SELECT.Checked = false;
        }

        #endregion

        #region File Menu - save/load etc

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            openFileDialog1.Title = "Save level file";
            saveFileDialog1.Filter = "Xml files|*.xml";
            saveFileDialog1.AddExtension = true;
            Type[] baseTypes = {
                typeof(SceneEntity),
                typeof(Level),
                typeof(TerrainEntity)
            }; //base types we want to support
            Type[] assemblyTypes = GetAssemblyTypesFromType(baseTypes.ToArray()).ToArray(); //find child types via reflection

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                XmlSerializer x = new System.Xml.Serialization.XmlSerializer(STATIC_EDITOR_MODE.LevelInstance.GetType(), assemblyTypes);
                using (System.IO.TextWriter writer = new System.IO.StreamWriter(saveFileDialog1.FileName)) {
                    x.Serialize(writer, STATIC_EDITOR_MODE.LevelInstance);
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            openFileDialog1.Title = "Open level file";
            openFileDialog1.Filter = "Xml files|*.xml";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                Type[] baseTypes = {
                    typeof(SceneEntity),
                    typeof(Level),
                    typeof(TerrainEntity)

                }; //base types we want to support
                Type[] assemblyTypes = GetAssemblyTypesFromType(baseTypes.ToArray()).ToArray(); //find child types via reflection
                XmlSerializer x = new System.Xml.Serialization.XmlSerializer(STATIC_EDITOR_MODE.LevelInstance.GetType(), assemblyTypes);
                using (System.IO.TextReader reader = new System.IO.StreamReader(openFileDialog1.FileName)) {
                    editorControl1.bDoNotDraw = true;
                    SceneEntity.Content = STATIC_EDITOR_MODE.contentMan;
                    STATIC_EDITOR_MODE.LevelInstance = x.Deserialize(reader) as Level;
                    //load all required assets into memory
                    for (int i = 0; i < STATIC_EDITOR_MODE.LevelInstance.Entities.Count; i++) {
                        STATIC_EDITOR_MODE.LevelInstance.Entities[i].LoadAsset(STATIC_EDITOR_MODE.contentMan);
                    }

                    Engine.Entities.Clear();
                    Engine.Entities.AddRange(STATIC_EDITOR_MODE.LevelInstance.Entities);
                    editorControl1.bDoNotDraw = false;
                    MessageBox.Show("Level Loaded!", "Level loaded info box", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopulateHeirarchy();
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        #endregion

        #region Asset Import & Refreshing


        private void RefreshAllXNBLists() {
            RefreshXNBModelList();
            RefreshXNBHeightList();
        }

        private void RefreshXNBModelList() {

            RefreshXNBList(listBox_Models,typeof(Model),"Models");

        }

        void RefreshXNBList(ListBox list, Type type, string folder) {
            list.Items.Clear();
            var files = Directory.GetFiles(Path.Combine(Application.StartupPath, $@"Content\{folder}"), "*.xnb", SearchOption.TopDirectoryOnly);

            foreach (var t in files)
            {
                if (t != null)
                {
                    var withoutExtension = Path.GetFileNameWithoutExtension(t);
                    if (IsValidContentFile($@"{folder}/{withoutExtension}", type))
                    {
                        list.Items.Add(withoutExtension);
                    }
                }
            }


        }

        private void RefreshXNBHeightList()
        {
            RefreshXNBList(listBox_Heights, typeof(Texture2D), "HeightMaps");
        }


        #endregion

        #region Helper Functions

        private bool IsSomethingSelected() {
            if (STATIC_EDITOR_MODE.SelectedObjects.Count > 0) {
                return true;
            }

            return false;
        }

        private bool IsValidContentFile(string fileToLoad, params Type[] desiredTypes) {
            object o;
            try {
                o = STATIC_EDITOR_MODE.contentMan.Load<object>(fileToLoad);
                foreach (Type type in desiredTypes)
                    if (o.GetType() == type)
                        return true;
            }
            catch (Exception exc) {
                if (exc is Microsoft.Xna.Framework.Content.ContentLoadException) {
                    //User is trying to load the wrong type of content or file is corrupt
                }
                else
                    throw exc;
            }

            return false;
        }

        private List<Type> GetAssemblyTypesFromType(params Type[] typeList) {
            //use reflection to get all derived types
            List<Type> knownTypes = new List<Type>();
            foreach (Type comparisonType in typeList) {
                foreach (Type t in System.Reflection.Assembly.GetAssembly(comparisonType).GetTypes()) {
                    if (comparisonType.IsAssignableFrom(t)) {
                        knownTypes.Add(t);
                    }
                }
            }

            return knownTypes;
        }

        #endregion

        private void Handle_Property_Grid_Items() {
            if (propertyGrid1.SelectedObjects.Length > 0) {
                propertyGrid1.SelectedObjects = editorControl1.Gizmo.Selection.ToArray();
            }
            else {
                propertyGrid1.SelectedObject = false;
            }
            propertyGrid1.RefreshTabs(PropertyTabScope.Component);
            propertyGrid1.Refresh();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            STATIC_EDITOR_MODE.LevelInstance = new Level();
            Engine.Entities.Clear();
        }

        #region Treeview

        /// <summary>
        ///     Recursive population of the tree from the assets collection
        /// </summary>
        public void PopulateHeirarchy()
        {
            hierarchyTreeView.BeginUpdate();
            hierarchyTreeView.Nodes.Clear();

            void AddNodes(string id, TreeNode node)
            {
                foreach (var sceneEntity in STATIC_EDITOR_MODE.LevelInstance.Entities.Where(entity => entity.ParentId == id))
                {
                    var childNode = node.Nodes.Add(sceneEntity.Id, sceneEntity.ModelName);
                    AddNodes(sceneEntity.Id, childNode);
                }
            }

            var root = new TreeNode("root");

            AddNodes(null, root);

            hierarchyTreeView.Nodes.Add(root);
            hierarchyTreeView.TopNode = root;

            root.Expand();

            hierarchyTreeView.EndUpdate();
        }

        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            // Retrieve the client coordinates of the drop location.
            if (sender is TreeView tree) {
                var targetPoint = tree.PointToClient(new Point(e.X, e.Y));

                // Retrieve the node at the drop location.
                var targetNode = tree.GetNodeAt(targetPoint);

                // Retrieve the node that was dragged.
                var draggedNode = (TreeNode) e.Data.GetData(typeof(TreeNode));

                // Confirm that the node at the drop location is not 
                // the dragged node and that target node isn't null
                // (for example if you drag outside the control)
                if (!draggedNode.Equals(targetNode) && (targetNode != null)) {
                    // check the target node is not a child of the dragged node
                    var child = false;
                    var currentNode = targetNode;
                    while (currentNode != null) {
                        if (currentNode == draggedNode) {
                            child = true;
                            break;
                        }

                        currentNode = currentNode.Parent;
                    }

                    if (child) {
                        return;
                    }

                    // Remove the node from its current 
                    // location and add it to the node at the drop location.
                    draggedNode.Remove();
                    targetNode.Nodes.Add(draggedNode);

                    // Expand the node at the location 
                    // to show the dropped node.
                    targetNode.Expand();

                    // Update the parentID of the asset

                    SceneEntity targetActor;
                    SceneEntity draggedActor;
                    if (tree == hierarchyTreeView) {
                        targetActor = STATIC_EDITOR_MODE.LevelInstance.Entities.FirstOrDefault(entity => entity.Id == targetNode.Name);
                        draggedActor = STATIC_EDITOR_MODE.LevelInstance.Entities.FirstOrDefault(entity => entity.Id == draggedNode.Name);
                        draggedActor.ParentId = targetNode?.Name ?? "";
                    }
                }
            }
        }

        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void hierarchyTreeViewSelectionChanged(object obj) {
            SceneEntity entity = null;
            if (obj is List<SceneEntity> selection) {
                if (selection.Count > 0) {
                    entity = selection[0];
                    hierarchyTreeView.SelectedNode = hierarchyTreeView.Nodes.Find(entity.Id, true).FirstOrDefault() ?? hierarchyTreeView.TopNode;
                }
            }
            updateMeshTree(entity);
        }

        private void updateMeshTree(SceneEntity entity) {
            
            meshTreeView.Nodes.Clear();
            if (entity?.EntityModel == null) {
                return;
            }

            meshTreeView.BeginUpdate();
            for (var index = 0; index < entity.EntityModel.Meshes.Count; index++) {
                var mesh = entity.EntityModel.Meshes[index];
                var node = meshTreeView.Nodes.Add(index.ToString(),mesh.Name);
                for (var i = 0; i < mesh.MeshParts.Count; i++) {
                    var meshPart = mesh.MeshParts[i];
                    node.Nodes.Add(i.ToString(),meshPart.Tag.ToString());
                }
            }

            meshTreeView.ExpandAll();
            meshTreeView.EndUpdate();
        }

        private void meshTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Node != null) {
                if (e.Node.Parent != null) {
                    // node is a meshpart
                    var nodeName = hierarchyTreeView.SelectedNode.Name;
                    var entity = STATIC_EDITOR_MODE.LevelInstance.Entities.FirstOrDefault(sceneEntity => sceneEntity.Id == nodeName);
                    if (entity?.EntityModel.Meshes != null) {
                        propertyGrid2.SelectedObject = entity.EntityModel.Meshes[int.Parse(e.Node.Parent.Name)].MeshParts[int.Parse(e.Node.Name)];
                    }
                }
                else {
                    var nodeName = hierarchyTreeView.SelectedNode.Name;
                    var entity = STATIC_EDITOR_MODE.LevelInstance.Entities.FirstOrDefault(sceneEntity => sceneEntity.Id == nodeName);
                    if (entity?.EntityModel.Meshes != null) {
                        propertyGrid2.SelectedObject = entity.EntityModel.Meshes[int.Parse(e.Node.Name)];
                    }
                }
            }

            //propertyGrid2.SelectedObject;
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null)
            {
                if (STATIC_EDITOR_MODE.LevelInstance.Entities.Count > 0)
                {
                    var entity = STATIC_EDITOR_MODE.LevelInstance.Entities.FirstOrDefault(sceneEntity => sceneEntity.Id == e.Node.Name);
                    editorControl1.SetSelectionTo(entity);
                }
            }
        }

        private void AddTreeNodeFromSceneEntity(SceneEntity asset, TreeView tree)
        {

            tree.BeginUpdate();
            // Add to the treeview
            if (tree.TopNode != null)
            {
                tree.SelectedNode = tree.TopNode.Nodes.Add(asset.Id, asset.ModelName);
            }
            else
            {
                tree.SelectedNode = tree.Nodes.Add(asset.Id, asset.ModelName);
            }
            tree.EndUpdate();
        }

        #endregion

        private void toolStripButtonSnap_Click(object sender, EventArgs e) {
            editorControl1.Gizmo.SnapEnabled = !editorControl1.Gizmo.SnapEnabled;
            (sender as ToolStripButton).Checked = editorControl1.Gizmo.SnapEnabled;
        }

        private void btnImportModel_Click(object sender, EventArgs e) {
            frm_Import frmImport = new frm_Import {ContentType = frm_Import.ImportType.Model};
            frmImport.ShowDialog();
            RefreshAllXNBLists();
        }

        private void btnImportHeight_Click(object sender, EventArgs e)
        {
            frm_Import frmImport = new frm_Import { ContentType = frm_Import.ImportType.HeightMap };
            frmImport.ShowDialog();
            RefreshAllXNBLists();
        }

        private void toolStripButton1_Click(object sender, EventArgs e) {
            toolStripButtonShowGrid.Checked = !toolStripButtonShowGrid.Checked;
            editorControl1.DrawDrid = toolStripButtonShowGrid.Checked;
        }

        private void DeleteSceneSelected() {
            if (editorControl1.Gizmo.Selection.Count > 0) {

                var selected = new List<SceneEntity>();

                // select selection and children
                foreach (var transformable in editorControl1.Gizmo.Selection) {
                    if(transformable is SceneEntity entity)
                    STATIC_EDITOR_MODE.ExecuteOnSelfAndChildEntities(entity,sceneEntity => selected.Add(sceneEntity));
                }
                foreach (var entity in selected.Distinct()) {
                    STATIC_EDITOR_MODE.RemoveEntity(entity);
                }

                editorControl1.Gizmo.Selection.Clear();
                selected = null;
                PopulateHeirarchy();
            }
        }

        private void btnDeleteModel_Click(object sender, EventArgs e) {
            RemoveModel();
        }

        private void RemoveModel() {
            RemoveAsset("Models", typeof(Model), listBox_Models.SelectedItem.ToString());
        }

        private void RemoveAsset(string path, Type assetType, string name) {
            if (MessageBox.Show("Really remove the asset from the project? \r\n This will remove all assets of this type in the scene.", "Remove Asset", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes) {

                if (IsValidContentFile($@"{path}\{name}", assetType)) {
                    if (assetType == typeof(Model)) {
                        STATIC_EDITOR_MODE.RemoveAssetsWithModel(name);
                    }
                    else if (assetType == typeof(Texture2D)) {
                        STATIC_EDITOR_MODE.RemoveAssetsWithHeight(name);
                    }

                    var files = Directory.GetFiles(Path.Combine(Application.StartupPath, $@"Content\{path}"), "*.xnb", SearchOption.TopDirectoryOnly);

                    foreach (var file in files) {
                        if (Path.GetFileNameWithoutExtension(file) == name) {
                            try {
                                File.Delete(file);
                            }
                            catch (Exception exception) {
                                Console.WriteLine(exception);
                                throw;
                            }
                        }
                    }
                }

                RefreshAllXNBLists();

                PopulateHeirarchy();
            }
        }

        private void btnDeleteHeight_Click(object sender, EventArgs e) {
            RemoveHeight();
        }

        private void RemoveHeight() {
            RemoveAsset("HeightMaps", typeof(Texture2D), listBox_Heights.SelectedItem.ToString());
        }


        private object LastRightClick = null;

        private void contextMenuDelete_Opened(object sender, EventArgs e) {
            if (((ContextMenuStrip) sender).SourceControl is TreeView view) {
                contextMenuDelete.Enabled = editorControl1.Gizmo.Selection.Count > 0;
                LastRightClick = view;
            }
            else if (((ContextMenuStrip)sender).SourceControl is EditorControl editor)
            {
                contextMenuDelete.Enabled = editorControl1.Gizmo.Selection.Count > 0;
                LastRightClick = editor;
            }
            else if (((ContextMenuStrip) sender).SourceControl is ListBox box) {
                if (box == listBox_Heights) {
                    contextMenuDelete.Enabled = listBox_Heights.SelectedItem != null;
                    LastRightClick = listBox_Heights;
                }
                else if (box == listBox_Models) {
                    contextMenuDelete.Enabled = listBox_Models.SelectedItem != null;
                    LastRightClick = listBox_Models;
                }
            }
        }

        private void toolStripMenuDelete_Click(object sender, EventArgs e)
        {
            if (LastRightClick is TreeView view)
            {
                DeleteSceneSelected();
            }
            else if (LastRightClick is EditorControl editor)
            {
                DeleteSceneSelected();
            }
            else if (LastRightClick is ListBox box)
            {
                if (box == listBox_Heights)
                {
                    RemoveHeight();

                }
                else if (box == listBox_Models)
                {
                    RemoveModel();
                }
            }
        }
    }

}
