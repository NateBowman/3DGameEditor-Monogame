using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharedGameData;
using SharedGameData.Editor;
using SharedGameData.ExtensionMethods;
using SharedGameData.Level_Classes;
using System.Xml.Serialization;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        #region Members
        
        #endregion

        #region Constructor & Load Event
        public Form1()
        {
            InitializeComponent();
            STATIC_EDITOR_MODE.LevelInstance = new Level();

            //attach an anonymous delegate function to the SelectionChanged event
            STATIC_EDITOR_MODE.SelectionChanged+= delegate(object o) { propertyGrid1.SelectedObject = o;};
            STATIC_EDITOR_MODE.SelectionChanged += (object o) => { propertyGrid1.SelectedObject = o; };
            this.MouseWheel += new MouseEventHandler(this.OnMouseWheel);

            //initialise input handlers
            STATIC_GLOBAL_INPUT.InitialiseInputHandlers(editorControl1);
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Do_Refresh_XNB_Asset_List();    
        }
        #endregion
        
        #region Render Control Mouse Event Handlers
        private void editorControl1_MouseMove(object sender, MouseEventArgs e)
        {
            STATIC_GLOBAL_INPUT.HandleMouseMove(e);
        }

        private void editorControl1_MouseDown(object sender, MouseEventArgs e)
        {
            STATIC_GLOBAL_INPUT.HandleMouseButtons(e, true);
            /*
            switch (STATIC_EDITOR_MODE.ED_MODE)
            {
                case EDITOR_MODE.ASSET_PLACEMENT:
                    {
                        if (listBox_Assets.SelectedItem != null)
                        {
                            if (IsValidContentFile(listBox_Assets.SelectedItem.ToString(), typeof(Microsoft.Xna.Framework.Graphics.Model)))
                            {
                            }
                        }
                        else
                        {
                            MessageBox.Show("This is not a Model asset", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    break;
            }
             */
        }

        private void editorControl1_MouseUp(object sender, MouseEventArgs e)
        {
            STATIC_GLOBAL_INPUT.HandleMouseButtons(e, false);
        }

        private void editorControl1_MouseEnter(object sender, EventArgs e)
        {
            if (!editorControl1.Focused)
                editorControl1.Focus();
        }

        private void editorControl1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            STATIC_GLOBAL_INPUT.HandleMouseWheel(e);
        }
        #endregion

        #region Editor Mode Selection Buttons
        private void BUTTON_EDITOR_MODE_SELECT_Click(object sender, EventArgs e)
        {
            UncheckToolStripButtons(Controls);
            (sender as ToolStripButton).CheckState = CheckState.Checked;
                    
        }

        private void BUTTON_EDITOR_MODE_MOVE_Click(object sender, EventArgs e)
        {
            UncheckToolStripButtons(Controls);
            (sender as ToolStripButton).CheckState = CheckState.Checked;
            
        }

        private void BUTTON_EDITOR_MODE_PLACE_Click(object sender, EventArgs e)
        {
            UncheckToolStripButtons(Controls);
            (sender as ToolStripButton).CheckState = CheckState.Checked;
            
        }

        private void BUTTON_EDITOR_MODE_ROTATE_Click(object sender, EventArgs e)
        {
            UncheckToolStripButtons(Controls);
            (sender as ToolStripButton).CheckState = CheckState.Checked;
            
        }

        private void BUTTON_EDITOR_MODE_SCALE_NONUNIFORM_Click(object sender, EventArgs e)
        {
            UncheckToolStripButtons(Controls);
            (sender as ToolStripButton).CheckState = CheckState.Checked;
            
        }

        private void BUTTON_EDITOR_MODE_SCALE_Click(object sender, EventArgs e)
        {
            UncheckToolStripButtons(Controls);
            (sender as ToolStripButton).CheckState = CheckState.Checked;
            
        }
        private void UncheckToolStripButtons(Control.ControlCollection controls)
        {
            BUTTON_EDITOR_MODE_SCALE.Checked = BUTTON_EDITOR_MODE_SCALE_NONUNIFORM.Checked = BUTTON_EDITOR_MODE_ROTATE.Checked = BUTTON_EDITOR_MODE_PLACE.Checked = BUTTON_EDITOR_MODE_SELECT.Checked = false;
        }
        #endregion

        #region File Menu - save/load etc
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Save level file";
            saveFileDialog1.Filter = "Xml files|*.xml";
            saveFileDialog1.AddExtension = true;
            Type[] baseTypes = { typeof(SceneEntity), typeof(Level) }; //base types we want to support
            Type[] assemblyTypes = GetAssemblyTypesFromType(baseTypes.ToArray()).ToArray(); //find child types via reflection

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                XmlSerializer x = new System.Xml.Serialization.XmlSerializer(STATIC_EDITOR_MODE.LevelInstance.GetType(), assemblyTypes);
                using (System.IO.TextWriter writer = new System.IO.StreamWriter(saveFileDialog1.FileName))
                {
                    x.Serialize(writer, STATIC_EDITOR_MODE.LevelInstance);
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open level file";
            openFileDialog1.Filter = "Xml files|*.xml";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Type[] baseTypes = {typeof(SceneEntity), typeof(Level)};    //base types we want to support
                Type[] assemblyTypes = GetAssemblyTypesFromType(baseTypes.ToArray()).ToArray(); //find child types via reflection
                XmlSerializer x = new System.Xml.Serialization.XmlSerializer(STATIC_EDITOR_MODE.LevelInstance.GetType(), assemblyTypes);
                using (System.IO.TextReader reader = new System.IO.StreamReader(openFileDialog1.FileName))
                {
                    editorControl1.bDoNotDraw = true;   
                    STATIC_EDITOR_MODE.LevelInstance= x.Deserialize(reader) as Level;
                    //load all required assets into memory

                    editorControl1.bDoNotDraw = false;
                    MessageBox.Show("Level Loaded!", "Level loaded info box", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                }   
            }  
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Scrollbars
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            this.Refresh();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            this.Refresh();
        }
        #endregion

        #region Asset Import & Refreshing
        private void button_ImportAsset_Click(object sender, EventArgs e)
        {
            frm_Import frmImport = new frm_Import();
            frmImport.ShowDialog();
            Do_Refresh_XNB_Asset_List();
        }

        private void Do_Refresh_XNB_Asset_List()
        {
            listBox_Assets.Items.Clear();
            string[] lst_Files =
            Directory.GetFiles(Path.Combine(Application.StartupPath, @"Content\Models"), "*.xnb", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < lst_Files.Length; i++)
            {
                listBox_Assets.Items.Add(
                    Path.GetFileNameWithoutExtension(
                lst_Files[i]));
            }
        }
        #endregion
        
        #region Helper Functions
        private bool IsSomethingSelected()
        {
            if (STATIC_EDITOR_MODE.SelectedObjects.Count > 0)
            {
                return true;
            }
            return false;
        }

        private bool IsValidContentFile(string fileToLoad, params Type[] desiredTypes)
        {
            object o;
            try
            {
                o = STATIC_EDITOR_MODE.contentMan.Load<object>(fileToLoad);
                foreach (Type type in desiredTypes)
                    if (o.GetType() == type)
                        return true;
            }
            catch (Exception exc)
            {
                if (exc is Microsoft.Xna.Framework.Content.ContentLoadException)
                {
                    //User is trying to load the wrong type of content or file is corrupt
                }
                else
                    throw exc;
            }

            return false;
        }

        private List<Type> GetAssemblyTypesFromType(params Type[] typeList)
        {
            //use reflection to get all derived types
            List<Type> knownTypes = new List<Type>();
            foreach (Type comparisonType in typeList)
            {
                foreach (Type t in System.Reflection.Assembly.GetAssembly(comparisonType).GetTypes())
                {
                    if (comparisonType.IsAssignableFrom(t))
                    {
                        knownTypes.Add(t);
                    }
                }
            }
            return knownTypes;
        }
        #endregion

        
    }
}
